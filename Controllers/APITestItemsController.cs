using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITest.Model;

namespace APITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APITestItemsController : ControllerBase
    {
        private readonly APITestContext _context;

        public APITestItemsController(APITestContext context)
        {
            _context = context;
        }

        // GET: api/APITestItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APITestItems>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/APITestItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APITestItems>> GetAPITestItems(long id)
        {
            var aPITestItems = await _context.TodoItems.FindAsync(id);

            if (aPITestItems == null)
            {
                return NotFound();
            }

            return aPITestItems;
        }

        // PUT: api/APITestItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPITestItems(long id, APITestItems aPITestItems)
        {
            if (id != aPITestItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(aPITestItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APITestItemsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/APITestItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<APITestItems>> PostAPITestItems(APITestItems aPITestItems)
        {
            _context.TodoItems.Add(aPITestItems);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetAPITestItems", new { id = aPITestItems.Id }, aPITestItems);
            
            return CreatedAtAction(nameof(GetAPITestItems), new { id = aPITestItems.Id }, aPITestItems);
        }

        // DELETE: api/APITestItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAPITestItems(long id)
        {
            var aPITestItems = await _context.TodoItems.FindAsync(id);
            if (aPITestItems == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(aPITestItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool APITestItemsExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
