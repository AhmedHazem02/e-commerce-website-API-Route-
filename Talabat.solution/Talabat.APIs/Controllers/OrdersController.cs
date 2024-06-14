using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.OrderAggragate;
using Talabat.Core.Service;
using Talabat.Repository.Data.Configurations;

namespace Talabat.APIs.Controllers
{
  
    public class OrdersController : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Order>>CreateOrder(OrderDto order)
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var AddressMapping = _mapper.Map<AddressDto, Address>(order.shipToAddress);
            var Order =await _orderService.CreateOrderAsync(Email, order.BasketId, order.DeliveryMethodId, AddressMapping);
            if (Order is null) return BadRequest(new ApiResponse(400, "Wrong in Your Order"));
            return Ok(Order);
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<Order>>> Orders(string buyeremail)
        {
             var Orders=await _orderService.GetAllOrderByEmailSpecUser(buyeremail);
            if(Orders is null) return BadRequest(new ApiResponse(400, "Wrong in Your Email"));
            var MapperOrder = _mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderDtoToReturn>>(Orders);
            return Ok(MapperOrder);

        }

        [HttpGet("{Id}")]
        [Authorize]
        [ProducesResponseType(typeof(OrderDtoToReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDtoToReturn>> Orders(string buyeremail, int Id)
        {
            var Orders = await _orderService.GetOrderByIdSpecUser(buyeremail,Id);
            if (Orders is null) return BadRequest(new ApiResponse(400, "Wrong in Your Email Or Id"));
            var MapperOrder = _mapper.Map<Order, OrderDtoToReturn>(Orders);
            return Ok(MapperOrder);

        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            var Res=await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return Ok(Res);
        }


    }
}
