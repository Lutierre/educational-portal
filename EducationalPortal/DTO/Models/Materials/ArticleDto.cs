namespace DTO.Models.Materials
{
    public class ArticleDto : MaterialDto
    {
        public int MaterialId { get; set; }
        
        public string Date { get; set; }
        
        public string Source { get; set; }
    }
}
