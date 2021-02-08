using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelPacker.Models
{
    public class CreateItemsViewModel
    {
        public string Id { get; set; }
        public string ItemTitle { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public TravelItem TravelItem { get; set; }
        public string Title { get; set; }
        public string ListTypeId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsCompleted { get; set; }
        public IEnumerable<SelectListItem> SelectListType { get; set; }
    }
}