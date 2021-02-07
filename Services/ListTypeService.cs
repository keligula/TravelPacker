using System.Collections.Generic;
using MongoDB.Driver;
using TravelPacker.Models;

namespace TravelPacker.Services
{
    public class ListTypeService
    {
        private readonly IMongoCollection<ListType> _listTypes;

        public ListTypeService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _listTypes = database.GetCollection<ListType>("ListTypes");
        }

        public ListType Create(ListType listType)
        {
            _listTypes.InsertOne(listType);
            return listType;
        }

        public IList<ListType> Read() => _listTypes.Find(i => true).ToList();

        public ListType Find(string id) => _listTypes.Find(i => i.Id == id).SingleOrDefault();

        public void Update(ListType item) => _listTypes.ReplaceOne(i => i.Id == item.Id, item);

        public void Delete(string id) => _listTypes.DeleteOne(i => i.Id == id);
    }
}