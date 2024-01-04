using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;

namespace Lab2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly LibraryContext _context;

    public AuthorsController(LibraryContext context)
    {
        _context = context;
    }

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthor()
    {
        return await _context.Authors.;
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return AuthorToDTO(author);
    }

    // PUT: api/Authors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, AuthorDTO authorDTO)
    {
        if (id != authorDTO.Id)
        {
            return BadRequest();
        }

        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        author.FirstName = authorDTO.FirstName;
        author.LastName = authorDTO.LastName;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!AuthorExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Authors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Author>> PostAuthor(AuthorDTO authorDTO)
    {
        var author = new Author
        {
            FirstName = authorDTO.FirstName,
            LastName = authorDTO.LastName
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetAuthor),
            new { id = author.Id },
            AuthorToDTO(author));
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AuthorExists(int id)
    {
        return _context.Authors.Any(e => e.Id == id);
    }

    private static AuthorDTO AuthorToDTO(Author author) =>
        new AuthorDTO
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName
        };
}
