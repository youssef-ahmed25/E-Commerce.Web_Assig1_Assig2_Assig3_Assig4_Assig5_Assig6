using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
     class ProductWithBrandAndTypeSpecification:BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecification(ProductQueryParams queryParams)
            : base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId)
            && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))

        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (queryParams.sortingOption)
            {
                case ProductSortingOption.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOption.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOption.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOption.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;

            }

            ApplyPagination(queryParams.PageSize,queryParams.PageIndex);
        }
        public ProductWithBrandAndTypeSpecification(int id):base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
