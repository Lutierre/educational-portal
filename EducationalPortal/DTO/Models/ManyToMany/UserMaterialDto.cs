namespace DTO.Models.ManyToMany
{
    public class UserMaterialDto : BaseEntityDto
    {
        public int UserId { get; set; }
        
        public int MaterialId { get; set; }
    }
}
