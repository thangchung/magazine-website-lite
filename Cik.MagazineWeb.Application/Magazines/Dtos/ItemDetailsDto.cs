using Cik.MagazineWeb.Domain;

namespace Cik.MagazineWeb.Application.Magazines.Dtos
{
    public class ItemDetailsDto : AuditEntity
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Content { get; set; }

        public string SmallImage { get; set; }

        public long NumOfView { get; set; }
    }
}