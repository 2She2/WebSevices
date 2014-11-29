namespace StudentSystem.Web.Controllers
{
    using StudentSystem.Data;
    using StudentSystem.Models;
    using StudentSystem.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class TestsController : ApiController
    {
        private IStudentSystemData data;

        public TestsController()
            : this(new StudentsSystemData())
        {
        }

        public TestsController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var tests = this.data.Tests
                .All()
                .Select(TestsModel.FromTests);
            return Ok(tests);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var test = this.data.Tests
                .All()
                .Where(t => t.Id == id)
                .Select(TestsModel.FromTests)
                .FirstOrDefault();

            if (test == null)
            {
                return BadRequest("Tests with id: " + id + " do not exists!");
            }

            return Ok(test);
        }
        [HttpPost]
        public IHttpActionResult Create(TestsModel test)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTest = new Test
            {
                CourseId = test.CourseId
            };

            this.data.Tests.Add(newTest);
            this.data.SaveChanges();

            test.Id = newTest.Id;
            return Ok(test);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TestsModel test)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTest = this.data.Tests
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (existingTest == null)
            {
                return BadRequest("Test with id: " + id + " do not exists");
            }

            existingTest.CourseId = test.CourseId;
            this.data.SaveChanges();

            test.Id = id;

            return Ok(test);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var test = this.data.Tests
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (test == null)
            {
                return BadRequest("Test with id: " + id + " do not exists!");
            }

            this.data.Tests.Delete(test);
            this.data.SaveChanges();

            return Ok();
        }

        public IHttpActionResult AddStudent(int id, int studentId)
        {
            var test = this.data.Tests
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (test == null)
            {
                return BadRequest("Test with id: " + id + " do not exists!");
            }

            var student = this.data.Students
                .All()
                .FirstOrDefault(x => x.StudentIdentification == studentId);

            if (student == null)
            {
                return BadRequest("Student with id: " + id + " do not exists!");
            }

            test.Students.Add(student);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
