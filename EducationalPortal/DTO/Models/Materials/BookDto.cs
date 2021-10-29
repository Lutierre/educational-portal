namespace DTO.Models.Materials
{
    public class BookDto : AbstractMaterialDto
    {
        public string Authors { get; set; }
        
        public int Year { get; set; }
        
        public int PageCount { get; set; }
        
        public string Format { get; set; }
    }
}
