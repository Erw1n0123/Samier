using CleanBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CleanBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EszkozHasznalatController : ControllerBase
    {
        [HttpPost("{uId}")]
        public IActionResult Post(EszkozHasznalat eszkozHasznalat, string uId)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank >= 5)
            {

                using (var context = new cleanContext())
                {
                    try
                    {
                        context.EszkozHasznalats.Add(eszkozHasznalat);
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
                        EszkozHasznalat eh = new EszkozHasznalat();
                        eh.EhId = id;
                        context.EszkozHasznalats.Remove(eh);
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
