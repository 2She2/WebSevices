namespace StudentSystem.Web.Models
{
    using System;
    using System.Linq.Expressions;

    using StudentSystem.Models;
    public class HomeworkModel
    {
        public static Expression<Func<Homework, HomeworkModel>> FromStudent
        {
            get
            {
                return homework => new HomeworkModel
                {
                    Id = homework.Id,
                    StudentIdentification = homework.StudentIdentification,
                    CourseId = homework.CourseId.ToString()
                };
            }
        }

        public int Id { get; set; }

        public int StudentIdentification { get; set; }

        public string CourseId { get; set; }
    }
}