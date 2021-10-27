namespace Core.Models.Materials
{
    public class Video : Material
    {
        public Material Material { get; set; }
        
        public string Duration { get; set; }
        
        public string Quality { get; set; }
    }
}
