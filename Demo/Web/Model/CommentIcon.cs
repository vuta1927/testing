namespace Web.Model
{
    public class CommentIcon
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public virtual Map Map { get; set; }
        public string Url { get; set; }
        public string Descriptions { get; set; }
    }
}
