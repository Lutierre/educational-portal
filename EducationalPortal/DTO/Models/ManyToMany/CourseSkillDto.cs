namespace DTO.Models.ManyToMany
{
    public class CourseSkillDto : BaseEntityDto
    {
        public int CourseId { get; set; }
        
        public int SkillId { get; set; }
    }
}
