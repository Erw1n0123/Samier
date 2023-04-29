using CleanBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;

namespace CleanBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("SaltRequest/{nev}")]
        public IActionResult SaltRequest(string nev)
        {
            using (var context = new cleanContext())
            {
                try
                {
                    List<Felhasznalo> talalat = new List<Felhasznalo>(context.Felhasznalos.Where(f => f.FelhasznaloNev == nev));
                    if (talalat.Count > 0)
                    {
                        return Ok(talalat[0].Salt);
                    }
                    else
                    {
                        return BadRequest("Hibás Felhasználónév!");
                    }
                }
                catch (System.Exception ex)
                {
                    return StatusCode(500, ex.Message);                
                }
            }
        }
        
        [HttpPost]
        [Route("web")]
        public IActionResult LoginWeb(string nev, string tmpHash)
        {
            using (var context = new cleanContext())
            {
                try
                {
                    List<Felhasznalo> talalat = new List<Felhasznalo>(context.Felhasznalos.Where(f => f.FelhasznaloNev == nev));
                    if (talalat.Count > 0 && talalat[0].Aktiv == 1)
                    {
                        /*bool talalt = false;
                        int index = 0;
                        int elemSzam = Program.LoggedInUsers.Count;
                        while (!talalt && index < elemSzam)
                        {
                            if (Program.LoggedInUsers.ElementAt(index).Value.FelhasznaloNev == nev)
                            {
                                lock (Program.LoggedInUsers)
                                {
                                    Program.LoggedInUsers.Remove(Program.LoggedInUsers.ElementAt(index).Key);
                                }
                                talalt = true;
                            }
                            index++;
                        }*/
                        string hash = CleanBackend.Program.CreateSHA256(tmpHash);
                        if (hash == talalat[0].Hash)
                        {
                            string token = Guid.NewGuid().ToString();
                            lock (Program.LoggedInUsers)
                            {
                                Program.LoggedInUsers.Add(token, talalat[0]);
                            }
                            string[] response = new string[8] { 
                                token,
                                talalat[0].FelhasznaloNev,
                                talalat[0].TeljesNev,
                                talalat[0].Email,
                                talalat[0].Telefonszam,
                                talalat[0].Iranyitoszam.ToString(),
                                talalat[0].Telepules,
                                talalat[0].Cim
                            };
                            return Ok(response);
                        }
                        else
                        {
                            string[] response = new string[2] { "", "Hibás jelszó!"};
                            return Ok(response);
                        }
                    }
                    else
                    {
                        string[] response = new string[2] { "", "Hibás név/Inaktív felhasználó!"};
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    string[] response = new string[2] { "", ex.Message};
                    return Ok(response);
                }
            }
        }

        [HttpPost]
        public IActionResult Login(string nev, string tmpHash)
        {
            using (var context = new cleanContext())
            {
                try
                {
                    List<Felhasznalo> talalat = new List<Felhasznalo>(context.Felhasznalos.Where(f => f.FelhasznaloNev == nev));
                    if (talalat.Count > 0 && talalat[0].Aktiv == 1)
                    {
                        bool talalt = false;
                        int index = 0;
                        int elemSzam = Program.LoggedInUsers.Count;
                        while (!talalt && index < elemSzam)
                        {
                            if (Program.LoggedInUsers.ElementAt(index).Value.FelhasznaloNev == nev)
                            {
                                lock (Program.LoggedInUsers)
                                {
                                    Program.LoggedInUsers.Remove(Program.LoggedInUsers.ElementAt(index).Key);
                                }
                                talalt = true;
                            }
                            index++;
                        }
                        string hash = CleanBackend.Program.CreateSHA256(tmpHash);
                        if (hash == talalat[0].Hash)
                        {
                            string token = Guid.NewGuid().ToString();
                            lock (Program.LoggedInUsers)
                            {
                                Program.LoggedInUsers.Add(token, talalat[0]);
                            }
                            string[] response = new string[3] { token, talalat[0].TeljesNev, talalat[0].Rank.ToString() };
                            return Ok(response);
                        }
                        else
                        {
                            string[] response = new string[3] { "Hibás jelszó!", "", "-1" };
                            return Ok(response);
                        }
                    }
                    else
                    {
                        string[] response = new string[3] { "Hibás név/Inaktív felhasználó!", "", "-1" };
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    string[] response = new string[3] { ex.Message, "", "-1" };
                    return Ok(response);
                }
            }
        }
    }
}
