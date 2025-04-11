namespace CatAPI.Models
{
    public class CatImage
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Breed> Breeds { get; set; }
    }
}
