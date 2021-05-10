using System;
using DeVes.Bazaar.GraphQl.Query;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace DeVes.Bazaar.GraphQl.Schemas
{
    public class GlobalAppSchema : Schema
    {
        public GlobalAppSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<GlobalAppQuery>();
        }
    }
}