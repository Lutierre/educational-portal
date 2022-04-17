using System.Collections.Generic;
using System.Linq;
using BLL.Abstractions.Interfaces;
using Core.Models;
using Core.Models.Materials;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IEntityDalService<Material> _materialDalService;
        private readonly ICurrentStateService _currentStateService;
        private readonly IEntityDalService<Course> _courseDalService;
        private readonly IEntityDalService<User> _userDalService;

        public MaterialService(ICurrentStateService currentStateService, 
            IEntityDalService<Course> courseDalService,
            IEntityDalService<User> userDalService,
            IEntityDalService<Material> materialDalService)
        {
            _currentStateService = currentStateService;
            _courseDalService = courseDalService;
            _userDalService = userDalService;
            _materialDalService = materialDalService;
        }

        private void AddMaterial(Material material)
        {
            var currentCourse = _currentStateService.CurrentCourse;
            currentCourse.Materials.Add(material);
            _courseDalService.Update(currentCourse);
        }
        
        public void AddNewArticle(string title, string date, string source)
        {
           var article = _materialDalService.Add(new Article
           {
               Title = title,
               Date = date,
               Source = source
           });
           
           AddMaterial(article);
        }

        public void AddExistingArticle(int id)
        {
            var article = _materialDalService.Get(id);
            AddMaterial(article);
        }

        public void AddNewBook(string title, string authors, int pageCount, string format, int year)
        {
            var book = _materialDalService.Add(new Book
            {
                Title = title,
                Authors = authors, 
                PageCount = pageCount,
                Year =  year
            });
            
            AddMaterial(book);
        }

        public void AddExistingBook(int id)
        {
            var book = _materialDalService.Get(id);
            AddMaterial(book);
        }

        public void AddNewVideo(string title, string duration, string quality)
        {
            var video = _materialDalService.Add(new Video { 
                Title = title, 
                Duration = duration,
                Quality = quality 
            });
            
            AddMaterial(video);
        }

        public void AddExistingVideo(int id)
        {
            var video = _materialDalService.Get(id);
            AddMaterial(video);
        }

        public List<Article> GetArticles()
        {
            var result = _currentStateService.CurrentCourse.Materials
                .Where(material => material.Type == "Article")
                .Select(material => material as Article).ToList();

            return result;
        }

        public List<Book> GetBooks()
        {
            var result = _currentStateService.CurrentCourse.Materials
                .Where(material => material.Type == "Book")
                .Select(material => material as Book).ToList();

            return result;
        }

        public List<Video> GetVideos()
        {
            var result = _currentStateService.CurrentCourse.Materials
                .Where(material => material.Type == "Video")
                .Select(material => material as Video).ToList();

            return result;
        }

        public void LearnMaterial()
        {
            var currentUser = _currentStateService.AuthorizedUser;
            currentUser.Materials.Add(_currentStateService.CurrentMaterial);
            _userDalService.Update(currentUser);
        }
    }
}
