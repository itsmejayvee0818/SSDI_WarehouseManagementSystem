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
    public class SAPSalesmanMastersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SAPSalesmanMastersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/SAPSalesmanMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SAPSalesmanMaster>>> GetSAP_SalesmanMasters()
        {
            return await _context.SAP_SalesmanMasters.ToListAsync();
        }

        // GET: api/SAPSalesmanMasters/5
        [HttpGet("{id}")]
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<ActionResult<SAPSalesmanMaster>> GetSAPSalesmanMaster(int id)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var sAPSalesmanMaster = (from emp in _context.SalesInvoice_Headers
                                     join us in _context.SAP_SalesmanMasters on emp.Salesman_Id equals us.Salesman_Code
                                     where emp.ID == id
                                     select new SAPSalesmanMaster { SalesLoc = us.SalesLoc, GiftLoc = us.GiftLoc, DamageLoc = us.DamageLoc }).FirstOrDefault();

            if (sAPSalesmanMaster == null)
            {
                return NotFound();
            }

            return Ok(sAPSalesmanMaster);
        }

        // PUT: api/SAPSalesmanMasters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSAPSalesmanMaster(int id, SAPSalesmanMaster sAPSalesmanMaster)
        {
            if (id != sAPSalesmanMaster.ID)
            {
                return BadRequest();
            }

            _context.Entry(sAPSalesmanMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SAPSalesmanMasterExists(id))
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

        // POST: api/SAPSalesmanMasters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SAPSalesmanMaster>> PostSAPSalesmanMaster(SAPSalesmanMaster sAPSalesmanMaster)
        {
            _context.SAP_SalesmanMasters.Add(sAPSalesmanMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSAPSalesmanMaster", new { id = sAPSalesmanMaster.ID }, sAPSalesmanMaster);
        }

        // DELETE: api/SAPSalesmanMasters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SAPSalesmanMaster>> DeleteSAPSalesmanMaster(int id)
        {
            var sAPSalesmanMaster = await _context.SAP_SalesmanMasters.FindAsync(id);
            if (sAPSalesmanMaster == null)
            {
                return NotFound();
            }

            _context.SAP_SalesmanMasters.Remove(sAPSalesmanMaster);
            await _context.SaveChangesAsync();

            return sAPSalesmanMaster;
        }

        private bool SAPSalesmanMasterExists(int id)
        {
            return _context.SAP_SalesmanMasters.Any(e => e.ID == id);
        }
    }
}
