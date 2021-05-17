namespace DeVes.Bazaar.Contracts.Dto
{
    public class MarkedResponseDto
    {
        public long   ArticleNumber { get; }
        public string ErrorCode     { get; }


        public MarkedResponseDto(long articleNumber, string errorCode)
        {
            ArticleNumber = articleNumber;
            ErrorCode     = errorCode;
        }


        public static MarkedResponseDto Create(long articleNumber)
            => Create(articleNumber, null);

        public static MarkedResponseDto Create(long articleNumber, string errorCode) =>
            new MarkedResponseDto(articleNumber, errorCode);
    }
}