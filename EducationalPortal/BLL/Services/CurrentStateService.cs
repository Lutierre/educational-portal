using BLL.Abstractions.Interfaces;
using Core.Models;
using Core.Models.Materials;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class CurrentStateService : ICurrentStateService
    {
        private readonly IEntityDalService<Course> _courseDalService;
        private readonly IEntityDalService<Material> _materialDalService;
        
        public CurrentStateService(IEntityDalService<Course> courseDalService,
            IEntityDalService<Material> materialDalService)
        {
            _courseDalService = courseDalService;
            _materialDalService = materialDalService;
        }

        public User AuthorizedUser { get; set; }
        
        public Course CurrentCourse { get; set; }
        
        public Material CurrentMaterial { get; set; }
        
        public void OpenCourse(int id)
        {
            CurrentCourse = _courseDalService.Get(id);
        }

        public void CloseCourse()
        {
            CurrentCourse = default;
        }

        public void OpenMaterial(int id)
        {
            CurrentMaterial = _materialDalService.Get(id);
        }

        public void CloseMaterial()
        {
            CurrentMaterial = default;
        }
    }
}
