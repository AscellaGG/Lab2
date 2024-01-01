using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;
using Microsoft.CodeAnalysis.Operations;

namespace Lab2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly LibraryContext _context;

    public BooksController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBook()
    {
        return await _context.Books
            .Select(x => BookToDTO(x)) 
            .ToListAsync();
    }

    // GET: api/Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDTO>> GetBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return BookToDTO(book);
    }

    // PUT: api/Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, BookDTO bookDTO)
    {
        if (id != bookDTO.Id)
        {
            return BadRequest();
        }

        var book = await _context.Books.FindAsync(id);

        if(book == null)
        {
            return NotFound();
        }

        book.Title = bookDTO.Title;
        book.ISBN = bookDTO.ISBN;
        book.YearPublished = bookDTO.YearPublished;
        book.Score = bookDTO.Score;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!BookExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<BookDTO>> PostBook(BookDTO bookDTO)
    {
        var book = new Book
        {
            Title = bookDTO.Title,
            ISBN = bookDTO.ISBN,
            YearPublished = bookDTO.YearPublished,
            Score = bookDTO.Score
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetBook),
            new { id = book.BookId },
            BookToDTO(book));
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.BookId == id);
    }

    private static BookDTO BookToDTO(Book book) =>
        new BookDTO
        {
            Id = book.BookId,
            Title = book.Title,
            ISBN = book.ISBN,
            YearPublished = book.YearPublished,
            Score = book.Score
        };
}
