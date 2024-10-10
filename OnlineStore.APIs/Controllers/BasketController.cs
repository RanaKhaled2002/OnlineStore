using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.APIs.Error;
using OnlineStore.Core.DTOs.Basket;
using OnlineStore.Core.Entities.Basket_Module;
using OnlineStore.Core.Repositories.Contract.Basket;

namespace OnlineStore.APIs.Controllers
{
    
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basket;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basket, IMapper mapper)
        {
            _basket = basket;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string?  id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400,"Invalid Id !!"));
            var basket = await _basket.GetBasketAsync(id);

            if(basket is null) new CustomerBasket(){ Id = id };

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDTO model)
        {
            var basket =  await _basket.UpdateBasketAsync(_mapper.Map<CustomerBasket>(model));

            if (basket is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(basket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basket.DeleteBasketAsync(id);
        }
    }
}
