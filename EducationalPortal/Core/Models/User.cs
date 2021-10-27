using System.Collections.Generic;
using Core.Models.ManyToMany;
using Core.Models.Materials;

namespace Core.Models
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        
        public string Password { get; set; }
        
        public List<Course> CurrentCourses { get; set; }
        
        public List<Material> Materials { get; set; }
        
        public List<UserSkill> Skills { get; set; }
    }
}
