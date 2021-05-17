namespace DeVes.Bazaar.Contracts.Dto
{
    public class ArticleReceiptDto
    {
        public long    ArticleNumber { get; }
        public double? Price         { get; }
        public string  ErrorCode     { get; }

        
        public ArticleReceiptDto(long articleNumber, double? price, string errorCode)
        {
            ArticleNumber = articleNumber;
            Price         = price;
            ErrorCode     = errorCode;
        }


        public static ArticleReceiptDto Create(long articleNumber, double price)
            => new ArticleReceiptDto(articleNumber, price, null);

        public static ArticleReceiptDto Create(long articleNumber, string errorCode)
            => new ArticleReceiptDto(articleNumber, null, errorCode);
    }
}