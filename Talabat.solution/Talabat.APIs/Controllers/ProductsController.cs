using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{ 
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public ProductsController(IGenericRepository<Product>ProductRepo
                                  ,IMapper mapper
                                  ,IGenericRepository<ProductType>TypeRepo
                                  , IGenericRepository<ProductBrand> BrandRepo)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _typeRepo = TypeRepo;
            _brandRepo = BrandRepo;
        }

        //Get All
        [Authorize]
        [HttpGet]
        
        public async Task<ActionResult<Pagination<ProductToReturn>>> GetAllProduct([FromQuery]ProductSpecPrams prams)
        {
            var Spec=new ProductBrandTypeSpecification(prams);
            var Products = await _productRepo.GetAllBySpec(Spec);
            if(Products == null) return NotFound(new ApiResponse(404));
            var ProductReturn = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList < ProductToReturn >> (Products);
            var CountSpec= new ProductWithFiltrationForCount(prams);
            var Count=await _productRepo.GetCountSpecAsync(CountSpec);
            return Ok(new Pagination<ProductToReturn>(prams.pagesize,prams.PageIndex,ProductReturn,Count));
        }
        //Get by ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturn),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturn>>GetProduct(int id)
        {
            var Spec = new ProductBrandTypeSpecification(id);
            var res = await _productRepo.GetByIDSpec(Spec);
            if (res == null) return NotFound(new ApiResponse(404));
            var ProductReturn = _mapper.Map<Product, ProductToReturn>(res);

            return Ok(ProductReturn);

        }

        // Get All Type
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllType()
        {
            var AllType = await _typeRepo.GetAllAsync();
            if (AllType == null) return NotFound(new ApiResponse(404));
            return Ok(AllType);

        }

        // Get All Brand
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var AllBrands=await _brandRepo.GetAllAsync();
            if (AllBrands == null) return NotFound(new ApiResponse(404));
            return Ok(AllBrands);
        }

    }
}
