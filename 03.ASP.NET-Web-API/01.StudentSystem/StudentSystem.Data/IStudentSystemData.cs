namespace StudentSystem.Data
{
    using StudentSystem.Data.Repositories;
    using StudentSystem.Models;

    public interface IStudentSystemData
    {
        IGenericRepository<Course> Courses { get; }

        IGenericRepository<Student> Students { get; }

        IGenericRepository<Test> Tests { get; }

        IGenericRepository<Homework> Homeworks { get; }

        void SaveChanges();
    }
}
