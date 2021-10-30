using System;
using BLL.Abstractions.Interfaces;
using UI.Abstractions.Interfaces;

namespace EducationalPortal.Views
{
    public class CoursesListView : ICoursesListView
    {
        private readonly IUserService _userService;
        private readonly ICourseView _courseView;

        public CoursesListView(IUserService userService, ICourseView courseView)
        {
            _userService = userService;
            _courseView = courseView;
        }

        public void Start()
        {
            Console.Clear();
            
            Console.WriteLine("Введите id курса (без точки), чтобы открыть его.\n" +
                              "Введите 0, если хотите выйти:");

            var coursesList = _userService.GetAvailableCourses();

            foreach (var course in coursesList)
            {
                Console.WriteLine($"{course.Id}. {course.Title}");
            }

            int.TryParse(Console.ReadLine(), out var input);

            if (input == 0)
            {
                return;
            }
            
            _userService.OpenCourse(input);
            _courseView.Start();
        }
    }
}
