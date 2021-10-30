using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models;

namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        public void CreateCourse(string title, string description, string[] skillNames)
        {
            // TODO: make course current
            
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

        public void SubscribeCourse()
        {
            throw new System.NotImplementedException();
        }
    }
}
