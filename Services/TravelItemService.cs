using System.Collections.Generic;
using MongoDB.Driver;
using TravelPacker.Models;

namespace TravelPacker.Services
{
    public class TravelItemService
    {
        private readonly IMongoCollection<TravelItem> _items;

        public TravelItemService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _items = database.GetCollection<TravelItem>("TravelItems");
        }

        public TravelItem Create(TravelItem item)
        {
            _items.InsertOne(item);
            return item;
        }

        public IList<TravelItem> Read() =>
            _items.Find(i => true).ToList();

        public TravelItem Find(string id) =>
            _items.Find(i => i.Id == id).SingleOrDefault();

        public void Update(TravelItem item) =>
            _items.ReplaceOne(i => i.Id == item.Id, item);

        public void Delete(string id) =>
            _items.DeleteOne(i => i.Id == id);
    }
}