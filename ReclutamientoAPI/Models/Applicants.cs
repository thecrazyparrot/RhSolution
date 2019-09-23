using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ReclutamientoAPI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReclutamientoAPI.API.Models
{
    public partial class Applicants
    {
        public Applicants()
        {
        }
        public Applicants(int? ApplicantId)
        {
            this.ApplicantId = ApplicantId;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("newid()")]
        public int? ApplicantId { get; set; }
       
        public string ApellidoMaterno { get; set; }
        
        public string ApellidoPaterno { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        //
        //public ProcessStatusEnum ApplicationStatus { get; set; }

        //public DbSet<Applicants> Applicants { get; set; }
    }

   

    


}
