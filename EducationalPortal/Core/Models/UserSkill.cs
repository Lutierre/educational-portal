namespace Core.Models
{
    public class UserSkill : BaseEntity
    {
        public Skill Skill { get; set; }
        
        public int Points { get; set; }
    }
}
