using SQLite;

namespace PDM
{
    public class Items
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string artist { get; set; }
        public string thumbnail_image { get; set; }
        public string url { get; set; }
    }
}