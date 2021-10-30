using System;
using BLL.Abstractions.Interfaces;
using UI.Abstractions.Interfaces;

namespace EducationalPortal
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
