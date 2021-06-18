using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class RecipieInfo
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string imageType { get; set; }
    }

    public class RecipieInfoList
    {
        public List<RecipieInfo> results { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
    }
}