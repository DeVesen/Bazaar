using System;
using System.Collections.Generic;
using System.Linq;
using DeVes.Bazaar.Data.Contracts.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeVes.Bazaar.Data.MongoDb
{
    public abstract class BaseRepository<TModel>
        where TModel : BaseModel
    {
        protected readonly IMongoCollection<BsonDocument> Collection;


        public long Count => Collection.CountDocuments(new BsonDocument());


        protected BaseRepository(IMongoDatabase mongoDatabase, string collectionName)
        {
            Collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);
        }


        public IEnumerable<TModel> GetItems()
        {
            var documents = Collection.Find(new BsonDocument()).ToList();
            var models = documents.Select(ConvertToModel);
            return models;
        }

        public TModel GetItem(long number)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Number", number);
            var document = Collection.Find(filter).FirstOrDefault();
            var model = document != null ? ConvertToModel(document) : null;
            return model;
        }


        public long GetNextFreeNumber()
        {
            for (long number = 1; number < long.MaxValue; number++)
            {
                var filter = Builders<BsonDocument>.Filter.Eq("Number", number);
                if (Collection.Find(filter).Any() is false)
                {
                    return number;
                }
            }

            throw new NotImplementedException();
        }


        public void Insert(TModel model)
        {
            var insertDocument = ConvertToInsert(model);
            Collection.InsertOne(insertDocument);
        }

        public void Update(TModel model)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Number", model.Number);
            var update = ConvertToUpdate(model);

            var updateResult = Collection.UpdateOne(filter, update);

        }

        public long Delete(long number)
        {
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("Number", number);
            return Collection.DeleteOne(deleteFilter).DeletedCount;
        }


        protected abstract TModel ConvertToModel(BsonDocument document);
        protected abstract BsonDocument ConvertToInsert(TModel model);
        protected abstract UpdateDefinition<BsonDocument> ConvertToUpdate(TModel model);
    }
}