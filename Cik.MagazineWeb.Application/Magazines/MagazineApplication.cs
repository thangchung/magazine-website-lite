using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cik.MagazineWeb.Application.Magazines.Dtos;
using Cik.MagazineWeb.Application.Magazines.Services;
using Cik.MagazineWeb.Application.Magazines.ViewModels;
using Cik.MagazineWeb.Domain.MagazineMgmt;
using Cik.MagazineWeb.Utilities;
using Cik.MagazineWeb.Utilities.DateTime;
using Cik.MagazineWeb.Utilities.Extensions;
using SharpLite.Domain.DataInterfaces;

namespace Cik.MagazineWeb.Application.Magazines
{
    public  partial class MagazineApplication : ApplicationBase, IMagazineApplication
    {
        #region variables & ctors

        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<ItemContent> _itemContentRepository;
        private readonly IItemSummaryService _itemSummaryService;

        public MagazineApplication(
            IRepository<Category> categoryRepository, 
            IRepository<Item> itemRepository, 
            IItemSummaryService itemSummaryService,
            IRepository<ItemContent> itemContentRepository)
        {
            Guard.ArgumentNotNull(categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");
            Guard.ArgumentNotNull(itemSummaryService, "ItemSummaryService");

            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
            _itemSummaryService = itemSummaryService;
            _itemContentRepository = itemContentRepository;
        }

        #endregion

        #region public APIs

        #region category

        public IEnumerable<CategorySummaryDto> GetCategorySummaries()
        {
            return GetAllCategorySummaries();
        }

        public CategorySummaryDto GetCategoryById(int id)
        {
            // return _categoryRepository.Get(id).MapTo<CategorySummaryDto>();

            // TODO: Demo queryable, it should be wrapped on Base Repository to improve performmace
            Expression<Func<Category, bool>> condition = x => x.Id == id;

            var result = this._categoryRepository.Query(condition).FirstOrDefault();

            return result.MapTo<CategorySummaryDto>();
        }

        public CategorySummaryViewModel GetCategoryPaging(int pageSize, int page)
        {
            var categories = GetAllCategorySummaries();
            var viewModel = new CategorySummaryViewModel();

            if (categories != null && categories.Any())
            {
                viewModel.TotalPage = categories.Count();
                viewModel.Categories = categories.Skip((page - 1)*pageSize).Take(pageSize);
            }

            return viewModel;
        }

        public void SaveCategory(CategorySummaryDto dto)
        {
            using (var context = _categoryRepository.DbContext)
            {
                Category newEntity = null;

                if (dto.Id > 0)
                {
                    var oldEntity = GetCategoryById(dto.Id); // this is actually not a valid way
                    if (oldEntity == null) return;
                    newEntity = dto.MapTo<Category>();
                    newEntity.CreatedBy = oldEntity.CreatedBy;
                    if (oldEntity.CreatedDate != null) newEntity.CreatedDate = oldEntity.CreatedDate.Value;
                    newEntity.ModifiedBy = "thangchung"; // need to remove hard code
                    newEntity.ModifiedDate = DatetimeRegion.GetCurrentTime();
                }
                else
                {
                    newEntity = dto.MapTo<Category>();
                    newEntity.CreatedBy = "thangchung"; // need to remove hard code
                    newEntity.CreatedDate = DatetimeRegion.GetCurrentTime();
                }

                _categoryRepository.SaveOrUpdate(newEntity);

                context.CommitChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            if (id <= 0) return; // should handle exception here

            using (var context = _categoryRepository.DbContext)
            {
                var entity = _categoryRepository.Get(id);
                if (entity == null) return; // should handle exception here

                _categoryRepository.Delete(entity);

                context.CommitChanges();
            }
        }

        #endregion

        #region item

        public ItemSummaryViewModel GetItemSummaryPaging(int pageSize, int page)
        {
            var viewModel = new ItemSummaryViewModel();
            var itemSummaries = _itemSummaryService.GetItemSummaries();

            if (itemSummaries == null || !itemSummaries.Any())
            {
                return new ItemSummaryViewModel();    
            }

            viewModel.TotalPage = itemSummaries.Count();
            viewModel.Items = itemSummaries.Skip((page - 1) * pageSize).Take(pageSize);

            return viewModel;
        }

        /// <summary>
        /// The get item by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ItemDetailsDto"/>.
        /// </returns>
        public ItemDetailsDto GetItemById(int id)
        {
            var data = this._itemRepository.Include("ItemContent").Get(id);
            return new ItemDetailsDto
                {
                    Id = data.Id,
                    CategoryId = data.Category.Id,
                    Title = data.ItemContent.Title,
                    SmallImageUrl = data.ItemContent.SmallImage,
                    MediumImageUrl = data.ItemContent.MediumImage,
                    BigImageUrl = data.ItemContent.BigImage,
                    Content = data.ItemContent.Content,
                    ShortDescription = data.ItemContent.ShortDescription,
                    CreatedDate = data.CreatedDate,
                    CreatedBy = data.CreatedBy,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                };
        }

        /// <summary>
        /// The save item.
        /// </summary>
        /// <param name="dto">
        /// The dto.
        /// </param>
        public void SaveItem(ItemDetailsDto dto)
        {
            using (var context = _itemRepository.DbContext)
            {
                Item itemEntity = null;
                ItemContent itemContentEntity = null;

                if (dto.Id > 0)
                {
                    itemEntity = _itemRepository.Get(dto.Id);

                    // Update category for Item
                    itemEntity.Category = _categoryRepository.Get(dto.CategoryId);

                    // Update Item Content
                    itemContentEntity = 
                        this._itemContentRepository.Get(itemEntity.ItemContentId); // this is actually not a valid way
                    
                    if (itemContentEntity == null)
                        return;

                    // Update Entity manually, Will be refactored to generic method applied for all entities
                    itemContentEntity.BigImage = dto.BigImageUrl;
                    itemContentEntity.Content = dto.Content;
                    itemContentEntity.MediumImage = dto.MediumImageUrl;
                    itemContentEntity.SmallImage = dto.SmallImageUrl;
                    itemContentEntity.ShortDescription = dto.ShortDescription;
                    itemContentEntity.Title = dto.Title;
                    itemContentEntity.ModifiedBy = "Actor login";
                    itemContentEntity.ModifiedDate = DatetimeRegion.GetCurrentTime();

                    itemEntity.Category = this._categoryRepository.Get(dto.CategoryId);
                    itemEntity.ModifiedBy = "Actor Login";
                    itemEntity.ModifiedDate = DatetimeRegion.GetCurrentTime();
                }
                else
                {
                    itemContentEntity = new ItemContent
                    {
                        BigImage = dto.BigImageUrl,
                        MediumImage = dto.MediumImageUrl,
                        SmallImage = dto.SmallImageUrl,
                        Content = dto.Content,
                        ShortDescription = dto.ShortDescription,
                        Title = dto.Title,
                        CreatedBy = "Actor login",
                        CreatedDate = DatetimeRegion.GetCurrentTime()
                    };

                    itemEntity = new Item
                        {
                            Category = this._categoryRepository.Get(dto.CategoryId),
                            CreatedBy = "Actor Login",
                            CreatedDate = DatetimeRegion.GetCurrentTime()
                        };
                }

                using (var itemDetailContext = this._itemContentRepository.DbContext)
                {
                    this._itemContentRepository.SaveOrUpdate(itemContentEntity);
                    itemDetailContext.CommitChanges();
                }

                // Add new Case
                if (dto.Id == 0)
                {
                    itemEntity.ItemContent = itemContentEntity;
                }

                this._itemRepository.SaveOrUpdate(itemEntity);

                context.CommitChanges();
            }
        }

        public void DeleteItem(int id)
        {
            if (id <= 0) return; // should handle exception here

            ItemContent itemContent = null;
            using (var context = this._itemRepository.DbContext)
            {
                var entity = this._itemRepository.Include("ItemContent").Get(id);

                if (entity == null) return; // should handle exception here

                itemContent = entity.ItemContent;
                
                this._itemRepository.Delete(entity);

                context.CommitChanges();
            }

            using (var context = this._itemContentRepository.DbContext)
            {
                this._itemContentRepository.Delete(itemContent);

                context.CommitChanges();
            }
        }

        #endregion

        #endregion

        #region private functions

        private IEnumerable<CategorySummaryDto> GetAllCategorySummaries()
        {
            var categories = _categoryRepository.GetAll();
            if (categories.Count() >= 0)
                return categories.ToList().MapTo<CategorySummaryDto>();

            return new List<CategorySummaryDto>();
        }

        #endregion
    }
}