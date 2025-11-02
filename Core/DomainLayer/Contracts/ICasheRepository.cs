using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ICasheRepository
    {
        Task<string?> GetAsync(string Cashekey);
        Task SetAsync(string Cashekey, string Cashevalue, TimeSpan TimetoLive);
    }
}
