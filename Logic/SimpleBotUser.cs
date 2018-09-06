using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        public static string Reply(Message message)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("db01");
            IMongoCollection<BsonDocument> col = db.GetCollection<BsonDocument>("tabela01");
            BsonDocument doc = new BsonDocument()
            {
                {"id", message.Id},
                {"texto", message.Text},
                {"usuario", message.User},
                {"app", "testeBot"}
            };

            col.InsertOne(doc);

            return $"{message.User} disse:'{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("db01");
            IMongoCollection<BsonDocument> col = db.GetCollection<BsonDocument>("tabela01");
            var filter = Builders<BsonDocument>.Filter.Eq("usuario", "User");
            var a = col.Find(filter).ToList();
          //  return a;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
        }
    }
}