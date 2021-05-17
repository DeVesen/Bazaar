using DeVes.Bazaar.Contracts.Models;

namespace DeVes.Bazaar.Contracts.Dto
{
    public class ArticleUpdateDto : BaseModel
    {
        public long?   SellerNumber { get; set; }
        public string Title        { get; set; }
        public string Category     { get; set; }
        public string Manufacturer { get; set; }
        public double? Price        { get; set; }
    }
}