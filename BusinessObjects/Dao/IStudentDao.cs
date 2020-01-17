using BusinessObjects.Entity;
using BusinessObjects.Util;
using System.Collections.Generic;
using System.Linq;

namespace BusinessObjects.Dao
{
    public interface IStudentDao:IDao<Student>
    {
        List<Student> GetStudent();
        int SaveStudent(Student student);
    }

    public class StudentDao: BaseDao<Student>, IStudentDao
    {
        public StudentDao(AppDbContext db) : base(db){}

        public List<Student> GetStudent()
        {
            return _db.Student.Where(student => student.Id == 1).Skip(0).Take(10).ToList();
        }

        public int SaveStudent(Student student)
        {
            _db.Add(student);
            return student.Id;
        }
    }
}
