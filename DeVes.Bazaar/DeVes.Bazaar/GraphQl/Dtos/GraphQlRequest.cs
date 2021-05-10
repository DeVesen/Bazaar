using Newtonsoft.Json.Linq;

namespace DeVes.Bazaar.GraphQl.Dtos
{
    public class GraphQlRequest
    {
        public string OperationName { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}