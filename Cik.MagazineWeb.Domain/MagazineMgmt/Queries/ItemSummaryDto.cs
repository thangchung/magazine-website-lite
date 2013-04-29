namespace Cik.MagazineWeb.Domain.MagazineMgmt.Queries
{
    public class ItemSummaryDto : AuditEntity
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int ItemId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }
    }
}