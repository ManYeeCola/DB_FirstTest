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
    }

    public class StudentServices : IStudentServices
    {
        private readonly IStudentDao studentDao;
        public StudentServices(IStudentDao _studentDao)
        {
            studentDao = _studentDao;
        }
        public List<Student> GetStudent()
        {
            return studentDao.GetStudent();
        }
    }
}
