using System.Collections.Generic;
using System.Linq;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeVes.Bazaar.Data.MongoDb
{
    public class ArticleRepository : BaseRepository<ArticleModel>, IArticleRepository
    {
        public ArticleRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, "ArticleRepository")
        {
        }


        public IEnumerable<ArticleModel> GetItems(long? sellerNumber)
        {
            FilterDefinition<BsonDocument> filter = new BsonDocument();

            if (sellerNumber.HasValue)
            {
                filter = Builders<BsonDocument>.Filter.Eq("SellerNumber", sellerNumber);
            }

            var documents = Collection.Find(filter).ToList();
            var models = documents.Select(ConvertToModel);
            return models;
        }


        protected override ArticleModel ConvertToModel(BsonDocument document)
        {
            var model = new ArticleModel
            {
                Number = document["Number"].AsInt64,
                SellerNumber = document["SellerNumber"].AsInt64,
                Title = document["Title"].AsString,
                Category = document["Category"].AsString,
                Manufacturer = document["Manufacturer"].AsString,
                Price = document["Price"].AsDouble,

                OnSaleSince = document["OnSaleSince"].ToNullableLocalTime(),
                SoldAt = document["SoldAt"].ToNullableLocalTime(),
                SoldFor = document["SoldFor"]?.ToDouble(),
                ReturnedAt = document["ReturnedAt"].ToNullableLocalTime()
            };
            return model;
        }

        protected override BsonDocument ConvertToInsert(ArticleModel model)
        {
            var properties     = model.GetType().GetProperties();
            var insertDocument = new BsonDocument()
            {
                ["Number"] = model.Number,
                ["SellerNumber"] = model.SellerNumber,
                ["Title"] = model.Title,
                ["Category"] = model.Category,
                ["Manufacturer"] = model.Manufacturer,
                ["Price"] = model.Price
            };

            if (model.OnSaleSince != null)
                insertDocument.Add("OnSaleSince", model.OnSaleSince);
            if (model.SoldAt != null)
                insertDocument.Add("SoldAt", model.SoldAt);
            if (model.SoldFor != null)
                insertDocument.Add("SoldFor", model.SoldFor);
            if (model.ReturnedAt != null)
                insertDocument.Add("ReturnedAt", model.ReturnedAt);

            return insertDocument;
        }

        protected override UpdateDefinition<BsonDocument> ConvertToUpdate(ArticleModel model)
        {
            var updateDocument = Builders<BsonDocument>.Update
                .Set("SellerNumber", model.SellerNumber)
                .Set("Title", model.Title)
                .Set("Category", model.Category)
                .Set("Manufacturer", model.Manufacturer)
                .Set("Price", model.Price);

            if (model.OnSaleSince != null)
                updateDocument.Set("OnSaleSince", model.OnSaleSince);
            if (model.SoldAt != null)
                updateDocument.Set("SoldAt", model.SoldAt);
            if (model.SoldFor != null)
                updateDocument.Set("SoldFor", model.SoldFor);
            if (model.ReturnedAt != null)
                updateDocument.Set("ReturnedAt", model.ReturnedAt);

            return updateDocument;
        }
    }
}