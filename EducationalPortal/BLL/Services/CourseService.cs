using System.Collections.Generic;
using System.Linq;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IEntityDalService<Course> _courseDalService;
        private readonly IEntityDalService<User> _userDalService;
        private readonly IEntityDalService<Skill> _skillDalService;
        private readonly ICurrentStateService _currentStateService;

        public CourseService(IEntityDalService<Course> courseDalService,
            IEntityDalService<Skill> skillDalService, 
            ICurrentStateService currentStateService, 
            IEntityDalService<User> userDalService)
        {
            _courseDalService = courseDalService;
            _skillDalService = skillDalService;
            _currentStateService = currentStateService;
            _userDalService = userDalService;
        }

        public void CreateCourse(string title, string description, string[] skillNames)
        {
            var skills = new List<Skill>();
            
            foreach (var skillName in skillNames)
            {
                var result = _skillDalService.Filter(skill => skill.Title == skillName);

                if (result.Count > 0)
                {
                    skills.Add(result[0]);
                }
                else
                {
                    var skill = _skillDalService.Add(new Skill { Title = skillName });
                    skills.Add(skill);
                }
            }

            var course = _courseDalService.Add(new Course
            {
                Title = title,
                Description = description,
                Author = _currentStateService.AuthorizedUser,
                Skills = skills
            });
            
            _currentStateService.CurrentCourse = course;
        }

        public List<Course> GetCompletedCourses()
        {
            var currentUser = _currentStateService.AuthorizedUser;
            var userMaterialIds = currentUser.Materials.Select(material => material.Id).ToHashSet();
            var results = new List<Course>();
            
            foreach (var course in currentUser.CurrentCourses)
            {
                var courseMaterialIds = course.Materials.Select(material => material.Id).ToHashSet();
                var unfinishedMaterialsIds = courseMaterialIds.Except(userMaterialIds).ToHashSet();

                if (unfinishedMaterialsIds.Count == 0)
                {
                    results.Add(course);
                }
            }

            return results;
        }

        public List<(Course, int)> GetCoursesProgress()
        {
            var currentUser = _currentStateService.AuthorizedUser;
            var userMaterialIds = currentUser.Materials.Select(material => material.Id).ToHashSet();
            var results = new List<(Course, int)>();
            
            foreach (var course in currentUser.CurrentCourses)
            {
                var courseMaterialIds = course.Materials.Select(material => material.Id).ToHashSet();
                var unfinishedMaterialsIds = courseMaterialIds.Except(userMaterialIds).ToHashSet();

                if (unfinishedMaterialsIds.Count > 0)
                {
                    var allMaterialsCount = courseMaterialIds.Count;
                    var finishedMaterialsCount = courseMaterialIds.Count - unfinishedMaterialsIds.Count;
                    var progress = finishedMaterialsCount / allMaterialsCount * 100;
                    
                    results.Add((course, progress));
                }
            }
            
            return results;
        }

        public List<Course> GetAvailableCourses()
        {
            var currentUser = _currentStateService.AuthorizedUser;
            var userCourseIds = currentUser.CurrentCourses.Select(course => course.Id).ToHashSet();
            var allCourses = _courseDalService.Filter(_ => true);
            
            var results = new List<Course>();

            foreach (var course in allCourses)
            {
                if (userCourseIds.Contains(course.Id))
                {
                    continue;
                }

                var courseFullInfo = _courseDalService.Get(course.Id);
                
                if (!courseFullInfo.IsAvailable)
                {
                    continue;
                }
                
                results.Add(course);
            }

            return results;
        }

        public void SubscribeCourse()
        {
            var currentUser = _currentStateService.AuthorizedUser;
            var userCourses = currentUser.CurrentCourses;
            var currentCourse = _currentStateService.CurrentCourse;
            
            userCourses.Add(currentCourse);
            _userDalService.Update(currentUser);
        }
    }
}
