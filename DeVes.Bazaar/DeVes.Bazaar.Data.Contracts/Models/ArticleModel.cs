using System;

namespace DeVes.Bazaar.Data.Contracts.Models
{
    public class ArticleModel : BaseModel
    {
        public long SellerNumber { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public DateTime? OnSaleSince { get; set; }
        public double Price { get; set; }


        public DateTime? SoldAt { get; set; }
        public double? SoldFor { get; set; }
        public DateTime? ReturnedAt { get; set; }
    }
}