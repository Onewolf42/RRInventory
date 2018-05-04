using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RRInventory.Data;
using RRInventory.Models;

namespace RRInventory.Controllers
{
    public class ItemsController : Controller
    {
        private readonly InventoryContext _context;

        public ItemsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var inventoryContext = _context.Items.Include(i => i.Account).Include(i => i.Image);
            return View(await inventoryContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Account)
                .Include(i => i.Image)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID");
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "ID");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountID,Type,Description,Railroad,Manufacturer,ModelNumber,ImageID")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.ID = Guid.NewGuid();
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID", item.AccountID);
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "ID", item.ImageID);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.SingleOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID", item.AccountID);
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "ID", item.ImageID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,AccountID,Type,Description,Railroad,Manufacturer,ModelNumber,ImageID")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID", item.AccountID);
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "ID", item.ImageID);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Account)
                .Include(i => i.Image)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var item = await _context.Items.SingleOrDefaultAsync(m => m.ID == id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.ID == id);
        }
    }
}
