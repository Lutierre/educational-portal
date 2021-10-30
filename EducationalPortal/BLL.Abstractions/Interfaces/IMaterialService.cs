using System.Collections.Generic;
using Core.Models.Materials;

namespace BLL.Abstractions.Interfaces
{
    public interface IMaterialService
    {
        void AddNewArticle(string title, string date, string source);
        
        void AddExistingArticle(int id);
        
        void AddNewBook(string title, string authors, int pageCount, string format, int year);
        
        void AddExistingBook(int id);
        
        void AddNewVideo(string title, string duration, string quality);
        
        void AddExistingVideo(int id);

        List<Article> GetArticles();
        
        List<Book> GetBooks();
        
        List<Video> GetVideos();
        
        void LearnMaterial();
    }
}
