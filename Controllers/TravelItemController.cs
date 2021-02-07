using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelPacker.Models;
using TravelPacker.Services;

namespace TravelPacker.Controllers
{
    [Authorize]
    public class TravelItemController : Controller
    {
        private readonly TravelItemService _itemSvc;
        private readonly ListTypeService _listSvc;

        public TravelItemController(TravelItemService itemService, ListTypeService listService)
        {
            _itemSvc = itemService;
            _listSvc = listService;
        }

        [AllowAnonymous]
        public ActionResult<IList<TravelItem>> Index() => View(_itemSvc.Read());

        [HttpGet]
        public ActionResult Create()
        {
            CreateItemsViewModel model = new CreateItemsViewModel();
            model.SelectListType = _listSvc.Read().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Title
            });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<TravelItem> Create(CreateItemsViewModel vm)
        {
            TravelItem item = new TravelItem();
            item.CreatedDate = DateTime.Now;
            item.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            item.UserName = User.Identity.Name;
            item.ListTypeId = vm.ListTypeId;
            item.ItemTitle = vm.ItemTitle;

            if (ModelState.IsValid)
            {
                _itemSvc.Create(item);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<CreateItemsViewModel> Edit(string id)
        {
            CreateItemsViewModel model = new CreateItemsViewModel();
            var item = _itemSvc.Find(id);
            model.ItemTitle = item.ItemTitle;
            model.UserId = item.UserId;
            model.UserName = item.UserName;
            model.Id = item.Id;
            model.SelectListType = _listSvc.Read().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Title,
                Selected = (x.Id == item.ListTypeId)
            });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateItemsViewModel vm)
        {
            TravelItem item = new TravelItem();
            item.ListTypeId = vm.ListTypeId;
            item.ItemTitle = vm.ItemTitle;
            item.UserId = vm.UserId;
            item.UserName = vm.UserName;
            item.Id = vm.Id;
            if (ModelState.IsValid)
            {
                if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value != item.UserId)
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