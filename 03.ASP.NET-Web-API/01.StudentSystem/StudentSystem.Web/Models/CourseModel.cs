namespace StudentSystem.Web.Models
{
    using StudentSystem.Models;
    using System;
    using System.Linq.Expressions;

    public class CourseModel
    {
        public static Expression<Func<Course, CourseModel>> FromCourse
        {
            get
            {
                return course => new CourseModel
                {
                    Id = course.Id.ToString(),
                    Name = course.Name,
                    Description = course.Description
                };
            }
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}