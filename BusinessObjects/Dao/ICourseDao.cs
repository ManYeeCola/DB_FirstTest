using BusinessObjects.Dao;
using BusinessObjects.Entity;
using BusinessObjects.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Dao
{
    public interface ICourseDao : IDao<Course>
    {
    }
    public class CourseDao : BaseDao<Course>, ICourseDao
    {
        public CourseDao(AppDbContext db) : base(db) { }
    }
}
