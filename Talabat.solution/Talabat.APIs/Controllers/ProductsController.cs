using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{ 
    public class ProductsController : APIBaseController
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProductsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Get All
        
        [HttpGet]
        
        public async Task<ActionResult<Pagination<ProductToReturn>>> GetAllProduct([FromQuery]ProductSpecPrams Prams)
        {
            var Spec=new ProductBrandTypeSpecification(Prams);
            var Products = await _unitOfWork.Repository<Product>().GetAllBySpec(Spec);
            if(Products == null) return NotFound(new ApiResponse(404)); 
            var ProductReturn = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList < ProductToReturn >> (Products);
            var CountSpec= new ProductWithFiltrationForCount(Prams);
            var Count=await _unitOfWork.Repository<Product>().GetCountSpecAsync(CountSpec);
            return Ok(new Pagination<ProductToReturn>(Prams.pagesize,Prams.PageIndex,ProductReturn,Count));
        }
        //Get by ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturn),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturn>>GetProduct(int id)
        {
            var Spec = new ProductBrandTypeSpecification(id);
            var res = await _unitOfWork.Repository<Product>().GetByIDSpec(Spec);
            if (res == null) return NotFound(new ApiResponse(404));
            var ProductReturn = _mapper.Map<Product, ProductToReturn>(res);

            return Ok(ProductReturn);

        }

        // Get All Type
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllType()
        {
            var AllType = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            if (AllType == null) return NotFound(new ApiResponse(404));
            return Ok(AllType);

        }

        // Get All Brand
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var AllBrands=await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            if (AllBrands == null) return NotFound(new ApiResponse(404));
            return Ok(AllBrands);
        }

    }
}
