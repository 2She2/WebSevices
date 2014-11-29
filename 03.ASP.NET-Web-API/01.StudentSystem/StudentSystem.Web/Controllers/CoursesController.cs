namespace StudentSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using StudentSystem.Data;
    using StudentSystem.Models;
    using StudentSystem.Web.Models;

    public class CoursesController : ApiController
    {
        private IStudentSystemData data;

        public CoursesController()
            : this(new StudentsSystemData())
        {
        }

        public CoursesController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var courses = this.data.Courses
                .All()
                .Select(CourseModel.FromCourse);

            return Ok(courses);
        }

        [HttpGet]
        public IHttpActionResult ById(Guid id)
        {
            var course = this.data.Courses
                .All()
                .Where(x => x.Id == id)
                .Select(CourseModel.FromCourse)
                .FirstOrDefault();

            if (course == null)
            {
                return BadRequest("Coures with id: " + id + " do not exists!");
            }

            return Ok(course);
        }

        [HttpPost]
        public IHttpActionResult Create(CourseModel course)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCourse = new Course
            {
                Name = course.Name,
                Description = course.Description
            };

            this.data.Courses.Add(newCourse);
            this.data.SaveChanges();

            course.Id = newCourse.Id.ToString();

            return Ok(course);
        }

        [HttpPut]
        public IHttpActionResult Update(string id, CourseModel course)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCourse = this.data.Courses
                .All()
                .FirstOrDefault(x => x.Id.ToString() == id);

            if (existingCourse == null)
            {
                return BadRequest("Course with id:" + id + " do not exists!");
            }

            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;
            this.data.SaveChanges();

            course.Id = existingCourse.Id.ToString();
            return Ok(course);
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var course = this.data.Courses
                .All()
                .FirstOrDefault(x => x.Id.ToString() == id);

            if (course == null)
            {
                return BadRequest("Course with id:" + id + " do not exists!");
            }

            this.data.Courses.Delete(course);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddStudent(string id, int studentId)
        {
            var course = this.data.Courses
                .All()
                .FirstOrDefault(x => x.Id.ToString() == id);

            if (course == null)
            {
                return BadRequest("Course with id:" + id + " do not exists!");
            }

            var student = this.data.Students
                .All()
                .FirstOrDefault(x => x.StudentIdentification == studentId);

            if (student == null)
            {
                return BadRequest("Student with id: " + id + " do not exists!");
            }

            course.Students.Add(student);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddHomework(string id, int homeworkId)
        {
            var course = this.data.Courses
                .All()
                .FirstOrDefault(x => x.Id.ToString() == id);

            if (course == null)
            {
                return BadRequest("Course with id:" + id + " do not exists!");
            }

            var homework = this.data.Homeworks
                .All()
                .FirstOrDefault(x => x.Id == homeworkId);

            if (homework == null)
            {
                return BadRequest("Homework with id: " + id + " do not exists!");
            }

            course.Homeworks.Add(homework);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
