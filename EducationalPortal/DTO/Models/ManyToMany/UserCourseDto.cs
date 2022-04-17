namespace DTO.Models.ManyToMany
{
    public class UserCourseDto : BaseEntityDto
    {
        public int UserId { get; set; }
        
        public int CourseId { get; set; }
    }
}
