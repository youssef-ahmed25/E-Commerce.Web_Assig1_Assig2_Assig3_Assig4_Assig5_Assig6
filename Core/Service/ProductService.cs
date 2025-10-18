﻿using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync();
            var brandDtos = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandDtos;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            #region قديم
            //var repo = _unitOfWork.GetRepository<Product, int>();
            //var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            //return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products); 
            #endregion
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecification(queryParams);
            var products = await repo.GetAllAsync(specification);
            var productsDto= _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var productsCount = productsDto.Count();
            var CountSpec= new ProductCountSpecification(queryParams);
            var totalCount = await repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>( productsCount, queryParams.PageIndex, totalCount, productsDto);

        }
        

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var types =await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecification(id);
            var product = await repo.GetByIdAsync(specification);
            if (product is null)
                throw new ProductNotFoundException(id);
            return _mapper.Map<Product, ProductDto>(product);
        }
    }
}
