using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeVes.Bazaar.Data.MongoDb
{
    public class CategoryRepository : BaseRepository<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, "CategoryRepository")
        {
        }


        protected override CategoryModel ConvertToModel(BsonDocument document)
        {
            var model = new CategoryModel
            {
                Number = document["Number"].AsInt64,
                Title = document["Title"].AsString
            };
            return model;
        }

        protected override BsonDocument ConvertToInsert(CategoryModel model)
        {
            var insertDocument = new BsonDocument
            {
                ["Number"] = model.Number,
                ["Title"] = model.Title
            };
            return insertDocument;
        }

        protected override UpdateDefinition<BsonDocument> ConvertToUpdate(CategoryModel model)
        {
            var updateDocument = Builders<BsonDocument>.Update.Set("Title", model.Title);
            return updateDocument;
        }
    }
}