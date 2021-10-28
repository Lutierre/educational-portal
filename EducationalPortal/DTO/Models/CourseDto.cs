using System.Collections.Generic;
using Core.Models.Materials;

namespace DTO.Models
{
    public class CourseDto : BaseEntityDto
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int AuthorId { get; set; }
    }
}
