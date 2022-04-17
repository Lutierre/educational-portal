using System;
using BLL.Abstractions.Interfaces;
using UI.Abstractions.Interfaces;

namespace EducationalPortal.Views
{
    public class AuthorizationView : IAuthorizationView
    {
        private readonly IUserService _userService;
        private readonly IProfileView _profileView;

        public AuthorizationView(IUserService userService, IProfileView profileView)
        {
            _userService = userService;
            _profileView = profileView;
        }
        
        public void Start()
        {
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine(
                    "Добро пожаловать на EducationalPortal!\n" +
                    "Введите свои логин и пароль (через пробел).\n" +
                    "Для выхода из программы введите Exit:");
                var input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    return;
                }

                var credentials = input.Split(" ");
                
                var nickname = credentials[0];
                var password = credentials[1];

                var result = _userService.Authorize(nickname, password);

                switch (result)
                {
                    case "wrong-password":
                        Console.WriteLine("Неверный пароль для пользователя\nНажмите любую клавишу:");
                        Console.ReadKey();
                        break;
                    
                    case "success":
                        _profileView.Start();
                        break;
                    
                    case "registered":
                        Console.WriteLine("Вы зарегистрированы!\n" +
                                          "Нажмите любую клавишу, чтобы перейти в личный кабинет:");
                        Console.ReadKey();
                        _profileView.Start();
                        break;
                    
                    default:
                        Console.WriteLine("Вы нашли пасхалку! Отпишитесь разработчику об этом кейсе на почту:\n" +
                                          "alice.lutierre@gmail.com\n" +
                                          "Нажмите любую клавишу для выхода:");
                        Console.ReadKey();
                        return;
                }
            }
        }
    }
}
