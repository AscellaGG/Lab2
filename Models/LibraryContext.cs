using Microsoft.EntityFrameworkCore;
using Lab2.Models;
using Lab2.Models.DTOs;

namespace Lab2.Models;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

public DbSet<Author> Authors { get; set; } = default!;

public DbSet<Book> Books { get; set; } = default!;

public DbSet<BookAuthor> BookAuthors { get; set; }

public DbSet<LibraryCard> LibraryCards { get; set; } = default!;

public DbSet<Loan> Loans { get; set; }
}