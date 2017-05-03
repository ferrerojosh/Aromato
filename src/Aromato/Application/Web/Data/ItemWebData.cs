namespace Aromato.Application.Web.Data
{
    public class ItemWebData : IData
    {
        public long Id { get; set; }
        public string UniqueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get;  set; }
        public string DateAdded { get;  set; }
        public string LastUpdated { get; set; }
}
}
