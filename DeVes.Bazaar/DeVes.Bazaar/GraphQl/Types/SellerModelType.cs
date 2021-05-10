using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Models;
using GraphQL;
using GraphQL.Types;

namespace DeVes.Bazaar.GraphQl.Types
{
    public sealed class SellerModelType : ObjectGraphType<SellerModel>
    {
        private static QueryArgument SellerNumberArgument => new QueryArgument<LongGraphType>
            { Name = "sellerNumber", Description = "Identifier of the Seller." };
        private static QueryArgument FirstNameArgument => new QueryArgument<StringGraphType>
            { Name = "firstName", Description = "The FirstName of the Seller." };
        private static QueryArgument LastNameArgument => new QueryArgument<StringGraphType>
            { Name = "lastName", Description = "The LastName of the Seller." };
        private static QueryArgument ZipArgument => new QueryArgument<StringGraphType>
            { Name = "zip", Description = "The ZIP of the Seller." };
        private static QueryArgument TownArgument => new QueryArgument<StringGraphType>
            { Name = "town", Description = "The Town of the Seller." };
        private static QueryArgument EMailArgument => new QueryArgument<StringGraphType>
            { Name = "eMail", Description = "The EMail of the Seller." };

        public static QueryArguments AllArguments => new QueryArguments(
            SellerNumberArgument,
            FirstNameArgument,
            LastNameArgument,
            ZipArgument,
            TownArgument,
            EMailArgument);

        public SellerModelType(ISellerLogic sellerLogic)
        {
            Field(x => x.Number).Description("The Number of the Seller.");
            Field(x => x.FirstName).Description("The FirstName of the Seller.");
            Field(x => x.LastName).Description("The LastName of the Seller.");
            Field(x => x.Street, true).Description("The Street of the Seller.");
            Field(x => x.Zip, true).Description("The Zip of the Seller.");
            Field(x => x.Town, true).Description("The Town of the Seller.");
            Field(x => x.EMail, true).Description("The EMail of the Seller.");
            Field(x => x.Phone, true).Description("The Phone of the Seller.");

            Field<ListGraphType<ArticleModelType>>(
                "articles",
                arguments: ArticleModelType.AllArguments,
                resolve: context => sellerLogic.GetItems(context.GetArgument<long?>("number"),
                    context.GetArgument<string>("firstName"),
                    context.GetArgument<string>("lastName"),
                    context.GetArgument<string>("zip"),
                    context.GetArgument<string>("town"),
                    context.GetArgument<string>("eMail")));
        }
    }
}