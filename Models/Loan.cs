namespace Lab2.Models;

public class Loan{
    public int BookId { get; set; }
    public Book Book { get; set; }
    
    public int CardId { get; set; }
    public LibraryCard Card { get; set; }

    public DateOnly CheckoutDate { get; set; }
    public DateOnly ReturnByDate { get; set; }
}