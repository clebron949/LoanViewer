namespace LoanViewer.Api.Models;

public class AmortizationTable
{
    public double MonthlyPayment { get; set; }
    public double YearlyPayment { get; set; }
    public double TotalInterest => AmortizationTableItems.Select(x => x.InterestPaid).Sum();
    public List<AmortizationTableItem> AmortizationTableItems { get; set; } = [];
}

public record AmortizationTableItem(
    int Term,
    double BeginningBalance,
    double InterestPaid,
    double PrincipalPaid,
    double RemainingPrincipal
);
