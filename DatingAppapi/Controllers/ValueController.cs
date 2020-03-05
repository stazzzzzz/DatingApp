using System.Linq;
using System.Threading.Tasks;
using DatingAppapi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppapi.Controllers
{
    [Authorize]
    [ApiController]
        [Route("api/[controller]")]
    public class ValueController : ControllerBase
    {
        public readonly DataContext _context;
        
        public ValueController(DataContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<IActionResult> getvalues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values) ;
        }

        [AllowAnonymous]
        
        [HttpGet("{id}")]
        public async Task<IActionResult> getvalues(int id)
        {
            var values = await _context.Values.FirstOrDefaultAsync(x=>x.id==id);
            return Ok(values) ;
        }
    }
}