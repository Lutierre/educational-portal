using DTO.Models;

namespace Core.Models.ManyToMany
{
    public class CourseSkill : BaseEntity
    {
        public int CourseId { get; set; }
        
        public int SkillId { get; set; }
    }
}
