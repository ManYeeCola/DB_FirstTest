using BusinessObjects.Dao;
using BusinessObjects.Entity;
using BusinessObjects.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Services
{
    public interface IStudentServices
    {
        List<Student> GetStudent();
        int SaveStudent(Student student, Course course);
    }

    public class StudentServices : IStudentServices
    {
        private readonly IStudentDao studentDao;
        private readonly ICourseDao courseDao;
        public StudentServices(IStudentDao _studentDao, ICourseDao _courseDao)
        {
            studentDao = _studentDao;
            courseDao = _courseDao;
        }
        public List<Student> GetStudent()
        {
            return studentDao.GetStudent();
        }

        public int SaveStudent(Student student,Course course)
        {
            student.RegisterDate = DateTime.Now;
            int resultA =studentDao.SaveStudent(student);
            int resultB = courseDao.SaveCourse(course);
            return resultA;
        }
    }
}
