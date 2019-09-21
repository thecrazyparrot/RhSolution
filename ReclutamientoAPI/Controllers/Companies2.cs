using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReclutamientoAPI.API.Models;

//namespace ReclutamientoAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    private class Companies2 : ControllerBase
//    {
//        private readonly ReclutamientoAPIDbContext _context;

//        public Companies2(ReclutamientoAPIDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Companies2
//        [HttpGet]
//        public IEnumerable<Companies> GetCompanies()
//        {
//            return _context.Companies;
//        }

//        // GET: api/Companies2/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetCompanies([FromRoute] int? id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var companies = await _context.Companies.FindAsync(id);

//            if (companies == null)
//            {
//                return NotFound();
//            }

//            return Ok(companies);
//        }

//        // PUT: api/Companies2/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutCompanies([FromRoute] int? id, [FromBody] Companies companies)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != companies.CompanyId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(companies).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!CompaniesExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Companies2
//        [HttpPost]
//        public async Task<IActionResult> PostCompanies([FromBody] Companies companies)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            _context.Companies.Add(companies);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetCompanies", new { id = companies.CompanyId }, companies);
//        }

//        // DELETE: api/Companies2/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCompanies([FromRoute] int? id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var companies = await _context.Companies.FindAsync(id);
//            if (companies == null)
//            {
//                return NotFound();
//            }

//            _context.Companies.Remove(companies);
//            await _context.SaveChangesAsync();

//            return Ok(companies);
//        }

//        private bool CompaniesExists(int? id)
//        {
//            return _context.Companies.Any(e => e.CompanyId == id);
//        }
//    }
//}