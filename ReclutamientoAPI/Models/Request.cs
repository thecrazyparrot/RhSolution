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

    public class PutCompanyRequest
    {
        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }
    }

    public static class Extensions
    {
        public static Companies ToEntity(this PostCompanyRequest request)
            => new Companies
            {
                //CompanyId = request.CompanyId,
                CompanyName = request.CompanyName
            };
    }
}
