using System.Collections.Generic;
using Core.Models;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserService
    {
       string Authorize(string nickname, string password);

        void RegisterUser(string nickname, string password);

        List<UserSkill> GetUserSkills();

        void Logout();
    }
}
