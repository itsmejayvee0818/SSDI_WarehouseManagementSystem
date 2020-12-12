﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem.Server.Data;
using WarehouseManagementSystem.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace SonicWarehouseManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderDetailsIndexController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PurchaseOrderDetailsIndexController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseOrderDetailsIndex
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDetails>>> GetPurchase_Details()
        {
            return await _context.Purchase_Details.ToListAsync();
        }

        // GET: api/PurchaseOrderDetailsIndex/5
        [HttpGet("{id}")]
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<ActionResult<PurchaseDetails>> GetPurchaseDetails(int id)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var purchaseDetails = _context.Purchase_Details.Where(x => x.Header_ID == id).ToList();

            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return Ok(purchaseDetails);
        }

        // PUT: api/PurchaseOrderDetailsIndex/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseDetails(int id, PurchaseDetails purchaseDetails)
        {
            if (id != purchaseDetails.ID)
            {
                return BadRequest();
            }

            _context.Entry(purchaseDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseDetailsExists(id))
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

        // POST: api/PurchaseOrderDetailsIndex
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PurchaseDetails>> PostPurchaseDetails(PurchaseDetails purchaseDetails)
        {
            _context.Purchase_Details.Add(purchaseDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseDetails", new { id = purchaseDetails.ID }, purchaseDetails);
        }

        // DELETE: api/PurchaseOrderDetailsIndex/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchaseDetails>> DeletePurchaseDetails(int id)
        {
            var purchaseDetails = await _context.Purchase_Details.FindAsync(id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }

            _context.Purchase_Details.Remove(purchaseDetails);
            await _context.SaveChangesAsync();

            return purchaseDetails;
        }

        private bool PurchaseDetailsExists(int id)
        {
            return _context.Purchase_Details.Any(e => e.ID == id);
        }
    }
}
