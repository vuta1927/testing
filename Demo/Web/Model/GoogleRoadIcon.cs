namespace Web.Model
{
    public class GoogleRoadIcon
    {
        public int Id { get; set; }
        public int GoogleRoadId { get; set; }
        public virtual GoogleRoad GoogleRoad { get; set; }
        public string Url { get; set; }
        public string Descriptions { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Location { get; set; }
    }
}
