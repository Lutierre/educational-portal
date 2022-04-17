using System.Collections.Generic;
using Core.Models.Materials;

namespace Core.Models
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public User Author { get; set; }
        
        public int AuthorId { get; set; }
        
        public List<Skill> Skills { get; set; }
        
        public List<Material> Materials { get; set; }

        public bool IsAvailable => Materials?.Count > 0;
    }
}
