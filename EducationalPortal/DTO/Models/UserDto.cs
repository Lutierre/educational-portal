namespace DTO.Models
{
    public class UserDto : BaseEntityDto
    {
        public string Nickname { get; set; }
        
        public string Password { get; set; }
    }
}
