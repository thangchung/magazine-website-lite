using System.Collections.Generic;
using System.Linq;
using Cik.MagazineWeb.Domain.MagazineMgmt;
using Cik.MagazineWeb.Domain.MagazineMgmt.Queries;
using Cik.MagazineWeb.Utilities;
using SharpLite.Domain.DataInterfaces;

namespace Cik.MagazineWeb.EntityFrameworkProvider.Queries
{
    public class QueryForItemSummaries : IQueryForItemSummaries
    {
        private readonly IRepository<Item> _itemRepository;

        public QueryForItemSummaries(IRepository<Item> itemRepository)
        {
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");

            _itemRepository = itemRepository;

            // include Category & ItemContent table as well
            _itemRepository.Include("Category").Include("ItemContent");
        }

        public IEnumerable<ItemSummaryDto> GetItemSummaries()
        {
            var queryable = _itemRepository.GetAll();
            return (from item in queryable
                   select new ItemSummaryDto
                       {
                            CategoryId = item.Category.Id,
                            CategoryName = item.Category.Name,
                            ItemId = item.Id,
                            Title = item.ItemContent.Title,
                            ShortDescription = item.ItemContent.ShortDescription,
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            ModifiedBy = item.ModifiedBy,
                            ModifiedDate = item.ModifiedDate
                       }).ToList();
        }
    }
}