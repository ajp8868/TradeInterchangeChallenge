namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("Employee")]
    public partial class Employee
    {
        public int EmployeeID { get; set; }

        public int Number { get; set; }

        [Required]
        [StringLength(255)]
        public string Forename { get; set; }

        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }
    }
}
