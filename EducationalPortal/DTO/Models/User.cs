using System.Collections.Generic;
using Core.Models.ManyToMany;
using Core.Models.Materials;
using DTO.Models;

namespace Core.Models
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        
        public string Password { get; set; }
    }
}
