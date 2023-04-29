using CleanBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using CleanBackend.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CleanBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FelhasznaloController : ControllerBase, IGet
    {
        [HttpGet("{uId}")]
        public async Task<IActionResult> Get(string uId)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank == 9)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        //List<Felhasznalo> felhasznaloks = new List<Felhasznalo>(context.Felhasznalos);
                        return Ok(await context.Felhasznalos.ToListAsync());
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

        [HttpPost("{uId}")]
        public IActionResult Post(string uId, Felhasznalo felhasznalo)
        {

            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank == 9)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        context.Felhasznalos.Add(felhasznalo);
                        context.SaveChanges();
                        return Ok("Új felhasználó létrehozva.");
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
        public IActionResult Put(string uId, Felhasznalo felhasznalo)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank == 9)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        context.Felhasznalos.Update(felhasznalo);
                        context.SaveChanges();
                        return Ok("A felhasználó módosítva.");
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
                        Felhasznalo felhasznalo = new Felhasznalo();
                        felhasznalo.Id = id;
                        context.Felhasznalos.Remove(felhasznalo);
                        context.SaveChanges();
                        return Ok("A felhasználó törölve.");
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

        [HttpDelete]
        public IActionResult DeleteUserName(string uId, string userName)
        {
            if (Program.LoggedInUsers.ContainsKey(uId))
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        var felhasznalok = context.Felhasznalos.Where(f => f.FelhasznaloNev == userName).ToList();
                        if (felhasznalok.Count > 0)
                        {
                            context.Felhasznalos.Remove(felhasznalok[0]);
                            context.SaveChanges();
                            return Ok("A bejelentkezési és személyes adatai törlésre kerültek.");
                        }
                        else
                        {
                            return StatusCode(210, "Nincs ilyen nevű felhasználó!");
                        }
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
        [Route("Dolgozo/{uId}")]
        public async Task<IActionResult> Dolgozo(string uId)
        {
            if (Program.LoggedInUsers.ContainsKey(uId) && Program.LoggedInUsers[uId].Rank >=5)
            {
                using (var context = new cleanContext())
                {
                    try
                    {
                        return Ok(await context.Felhasznalos.Where(f => f.Rank == 5).ToListAsync());
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
