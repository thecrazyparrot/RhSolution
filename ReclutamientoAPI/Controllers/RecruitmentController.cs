using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReclutamientoAPI.API.Models;
using ReclutamientoAPI.Models;

namespace ReclutamientoAPI.Controllers
{
#pragma warning disable CS1591
    [Route("api/v1/recruitment/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {

        protected readonly ILogger Logger;
        protected readonly ReclutamientoAPIDbContext DbContext;

        public CompaniesController(ILogger<CompaniesController> logger, ReclutamientoAPIDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

#pragma warning restore CS1591

        // GET
        // api/v1/Recruitment/Companies/

        /// <summary>
        /// Retrieves Companies
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <returns>A response with Companies list</returns>
        /// <response code="200">Returns the Companies list</response>
        /// <response code="500">If there was an internal server error</response>

        [HttpGet("Companies")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStockItemsAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemsAsync));

            var response = new PagedResponse<Companies>();

            try
            {
                // Get the "proposed" query from repository
                var query = DbContext.GetCompanies();

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

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemsAsync), ex);
            }

            return response.ToHttpResponse();
        }

        //GET
        // api/v1/Recruitment/Companies/5

        /// <summary>
        /// Retrieves a Company by ID
        /// </summary>
        /// <param name="id">Company id</param>
        /// <returns>A response with Company</returns>
        /// <response code="200">Returns a Companies list</response>
        /// <response code="404">If Company is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Companies/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStockItemAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemAsync));

            var response = new Models.SingleResponse<Companies>();

            try
            {
                // Get the stock item by id
                response.Model = await DbContext.GetCompaniesAsync(new Companies(id));
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // POST
        // api/v1/Recruitment/Companies/

        /// <summary>
        /// Creates a Company
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new stock item</returns>
        /// <response code="200">Returns companies list</response>
        /// <response code="201">A response as creation of company</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("Companies")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostCompanyAsync([FromBody]PostCompanyRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostCompanyAsync));

            var response = new SingleResponse<Companies>();

            try
            {
                var existingEntity = await DbContext
                    .GetCompaniesByCompanyNameAsync(new Companies { CompanyName = request.CompanyName });

                if (existingEntity != null)
                    ModelState.AddModelError("CompanyName", "Company name already exists");

                if (!ModelState.IsValid)
                    return BadRequest();

                // Create entity from request model
                var entity = request.ToEntity();

                // Add entity to repository
                DbContext.Add(entity);

                // Save entity in database
                await DbContext.SaveChangesAsync();

                // Set the entity to response model
                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostCompanyAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // PUT
        // api/v1/Recruitment/Companies/5

        /// <summary>
        /// Updates an existing Company
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <param name="request">Request model</param>
        /// <returns>A response as update stock item result</returns>
        /// <response code="200">If Company was updated successfully</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("Company/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutCompaniesAsync(int id, [FromBody]PutCompanyRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PutCompaniesAsync));

            var response = new Response();

            try
            {
                // Get stock item by id
                var entity = await DbContext.GetCompaniesAsync(new Companies(id));

                // Validate if entity exists
                if (entity == null)
                    return NotFound();

                // Set changes to entity
                entity.CompanyName = request.CompanyName;


                // Update entity in repository
                DbContext.Update(entity);

                // Save entity in database
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutCompaniesAsync), ex);
            }

            return response.ToHttpResponse();
        }

        // DELETE
        // api/v1/Warehouse/StockItem/5

        /// <summary>
        /// Deletes an existing Company
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <returns>A response as delete stock item result</returns>
        /// <response code="200">If stock item was deleted successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("Companies/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteStockItemAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteStockItemAsync));

            var response = new Response();

            try
            {
                // Get stock item by id
                var entity = await DbContext.GetCompaniesAsync(new Companies(id));

                // Validate if entity exists
                if (entity == null)
                    return NotFound();

                // Remove entity from repository
                DbContext.Remove(entity);

                // Delete entity in database
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(DeleteStockItemAsync), ex);
            }

            return response.ToHttpResponse();
        }

        //// POST: api/Values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
