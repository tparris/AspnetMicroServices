using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Formats.Asn1;

namespace Basket.API.Repositories
{
    public class BasketRepository: IBasketRepository
    {
        private readonly ILogger _logger;
        private readonly IDistributedCache _redisCache;
        public BasketRepository(ILogger<BasketRepository>logger, IDistributedCache distributedCache)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redisCache = distributedCache?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
