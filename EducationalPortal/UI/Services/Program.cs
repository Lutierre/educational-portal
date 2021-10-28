using System;
using DAL.Abstractions.Interfaces;
using DAL.Services;
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
                .AddTransient(typeof(IEntityDalService<>), typeof(UserDalService<>))
                .AddTransient(typeof(IGenericDalService<>), typeof(GenericDalService<>))
                .BuildServiceProvider();

            var startPoint = serviceProvider.GetService<IApplication>();

            startPoint.Start();
        }
    }
}
