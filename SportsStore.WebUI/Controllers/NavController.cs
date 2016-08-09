﻿using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            if (category == "List" || category == null) category = "Home";
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Products
                                        .Select(x => x.Category)
                                        .Distinct()
                                        .OrderBy(x => x);

            return PartialView("FlexMenu", categories);
        }
    }
}