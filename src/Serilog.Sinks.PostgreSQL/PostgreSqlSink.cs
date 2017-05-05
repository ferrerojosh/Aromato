using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.PostgreSQL
{
    internal class PostgreSqlSink : PeriodicBatchingSink
    {
        /// <summary>
        ///     A reasonable default for the number of events posted in
        ///     each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        ///     A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(5);

        private readonly string _connectionString;

        private readonly string _tableName;
        private readonly string _schemaName;

        public PostgreSqlSink(string connectionString,
            string tableName,
            int batchPostingLimit,
            TimeSpan period,
            string schemaName = "public") : base(batchPostingLimit, period)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentNullException(nameof(tableName));

            _connectionString = connectionString;
            _tableName = tableName;
            _schemaName = schemaName;
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            var logEvents = events as LogEvent[] ?? events.ToArray();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            var insertBuilder = new StringBuilder();
                            insertBuilder.Append($"INSERT INTO {_schemaName}.{_tableName} (");
                            insertBuilder.Append("Timestamp, Level, Message, Exception, Properties) ");
                            insertBuilder.Append("VALUES (@timestamp, @level, @message, @exception, @properties)");

                            cmd.CommandText = insertBuilder.ToString();

                            cmd.Parameters.Add(new NpgsqlParameter("@timestamp", NpgsqlDbType.TimestampTZ));
                            cmd.Parameters.Add(new NpgsqlParameter("@level", NpgsqlDbType.Varchar));
                            cmd.Parameters.Add(new NpgsqlParameter("@message", NpgsqlDbType.Varchar));
                            cmd.Parameters.Add(new NpgsqlParameter("@exception", NpgsqlDbType.Varchar));
                            cmd.Parameters.Add(new NpgsqlParameter("@properties", NpgsqlDbType.Json));

                            cmd.Transaction = transaction;

                            foreach (var logEvent in logEvents)
                            {
                                cmd.Parameters["@timestamp"].Value = logEvent.Timestamp;
                                cmd.Parameters["@level"].Value = logEvent.Level.ToString();
                                cmd.Parameters["@message"].Value = logEvent.MessageTemplate.ToString();
                                cmd.Parameters["@exception"].Value =
                                    (object)logEvent.Exception?.ToString() ?? DBNull.Value;
                                cmd.Parameters["@properties"].Value = logEvent.Properties.Json();

                                await cmd.ExecuteNonQueryAsync();
                            }

                            await transaction.CommitAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Error when writing {0} events to database: {1}", logEvents.Count(), ex.Message);
            }
        }
    }
}