using DeVes.Bazaar.Contracts.Models;
using GraphQL.Types;

namespace DeVes.Bazaar.GraphQl.Types
{
    public sealed class CategoryModelType : ObjectGraphType<CategoryModel>
    {
        public CategoryModelType()
        {
            Field(x => x.Number).Description("The Number of the Category.");
            Field(x => x.Title).Description("The Title of the Category.");
        }
    }
}