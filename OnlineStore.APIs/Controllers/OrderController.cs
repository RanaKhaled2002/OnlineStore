using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.APIs.Error;
using OnlineStore.Core.DTOs.Orders;
using OnlineStore.Core.Entities.Order;
using OnlineStore.Core.Services.Contract.Orders;
using System.Security.Claims;

namespace OnlineStore.APIs.Controllers
{
    
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> createOrder(OrderDto model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var Address = _mapper.Map<Address>(model.shippingAddress);

            var order =  await _orderService.CreateOrderAsync(userEmail, model.BasketId, int.Parse(model.DeliveryMethodId), Address);

            if(order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(_mapper.Map<OrderReturnDto>(order));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrderForSpecificUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var Orders = await _orderService.GetOrderFromSpecificUserAsync(email);

            if (Orders is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(_mapper.Map<IEnumerable<OrderReturnDto>>(Orders));
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdForSpecificUser(int orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            var Order = await _orderService.GetOrderByIdFromSpecificUserAsync(email,orderId);

            if (Order is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(_mapper.Map<OrderReturnDto>(Order));
        }
    }
}
