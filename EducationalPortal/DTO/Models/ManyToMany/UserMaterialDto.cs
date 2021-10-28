namespace DTO.Models.ManyToMany
{
    public class UserMaterial : BaseEntityDto
    {
        public int UserId { get; set; }
        
        public int MaterialId { get; set; }
    }
}
