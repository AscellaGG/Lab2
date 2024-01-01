using Microsoft.EntityFrameworkCore;
using Lab2.Models;

namespace Lab2.Models;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

public DbSet<Lab2.Models.Author> Author { get; set; } = default!;

public DbSet<Lab2.Models.Book> Book { get; set; } = default!;

public DbSet<Lab2.Models.LibraryCard> LibraryCard { get; set; } = default!;

public DbSet<Lab2.Models.BookDTO> BookDTO { get; set; } = default!;
    
    
}