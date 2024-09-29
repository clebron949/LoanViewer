namespace LoanViewer.Api.Models;

public class LoanTerms
{
    public double Amount { get; set; }
    public double Rate { get; set; }
    public int Duration { get; set; }
    public TermType Term { get; set; }

    public enum TermType
    {
        Monthly,
        Yearly
    }
}
