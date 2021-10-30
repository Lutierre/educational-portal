using System;
using BLL.Abstractions.Interfaces;
using UI.Abstractions.Interfaces;

namespace EducationalPortal.Views
{
    public class CourseView : ICourseView
    {
        private readonly IUserService _userService;
        private readonly IMaterialView _materialView;

        public CourseView(IUserService userService, IMaterialView materialView)
        {
            _userService = userService;
            _materialView = materialView;
        }

        public void Start()
        {
            var course = _userService.CurrentCourse;

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
                        _userService.SubscribeCourse();
                        Console.WriteLine("Поздравляем с подпиской на курс!");
                        break;
                        
                    case "h":
                        break;
                    
                    default:
                        int.TryParse(input, out var id);
                        _userService.OpenMaterial(id);
                        _materialView.Start();
                        break;
                }
            }
            
            _userService.CloseCourse();
        }
    }
}
