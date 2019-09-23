using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReclutamientoAPI.API.Models
{
#pragma warning disable CS1591
    public class PostCompanyRequest
    {
        //[Key]
        //public int? CompanyId { get; set; }

        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }
    }

    public class PostApplicantRequest
    {
        //[Key]
        //public int? ApplicantId { get; set; }

        [Required]
        [StringLength(50)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [StringLength(50)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }
    }

    public class PutCompanyRequest
    {
        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }
    }
    public class PutApplicantRequest
    {
        //[Key]
        //public int? ApplicantId { get; set; }

        [Required]
        [StringLength(50)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [StringLength(50)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

       
    }

    public static class Extensions
    {
        public static Companies ToEntity(this PostCompanyRequest request)
            => new Companies
            {
            //CompanyId = request.CompanyId,
            CompanyName = request.CompanyName
            };
        public static Applicants ToEntity(this PostApplicantRequest request)

            => new Applicants
            {
                ApellidoPaterno = request.ApellidoPaterno,
                ApellidoMaterno = request.ApellidoMaterno,
                Name = request.Name,
                RegisterDate = request.RegisterDate
            };
        

    }

#pragma warning restore CS1591
}
