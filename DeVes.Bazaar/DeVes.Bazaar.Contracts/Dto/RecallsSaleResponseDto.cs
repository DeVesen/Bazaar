namespace DeVes.Bazaar.Contracts.Dto
{
    public class RecallsSaleResponseDto
    {
        public long   ArticleNumber { get; }
        public string ErrorCode     { get; }


        public RecallsSaleResponseDto(long articleNumber, string errorCode)
        {
            ArticleNumber = articleNumber;
            ErrorCode     = errorCode;
        }


        public static RecallsSaleResponseDto Create(long articleNumber)
            => Create(articleNumber, null);

        public static RecallsSaleResponseDto Create(long articleNumber, string errorCode) =>
            new RecallsSaleResponseDto(articleNumber, errorCode);
    }
}