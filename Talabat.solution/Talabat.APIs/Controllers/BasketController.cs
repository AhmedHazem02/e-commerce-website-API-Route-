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
        public async Task<ActionResult<CustomerBasket>>GetBasket(string BasketId)
        {
            var Basket=await _basketRepository.GetBasketAsync(BasketId);
            return Basket is null ? new CustomerBasket(BasketId) : Basket;
        }

        // CreateOr Updata
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>>UpdataOrCreate(CustomerBasketDto basket)
        { 
           var MappingBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var CreateOrUpdata=await _basketRepository.UpdataBasketAsync(MappingBasket);
            if(CreateOrUpdata is null)return BadRequest(new ApiResponse(400));
            return Ok(CreateOrUpdata);
        }
        //Delete
        [HttpDelete]
        public async Task<ActionResult<bool>>DeleteBasket(string Id)
        {
            return await _basketRepository.DeleteBasketAsync(Id);
        }
    }
}
