using System.Collections.Generic;
using Core.Models;
using Core.Models.Materials;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
        public User AuthorizedUser { get; set; }
        
        public Course CurrentCourse { get; set; }
        
        public Material CurrentMaterial { get; set; }
        
        string Authorize(string nickname, string password);

        void RegisterUser(string nickname, string password);

        List<UserSkill> GetUserSkills();

        List<Course> GetCompletedCourses();

        List<(Course, int)> GetCoursesProgress();

        List<Course> GetAvailableCourses();

        void OpenCourse(int id);
        
        void CloseCourse();

        void SubscribeCourse();

        void OpenMaterial(int id);

        void CloseMaterial();

        void LearnMaterial();

        void CreateCourse(string title, string description, string[] skillNames);

        void AddNewArticle(string title, string date, string source);
        
        void AddExistingArticle(int id);
        
        void AddNewBook(string title, string authors, int pageCount, string format, int year);
        
        void AddExistingBook(int id);
        
        void AddNewVideo(string title, string duration, string quality);
        
        void AddExistingVideo(int id);

        List<Article> GetArticles();
        
        List<Book> GetBooks();
        
        List<Video> GetVideos();

        void Logout();
    }
}
