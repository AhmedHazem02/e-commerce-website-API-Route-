using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer Redis) {
            _database = Redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
           var Basket=await _database.StringGetAsync(BasketId);
           return Basket.IsNull?null:JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdataBasketAsync(CustomerBasket customerBasket)
        {
             var JsonBasket=JsonSerializer.Serialize(customerBasket);
            var Created= await _database.StringSetAsync(customerBasket.Id, JsonBasket, TimeSpan.FromDays(1));
            if(!Created)return null;
            return await GetBasketAsync(customerBasket.Id);
        }
    }
}
