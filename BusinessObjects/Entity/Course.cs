using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entity
{
    public partial class Course:Entity
    {
        public string Name { get; set; }
        public KeyValue<Credits> Credit { get; set; }

        public Course()
        {
            this.Credit = new KeyValue<Credits>();
        }
    }

    public enum Credits
    {
        [Display(Name ="优秀")]
        A=10,
        [Display(Name = "一般")]
        B =5,
        [Display(Name = "较差")]
        C =1,
    }
}
