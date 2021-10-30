using BLL.Abstractions.Interfaces;
using Core.Models;
using Core.Models.Materials;

namespace BLL.Services
{
    public class CurrentStateService : ICurrentStateService
    {
        public User AuthorizedUser { get; set; }
        
        public Course CurrentCourse { get; set; }
        
        public Material CurrentMaterial { get; set; }
        
        public void OpenCourse(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CloseCourse()
        {
            throw new System.NotImplementedException();
        }

        public void OpenMaterial(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CloseMaterial()
        {
            throw new System.NotImplementedException();
        }
    }
}
