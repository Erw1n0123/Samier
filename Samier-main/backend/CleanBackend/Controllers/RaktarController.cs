using CleanBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaktarController : ControllerBase
    {
        [HttpGet("{uId}")]
        public IActionResult Get(string uId)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank >= 5)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        List<Raktar> raktars = new List<Raktar>(context.Raktars);
                        return Ok(raktars);
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
        public async Task<IActionResult> GetActive()
        {
         
                using (var context = new cleanContext())
                {
                    try
                    {
                        return Ok(await context.Raktars.Where(a => a.Megjelenes).ToListAsync());
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
        }

        [HttpPost("{uId}")]
        public IActionResult Post(string uId, Raktar raktar)
        {

            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank >= 5)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        context.Raktars.Add(raktar);
                        context.SaveChanges();
                        return Ok("Új elem mentve.");
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

        [HttpPut("{uId}")]
        public IActionResult Put(string uId, Raktar raktar)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank >= 5)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        context.Raktars.Update(raktar);
                        context.SaveChanges();
                        return Ok("Az elem módosítva.");
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

        [HttpDelete("{uId}")]
        public IActionResult Delete(string uId, int id)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank == 9)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        Raktar raktar = new Raktar();
                        raktar.RId = id;
                        context.Raktars.Remove(raktar);
                        context.SaveChanges();
                        return Ok("Az elem törölve.");
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
