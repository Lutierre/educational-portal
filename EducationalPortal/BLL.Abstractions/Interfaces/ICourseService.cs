using System.Collections.Generic;
using Core.Models;

namespace BLL.Abstractions.Interfaces
{
    public interface ICourseService
    {
        void CreateCourse(string title, string description, string[] skillNames);
        
        List<Course> GetCompletedCourses();

        List<(Course, int)> GetCoursesProgress();

        List<Course> GetAvailableCourses();

        void SubscribeCourse();
    }
}
