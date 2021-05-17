namespace DeVes.Bazaar.Logic
{
    public static class ErrorCodes
    {
        // Seller
        public const string SellerNotKnown          = "SellerNotKnown";
        public const string SellerNoUnBilledArticle = "SellerNoUnBilledArticle";

        // Article
        public const string ArticleNotKnown        = "ArticleNotKnown";
        public const string ArticlePriceNotValid   = "ArticlePriceNotValid";
        public const string ArticleNotFreeForSale  = "ArticleNotFreeForSale";
        public const string ArticleAlreadySold     = "ArticleAlreadySold";
        public const string ArticleAlreadyReturned = "ArticleAlreadyReturned";
    }
}