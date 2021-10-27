namespace Core.Models.Materials
{
    public class Article : Material
    {
        public Material Material { get; set; }
        
        public string Date { get; set; }
        
        public string Source { get; set; }
    }
}
