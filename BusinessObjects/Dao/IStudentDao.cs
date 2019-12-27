using BusinessObjects.Dao;
using BusinessObjects.Entity;
using BusinessObjects.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessObjects.Dao
{
    public interface IStudentDao
    {
        List<Student> GetStudent();
        int SaveStudent(Student student);
    }

    public class StudentDao: BaseDao<Student>, IStudentDao
    {
        public StudentDao(AppDbContext db) : base(db) { }
        public List<Student> GetStudent()
        {
            //var jobNumberParam = new SqlParameter("ID", 1);
            //jobNumberParam.DbType = DbType.Int32;
            //string sql = "select * from Student where ID=@ID";
            //return db.Student.FromSqlRaw(sql, jobNumberParam).ToList();
            //return db.Student.Where(student => student.Id == 1).ToList();
            return db.Student.Where(student => student.Id == 1).Skip(0).Take(10).ToList();
        }

        public int SaveStudent(Student student)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                db.Student.Add(student);
                db.SaveChanges();
                return student.Id;
            }
        }
    }
}
