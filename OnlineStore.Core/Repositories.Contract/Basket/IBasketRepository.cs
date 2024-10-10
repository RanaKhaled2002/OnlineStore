using OnlineStore.Core.Entities.Basket_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Repositories.Contract.Basket
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string id);
        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        public Task<bool> DeleteBasketAsync(string id);
    }
}
