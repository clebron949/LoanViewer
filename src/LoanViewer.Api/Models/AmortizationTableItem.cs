namespace LoanViewer.Api.Models;

public class AmortizationTableItem
{
    public int PaymentNumber { get; set; }
    public double PaymentAmount { get; set; }
    public double InterestPaid { get; set; }
    public double PrincipalPaid { get; set; }
    public double RemainingBalance { get; set; }
}
