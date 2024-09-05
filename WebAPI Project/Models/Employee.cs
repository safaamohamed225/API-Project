using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Project.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }
        [Range(23,55)]
        public int Age { get; set; }

        public int Salary { get; set; }
        [Required]
        public string Address { get; set; }

        [ForeignKey("Department")]
        public int? Dept_id { get; set; }

        public virtual Department? Department { get; set; }
    }
}
