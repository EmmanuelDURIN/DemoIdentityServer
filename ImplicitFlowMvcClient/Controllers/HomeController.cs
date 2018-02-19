using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImplicitFlowMvcClient.Models;
using Microsoft.AspNetCore.Authorization;

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
      //var userInfoClient = new UserInfoClient(doc.UserInfoEndpoint, token);
      //var response = await userInfoClient.GetAsync();
      //var claims = response.Claims;
      //await GetClaims();
      ViewData["Message"] = "Your contact page.";
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

    //private async Task GetClaims()
    //{
    //  String id_token = Request.Form["id_token"];
    //  var client = new System.Net.Http.HttpClient { BaseAddress = new Uri("https://localhost:44397/") };
    //  var responseMessage = await client.GetAsync("connect/userinfo");
    //  string claims = await responseMessage.Content.ReadAsStringAsync();
    //}

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
