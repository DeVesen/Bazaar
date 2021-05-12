using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.GraphQl.Types;
using GraphQL;
using GraphQL.Types;

namespace DeVes.Bazaar.GraphQl.Query
{
    public class GlobalAppQuery : ObjectGraphType
    {
        public GlobalAppQuery(ISellerLogic       sellerLogic,
                              IArticleLogic      articleLogic,
                              ICategoryLogic     categoryLogic,
                              IManufacturerLogic manufacturerLogic)
        {
            Field<ListGraphType<SellerModelType>>("sellers",
                                                  arguments: SellerModelType.AllArguments,
                                                  resolve: ctx => sellerLogic.GetItems(ctx.GetArgument<long?>("number"),
                                                      ctx.GetArgument<string>("firstName"),
                                                      ctx.GetArgument<string>("lastName"),
                                                      ctx.GetArgument<string>("zip"),
                                                      ctx.GetArgument<string>("town"),
                                                      ctx.GetArgument<string>("eMail")));

            Field<ListGraphType<ArticleModelType>>("articles",
                                                   arguments: ArticleModelType.AllArguments,
                                                   resolve: ctx =>
                                                       articleLogic.GetItems(ctx.GetArgument<long?>("articleNumber"),
                                                                             ctx.GetArgument<long?>("sellerNumber"),
                                                                             ctx.GetArgument<string>("title"),
                                                                             ctx.GetArgument<string>("category"),
                                                                             ctx.GetArgument<string>("manufacturer"),
                                                                             ctx.GetArgument<bool?>("isSold"),
                                                                             ctx.GetArgument<bool?>("isReturned")));

            Field<ListGraphType<CategoryModelType>>("categories",
                                                    resolve: ctx => categoryLogic.GetItems());

            Field<ListGraphType<ManufacturerModelType>>("manufacturer",
                                                        arguments: ManufacturerModelType.AllArguments,
                                                        resolve: ctx => manufacturerLogic.GetItems());
        }
    }
}