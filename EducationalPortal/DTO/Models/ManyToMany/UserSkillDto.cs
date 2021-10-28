namespace DTO.Models.ManyToMany
{
    public class UserSkill : BaseEntityDto
    {
        public int UserId { get; set; }
        
        public int SkillId { get; set; }
        
        public int Points { get; set; }
    }
}
