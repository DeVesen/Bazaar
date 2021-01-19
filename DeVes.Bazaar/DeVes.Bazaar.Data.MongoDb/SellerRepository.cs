using System;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeVes.Bazaar.Data.MongoDb
{
    public class SellerRepository : BaseRepository<SellerModel>, ISellerRepository
    {
        public SellerRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, "SellerRepository")
        {
        }


        protected override SellerModel ConvertToModel(BsonDocument document)
        {
            var model = new SellerModel
            {
                Number = document.AsBValue("Number").AsNullableInt64() ?? 0,
                FirstName = document.AsBValue("FirstName").AsString(),
                LastName = document.AsBValue("LastName").AsString(),

                Street = document.AsBValue("Street").AsString(),
                Zip = document.AsBValue("Zip").AsString(),
                Town = document.AsBValue("Town").AsString(),
                EMail = document.AsBValue("EMail").AsString(),
                Phone = document.AsBValue("Phone").AsString()
            };
            return model;
        }

        protected override BsonDocument ConvertToInsert(SellerModel model)
        {
            var insertDocument = new BsonDocument
            {
                ["Number"] = model.Number,
                ["FirstName"] = model.FirstName,
                ["LastName"] = model.LastName
            };

            if (model.Phone != null)
                insertDocument.Add("Phone", model.Phone);
            if (model.Street != null)
                insertDocument.Add("Street", model.Street);
            if (model.Street != null)
                insertDocument.Add("Zip", model.Zip);
            if (model.Town != null)
                insertDocument.Add("Town", model.Town);
            if (model.EMail != null)
                insertDocument.Add("EMail", model.EMail);

            return insertDocument;
        }

        protected override UpdateDefinition<BsonDocument> ConvertToUpdate(SellerModel model)
        {
            var updateDocument = Builders<BsonDocument>.Update.Set("FirstName", model.FirstName)
                                                              .Set("LastName", model.LastName)
                                                              .Set("Phone", model.Phone)
                                                              .Set("Street", model.Street)
                                                              .Set("Zip", model.Zip)
                                                              .Set("Town", model.Town)
                                                              .Set("EMail", model.EMail);

            return updateDocument;
        }
    }


    internal static class BsonDocumentExtensions
    {
        public static BValue AsBValue(this BsonDocument document, string name)
        {
            return document.TryGetValue(name, out var elementValue)
                ? new BValue(elementValue)
                : new BValue(BsonNull.Value);
        }
    }

    internal static class BValueExtensions
    {
        public static string AsString(this BValue bValue, string defaultValue = null)
        {
            return bValue == null || bValue.BsonValue == BsonNull.Value
                ? defaultValue
                : bValue.BsonValue.AsString;
        }

        public static long AsInt64(this BValue bValue, long defaultValue)
        {
            return bValue.AsNullableInt64() ?? defaultValue;
        }

        public static long? AsNullableInt64(this BValue bValue, long? defaultValue = null)
        {
            return bValue == null || bValue.BsonValue == BsonNull.Value
                ? defaultValue
                : bValue.BsonValue.AsInt64;
        }

        public static DateTime AsLocalTime(this BValue bValue, DateTime defaultValue)
        {
            return bValue.AsNullableLocalTime() ?? defaultValue;
        }

        public static DateTime? AsNullableLocalTime(this BValue bValue, DateTime? defaultValue = null)
        {
            return bValue == null || bValue.BsonValue == BsonNull.Value
                ? defaultValue
                : bValue.BsonValue.ToLocalTime();
        }
    }

    internal class BValue
    {
        public BsonValue BsonValue { get; }


        public BValue(BsonValue bsonValue)
        {
            BsonValue = bsonValue;
        }
    }
}