using Autofac.Extras.DynamicProxy;
using BusinessObjects.Aspect;
using BusinessObjects.Dao;
using BusinessObjects.Entity;
using BusinessObjects.Interceptor;
using BusinessObjects.Util;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Services
{
    public interface IStudentServices: IServices<Student>
    {
        List<Student> GetStudent();
        int SaveStudent(Student student, Course course);
    }

    [Intercept(typeof(TransactionInterceptor))]
    public class StudentServices : BaseServices<Student>,IStudentServices
    {
        private readonly IStudentDao _studentDao;
        private readonly ICourseDao _courseDao;
        public StudentServices(AppDbContext db, IStudentDao studentDao, ICourseDao courseDao) : base(db, studentDao)
        {
            _studentDao = studentDao;
            _courseDao = courseDao;
        }
        [Transaction]
        public List<Student> GetStudent()
        {
            return _studentDao.GetStudent();
        }
        [Transaction]
        public int SaveStudent(Student student,Course course)
        {
            this.OpenProxy();
            student.RegisterDate = DateTime.Now;
            int resultA =_studentDao.SaveStudent(student);
            int resultB = _courseDao.SaveCourse(course);
            this.SaveChanges();
            return resultA;
        }
    }
}
