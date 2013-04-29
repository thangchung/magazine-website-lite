using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace Cik.MagazineWeb.Domain.MagazineMgmt
{
    [HasUniqueDomainSignature(ErrorMessage = "A category already exists with the same name")]
    public sealed class Category : AuditEntity
    {
        public Category()
        {
            Items = new List<Item>();
        }

        [DomainSignature]
        [Required(ErrorMessage = "Name must be provided")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
