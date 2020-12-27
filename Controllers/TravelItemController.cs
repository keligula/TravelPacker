using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
//using Microsoft.AspNetCore.Razor.Language;
using TravelPacker.Models;
using TravelPacker.Services;

namespace TravelPacker.Controllers
{
    [Authorize]
    public class TravelItemController : Controller
    {
        private readonly TravelItemService _itemSvc;

        public TravelItemController(TravelItemService itemService)
        {
            _itemSvc = itemService;
        }

        [AllowAnonymous]
        public ActionResult<IList<TravelItem>> Index() => View(_itemSvc.Read());

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<TravelItem> Create(TravelItem item)
        {
            item.CreatedDate = DateTime.Now;
            item.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            item.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _itemSvc.Create(item);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Submission> Edit(string id) =>
            View(_itemSvc.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TravelItem item)
        {
            //item.LastUpdated = DateTime.Now;
            //item.Created = item.Created.ToLocalTime();
            if (ModelState.IsValid)
            {
                if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value != item.Id)
                {
                    return Unauthorized();
                }
                _itemSvc.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _itemSvc.Delete(id);
            return RedirectToAction("Index");
        }
    }
}