using System;
using System.Collections.Generic;
using System.Linq;
using Cik.MagazineWeb.Application.Magazines.Dtos;
using Cik.MagazineWeb.Application.Magazines.Services;
using Cik.MagazineWeb.Application.Magazines.ViewModels;
using Cik.MagazineWeb.Domain.MagazineMgmt;
using Cik.MagazineWeb.Utilities;
using Cik.MagazineWeb.Utilities.Extensions;
using SharpLite.Domain.DataInterfaces;

namespace Cik.MagazineWeb.Application.Magazines
{
    public  partial class MagazineApplication : ApplicationBase, IMagazineApplication
    {
        #region variables & ctors

        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IItemSummaryService _itemSummaryService;

        public MagazineApplication(
            IRepository<Category> categoryRepository, 
            IRepository<Item> itemRepository, 
            IItemSummaryService itemSummaryService)
        {
            Guard.ArgumentNotNull(categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");
            Guard.ArgumentNotNull(itemSummaryService, "ItemSummaryService");

            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
            _itemSummaryService = itemSummaryService;
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
            return _categoryRepository.Get(id).MapTo<CategorySummaryDto>();
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
                    newEntity.ModifiedDate = DateTime.Now;
                }
                else
                {
                    newEntity = dto.MapTo<Category>();
                    newEntity.CreatedBy = "thangchung"; // need to remove hard code
                    newEntity.CreatedDate = DateTime.Now;
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