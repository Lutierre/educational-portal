using System;
using BLL.Abstractions.Interfaces;
using UI.Abstractions.Interfaces;

namespace EducationalPortal.Views
{
    public class CoursesListView : ICoursesListView
    {
        private readonly ICurrentStateService _currentStateService;
        private readonly ICourseService _courseService;
        private readonly ICourseView _courseView;

        public CoursesListView(ICurrentStateService currentStateService, 
            ICourseView courseView,
            ICourseService courseService)
        {
            _courseView = courseView;
            _courseService = courseService;
            _currentStateService = currentStateService;
        }

        void IView.Start()
        {
            Console.Clear();
            
            Console.WriteLine("Введите id курса (без точки), чтобы открыть его.\n" +
                              "Введите 0, если хотите выйти:");

            var coursesList = _courseService.GetAvailableCourses();

            foreach (var course in coursesList)
            {
                Console.WriteLine($"{course.Id}. {course.Title}");
            }

            int.TryParse(Console.ReadLine(), out var input);

            if (input == 0)
            {
                return;
            }
            
            _currentStateService.OpenCourse(input);
            _courseView.Start();
        }
    }
}
