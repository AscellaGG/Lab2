namespace Lab2.Models.DTOs;

public class BookDTO
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int YearPublished { get; set; }
    public float Score { get; set; }
}