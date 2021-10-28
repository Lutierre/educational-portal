namespace DTO.Models.ManyToMany
{
    public class CourseSkill : BaseEntityDto
    {
        public int CourseId { get; set; }
        
        public int SkillId { get; set; }
    }
}
