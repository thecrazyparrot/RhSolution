using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReclutamientoAPI.API.Models;
using ReclutamientoAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace ReclutamientoAPI.Controllers
//{
//#pragma warning disable CS1591
//    [ApiController]
//    [Route("api/v1/[controller]")]
//    private class CompaniesController : ControllerBase
//    {
//        protected readonly ILogger Logger;
//        protected readonly ReclutamientoAPIDbContext DbContext;

//        public CompaniesController(ILogger<CompaniesController> logger, ReclutamientoAPIDbContext dbContext)
//        {
//            Logger = logger;
//            DbContext = dbContext;
//        }
//#pragma warning restore CS1591
//       
//        // GET
//        // api/v1/Warehouse/StockItem

//        /// <summary>
//        /// Retrieves stock items
//        /// </summary>
//        /// <param name="pageSize">Page size</param>
//        /// <param name="pageNumber">Page number</param>
//        /// <returns>A response with stock items list</returns>
//        /// <response code="200">Returns the stock items list</response>
//        /// <response code="500">If there was an internal server error</response>
//        [HttpGet("Companies")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> GetCompaniesAsync(int pageSize = 10, int pageNumber = 1)
//        {
//            Logger?.LogDebug("'{0}' has been invoked", nameof(GetCompaniesAsync));

//            var response = new PagedResponse<Companies>();

//            try
//            {
//                // Get the "proposed" query from repository
//                var query = DbContext.GetCompanies();

//                // Set paging values
//                response.PageSize = pageSize;
//                response.PageNumber = pageNumber;

//                // Get the total rows
//                response.ItemsCount = await query.CountAsync();

//                // Get the specific page from database
//                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

//                response.Message = string.Format("Page {0} of {1}, Total of products: {2}.", pageNumber, response.PageCount, response.ItemsCount);

//                Logger?.LogInformation("The stock items have been retrieved successfully.");
//            }
//            catch (Exception ex)
//            {
//                response.DidError = true;
//                response.ErrorMessage = "There was an internal error, please contact to technical support.";

//                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetCompaniesAsync), ex);
//            }

//            return response.ToHttpResponse();
//        }


//        // GET
//        // api/v1/Warehouse/StockItem/5

//        /// <summary>
//        /// Retrieves a stock item by ID
//        /// </summary>
//        /// <param name="id">Stock item id</param>
//        /// <returns>A response with stock item</returns>
//        /// <response code="200">Returns the stock items list</response>
//        /// <response code="404">If stock item is not exists</response>
//        /// <response code="500">If there was an internal server error</response>
//       [HttpGet("Companies/{id}")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> GetCompanyAsync(int id)
//        {
//            Logger?.LogDebug("'{0}' has been invoked", nameof(GetCompanyAsync));

//            var response = new SingleResponse<Companies>();

//            try
//            {
//                // Get the stock item by id
//                response.Model = await DbContext.GetCompaniesAsync(new Companies(id));
//            }
//            catch (Exception ex)
//            {
//                response.DidError = true;
//                response.ErrorMessage = "There was an internal error, please contact to technical support.";

//                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetCompanyAsync), ex);
//            }

//            return response.ToHttpResponse();
//        }


//        // POST
//        // api/v1/Warehouse/StockItem/

//        /// <summary>
//        /// Creates a new stock item
//        /// </summary>
//        /// <param name="request">Request model</param>
//        /// <returns>A response with new stock item</returns>
//        /// <response code="200">Returns the stock items list</response>
//        /// <response code="201">A response as creation of stock item</response>
//        /// <response code="400">For bad request</response>
//        /// <response code="500">If there was an internal server error</response>
//        [HttpPost("StockItem")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(201)]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> PostCompanyAsync([FromBody]PostCompanyRequest request)
//        {
//            Logger?.LogDebug("'{0}' has been invoked", nameof(PostCompanyAsync));

//            var response = new SingleResponse<Companies>();

//            try
//            {
//                var existingEntity = await DbContext
//                    .GetCompaniesByCompanyNameAsync(new Companies { CompanyName = request.CompanyName });

//                if (existingEntity != null)
//                    ModelState.AddModelError("StockItemName", "Stock item name already exists");

//                if (!ModelState.IsValid)
//                    return BadRequest();

//                // Create entity from request model
//                var entity = request.ToEntity();

//                // Add entity to repository
//                DbContext.Add(entity);

//                // Save entity in database
//                await DbContext.SaveChangesAsync();

//                // Set the entity to response model
//                response.Model = entity;
//            }
//            catch (Exception ex)
//            {
//                response.DidError = true;
//                response.ErrorMessage = "There was an internal error, please contact to technical support.";

//                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostCompanyAsync), ex);
//            }

//            return response.ToHttpResponse();
//        }

//        // PUT
//        // api/v1/Warehouse/StockItem/5

//        /// <summary>
//        /// Updates an existing stock item
//        /// </summary>
//        /// <param name="id">Stock item ID</param>
//        /// <param name="request">Request model</param>
//        /// <returns>A response as update stock item result</returns>
//        /// <response code="200">If stock item was updated successfully</response>
//        /// <response code="400">For bad request</response>
//        /// <response code="500">If there was an internal server error</response>
//        [HttpPut("StockItem/{id}")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> PutCompanyAsync(int id, [FromBody]PutCompanyRequest request)
//        {
//            Logger?.LogDebug("'{0}' has been invoked", nameof(PutCompanyAsync));

//            var response = new Response();

//            try
//            {
//                // Get stock item by id
//                var entity = await DbContext.GetCompaniesAsync(new Companies(id));

//                // Validate if entity exists
//                if (entity == null)
//                    return NotFound();

//                // Set changes to entity
//                entity.CompanyName = request.CompanyName;
               
//                // Update entity in repository
//                DbContext.Update(entity);

//                // Save entity in database
//                await DbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                response.DidError = true;
//                response.ErrorMessage = "There was an internal error, please contact to technical support.";

//                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutCompanyAsync), ex);
//            }

//            return response.ToHttpResponse();
//        }

//        // DELETE
//        // api/v1/Warehouse/StockItem/5

//        /// <summary>
//        /// Deletes an existing stock item
//        /// </summary>
//        /// <param name="id">Stock item ID</param>
//        /// <returns>A response as delete stock item result</returns>
//        /// <response code="200">If stock item was deleted successfully</response>
//        /// <response code="500">If there was an internal server error</response>
//        [HttpDelete("StockItem/{id}")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> DeleteStockItemAsync(int id)
//        {
//            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteStockItemAsync));

//            var response = new Response();

//            try
//            {
//                // Get stock item by id
//                var entity = await DbContext.GetCompaniesAsync(new Companies(id));

//                // Validate if entity exists
//                if (entity == null)
//                    return NotFound();

//                // Remove entity from repository
//                DbContext.Remove(entity);

//                // Delete entity in database
//                await DbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                response.DidError = true;
//                response.ErrorMessage = "There was an internal error, please contact to technical support.";

//                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(DeleteStockItemAsync), ex);
//            }

//            return response.ToHttpResponse();
//        }

//    }
//}
