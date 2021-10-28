namespace Core.Models.Materials
{
    public class Video : Material
    {
        public int MaterialId { get; set; }
        
        public string Duration { get; set; }
        
        public string Quality { get; set; }
    }
}
