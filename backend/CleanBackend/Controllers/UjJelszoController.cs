using CleanBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CleanBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UjJelszoController : ControllerBase
    {
        [HttpPost("{felhasznaloNev},{regiJelszo},{ujJelszo}")]
        public IActionResult JelszoValtoztatas(string felhasznaloNev, string regiJelszo, string ujJelszo)
        {
            try
            {
                using (cleanContext context = new cleanContext())
                {
                    var felhasznalo = context.Felhasznalos.Where(f => f.FelhasznaloNev == felhasznaloNev).ToList();
                    if (felhasznalo.Count > 0)
                    {
                        if (regiJelszo == felhasznalo[0].Hash)
                        {
                            Felhasznalo f = felhasznalo[0];
                            f.Hash = ujJelszo;
                            context.Felhasznalos.Update(f);
                            context.SaveChanges();
                            return Ok("Sikeres módosítás.");
                        }
                        else
                        {
                            return StatusCode(201, "Hibás jelszó!");
                        }
                    }
                    else
                    {
                        return BadRequest("Nincs ilyen nevű felhasználó!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{Email}")]
        public IActionResult ElfelejtettJelszo(string Email)
        {
            using (cleanContext context = new cleanContext())
            {
                try
                {
                    var felhasznalo = context.Felhasznalos.Where(f => f.Email == Email).ToList();
                    if (felhasznalo.Count > 0)
                    {
                        string jelszo = Program.GenerateSalt().Substring(0, 12);
                        felhasznalo[0].Hash = Program.CreateSHA256(Program.CreateSHA256(jelszo + felhasznalo[0].Salt));
                        context.Felhasznalos.Update(felhasznalo[0]);
                        context.SaveChanges();
                        Program.SendEmail(@felhasznalo[0].Email, "Elfelejtett jelszó", "Az új jelszava: " + jelszo);
                        return Ok("Email küldése megtörtént.");
                    }
                    else
                    {
                        return StatusCode(210, "Nincs ilyen email cím!");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(211, ex.Message);
                }
            }
        }
    }
}
