using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanBackend.Models;
using System.Linq;
using System;
using System.Security.Cryptography.X509Certificates;

namespace CleanBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistryController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(Registry registry)
        {
            using (var context = new cleanContext())
            {
                try
                {
                    if(context.Felhasznalos.Where(f => f.FelhasznaloNev == registry.FelhasznaloNev).ToList().Count != 0)
                    {
                        return StatusCode(201, "Ez a felhasználónév már foglalt!");
                    }
                    if (context.Felhasznalos.Where(f => f.Email == registry.Email).ToList().Count != 0)
                    {
                        return StatusCode(202, "Ezzel az email címmel már regisztráltál!");
                    }
                    if(registry.FelhasznaloNev != "" && registry.Email != "")
                    {
                        registry.Key = Program.GenerateSalt();
                        context.Add(registry);
                        context.SaveChanges();
                        Program.SendEmail(registry.Email, "Registráció megerősítése", "A regisztráció befejezéséhez kattints az alábbi linkre: "
                            + "https://localhost:6969/Registry/" + registry.Key);
                        return StatusCode(200, "A regisztráció befejezéséhez kattintson az emailjében található linkre!");
                    }
                    else
                    {
                        return StatusCode(200, "Ellenőrzés lefuttatva.");
                    }
                }
                catch (System.Exception ex)
                {
                    return StatusCode(200, ex.Message);
                }
            }
        }

        [HttpGet("{Key}")]
        public IActionResult Get(string Key)
        {
            using (var context = new cleanContext())
            {
                try
                {
                    var registryUser = context.Registries.Where(c => c.Key == Key).ToList();
                    if(registryUser.Count != 0) 
                    {
                        Felhasznalo f = new Felhasznalo();
                        f.FelhasznaloNev = registryUser[0].FelhasznaloNev;
                        f.TeljesNev = registryUser[0].TeljesNev;
                        f.Email = registryUser[0].Email;
                        f.Telefonszam = registryUser[0].Telefonszam;
                        f.Iranyitoszam = registryUser[0].Iranyitoszam;
                        f.Telepules = registryUser[0].Telepules;
                        f.Cim = registryUser[0].Cim;
                        f.Salt = registryUser[0].Salt;
                        f.Hash = registryUser[0].Hash;
                        f.Rank = 0;
                        f.Aktiv = 1;
                        context.Felhasznalos.Add(f);
                        context.Registries.Remove(registryUser[0]);
                        context.SaveChanges();
                        return Ok("Regisztrálva.");
                    }
                    else
                    {
                        return BadRequest("A regisztráció már megtörtént vagy hibás kulcs került megadásra!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
