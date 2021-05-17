using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVes.Bazaar.Contracts.Extensions
{
    public static class AsyncExtensions
    {
        public static async Task<IEnumerable<TR>> SelectAsync<TI, TR>(this IEnumerable<TI> input, Func<TI, int, Task<TR>> actionFunc)
        {
            var elements = input.ToArray();
            var results  = new List<TR>();
            for (var i = 0; i < elements.Length; i++)
            {
                results.Add(await actionFunc(elements[i], i));
            }
            return results;
        }
    }
}