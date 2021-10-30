using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models;

namespace BLL.Services
{
    public class SkillService : ISkillService
    {
        private readonly ICurrentStateService _currentStateService;

        public SkillService(ICurrentStateService currentStateService)
        {
            _currentStateService = currentStateService;
        }

        public List<UserSkill> GetUserSkills()
        {
            return _currentStateService.AuthorizedUser.Skills;
        }
    }
}
