using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models;
using Core.Models.Materials;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        public User AuthorizedUser { get; set; }
        
        public Course CurrentCourse { get; set; }
        
        public Material CurrentMaterial { get; set; }

        public string Authorize(string nickname, string password)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterUser(string nickname, string password)
        {
            throw new System.NotImplementedException();
        }

        public List<UserSkill> GetUserSkills()
        {
            throw new System.NotImplementedException();
        }

        public List<Course> GetCompletedCourses()
        {
            throw new System.NotImplementedException();
        }

        public List<(Course, int)> GetCoursesProgress()
        {
            throw new System.NotImplementedException();
        }

        public List<Course> GetAvailableCourses()
        {
            throw new System.NotImplementedException();
        }

        public void OpenCourse(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CloseCourse()
        {
            throw new System.NotImplementedException();
        }

        public void SubscribeCourse()
        {
            throw new System.NotImplementedException();
        }

        public void OpenMaterial(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CloseMaterial()
        {
            throw new System.NotImplementedException();
        }

        public void LearnMaterial()
        {
            throw new System.NotImplementedException();
        }

        public void CreateCourse(string title, string description, string[] skillNames)
        {
            // TODO: make course current
            
            throw new System.NotImplementedException();
        }

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

        public void Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}
