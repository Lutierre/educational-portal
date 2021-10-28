using DTO.Models;

namespace Core.Models.ManyToMany
{
    public class CourseMaterial : BaseEntity
    {
        public int CourseId { get; set; }
        
        public int MaterialId { get; set; }
    }
}
