namespace DeVes.Bazaar.Data.Contracts.Models
{
    public class SellerModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string Town { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
    }
}