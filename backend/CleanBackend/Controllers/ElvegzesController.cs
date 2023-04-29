using CleanBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ElvegzesController : ControllerBase
    {
        [HttpGet]
        [Route("Dolgozo/{uId}/{id}")]
        public async Task<IActionResult> GetD(string uId, int id)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank >= 5)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        return Ok(await context.ElvegzesDolgozoRovids.Where(a => a.MunkaId == id).ToListAsync());
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Nincs bejelentkezve/jogosultsága!");
            }
        }

        [HttpGet]
        [Route("Raktar/{uId}/{id}")]
        public async Task<IActionResult> GetR(string uId, int id)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank >= 5)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        return Ok(await context.ElvegzesRaktarRovids.Where(a => a.MunkaId == id).ToListAsync());
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Nincs bejelentkezve/jogosultsága!");
            }
        }
    }
}
