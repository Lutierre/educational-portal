using System;
using Core.Models;
using Core.Models.Materials;
using DAL.Abstractions.Interfaces;
using DAL.Services;
using DAL.Services.EntityDalService;
using Microsoft.Extensions.DependencyInjection;
using UI.Abstractions.Interfaces;

namespace EducationalPortal.Services
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
                .AddTransient(typeof(IGenericDalService<>), typeof(GenericDalService<>))
                .BuildServiceProvider();

            var startPoint = serviceProvider.GetService<IApplication>();

            startPoint.Start();
        }
    }
}
