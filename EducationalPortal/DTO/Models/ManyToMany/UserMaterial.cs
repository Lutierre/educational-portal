using DTO.Models;

namespace Core.Models.ManyToMany
{
    public class UserMaterial : BaseEntity
    {
        public int UserId { get; set; }
        
        public int MaterialId { get; set; }
    }
}
