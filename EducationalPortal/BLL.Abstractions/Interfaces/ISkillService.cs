using System.Collections.Generic;
using Core.Models;

namespace BLL.Abstractions.Interfaces
{
    public interface ISkillService
    {
        List<UserSkill> GetUserSkills();
    }
}
