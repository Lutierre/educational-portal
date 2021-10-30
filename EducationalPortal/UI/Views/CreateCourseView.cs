using System;
using BLL.Abstractions.Interfaces;
using UI.Abstractions.Interfaces;
using static System.String;

namespace EducationalPortal.Views
{
    public class CreateCourseView : ICreateCourseView
    {
        private readonly IUserService _userService;

        public CreateCourseView(IUserService userService)
        {
            _userService = userService;
        }

        public void Start()
        {
            Console.Clear();
            
            Console.WriteLine("Введите название вашего курса:");
            var courseTitle = Console.ReadLine();
            
            Console.WriteLine("Введите краткое описание курса:");
            var description = Console.ReadLine();
            
            Console.WriteLine("Введите умения вашего курса через запятую");
            var skillNames = Console.ReadLine().Split(',');

            _userService.CreateCourse(courseTitle, description, skillNames);

            var input = Empty;
            
            while (input != "h")
            {
                Console.WriteLine("Добавьте материалы в курс:\n" +
                                  "na - новая статья" +
                                  "nb - новая книга" +
                                  "nv - новое видео" +
                                  "a - существующая статья" +
                                  "b - существующая книга" +
                                  "v - существующее видео" +
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

                        _userService.AddNewArticle(title, date, source);
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

                        _userService.AddNewBook(title, authors, pageCount, format, year);
                        break;
                    
                    case "nv":
                        Console.WriteLine("Введите название:");
                        title = Console.ReadLine();

                        Console.WriteLine("Введите длительность видео:");
                        var duration = Console.ReadLine();

                        Console.WriteLine("Введите качество:");
                        var quality = Console.ReadLine();

                        _userService.AddNewVideo(title, duration, quality);
                        break;
                    
                    case "a":
                        Console.WriteLine("Введите id нужной статьи:");
                        var articles = _userService.GetArticles();

                        foreach (var article in articles)
                        {
                            Console.WriteLine($"{article.Id}. {article.Title}");
                        }

                        int.TryParse(Console.ReadLine(), out var id);
                        _userService.AddExistingArticle(id);
                        break;
                    
                    case "b":
                        Console.WriteLine("Введите id нужной книги:");
                        var books = _userService.GetBooks();

                        foreach (var book in books)
                        {
                            Console.WriteLine($"{book.Id}. {book.Title}");
                        }

                        int.TryParse(Console.ReadLine(), out id);
                        _userService.AddExistingBook(id);
                        break;
                    
                    case "v":
                        Console.WriteLine("Введите id нужного видео:");
                        var videos = _userService.GetVideos();

                        foreach (var video in videos)
                        {
                            Console.WriteLine($"{video.Id}. {video.Title}");
                        }

                        int.TryParse(Console.ReadLine(), out id);
                        _userService.AddExistingVideo(id);
                        break;
                }
            }
        }
    }
}
