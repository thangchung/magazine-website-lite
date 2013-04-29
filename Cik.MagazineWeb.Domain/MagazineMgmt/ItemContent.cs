using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace Cik.MagazineWeb.Domain.MagazineMgmt
{
    [HasUniqueDomainSignature(ErrorMessage = "An item already exists with the same title")]
    public sealed class ItemContent : AuditEntity
    {
        public ItemContent()
        {
            Items = new List<Item>();   
        }

        [DomainSignature]
        [Required(ErrorMessage = "Title must be provided")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Short description must be provided")]
        [Display(Name = "Short description")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Content must be provided")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Small image")]
        public string SmallImage { get; set; }

        [Display(Name = "Medium image")]
        public string MediumImage { get; set; }

        [Display(Name = "Large image")]
        public string BigImage { get; set; }

        public long NumOfView { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}