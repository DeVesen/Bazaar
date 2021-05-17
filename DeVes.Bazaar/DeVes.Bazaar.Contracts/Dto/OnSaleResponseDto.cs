namespace DeVes.Bazaar.Contracts.Dto
{
    public class OnSaleResponseDto
    {
        public long   ArticleNumber { get; }
        public string ErrorCode     { get; }


        public OnSaleResponseDto(long articleNumber, string errorCode)
        {
            ArticleNumber = articleNumber;
            ErrorCode     = errorCode;
        }


        public static OnSaleResponseDto Create(long articleNumber)
            => Create(articleNumber, null);

        public static OnSaleResponseDto Create(long articleNumber, string errorCode) =>
            new OnSaleResponseDto(articleNumber, errorCode);
    }
}