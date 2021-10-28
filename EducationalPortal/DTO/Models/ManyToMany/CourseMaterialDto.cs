namespace DTO.Models.ManyToMany
{
    public class CourseMaterialDto : BaseEntityDto
    {
        public int CourseId { get; set; }
        
        public int MaterialId { get; set; }
    }
}
