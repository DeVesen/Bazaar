using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DeVes.Bazaar.Extensions
{
    public static class QueryCollectionExtensions
    {
        public enum ParameterPick
        {
            First,
            Last
        }

        public static IEnumerable<T> All<T>(this IQueryCollection collection, string key)
        {
            var values = new List<T>();

            if (!collection.TryGetValue(key, out var results)) return values;

            foreach (var s in results)
            {
                try
                {
                    var result = (T)Convert.ChangeType(s, typeof(T));
                    values.Add(result);
                }
                catch (Exception)
                {
                    // conversion failed
                    // skip value
                }
            }

            return values;
        }

        public static T Get<T>(this IQueryCollection collection, string key, T @default = default, ParameterPick option = ParameterPick.First)
        {
            var values = All<T>(collection, key).ToArray();
            var value = @default;

            if (values.Any())
            {
                value = option switch
                {
                    ParameterPick.First => values.FirstOrDefault(),
                    ParameterPick.Last => values.LastOrDefault(),
                    _ => value
                };
            }

            return value ?? @default;
        }
    }
}