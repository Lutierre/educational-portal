using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IEntityDalService<User> _userDalService;
        private readonly ICurrentStateService _currentStateService;

        public UserService(IEntityDalService<User> userDalService, ICurrentStateService currentStateService)
        {
            _userDalService = userDalService;
            _currentStateService = currentStateService;
        }

        public string Authorize(string nickname, string password)
        {
            var result = 
                _userDalService.Filter(user => user.Nickname == nickname && user.Password == password);

            if (result.Count > 0)
            {
                _currentStateService.AuthorizedUser = result[0];
                
                return "success";
            }
            
            result = _userDalService.Filter(user => user.Nickname == nickname);

            if (result.Count > 0)
            {
                return "wrong-password";
            }
            
            var user = RegisterUser(nickname, password);
            _currentStateService.AuthorizedUser = user;

            return "registered";
        }

        public User RegisterUser(string nickname, string password)
        {
            var user = _userDalService.Add(new User
            {
                Nickname = nickname, 
                Password = password
            });
            
            return user;
        }

        public void Logout()
        {
            _currentStateService.AuthorizedUser = default;
        }
    }
}
