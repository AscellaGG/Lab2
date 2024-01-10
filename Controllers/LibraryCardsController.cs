using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;
using Lab2.Models.DTOs;

namespace Lab2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryCardsController : ControllerBase
{
    private readonly LibraryContext _context;

    public LibraryCardsController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/LibraryCards
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LibraryCard>>> GetLibraryCards()
    {
        return await _context.LibraryCards
            .Include(c => c.Loans)
            .ToListAsync();
    }

    // GET: api/LibraryCards/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LibraryCard>> GetLibraryCard(int id)
    {
        var libraryCard = await _context.LibraryCards.FindAsync(id);

        if (libraryCard == null)
        {
            return NotFound();
        }

        return libraryCard;
    }

    // PUT: api/LibraryCards/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLibraryCard(int id, LibraryCard libraryCard)
    {
        if (id != libraryCard.LibraryCardId)
        {
            return BadRequest();
        }

        _context.Entry(libraryCard).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LibraryCardExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/LibraryCards
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<LibraryCard>> PostLibraryCard(LibraryCardDTO libraryCardDTO)
    {
        var libraryCard = LibraryCardToDTO(libraryCardDTO);
        _context.LibraryCards.Add(libraryCard);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLibraryCard", new { id = libraryCard.LibraryCardId }, libraryCard);
    }

    // DELETE: api/LibraryCards/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLibraryCard(int id)
    {
        var libraryCard = await _context.LibraryCards.FindAsync(id);
        if (libraryCard == null)
        {
            return NotFound();
        }

        _context.LibraryCards.Remove(libraryCard);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LibraryCardExists(int id)
    {
        return _context.LibraryCards.Any(e => e.LibraryCardId == id);
    }

    public static LibraryCard LibraryCardToDTO(LibraryCardDTO libraryCardDTO)
    {
        return new LibraryCard
        {
            FirstName = libraryCardDTO.FirstName,
            LastName = libraryCardDTO.LastName
        };
    }
}
