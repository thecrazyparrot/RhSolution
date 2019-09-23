using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReclutamientoAPI.API.Models
{

    public static class ApplicantsDbContextExtensions
    {
        public static IQueryable<Applicants> GetApplicants(this ReclutamientoAPIDbContext dbContext, int pageSize = 10, int pageNumber = 1)
        {
            // Get query from DbSet
            var query = dbContext.Applicants.AsQueryable();

            return query;
        }
        public static async Task<Applicants> GetApplicantsAsync(this ReclutamientoAPIDbContext dbContext, Applicants entity)
        => await dbContext.Applicants.FirstOrDefaultAsync(item => item.ApplicantId == entity.ApplicantId);

        public static async Task<Applicants> GetApplicantByNameAsync(this ReclutamientoAPIDbContext dbContext, Applicants entity)
            => await dbContext.Applicants.FirstOrDefaultAsync(item => item.Name == entity.Name); 
            //&& item.ApellidoMaterno == entity.ApellidoMaterno && item.ApellidoPaterno == entity.ApellidoPaterno) ;
    }

    public static class CompaniesDbContextExtensions
    {
        public static IQueryable<Companies> GetCompanies(this ReclutamientoAPIDbContext dbContext, int pageSize = 10, int pageNumber = 1)
        {
            // Get query from DbSet
            var query = dbContext.Companies.AsQueryable();

            return query;
        }
        public static async Task<Companies> GetCompaniesAsync(this ReclutamientoAPIDbContext dbContext, Companies entity)
        => await dbContext.Companies.FirstOrDefaultAsync(item => item.CompanyId == entity.CompanyId);

        public static async Task<Companies> GetCompaniesByCompanyNameAsync(this ReclutamientoAPIDbContext dbContext, Companies entity)
            => await dbContext.Companies.FirstOrDefaultAsync(item => item.CompanyName == entity.CompanyName);

    }



    public static class IQueryableExtensions
    {
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }

}
