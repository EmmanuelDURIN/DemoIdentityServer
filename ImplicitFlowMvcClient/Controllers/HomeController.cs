using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImplicitFlowMvcClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace ImplicitFlowMvcClient.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }
    [Authorize]
    public IActionResult Contact()
    {
      ViewData["Message"] = "Your claims :";
      foreach (System.Security.Claims.Claim c in ((System.Security.Claims.ClaimsIdentity)this.HttpContext.User.Identity).Claims )
      {
        Debug.WriteLine(c.Type);
        Debug.WriteLine(c.Value);
        foreach( var pair in c?.Properties)
        {
          Debug.WriteLine($"\t{pair.Key} {pair.Value}");
        }
      }
      return View();
    }
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public async Task Logout()
    {
      await HttpContext.SignOutAsync("cookie");
      await HttpContext.SignOutAsync("oidc");
    }
  }
}
