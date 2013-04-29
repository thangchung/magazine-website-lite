using System;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;

namespace Cik.MagazineWeb.Domain
{
    public class AuditEntity : Entity
    {
        [Required(ErrorMessage = "CreatedBy must be provided")]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Modified by")]
        public string ModifiedBy { get; set; }

        [Required(ErrorMessage = "CreatedDate must be provided")]
        [Display(Name = "Created date")]
        public DateTime CreatedDate { get; set; }
        
        [Display(Name = "Modified date")]
        public DateTime? ModifiedDate { get; set; }
    }
}