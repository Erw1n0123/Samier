using CleanBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SzolgaltatasController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
                using (var context = new cleanContext())
                {
                    try
                    {
                        return Ok(await context.Szolgaltatas.ToListAsync());
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
        }
    }
}
