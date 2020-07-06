using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace mongodb
{
    public class FruitEntity 
    {
	public ObjectId  Id { get; set; }
        public string    name  { get; set; }
        public int       price { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
	    var connString = "mongodb://127.0.0.1:27017";
	    MongoClient client = new MongoClient(connString);

	    //List all the MongoDB databases
	    var allDatabases = client.ListDatabases().ToList();
//	    Console.WriteLine("MongoDB db array type: " + allDatabases.GetType());
//          Console.WriteLine("MongoDB databases:");
//          Console.WriteLine(string.Join(", ", allDatabases));

	    IMongoDatabase db = client.GetDatabase("mydb");
	    IMongoCollection<FruitEntity> collection = db.GetCollection<FruitEntity>("Fruit");

	    if (args[0] == "query")
    	    {
	      	// lambda-shiki de true wo mitasu recode wo list ni ireru (tumari zenbu).
	        var list = collection.Find(a=>true).ToList();
	        foreach (var tmp in list)
                {
	            Console.WriteLine(tmp.Id + "," + tmp.name + "," + tmp.price);
	        }
	    }

	    else if (args[0] == "create")
	    {
		    var doc = new FruitEntity
		    {
	         	    name = "apple",
	         	    price = 100
	            };
		    collection.InsertOne(doc);
		    Console.WriteLine(doc.Id);
	    }
	    
	    else if (args[0] == "update")
	    {
		    var filter = Builders<FruitEntity>.Filter.Eq("name","apple");
		    var update = Builders<FruitEntity>.Update.Set("price",200);

		    collection.UpdateOne(filter,update);
	    }

	    else if (args[0] == "update_from_json")
	    {
	   	    string[] lines = System.IO.File.ReadAllLines(@"update.json"); 
		    foreach (string line in lines)
		    {
		    	var doc = BsonSerializer.Deserialize<FruitEntity>(line);

		        var filter = Builders<FruitEntity>.Filter.Eq(s => s.name, doc.name);
		        var update = Builders<FruitEntity>.Update.Set(s => s.price, doc.price);

		        collection.UpdateOne(filter,update);
		    }
	    }

	    else if (args[0] == "import")
	    {
	   	    string[] lines = System.IO.File.ReadAllLines(@"import.json"); 

		    foreach (string line in lines)
		    {
		    	var doc = BsonSerializer.Deserialize<FruitEntity>(line);
		    	collection.InsertOne(doc);
		    }
	    }

	    else if (args[0] == "delete")
	    {
		    collection.DeleteOne(a=>true);
	    }

	    else if (args[0] == "deleteall")
	    {
		    collection.DeleteMany(a=>true);
	    }
        }
    }
}
