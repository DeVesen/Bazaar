using System;
using System.Threading.Tasks;
using DeVes.Bazaar.GraphQl.Dtos;
using DeVes.Bazaar.GraphQl.Schemas;
using GraphQL.NewtonsoftJson;

namespace DeVes.Bazaar.GraphQl
{
    public class GraphQlLogic
    {
        private readonly GlobalAppSchema _globalAppSchema;

        public GraphQlLogic(GlobalAppSchema globalAppSchema)
        {
            _globalAppSchema = globalAppSchema;
        }

        public async Task<string> ExecuteAsync(GraphQlRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var tmp = await _globalAppSchema.ExecuteAsync(_ =>
            {
                _.OperationName = request?.OperationName;
                _.Query         = request.Query;
                _.Inputs        = request?.Variables.ToInputs();
            });

            return tmp;
        }
    }
}