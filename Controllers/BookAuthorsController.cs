using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BookAuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/BookAuthors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookAuthor>>> GetBookAuthor()
        {
            return await _context.BookAuthors
                .Include(ba => ba.Author)
                .Include(ba => ba.Book)
                .ToListAsync();
        }

        // GET: api/BookAuthors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookAuthor>> GetBookAuthor(int id)
        {
            var bookAuthors = await _context.BookAuthors.FindAsync(id);

            if (bookAuthors == null)
            {
                return NotFound();
            }

            return bookAuthors;
        }

        // PUT: api/BookAuthors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookAuthor(int id, BookAuthor bookAuthors)
        {
            if (id != bookAuthors.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookAuthors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookAuthorsExists(id))
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

        // POST: api/BookAuthors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("New")]
        public async Task<ActionResult<BookAuthor>> PostBookAuthor(BookAuthorDTO bookAuthorDTO)
        {
            Book? book = await _context.Books.FindAsync(bookAuthorDTO.BookId);
            Author author = await _context.Authors.FindAsync(bookAuthorDTO.AuthorId);

            if (book == null)
            {
                return NotFound(bookAuthorDTO.BookId);
            }
            if (author == null)
            {
                return NotFound(bookAuthorDTO.AuthorId);
            }

            var bookAuthor = new BookAuthor
            {
                Book = book,
                Author = author,
            };

            _context.BookAuthors.Add(bookAuthor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBookAuthor),
                new { id = bookAuthor.Id },
                bookAuthor);
        }

        // DELETE: api/BookAuthors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAuthor(int id)
        {
            var bookAuthors = await _context.BookAuthors.FindAsync(id);
            if (bookAuthors == null)
            {
                return NotFound();
            }

            _context.BookAuthors.Remove(bookAuthors);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookAuthorsExists(int id)
        {
            return _context.BookAuthors.Any(e => e.Id == id);
        }
    }
}
