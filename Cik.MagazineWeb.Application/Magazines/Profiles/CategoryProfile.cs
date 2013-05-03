using AutoMapper;
using Cik.MagazineWeb.Application.Magazines.Dtos;
using Cik.MagazineWeb.Domain.MagazineMgmt;

namespace Cik.MagazineWeb.Application.Magazines.Profiles
{
    public class CategoryProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Category, CategorySummaryDto>();
            Mapper.CreateMap<CategorySummaryDto, Category>();
        }
    }
}