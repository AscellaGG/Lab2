using Microsoft.EntityFrameworkCore;

namespace Lab2.Models;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }
    
    
}