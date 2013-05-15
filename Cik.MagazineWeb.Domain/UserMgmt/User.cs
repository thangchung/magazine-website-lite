using System;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace Cik.MagazineWeb.Domain.UserMgmt
{
    [HasUniqueDomainSignature(ErrorMessage = "A UserName already exists with the same name")]
    public class User : Entity
    {
        [DomainSignature]
        [Required(ErrorMessage = "UserName must be provided")]
        [Display(Name = "Username")]
        public virtual string UserName { get; set; }
 
        [Display(Name = "Display name")]
        public virtual string DisplayName { get; set; }

        [Display(Name = "Password")]
        public virtual string Password { get; set; }

        [Display(Name = "Email")]
        public virtual string Email { get; set; }
   
        [Display(Name = "Role")]
        public int Role { get; set; }
        
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Modified by")]
        public string ModifiedBy { get; set; }
        
        [Display(Name = "Created date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Modified date")]
        public DateTime? ModifiedDate { get; set; }

        public Role RoleEnum
        {
            get { return (Role)Role; }
        }
    }
}