using System;
using BLL.Abstractions.Interfaces;
using UI.Abstractions.Interfaces;

namespace EducationalPortal.Views
{
    public class ProfileView : IProfileView
    {
        private readonly IUserService _userService;
        private readonly ICreateCourseView _createCourseView;
        private readonly ICoursesListView _coursesListView;

        public ProfileView(IUserService userService,
            ICreateCourseView createCourseView, 
            ICoursesListView coursesListView)
        {
            _userService = userService;
            _createCourseView = createCourseView;
            _coursesListView = coursesListView;
        }


        public void Start()
        {
            Console.Clear();
            
            var nickname = _userService.AuthorizedUser.Nickname;
            var password = new string('*', _userService.AuthorizedUser.Password.Length);   
            
            Console.WriteLine($"Добро пожаловать в личный кабинет, {nickname}!\n" +
                              $"Ваш пароль: {password}");
            Console.WriteLine("Список всех ваших умений и очков:");

            var userSkills = _userService.GetUserSkills();

            foreach (var skill in userSkills)
            {
                Console.WriteLine($"- {skill.Skill.Title}: {skill.Points}");
            }
            
            Console.WriteLine("Список всех пройденных курсов:");

            var completedCourses = _userService.GetCompletedCourses();

            foreach (var course in completedCourses)
            {
                Console.WriteLine($"- {course.Title}");
            }
            
            Console.WriteLine("Список всех курсов с процентом прохождения:");

            var coursesProgress = _userService.GetCoursesProgress();

            foreach (var (course, progress) in coursesProgress)
            {
                Console.WriteLine($"- {course.Title}: ${progress}%");
            }
            
            Console.WriteLine("Чего изволите? Выберите действие:\n" +
                              "c - список всех доступных курсов;\n" +
                              "n - создание нового курса;" +
                              "q - разлогиниться");

            var input = Console.ReadKey().KeyChar;

            switch (input)
            {
                case 'c':
                    _coursesListView.Start();
                    break;
                
                case 'n':
                    _createCourseView.Start();
                    break;
                
                case 'q':
                    _userService.Logout();
                    return;
            }
        }
    }
}
