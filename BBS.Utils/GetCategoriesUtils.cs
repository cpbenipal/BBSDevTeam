using AutoMapper;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetCategoriesUtils
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetCategoriesUtils(IMapper mapper, IRepositoryWrapper repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public List<GetCategoryResultItem> ParseCategoriesToDto(List<Category> categories)
        {
            List<GetCategoryResultItem> result = new();
            foreach (Category category in categories)
            {
                GetCategoryResultItem getCategoryDto = BuildCategoryDto(category);

                result.Add(getCategoryDto);
            }

            return result;
        }

        private GetCategoryResultItem BuildCategoryDto(Category category)
        {
            var categoryDto = _mapper.Map<GetCategoryResultItem>(category);

            var offerShareMainType = _repository
                .OfferedShareMainTypeManager
                .GetOfferedShareMainType(category.OfferedShareMainTypeId);

            categoryDto.OfferedShareMainType = offerShareMainType.Name;

            return categoryDto;
        }
    }
}
