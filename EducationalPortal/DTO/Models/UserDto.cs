using System.Collections.Generic;
using Core.Models.Materials;
using DTO.Models;

namespace Core.Models
{
    public class UserDto : BaseEntityDto
    {
        public string Nickname { get; set; }
        
        public string Password { get; set; }
    }
}
