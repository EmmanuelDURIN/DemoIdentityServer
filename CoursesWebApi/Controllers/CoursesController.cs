using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoursesWebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoursesWebApi.Controllers
{
  [Route("api/[controller]")]
  public class CoursesController : Controller
  {
    private static List<Course> courses = new List<Course> {
              new Course{
                Id = 1,
                Title = "C# pour tous",
                Duration=5
              },
              new Course{
                Id = 2,
                Title = "ASP.Net Core Mvc",
                Duration=4
              }
            };
    // GET api/courses
    [Authorize]
    [HttpGet]
    public IEnumerable<Course> Get()
    {
      return courses;
    }
    // GET api/courses/5
    public IActionResult Get(int id)
    {
      var course = courses.FirstOrDefault(c => c.Id == id);
      if (course == null)
        return NotFound();
      return new ObjectResult(course);
    }

    // POST api/courses
    [HttpPost]
    [Authorize(Policy = "courseWritePolicy")]
    public IActionResult Post([FromBody]Course course)
    {
      if (course == null)
      {
        return BadRequest();
      }
      if ( !ModelState.IsValid)
      {
        return BadRequest(String.Join(" - ", ModelState.Values.SelectMany(entry => entry.Errors.Select(e => e.ErrorMessage))));
      }
      // Simulate create course in db 
      course.Id = courses.Max(c => c.Id) + 1;
      courses.Add(course);
      return CreatedAtRoute(routeValues: new { controller = "courses", id = course.Id }, value: course);
    }

    // PUT api/courses/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]Course course)
    {
    }

    // DELETE api/courses/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
