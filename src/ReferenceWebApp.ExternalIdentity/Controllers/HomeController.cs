using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReferenceWebApp.Models;

namespace ReferenceWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";
            var result = HttpContext.User.Claims.Select(
                c => new ClaimType { Type = c.Type, Value = c.Value });

            if (User.Identity.IsAuthenticated)
            {
                string accessToken = await HttpContext.GetTokenAsync(IdentityConstants.ExternalScheme, "access_token");
                string idToken = await HttpContext.GetTokenAsync(IdentityConstants.ExternalScheme, "id_token");

                // Now you can use them. For more info on when and how to use the 
                // access_token and id_token, see https://auth0.com/docs/tokens
            }

            return View(result);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
