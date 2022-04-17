using System;
using BLL.Abstractions.Interfaces;
using UI.Abstractions.Interfaces;

namespace EducationalPortal.Views
{
    public class CourseView : ICourseView
    {
        private readonly IMaterialView _materialView;
        private readonly ICurrentStateService _currentStateService;
        private readonly ICourseService _courseService;

        public CourseView(IMaterialView materialView,
            ICurrentStateService currentStateService,
            ICourseService courseService)
        {
            _materialView = materialView;
            _currentStateService = currentStateService;
            _courseService = courseService;
        }

        public void Start()
        {
            var course = _currentStateService.CurrentCourse;

            Console.WriteLine($"Название курса: {course.Title}\n" +
                              $"Создатель курса: {course.Author.Nickname}\n" +
                              $"Описание курса: {course.Description}");

            Console.WriteLine($"Список умений:");

            foreach (var skill in course.Skills)
            {
                Console.WriteLine($"- {skill.Title}");
            }

            Console.WriteLine("Список материалов:");

            foreach (var material in course.Materials)
            {
                Console.WriteLine($"{material.Id}. {material.Title}");
            }

            var input = String.Empty;

            while (input != "h")
            {
                Console.WriteLine("Введите id материала, чтобы открыть его.\n" +
                                  "Введите s, чтобы подписаться на курс." +
                                  "Введите h, если харе!");

                input = Console.ReadLine();

                switch (input)
                {
                    case "s": 
                        _courseService.SubscribeCourse();
                        Console.WriteLine("Поздравляем с подпиской на курс!");
                        break;
                        
                    case "h":
                        break;
                    
                    default:
                        int.TryParse(input, out var id);
                        _currentStateService.OpenMaterial(id);
                        _materialView.Start();
                        break;
                }
            }
            
            _currentStateService.CloseCourse();
        }
    }
}
