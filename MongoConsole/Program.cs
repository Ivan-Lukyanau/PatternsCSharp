using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoConsole.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Console;

namespace MongoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            IMongoDatabase db = client.GetDatabase("IDG");
            Author author = new Author
            {
                Id = 1,
                FirstName = "Joydip",
                LastName = "Kanjilal"
            };
            var collection = db.GetCollection<Author>("authors");
            try
            {
                collection.InsertOne(author);
            }
            catch (Exception e)
            {
                WriteLine(e);
            }

            using (var cursor = client.ListDatabases())
            {
                var databaseDocuments = cursor.ToList();
                foreach (var dbItem in databaseDocuments)
                {
                    Console.WriteLine(dbItem["name"].ToString());
                }
            }

           
            ReadKey();
        }

        private static async Task DisplayDatabaseNames()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            try
            {
                using (var cursor = await client.ListDatabasesAsync())
                {
                    await cursor.ForEachAsync(document => Console.WriteLine(document.ToString()));
                }
            }
            catch
            {
                //Write your own code here to handle exceptions
            }
        }

        private static void InsertManyExample(IMongoDatabase db)
        {
            var collection = db.GetCollection<BsonDocument>("IDGAuthors");
            var author1 = new BsonDocument
            {
                {"id", 1},
                {"firstname", "Joydip"},
                {"lastname", "Kanjilal"}
            };
            var author2 = new BsonDocument
            {
                {"id", 2},
                {"firstname", "Steve"},
                {"lastname", "Smith"}
            };
            var author3 = new BsonDocument
            {
                {"id", 3},
                {"firstname", "Gary"},
                {"lastname", "Stevens"}
            };
            var authors = new List<BsonDocument>();
            authors.Add(author1);
            authors.Add(author2);
            authors.Add(author3);
            collection.InsertMany(authors);
        }
    }
}
