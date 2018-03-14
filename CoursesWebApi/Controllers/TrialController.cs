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
    public class TrialController : Controller
    {
        // GET api/courses
        [Authorize(Policy = "NoMoreThanTwice")]
        [HttpGet]
        public String Get()
        {
            return "Trial ok";
        }
    }
}
