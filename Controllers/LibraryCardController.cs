using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class LibraryCardController : Controller
    {
        private readonly LibraryContext _context;

        public LibraryCardController(LibraryContext context)
        {
            _context = context;
        }

        // GET: LibraryCard
        public async Task<IActionResult> Index()
        {
            return View(await _context.LibraryCard.ToListAsync());
        }

        // GET: LibraryCard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryCard = await _context.LibraryCard
                .FirstOrDefaultAsync(m => m.LibraryCardId == id);
            if (libraryCard == null)
            {
                return NotFound();
            }

            return View(libraryCard);
        }

        // GET: LibraryCard/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LibraryCard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibraryCardId,FirstName,LastName")] LibraryCard libraryCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libraryCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libraryCard);
        }

        // GET: LibraryCard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryCard = await _context.LibraryCard.FindAsync(id);
            if (libraryCard == null)
            {
                return NotFound();
            }
            return View(libraryCard);
        }

        // POST: LibraryCard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibraryCardId,FirstName,LastName")] LibraryCard libraryCard)
        {
            if (id != libraryCard.LibraryCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libraryCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryCardExists(libraryCard.LibraryCardId))
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
            return View(libraryCard);
        }

        // GET: LibraryCard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryCard = await _context.LibraryCard
                .FirstOrDefaultAsync(m => m.LibraryCardId == id);
            if (libraryCard == null)
            {
                return NotFound();
            }

            return View(libraryCard);
        }

        // POST: LibraryCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryCard = await _context.LibraryCard.FindAsync(id);
            if (libraryCard != null)
            {
                _context.LibraryCard.Remove(libraryCard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryCardExists(int id)
        {
            return _context.LibraryCard.Any(e => e.LibraryCardId == id);
        }
    }
}
