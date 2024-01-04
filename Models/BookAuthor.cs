namespace Lab2.Models;

public class BookAuthor
{
    public int Id { get; set; }
    public required Book Book { get; set; }
    public required Author Author { get; set; }
}
