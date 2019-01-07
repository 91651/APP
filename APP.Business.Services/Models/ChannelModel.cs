namespace APP.Business.Services.Models
{
    public class ChannelModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
    }
}
