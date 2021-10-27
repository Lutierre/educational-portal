namespace Core.Models.Materials
{
    public class Book : Material
    {
        public Material Material { get; set; }
        
        public string Authors { get; set; }
        
        public int Year { get; set; }
        
        public int PageCount { get; set; }
        
        public string Format { get; set; }
    }
}
