using Cik.MagazineWeb.Domain;

namespace Cik.MagazineWeb.Application.Magazines.Dtos
{
    public class ItemSummaryDto : AuditEntity
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int ItemId { get; set; }

        public string Title { get; set; }

        public string AvatarImage { get; set; }

        public string ShortDescription { get; set; }
    }
}