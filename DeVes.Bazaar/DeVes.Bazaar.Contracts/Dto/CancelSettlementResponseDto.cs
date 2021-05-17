namespace DeVes.Bazaar.Contracts.Dto
{
    public class CancelSettlementResponseDto
    {
        public long   ArticleNumber { get; }
        public string ErrorCode     { get; }


        public CancelSettlementResponseDto(long articleNumber, string errorCode)
        {
            ArticleNumber = articleNumber;
            ErrorCode     = errorCode;
        }


        public static CancelSettlementResponseDto Create(long articleNumber)
            => Create(articleNumber, null);

        public static CancelSettlementResponseDto Create(long articleNumber, string errorCode) =>
            new CancelSettlementResponseDto(articleNumber, errorCode);
    }
}