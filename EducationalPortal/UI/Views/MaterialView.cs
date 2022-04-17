using System;
using BLL.Abstractions.Interfaces;
using Core.Models.Materials;
using UI.Abstractions.Interfaces;

namespace EducationalPortal.Views
{
    public class MaterialView : IMaterialView
    {
        private readonly IMaterialService _materialService;
        private readonly ICurrentStateService _currentStateService;

        public MaterialView(ICurrentStateService currentStateService, IMaterialService materialService)
        {
            _currentStateService = currentStateService;
            _materialService = materialService;
        }

        public void Start()
        {
            Console.Clear();

            var material = _currentStateService.CurrentMaterial;
            
            Console.WriteLine("Информация о материале:\n" +
                              $"Название: {material.Title}");

            switch (material.Type)
            {
                case "Article":
                    var article = material as Article;
                    Console.WriteLine($"Дата публикации: {article.Date}\n" +
                                      $"Ресурс: {article.Source}");
                    break;
                case "Book": 
                    var book = material as Book;
                    Console.WriteLine($"Автор(ы): {book.Authors}\n" +
                                      $"Количество страниц: {book.PageCount}\n" +
                                      $"Формат: {book.Format}\n" +
                                      $"Год издания: {book.Year}");
                    break;
                case "Video": 
                    var video = material as Video;
                    Console.WriteLine($"Длительность: {video.Duration}\n" +
                                      $"Качество: {video.Quality}");
                    break;
            }
            
            Console.WriteLine("Вы ознакомились с материалом? (y - да)");

            if (Console.ReadKey().KeyChar == 'y')
            {
                _materialService.LearnMaterial();
                Console.WriteLine("Поздравляем! Вы изучили материал :)");
            }
            else
            {
                Console.WriteLine("Вы обязательно изучите его потом, мы вам обещаем (и ни разу не угрожаем)");
            }
            
            Console.WriteLine("Нажмите любую клавишу для возврата:");
            Console.ReadKey();
            
            _currentStateService.CloseMaterial();
        }
    }
}
