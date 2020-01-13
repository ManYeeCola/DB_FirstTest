using System;
using System.Collections.Generic;

namespace BusinessObjects.Aspect
{
    public partial class TblStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int JobTitleId { get; set; }
        public string HospitalName { get; set; }
        public int PhoneNum { get; set; }
        public int? PhoneNumViceOne { get; set; }
        public int? PhoneNumViceTwo { get; set; }
        public int? AttendanceId { get; set; }
    }
}
