namespace StudentSystem.Web.Models
{
    using StudentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

    public class TestsModel
    {
        public static Expression<Func<Test, TestsModel>> FromTests
        {
            get
            {
                return t => new TestsModel
                {
                    Id = t.Id,
                    CourseId = t.CourseId
                };
            }
        }

        public int Id { get; set; }

        public virtual Guid CourseId { get; set; }
    }
}