using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDataContext _context;

        public ValuesController(ApplicationDataContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var values = await _context.Values.ToListAsync();
                return Ok(values);
            }
            catch(Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode);
            }
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
                if(value == null)
                {
                    return NoContent();
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}