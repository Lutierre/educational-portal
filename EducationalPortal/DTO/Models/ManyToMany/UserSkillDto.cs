namespace DTO.Models.ManyToMany
{
    public class UserSkillDto : BaseEntityDto
    {
        public int UserId { get; set; }
        
        public int SkillId { get; set; }
        
        public int Points { get; set; }
    }
}
