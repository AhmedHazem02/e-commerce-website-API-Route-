using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{
   
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        // Get
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>GetBasket(string Id)
        {
            var Basket=_basketRepository.GetBasketAsync(Id);
            return Basket is null ? new CustomerBasket(Id) : Ok(Basket);
        }

        // CreateOr Updata
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>>UpdataOrCreate(CustomerBasketDto basket)
        { 
            var MappingBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var CreateOrDelete=await _basketRepository.UpdataBasketAsync(MappingBasket);
            if(CreateOrDelete is null)return BadRequest(new ApiResponse(400));
            return Ok(CreateOrDelete);
        }
        //Delete
        [HttpDelete]
        public async Task<bool>DeleteBasket(string Id)
        {
            return await _basketRepository.DeleteBasketAsync(Id);
        }
    }
}
