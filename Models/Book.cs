namespace Lab2.Models;

public class Book{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int YearPublished { get; set; }
    public float Score { get; set; }

    public List<Author> Authors { get; set; }
}