using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelPacker.Models
{
    public class TravelItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ItemTitle { get; set; }
        public string ListTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public SelectList ListTypes { get; set; }
    }
}