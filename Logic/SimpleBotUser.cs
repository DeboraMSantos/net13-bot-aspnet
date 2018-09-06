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
        static Dictionary<string, UserProfile> _dictProfiles = new Dictionary<string, UserProfile>();
        public static string Reply(Message message)
        {
            //    MongoClient client = new MongoClient("mongodb://localhost:27017");
            //    IMongoDatabase db = client.GetDatabase("db01");
            //    IMongoCollection<BsonDocument> col = db.GetCollection<BsonDocument>("tabela01");
            //    BsonDocument doc = new BsonDocument()
            //    {
            //        {"id", message.Id},
            //        {"texto", message.Text},
            //        {"usuario", message.User},
            //        {"app", "testeBot"}
            //    };

            //    col.InsertOne(doc);

            //    UserProfile getProfile = GetProfile(message.Id);
            //    SetProfile(message.Id, getProfile);

            //    return $"{message.Id} - {getProfile.Visitas}";
            var id = message.Id;
            var profile = GetProfile(id);
            profile.Visitas++;
            SetProfile(id, profile);
            return $"{message.User} disse '{message.Text}' e mandou {profile.Visitas}";
        }

        public static UserProfile GetProfile(string id)
        {
            //MongoClient client = new MongoClient("mongodb://localhost:27017");
            //IMongoDatabase db = client.GetDatabase("db01");
            //IMongoCollection<BsonDocument> col = db.GetCollection<BsonDocument>("tabela01");
            //var filter = Builders<BsonDocument>.Filter.Eq("id", id);
            //var a = col.Find(filter).ToList();

            //UserProfile userProfile = new UserProfile()
            //{
            //    Id = id,
            //    Visitas = a.Count()
            //};             

            //return userProfile;

            _dictProfiles.TryGetValue(id, out var profile);

            if(profile == null)
            {

            }
        }
        public static void SalvarHistorico(Message message) { }

        public static void SetProfile(string id, UserProfile profile)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("db01");
            IMongoCollection<BsonDocument> col = db.GetCollection<BsonDocument>("profile");
            var filter = Builders<BsonDocument>.Filter.Eq("id", id);
            BsonDocument doc = new BsonDocument()
            {
                {"id", id},
                {"visitas",profile.Visitas},
                {"app", "testeBot"}
            };

            if (profile.Visitas > 100)
            {
                col.UpdateOne(filter,doc);
            }
            else
            {
                col.InsertOne(doc);
            }

        }
    }
}