using System;
using Serilog.Configuration;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Sinks.PostgreSQL
{
    public static class LoggerConfigurationPostgreSqlExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events to a table in a PostgreSQL database.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="connectionString">The connection string to the database where to store the events.</param>
        /// <param name="tableName">Name of the table to store the events in.</param>
        /// <param name="schemaName">Name of the schema for the table to store the data in. The default is 'dbo'.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration PostgreSqlServer(
            this LoggerSinkConfiguration loggerConfiguration,
            string connectionString,
            string tableName,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = PostgreSqlSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            string schemaName = "public"
        )
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));

            var defaultedPeriod = period ?? PostgreSqlSink.DefaultPeriod;

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            try
            {
                return loggerConfiguration.Sink(
                new PostgreSqlSink(
                    connectionString,
                    tableName,
                    batchPostingLimit,
                    defaultedPeriod,
                    schemaName
                ),
                restrictedToMinimumLevel);
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine(ex.Message);
                throw;
            }
        }
    }
}