using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;

namespace Cik.MagazineWeb.Domain.MagazineMgmt
{
    public class Item : AuditEntity
    {
        public Item(){ }

        public virtual int ItemContentId { get; set; }

        [Required(ErrorMessage = "Category must be provided")]
        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "ItemContent must be provided")]
        public virtual ItemContent ItemContent { get; set; }
    }
}