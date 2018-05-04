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
    public class AccountsController : Controller
    {
        private readonly InventoryContext _context;

        public AccountsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index( string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var accounts = from s in _context.Accounts
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(s => s.Name.Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    accounts = accounts.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    accounts = accounts.OrderBy(s => s.SubscriptionEndDate);
                    break;
                case "date_desc":
                    accounts = accounts.OrderByDescending(s => s.SubscriptionEndDate);
                    break;
                default:
                    accounts = accounts.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Account>.CreateAsync(accounts.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                 .Include(s => s.Items)
                 .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address1,Address2,City,State,Zipcode,SubscriptionEndDate")] Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    account.ID = Guid.NewGuid();
                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.SingleOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Address1,Address2,City,State,Zipcode,SubscriptionEndDate")] Account account)
        {
            if (id != account.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException ex )
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. (" + ex.Message + 
                        ") Try again, and if the problem persists, " +
                        "see your system administrator.");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var account = await _context.Accounts
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (account == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool AccountExists(Guid id)
        {
            return _context.Accounts.Any(e => e.ID == id);
        }
    }
}
