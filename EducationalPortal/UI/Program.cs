using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.Models;
using Core.Models.Materials;
using DAL.Abstractions.Interfaces;
using DAL.Services;
using DAL.Services.EntityDalService;
using EducationalPortal.Views;
using Microsoft.Extensions.DependencyInjection;
using UI.Abstractions.Interfaces;

namespace EducationalPortal
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IApplication, Application>()
                .AddTransient<IEntityDalService<User>, UserDalService>()
                .AddTransient<IEntityDalService<Course>, CourseDalService>()
                .AddTransient<IEntityDalService<Material>, MaterialDalService>()
                .AddTransient<IEntityDalService<Skill>, SkillDalService>()
                .AddTransient(typeof(IGenericDtoService<>), typeof(GenericDtoService<>))
                .AddTransient<IUserService, UserService>()
                .AddTransient<IAuthorizationView, AuthorizationView>()
                .AddTransient<ICoursesListView, CoursesListView>()
                .AddTransient<ICourseView, CourseView>()
                .AddTransient<ICreateCourseView, CreateCourseView>()
                .AddTransient<IMaterialView, MaterialView>()
                .AddTransient<IProfileView, ProfileView>()
                .AddTransient<ICourseService, CourseService>()
                .AddTransient<IMaterialService, MaterialService>()
                .AddTransient<ISkillService, SkillService>()
                .AddTransient<IUserService, UserService>()
                .AddSingleton<ICurrentStateService, CurrentStateService>()
                .BuildServiceProvider();

            var startPoint = serviceProvider.GetService<IApplication>();

            startPoint.Start();
        }
    }
}
