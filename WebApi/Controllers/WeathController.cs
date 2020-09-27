using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
 
namespace WebApi.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
   
    public class WeathController : ControllerBase
    {




        [HttpPost("getname1")]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }

        [Authorize]
        [HttpPost("getname2")]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };

            }
            return null;
        }

        [Authorize]
        [HttpPost("getname3")]
        public Object GetName3()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "groups").Where(d => ( d.Value ==  "YG-WCDTR-USERS" || d.Value == "PG8-TFS-All-Projxect-Admins")).FirstOrDefault()?.Value; ;


                if (name == null)
                {
                    return "Invalid";
                }
                else
                {
                   
                        //data = name
                           return "Authorized";
               
                }

           

            }
            return null;
        }


 

    private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


      

    private readonly ILogger<WeathController> _logger;

        public WeathController(ILogger<WeathController> logger)
        {
            _logger = logger;
        }



        [HttpGet]
        [Authorize(Policy = "AllowTom")]

        public IEnumerable<Weather> Get()
        {
            var identity = User.Identity as ClaimsIdentity;
         
       
            //IEnumerable<Claim> claims = identity.Claims;
            //var name = claims.Where(p => p.Type == "groups").Where(d => (d.Value == "YG-WCDTR-USERS" || d.Value == "PG8-TFS-All-Projxect-Admins")).FirstOrDefault()?.Value; ;


            //if (name == null)
            //{
            //    return null;
            //}
            //else
            //{
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new Weather
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            //}




         
        }
    }
}
