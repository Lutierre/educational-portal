using Core.Models;
using Core.Models.Materials;

namespace BLL.Abstractions.Interfaces
{
    public interface ICurrentStateService
    {
        public User AuthorizedUser { get; set; }
        
        public Course CurrentCourse { get; set; }
        
        public Material CurrentMaterial { get; set; }
        
        void OpenCourse(int id);
        
        void CloseCourse();
        
        void OpenMaterial(int id);

        void CloseMaterial();
        
        
    }
}
