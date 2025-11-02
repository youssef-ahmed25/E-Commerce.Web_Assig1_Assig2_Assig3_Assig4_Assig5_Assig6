using DomainLayer.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICasheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string Cashekey)
        {
            var cashevalue =await _database.StringGetAsync(Cashekey);
            return cashevalue.IsNullOrEmpty ? null : cashevalue.ToString();
        }

        public async Task SetAsync(string Cashekey, string Cashevalue, TimeSpan TimetoLive)
        {
          await  _database.StringSetAsync(Cashekey, Cashevalue, TimetoLive);
        }
    }
}
