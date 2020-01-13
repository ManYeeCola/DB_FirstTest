using System;
using System.Collections.Generic;

namespace BusinessObjects.Aspect
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Password { get; set; }
        public int? TestPropertyId { get; set; }
    }
}
