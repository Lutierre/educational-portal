using DTO.Models;

namespace Core.Models.ManyToMany
{
    public class UserSkill : BaseEntity
    {
        public int UserId { get; set; }
        
        public int SkillId { get; set; }
        
        public int Points { get; set; }
    }
}
