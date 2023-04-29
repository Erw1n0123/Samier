using CleanBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CleanBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DolgozoBeosztaController : ControllerBase
    {
        [HttpPost("{uId}")]
        public IActionResult Post(DolgozoBeoszta dolgozoBeoszta, string uId)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank >= 5)
            {

                using (var context = new cleanContext())
                {
                    try
                    {
                        context.DolgozoBeosztas.Add(dolgozoBeoszta);
                        context.SaveChanges();
                        return Ok("Sikeres adatrögzítés.");
                    }
                    catch (Exception ex)
                    {
                        return Ok(ex.Message);
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
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank >= 5)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        DolgozoBeoszta db = new DolgozoBeoszta();
                        db.DbId = id;
                        context.DolgozoBeosztas.Remove(db);
                        context.SaveChanges();
                        return Ok("Adatok törölve.");
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
