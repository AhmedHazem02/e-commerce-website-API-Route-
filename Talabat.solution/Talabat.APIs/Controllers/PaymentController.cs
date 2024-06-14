using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Service;

namespace Talabat.APIs.Controllers
{ 
    public class PaymentsController : APIBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService,
                                 IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
       
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePayment(string basketId)
        { 
            var Basket=await _paymentService.CreateOrUpdataPayment(basketId);
            if (Basket == null) return BadRequest(new ApiResponse(400, "Error in Your Basket"));
           // var BasketMapper = _mapper.Map<CustomerBasket, CustomerBasketDto>(Basket);
            return Ok(Basket);

        }
    }
}
