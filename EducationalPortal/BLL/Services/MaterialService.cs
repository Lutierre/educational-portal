using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models.Materials;

namespace BLL.Services
{
    public class MaterialService : IMaterialService
    {
        public void AddNewArticle(string title, string date, string source)
        {
            throw new System.NotImplementedException();
        }

        public void AddExistingArticle(int id)
        {
            throw new System.NotImplementedException();
        }

        public void AddNewBook(string title, string authors, int pageCount, string format, int year)
        {
            throw new System.NotImplementedException();
        }

        public void AddExistingBook(int id)
        {
            throw new System.NotImplementedException();
        }

        public void AddNewVideo(string title, string duration, string quality)
        {
            throw new System.NotImplementedException();
        }

        public void AddExistingVideo(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Article> GetArticles()
        {
            throw new System.NotImplementedException();
        }

        public List<Book> GetBooks()
        {
            throw new System.NotImplementedException();
        }

        public List<Video> GetVideos()
        {
            throw new System.NotImplementedException();
        }

        public void LearnMaterial()
        {
            throw new System.NotImplementedException();
        }
    }
}
