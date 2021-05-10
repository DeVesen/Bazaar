using System;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Models;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

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
                _.Query = request.Query;
                _.Inputs = request?.Variables.ToInputs();
            });

            return tmp;
        }
    }


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

    public sealed class ArticleModelType : ObjectGraphType<ArticleModel>
    {
        private static QueryArgument ArticleNumberArgument => new QueryArgument<LongGraphType>
            { Name = "articleNumber", Description = "Identifier of the Article." };
        private static QueryArgument SellerNumberArgument => new QueryArgument<LongGraphType>
            { Name = "sellerNumber", Description = "Identifier of the Seller." };
        private static QueryArgument TitleArgument => new QueryArgument<StringGraphType>
            { Name = "title", Description = "Title of the Article." };
        private static QueryArgument CategoryArgument => new QueryArgument<StringGraphType>
            { Name = "category", Description = "Category of the Article." };
        private static QueryArgument ManufacturerArgument => new QueryArgument<StringGraphType>
            { Name = "manufacturer", Description = "Manufacturer of the Article." };
        private static QueryArgument IsSoldArgument => new QueryArgument<BooleanGraphType>
            { Name = "isSold", Description = "IsSold of the Article." };
        private static QueryArgument IsReturnedArgument => new QueryArgument<BooleanGraphType>
            { Name = "isReturned", Description = "IsReturned of the Article." };

        public static QueryArguments AllArguments => new QueryArguments(
            ArticleNumberArgument,
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
                resolve: context => sellerLogic.GetItem(context.Source.SellerNumber));
        }
    }

    public sealed class CategoryModelType : ObjectGraphType<CategoryModel>
    {
        public CategoryModelType()
        {
            Field(x => x.Number).Description("The Number of the Category.");
            Field(x => x.Title).Description("The Title of the Category.");
        }
    }

    public sealed class ManufacturerModelType : ObjectGraphType<ManufacturerModel>
    {
        private static QueryArgument ManufacturerNumberArgument => new QueryArgument<LongGraphType>
            { Name = "manufacturerNumber", Description = "Identifier of the Article." };
        private static QueryArgument TitleArgument => new QueryArgument<StringGraphType>
            { Name = "title", Description = "Title of the Article." };

        public static QueryArguments AllArguments => new QueryArguments(
            ManufacturerNumberArgument,
            TitleArgument);

        public ManufacturerModelType()
        {
            Field(x => x.Number).Description("The Number of the Manufacturer.");
            Field(x => x.Title).Description("The Title of the Manufacturer.");
        }
    }


    public class GlobalAppQuery : ObjectGraphType
    {
        public GlobalAppQuery(ISellerLogic       sellerLogic,
                              IArticleLogic      articleLogic,
                              ICategoryLogic     categoryLogic,
                              IManufacturerLogic manufacturerLogic)
        {
            Field<ListGraphType<SellerModelType>>(
                "sellers",
                arguments: SellerModelType.AllArguments,
                resolve: ctx => sellerLogic.GetItems(ctx.GetArgument<long?>("number"),
                                                     ctx.GetArgument<string>("firstName"),
                                                     ctx.GetArgument<string>("lastName"),
                                                     ctx.GetArgument<string>("zip"),
                                                     ctx.GetArgument<string>("town"),
                                                     ctx.GetArgument<string>("eMail")));

            Field<ListGraphType<ArticleModelType>>(
                "articles",
                arguments: ArticleModelType.AllArguments,
                resolve: ctx => articleLogic.GetItems(ctx.GetArgument<long?>("articleNumber"),
                                                      ctx.GetArgument<long?>("sellerNumber"),
                                                      ctx.GetArgument<string>("title"),
                                                      ctx.GetArgument<string>("category"),
                                                      ctx.GetArgument<string>("manufacturer"),
                                                      ctx.GetArgument<bool?>("isSold"),
                                                      ctx.GetArgument<bool?>("isReturned")));

            Field<ListGraphType<CategoryModelType>>(
                "categories",
                resolve: ctx => categoryLogic.GetItems());

            Field<ListGraphType<ManufacturerModelType>>(
                "manufacturer",
                arguments: ManufacturerModelType.AllArguments,
                resolve: ctx => manufacturerLogic.GetItems());
        }
    }

    public class GlobalAppSchema : Schema
    {
        public GlobalAppSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<GlobalAppQuery>();
        }
    }


    public class GraphQlRequest
    {
        public string OperationName { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
