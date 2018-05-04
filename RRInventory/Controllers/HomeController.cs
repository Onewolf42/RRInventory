using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRInventory.Models;
using RRInventory.Models.InventoryViewModels;
using RRInventory.Data;

namespace RRInventory.Controllers
{
    public class HomeController : Controller
    {
        private readonly InventoryContext _context;

        public HomeController(InventoryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            IQueryable<ItemTypeGroup> data =
            from item in _context.Items
            group item by item.Type into itemTypeGroup
            select new ItemTypeGroup()
            {
                ItemType = itemTypeGroup.Key,
                ItemCount = itemTypeGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
