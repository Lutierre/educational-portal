namespace DTO.Models.ManyToMany
{
    public class UserCourse : BaseEntityDto
    {
        public int UserId { get; set; }
        
        public int CourseId { get; set; }
    }
}
