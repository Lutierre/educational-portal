using DTO.Models;

namespace Core.Models.ManyToMany
{
    public class UserCourse : BaseEntity
    {
        public int UserId { get; set; }
        
        public int CourseId { get; set; }
    }
}
