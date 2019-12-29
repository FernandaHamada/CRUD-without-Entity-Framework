using CrudWEF2.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWEF2.Models
{
    public class Teacher
    {
        [Required]
        public int Id { get; set; } [Required]
        public string Name { get; set; } [Required]
        [SkillsValidate(Allowed = new string[] { "ASP.NET Core", "ASP.NET MVC", "ASP.NET Web Forms" }, ErrorMessage = "You skills are invalid")]
        public string Skills { get; set; } [Range(5, 50)]
        public int TotalStudents { get; set; } [Required]
        public decimal Salary { get; set; }

        [Required]
        public DateTime AddeOn { get; set; }

    }

}