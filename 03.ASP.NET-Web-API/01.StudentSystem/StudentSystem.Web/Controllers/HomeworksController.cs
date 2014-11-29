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

    public class HomeworksController : ApiController
    {
        private IStudentSystemData data;

        public HomeworksController()
            : this(new StudentsSystemData())
        {
        }

        public HomeworksController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var homeworks = this.data.Homeworks
                .All()
                .Select(HomeworkModel.FromStudent);

            return Ok(homeworks);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var homework = this.data.Homeworks
                .All()
                .Where(x => x.Id == id)
                .Select(HomeworkModel.FromStudent)
                .FirstOrDefault();

            if (homework == null)
            {
                return BadRequest("Homework with id: " + id + " do not exists!");
            }

            return Ok(homework);
        }

        [HttpPost]
        public IHttpActionResult Create(HomeworkModel homework)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newHomework = new Homework
            {
                StudentIdentification = homework.StudentIdentification,
                CourseId = Guid.Parse(homework.CourseId)
            };

            this.data.Homeworks.Add(newHomework);
            this.data.SaveChanges();

            homework.Id = newHomework.Id;
            return Ok(homework);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, HomeworkModel homework)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingHomework = this.data.Homeworks
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (existingHomework == null)
            {
                return BadRequest("Homework with id: " + id + " do not exists");
            }

            existingHomework.CourseId = Guid.Parse(homework.CourseId);
            this.data.SaveChanges();

            homework.Id = id;

            return Ok(homework);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var homework = this.data.Homeworks
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (homework == null)
            {
                return BadRequest("Homework with id: " + id + " do not exists");
            }

            this.data.Homeworks.Delete(homework);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
