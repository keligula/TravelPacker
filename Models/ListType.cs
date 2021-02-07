using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelPacker.Models
{
    public class ListType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }

        public string Title { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Active { get; set; }
    }
}