using System;
using UI.Abstractions.Interfaces;

namespace EducationalPortal.Services
{
    public class Application : IApplication
    {
        public void Start()
        {
            Console.WriteLine("Hello world!");
        }

        public void Add()
        {
            throw new System.NotImplementedException();
        }
    }
}
