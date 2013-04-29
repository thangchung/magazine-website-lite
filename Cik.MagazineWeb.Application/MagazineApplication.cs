using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cik.MagazineWeb.Application.Dtos;
using Cik.MagazineWeb.Application.ViewModels;
using Cik.MagazineWeb.Domain.MagazineMgmt;
using Cik.MagazineWeb.Domain.MagazineMgmt.Queries;
using Cik.MagazineWeb.Utilities;
using Cik.MagazineWeb.Utilities.Extensions;
using SharpLite.Domain.DataInterfaces;

namespace Cik.MagazineWeb.Application
{
    public class MagazineApplication : ApplicationBase, IMagazineApplication
    {
        #region variables & ctors

        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IQueryForItemSummaries _queryForItemSummaries;

        public MagazineApplication()
            : this(DependencyResolver.Current.GetService<IRepository<Category>>(),
                    DependencyResolver.Current.GetService<IRepository<Item>>(), 
                    DependencyResolver.Current.GetService<IQueryForItemSummaries>())
        {
        }

        public MagazineApplication(IRepository<Category> categoryRepository, IRepository<Item> itemRepository, IQueryForItemSummaries queryForItemSummaries)
        {
            Guard.ArgumentNotNull(categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");
            Guard.ArgumentNotNull(queryForItemSummaries, "QueryForItemSummaries");

            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
            _queryForItemSummaries = queryForItemSummaries;
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
            Category newEntity = null;

            if (dto.Id > 0)
            {
                var oldEntity = GetCategoryById(dto.Id);
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
            _categoryRepository.DbContext.CommitChanges();
        }

        public void DeleteCategory(int id)
        {
            if (id <= 0) return; // should handle exception here

            var entity = _categoryRepository.Get(id);
            if (entity == null) return; // should handle exception here

            _categoryRepository.Delete(entity);
            _categoryRepository.DbContext.CommitChanges();
        }

        #endregion

        #region item

        public ItemSummaryViewModel GetItemSummaryPaging(int pageSize, int page)
        {
            var viewModel = new ItemSummaryViewModel();
            var itemSummaries = _queryForItemSummaries.GetItemSummaries();

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