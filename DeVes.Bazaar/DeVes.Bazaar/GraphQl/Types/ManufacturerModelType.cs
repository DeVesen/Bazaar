using DeVes.Bazaar.Contracts.Models;
using GraphQL.Types;

namespace DeVes.Bazaar.GraphQl.Types
{
    public sealed class ManufacturerModelType : ObjectGraphType<ManufacturerModel>
    {
        private static QueryArgument ManufacturerNumberArgument => new QueryArgument<LongGraphType>
            {Name = "manufacturerNumber", Description = "Identifier of the Article."};

        private static QueryArgument TitleArgument => new QueryArgument<StringGraphType>
            {Name = "title", Description = "Title of the Article."};

        public static QueryArguments AllArguments => new QueryArguments(ManufacturerNumberArgument,
                                                                        TitleArgument);

        public ManufacturerModelType()
        {
            Field(x => x.Number).Description("The Number of the Manufacturer.");
            Field(x => x.Title).Description("The Title of the Manufacturer.");
        }
    }
}