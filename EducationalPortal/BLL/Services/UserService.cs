using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models;

namespace BLL.Services
{
    public class UserService : IUserService
    {
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

        public void Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}
