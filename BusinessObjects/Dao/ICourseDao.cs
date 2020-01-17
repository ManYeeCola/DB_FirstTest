using BusinessObjects.Entity;
using BusinessObjects.Util;

namespace BusinessObjects.Dao
{
    public interface ICourseDao : IDao<Course>
    {
        int SaveCourse(Course course);
    }
    public class CourseDao : BaseDao<Course>, ICourseDao
    {
        public CourseDao(AppDbContext db) : base(db){}
        public int SaveCourse(Course course)
        {
            _db.Course.Add(course);
            Course coursetest=null;
            int test= coursetest.Id;
            _db.SaveChanges();
            return course.Id;
        }
    }
}
