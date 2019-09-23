using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReclutamientoAPI.API.Models;
using ReclutamientoAPI.Models;

namespace ReclutamientoAPI.Controllers
{
    [Route("api/v1/recruitment/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly ReclutamientoAPIDbContext DbContext_;

        public ApplicantsController(ILogger<ApplicantsController> logger, ReclutamientoAPIDbContext DbContext)
        {
            Logger = logger;
            DbContext_ = DbContext;
        }

        // GET
        // api/v1/Recruitment/Companies/

        /// <summary>
        /// Retrieves Applicants List
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <returns>A response with Applicants list</returns>
        /// <response code="200">Returns the Applicants list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet()]
        public async Task<IActionResult> GetApplicantsAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetApplicantsAsync));

            var response = new PagedResponse<Applicants>();

            try
            {
                // Get the "proposed" query from repository
                var query = DbContext_.GetApplicants();

                // Set paging values
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;

                // Get the total rows
                response.ItemsCount = await query.CountAsync();

                // Get the specific page from database
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

                response.Message = string.Format("Page {0} of {1}, Total of products: {2}.", pageNumber, response.PageCount, response.ItemsCount);

                Logger?.LogInformation("The stock items have been retrieved successfully.");
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetApplicantsAsync), ex);
            }

            return response.ToHttpResponse();
        }
        //public async Task<ActionResult<IEnumerable<Applicants>>> GetApplicants()
        //{
        //return await DbContext.Applicants.ToListAsync();
        //}

        //GET
        // api/v1/Recruitment/Applicants/5

        /// <summary>
        /// Retrieves an Applicant by Id
        /// </summary>
        /// <param name="id">Applicant id</param>
        /// <returns>A response with an Applicant</returns>
        /// <response code="200">Returns a Applicant list</response>
        /// <response code="404">If Applicant is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GeApplicantsmAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GeApplicantsmAsync));

            var response = new Models.SingleResponse<Applicants>();

            try
            {
                // Get the stock item by id
                response.Model = await DbContext_.GetApplicantsAsync(new Applicants(id));
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GeApplicantsmAsync), ex);
            }

            return response.ToHttpResponse();
        }
        //// GET: api/Applicants/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Applicants>> GetApplicants(int? id)
        //{
        //    var applicants = await DbContext.Applicants.FindAsync(id);

        //    if (applicants == null)
        //    {
        //        return NotFound();
        //    }

        //    return applicants;
        //}


        // PUT
        // api/v1/Recruitment/Applicatn/5

        /// <summary>
        /// Updates an existing Applicant
        /// </summary>
        /// <param name="id">Applicant ID</param>
        /// <param name="request">Request model</param>
        /// <returns>A response as update Applicant result</returns>
        /// <response code="200">If Applicantion was updated successfully</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutApplicationAsync(int id, [FromBody]PutApplicantRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PutApplicationAsync));

            var response = new Response();

            try
            {
                // Get stock item by id
                var entity = await DbContext_.GetApplicantsAsync(new Applicants(id));

                // Validate if entity exists
                if (entity == null)
                    return NotFound();

                // Set changes to entity
                entity.Name = request.Name;
                entity.ApellidoPaterno = request.ApellidoPaterno;
                entity.ApellidoMaterno = request.ApellidoMaterno;
                //entity.RegisterDate = request.RegisterDate;


                // Update entity in repository
                DbContext_.Update(entity);

                // Save entity in database
                await DbContext_.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutApplicationAsync), ex);
            }

            return response.ToHttpResponse();
        }

        //// PUT: api/Applicants/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutApplicants(int? id, Applicants applicants)
        //{
        //    if (id != applicants.ApplicantId)
        //    {
        //        return BadRequest();
        //    }

        //    DbContext_.Entry(applicants).State = EntityState.Modified;

        //    try
        //    {
        //        await DbContext_.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ApplicantsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST
        // api/v1/Recruitment/Applicants/

        /// <summary>
        /// Creates an Applicant
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new Applicant</returns>
        /// <response code="200">Returns Applicant list</response>
        /// <response code="201">A response as creation of an Applicant</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostApplicantAsync([FromBody]PostApplicantRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostApplicantAsync));

            var response = new SingleResponse<Applicants>();

            try
            {
                var existingEntity = await DbContext_
                    .GetApplicantByNameAsync(new Applicants
                    {
                        Name = request.Name
                    });

                if (existingEntity != null)
                    ModelState.AddModelError("Applicant", "Aplicant Full name already exists");

                if (!ModelState.IsValid)
                    return BadRequest();

                // Create entity from request model
                var entity = request.ToEntity();

                // Add entity to repository
                DbContext_.Add(entity);

                // Save entity in database
                await DbContext_.SaveChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostApplicantAsync), ex);
            }

            return response.ToHttpResponse();
        }

        //// POST: api/Applicants
        //[HttpPost]
        //public async Task<ActionResult<Applicants>> PostApplicants(Applicants applicants)
        //{
        //    DbContext_.Applicants.Add(applicants);
        //    await DbContext_.SaveChangesAsync();

        //    return CreatedAtAction("GetApplicants", new { id = applicants.ApplicantId }, applicants);
        //}

        // DELETE
        // api/v1/Recruitmen/Applicants/5

        /// <summary>
        /// Deletes an existing Applicant
        /// </summary>
        /// <param name="id">Applicant ID</param>
        /// <returns>A response as delete Applicant result</returns>
        /// <response code="200">If Applicant was deleted successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteApplicantAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteApplicantAsync));

            var response = new Response();

            try
            {
                // Get stock item by id
                var entity = await DbContext_.GetApplicantsAsync(new Applicants(id));

                // Validate if entity exists
                if (entity == null)
                    return NotFound();

                // Remove entity from repository
                DbContext_.Remove(entity);

                // Delete entity in database
                await DbContext_.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(DeleteApplicantAsync), ex);
            }

            return response.ToHttpResponse();
        }

        //// DELETE: api/Applicants/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Applicants>> DeleteApplicants(int? id)
        //{
        //    var applicants = await DbContext_.Applicants.FindAsync(id);
        //    if (applicants == null)
        //    {
        //        return NotFound();
        //    }

        //    DbContext_.Applicants.Remove(applicants);
        //    await DbContext_.SaveChangesAsync();

        //    return applicants;
        //}

        //private bool ApplicantsExists(int? id)
        //{
        //    return DbContext_.Applicants.Any(e => e.ApplicantId == id);
        //}
    }
}
