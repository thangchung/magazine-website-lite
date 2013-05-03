using System.Collections.Generic;
using System.Linq;
using Cik.MagazineWeb.Application.Magazines.Dtos;
using Cik.MagazineWeb.Domain.MagazineMgmt;
using Cik.MagazineWeb.Utilities;
using SharpLite.Domain.DataInterfaces;

namespace Cik.MagazineWeb.Application.Magazines.Services
{
    public class ItemSummaryService : IItemSummaryService
    {
        private readonly IRepository<Item> _itemRepository;

        public ItemSummaryService(IRepository<Item> itemRepository)
        {
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");

            _itemRepository = itemRepository;

            // include Category & ItemContent table as well
            _itemRepository.Include("Category").Include("ItemContent");
        }

        public IEnumerable<ItemSummaryDto> GetItemSummaries()
        {
            var queryable = _itemRepository.GetAll();
            return ConvertToItemSummaryDtoQuery(queryable);
        }

        public IEnumerable<ItemSummaryDto> GetHottestItems(int numOfItemOnHomePage)
        {
            var queryable = _itemRepository.GetAll();
            return ConvertToItemSummaryDtoQuery(
                        queryable.OrderByDescending(item => item.CreatedDate),
                        numOfItemOnHomePage);
        }

        public IEnumerable<ItemSummaryDto> GetLatestItems(int numOfItemOnHomePage)
        {
            var queryable = _itemRepository.GetAll();

            return ConvertToItemSummaryDtoQuery(
                        queryable.OrderByDescending(item => item.ItemContent.NumOfView), 
                        numOfItemOnHomePage);
        }

        public IEnumerable<ItemSummaryDto> GetItemsByCategoryId(int categoryId)
        {
            var queryable = _itemRepository.GetAll();
            return ConvertToItemSummaryDtoQuery(
                        queryable.OrderByDescending(item => item.CreatedDate))
                            .Where(item => item.CategoryId == categoryId);
        }

        public ItemDetailsDto GetItemDetails(int itemId)
        {
            return (from item in _itemRepository.GetAll()
                    where item.Id == itemId
                    select new ItemDetailsDto
                        {
                            Title = item.ItemContent.Title,
                            Content = item.ItemContent.Content,
                            SmallImage = item.ItemContent.SmallImage
                        }).FirstOrDefault();
        }

        private IEnumerable<ItemSummaryDto> ConvertToItemSummaryDtoQuery(IQueryable<Item> sourceQuery,  int? numOfItemOnHomePage = null)
        {
            var queryableResult = sourceQuery.Select(
                item => new ItemSummaryDto
                {
                    CategoryId = item.Category.Id,
                    CategoryName = item.Category.Name,
                    ItemId = item.Id,
                    Title = item.ItemContent.Title,
                    AvatarImage = item.ItemContent.SmallImage,
                    ShortDescription = item.ItemContent.ShortDescription,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedDate = item.ModifiedDate
                });

            if (numOfItemOnHomePage.HasValue)
            {
                queryableResult = queryableResult.Take(numOfItemOnHomePage.Value);
            }

            return queryableResult;
        }
    }
}