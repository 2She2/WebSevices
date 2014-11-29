namespace StudentSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using StudentSystem.Data;
    using StudentSystem.Web.Models;
    using StudentSystem.Models;

    public class StudentsController : ApiController
    {
        private IStudentSystemData data;

        public StudentsController()
            : this(new StudentsSystemData())
        {
        }

        public StudentsController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var students = this.data.Students
                .All()
                .Select(StudentModel.FromStudent);

            return Ok(students);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var students = this.data
                .Students
                .All()
                .Where(student => student.StudentIdentification == id)
                .Select(StudentModel.FromStudent)
                .FirstOrDefault();

            if (students == null)
            {
                return BadRequest("Student with id: " + id + " do not exists!");
            }

            return Ok(students);
        }

        [HttpPost]
        public IHttpActionResult Create(StudentModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStudent = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Level = student.Level
            };

            this.data.Students.Add(newStudent);
            this.data.SaveChanges();

            student.StudentIdentification = newStudent.StudentIdentification;
            return Ok(newStudent);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, StudentModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStudent = this.data.Students
                .All()
                .FirstOrDefault(x => x.StudentIdentification == id);

            if (existingStudent == null)
            {
                return BadRequest("Such student do not exists!");
            }

            existingStudent.FirstName = student.FirstName;
            this.data.SaveChanges();

            student.StudentIdentification = id;

            return Ok(student);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var student = this.data.Students
                .All()
                .FirstOrDefault(x => x.StudentIdentification == id);

            if (student == null)
            {
                return BadRequest("Such student do not exists!");
            }

            this.data.Students.Delete(student);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddCourse(int id, Guid courseId)
        {
            var student = this.data.Students
                .All()
                .FirstOrDefault(x => x.StudentIdentification == id);

            if (student == null)
            {
                return BadRequest("Such student do not exists!");
            }

            var course = this.data.Courses
                .All()
                .FirstOrDefault(x => x.Id == courseId);

            if (course == null)
            {
                return BadRequest("Such course do not exists!");
            }

            student.Courses.Add(course);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
