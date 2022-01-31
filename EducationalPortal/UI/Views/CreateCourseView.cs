using System;
using System.Text.RegularExpressions;
using BLL.Abstractions.Interfaces;
using UI.Abstractions.Interfaces;
using static System.String;

namespace EducationalPortal.Views
{
    public class CreateCourseView : ICreateCourseView
    {
        private readonly ICourseService _courseService;
        private readonly IMaterialService _materialService;

        public CreateCourseView(ICourseService courseService, IMaterialService materialService)
        {
            _courseService = courseService;
            _materialService = materialService;
        }

        public void Start()
        {
            Console.Clear();
            
            Console.WriteLine("Введите название вашего курса:");
            var courseTitle = Console.ReadLine();
            
            Console.WriteLine("Введите краткое описание курса:");
            var description = Console.ReadLine();
            
            Console.WriteLine("Введите умения вашего курса через запятую");
            var skillNames = Regex.Split(Console.ReadLine(), @", *");

            _courseService.CreateCourse(courseTitle, description, skillNames);

            var input = Empty;
            
            while (input != "h")
            {
                Console.WriteLine("Добавьте материалы в курс:\n" +
                                  "na - новая статья\n" +
                                  "nb - новая книга\n" +
                                  "nv - новое видео\n" +
                                  "a - существующая статья\n" +
                                  "b - существующая книга\n" +
                                  "v - существующее видео\n" +
                                  "h - харе!");

                input = Console.ReadLine();

                switch (input)
                {
                    case "na":
                        Console.WriteLine("Введите название:");
                        var title = Console.ReadLine();

                        Console.WriteLine("Введите дату публикации:");
                        var date = Console.ReadLine();

                        Console.WriteLine("Введите ресурс:");
                        var source = Console.ReadLine();

                        _materialService.AddNewArticle(title, date, source);
                        break;
                    
                    case "nb":
                        Console.WriteLine("Введите название:");
                        title = Console.ReadLine();

                        Console.WriteLine("Введите авторов:");
                        var authors = Console.ReadLine();

                        Console.WriteLine("Введите количество страниц числом:");
                        int.TryParse(Console.ReadLine(), out var pageCount);
                        
                        Console.WriteLine("Введите формат:");
                        var format = Console.ReadLine();
                        
                        Console.WriteLine("Введите год издания:");
                        int.TryParse(Console.ReadLine(), out var year);

                        _materialService.AddNewBook(title, authors, pageCount, format, year);
                        break;
                    
                    case "nv":
                        Console.WriteLine("Введите название:");
                        title = Console.ReadLine();

                        Console.WriteLine("Введите длительность видео:");
                        var duration = Console.ReadLine();

                        Console.WriteLine("Введите качество:");
                        var quality = Console.ReadLine();

                        _materialService.AddNewVideo(title, duration, quality);
                        break;
                    
                    case "a":
                        Console.WriteLine("Введите id нужной статьи:");
                        var articles = _materialService.GetArticles();

                        foreach (var article in articles)
                        {
                            Console.WriteLine($"{article.Id}. {article.Title}");
                        }

                        int.TryParse(Console.ReadLine(), out var id);
                        _materialService.AddExistingArticle(id);
                        break;
                    
                    case "b":
                        Console.WriteLine("Введите id нужной книги:");
                        var books = _materialService.GetBooks();

                        foreach (var book in books)
                        {
                            Console.WriteLine($"{book.Id}. {book.Title}");
                        }

                        int.TryParse(Console.ReadLine(), out id);
                        _materialService.AddExistingBook(id);
                        break;
                    
                    case "v":
                        Console.WriteLine("Введите id нужного видео:");
                        var videos = _materialService.GetVideos();

                        foreach (var video in videos)
                        {
                            Console.WriteLine($"{video.Id}. {video.Title}");
                        }

                        int.TryParse(Console.ReadLine(), out id);
                        _materialService.AddExistingVideo(id);
                        break;
                }
            }
        }
    }
}
