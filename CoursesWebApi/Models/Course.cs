using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesWebApi.Models
{
  public class Course
  {
    public int Id { get; set; }
    [Required]
    [DefaultValue("C# pour la plèbe")]
    public String Title { get; set; }
    [Required]
    [DefaultValue("CSN")]
    [RegularExpression("[A-Z]{3}")]
    public String Code { get; set; }
    [DefaultValue(5)]
    [Range(1, 5, ErrorMessage = "Courses last from 1 day to 5 days")]
    public int Duration { get; set; }
  }
}
