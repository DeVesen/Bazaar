using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Models;
using GraphQL.Types;

namespace DeVes.Bazaar.GraphQl.Types
{
    public sealed class ArticleModelType : ObjectGraphType<ArticleModel>
    {
        private static QueryArgument ArticleNumberArgument => new QueryArgument<LongGraphType>
            {Name = "articleNumber", Description = "Identifier of the Article."};

        private static QueryArgument SellerNumberArgument => new QueryArgument<LongGraphType>
            {Name = "sellerNumber", Description = "Identifier of the Seller."};

        private static QueryArgument TitleArgument => new QueryArgument<StringGraphType>
            {Name = "title", Description = "Title of the Article."};

        private static QueryArgument CategoryArgument => new QueryArgument<StringGraphType>
            {Name = "category", Description = "Category of the Article."};

        private static QueryArgument ManufacturerArgument => new QueryArgument<StringGraphType>
            {Name = "manufacturer", Description = "Manufacturer of the Article."};

        private static QueryArgument IsSoldArgument => new QueryArgument<BooleanGraphType>
            {Name = "isSold", Description = "IsSold of the Article."};

        private static QueryArgument IsReturnedArgument => new QueryArgument<BooleanGraphType>
            {Name = "isReturned", Description = "IsReturned of the Article."};

        public static QueryArguments AllArguments => new QueryArguments(ArticleNumberArgument,
                                                                        SellerNumberArgument,
                                                                        TitleArgument,
                                                                        CategoryArgument,
                                                                        ManufacturerArgument,
                                                                        IsSoldArgument,
                                                                        IsReturnedArgument);

        public ArticleModelType(ISellerLogic sellerLogic)
        {
            Field(x => x.Number).Description("The Number of the Article.");
            Field(x => x.SellerNumber).Description("The SellerNumber of the Article.");
            Field(x => x.Title).Description("The Title of the Article.");
            Field(x => x.Category).Description("The Category of the Article.");
            Field(x => x.Manufacturer).Description("The Manufacturer of the Article.");
            Field(x => x.OnSaleSince, true).Description("The OnSaleSince of the Article.");
            Field(x => x.Price).Description("The Price of the Article.");
            Field(x => x.SoldAt, true).Description("The SoldAt of the Article.");
            Field(x => x.SoldFor, true).Description("The SoldFor of the Article.");
            Field(x => x.ReturnedAt, true).Description("The ReturnedAt of the Article.");

            Field<ListGraphType<ArticleModelType>>("seller",
                                                   resolve: context =>
                                                       sellerLogic.GetItem(context.Source.SellerNumber));
        }
    }
}