namespace Lab2.Models;

public class Loan{
    public int LoanId { get; set; }

    public required Book Book { get; set; }
    public required LibraryCard Card { get; set; }

    public DateTime CheckoutDate { get; set; }
    public DateTime ReturnDate { get; set; }
}