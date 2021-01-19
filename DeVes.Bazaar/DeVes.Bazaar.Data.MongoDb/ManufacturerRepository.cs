using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeVes.Bazaar.Data.MongoDb
{
    public class ManufacturerRepository : BaseRepository<ManufacturerModel>, IManufacturerRepository
    {
        public ManufacturerRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, "ManufacturerRepository")
        {
        }


        protected override ManufacturerModel ConvertToModel(BsonDocument document)
        {
            var model = new ManufacturerModel
            {
                Number = document["Number"].AsInt64,
                Title = document["Title"].AsString
            };
            return model;
        }

        protected override BsonDocument ConvertToInsert(ManufacturerModel model)
        {
            var insertDocument = new BsonDocument
            {
                ["Number"] = model.Number,
                ["Title"] = model.Title
            };
            return insertDocument;
        }

        protected override UpdateDefinition<BsonDocument> ConvertToUpdate(ManufacturerModel model)
        {
            var updateDocument = Builders<BsonDocument>.Update.Set("Title", model.Title);
            return updateDocument;
        }
    }
}