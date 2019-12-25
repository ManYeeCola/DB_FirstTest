using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Entity
{
    public partial class Student : Entity
    {
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Password { get; set; }

        public TestStatus Status { get; set; }

        public string StatusName { get { return Status.EnumMetadataDisplay(); } }

        private KeyValue testProperty;
        public KeyValue TestProperty { 
            get { 
                this.testProperty.Name= this.testProperty.Id.ToString();
                return this.testProperty; 
            }  
            set { this.testProperty = value; } 
        }

        public Student()
        {
            this.TestProperty = new KeyValue();
        }
    }

    public enum TestStatus
    {
        [Display(Name ="开始")]
        a=1,
        [Display(Name = "进行中")]
        b =2,
        [Display(Name = "结束")]
        c =3
    }
}
