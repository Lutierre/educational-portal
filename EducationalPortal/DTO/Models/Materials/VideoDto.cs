namespace Core.Models.Materials
{
    public class VideoDto : MaterialDto
    {
        public int MaterialId { get; set; }
        
        public string Duration { get; set; }
        
        public string Quality { get; set; }
    }
}
