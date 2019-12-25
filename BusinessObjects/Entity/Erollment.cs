using System;
using System.Collections.Generic;

namespace BusinessObjects.Entity
{
    public partial class Erollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Grade { get; set; }
    }
}
