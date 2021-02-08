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
    public class ListTypeController : Controller
    {
        private readonly ListTypeService _listService;

        public ListTypeController(ListTypeService listService)
        {
            _listService = listService;
        }

        [AllowAnonymous]
        public ActionResult<IList<ListType>> Index() => View(_listService.Read());

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<ListType> Create(ListType listType)
        {
            listType.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            listType.DateCreated = DateTime.Now;
            listType.Active = true;
            if (ModelState.IsValid)
                _listService.Create(listType);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<ListType> Edit(string id) => View(_listService.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ListType listType)
        {
            //item.LastUpdated = DateTime.Now;
            //item.Created = item.Created.ToLocalTime();
            if (ModelState.IsValid)
            {
                if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value != listType.UserId)
                {
                    return Unauthorized();
                }
                _listService.Update(listType);
                return RedirectToAction("Index");
            }
            return View(listType);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _listService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}