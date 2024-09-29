using LoanViewer.Api.Models;

namespace LoanViewer.Api.Services;

public class LoanCalculationService
{
    public double Amount { get; private set; }
    public double Rate { get; private set; }
    public int Terms { get; private set; }

    public AmortizationTable AmortizationTable => GetAmortizationTable();
    public string? MonthlyPayment => "$" + GetMonthlyPayment().ToString("N");
    public string? YearlyPayment => "$" + GetYearlyPayment().ToString("N");
    public string? TotalInterest =>
        "$" + AmortizationTable.AmortizationTableItems.Select(x => x.InterestPaid).Sum().ToString("N");

    public void SetLoanDetails(
        double amount,
        double rate,
        int terms,
        LoanTerms.TermType term = LoanTerms.TermType.Yearly
    )
    {
        var loanTermns = new LoanTerms
        {
            Amount = amount,
            Rate = rate,
            Duration = terms,
            Term = term
        };
        SetLoanDetails(loanTermns);
    }

    public void SetLoanDetails(LoanTerms loanTerms)
    {
        var amount = loanTerms.Amount;
        var rate = loanTerms.Rate;
        var terms = loanTerms.Duration;

        Amount = amount;
        Terms = loanTerms.Term == LoanTerms.TermType.Yearly ? terms : terms / 12;
        Rate = rate < 1 ? rate : rate / 100;
    }

    private double GetYearlyPayment() =>
        Amount * ((Rate * Math.Pow(1 + Rate, Terms)) / (Math.Pow(1 + Rate, Terms) - 1));

    private double GetMonthlyPayment() => GetYearlyPayment() / 12;

    private AmortizationTable GetAmortizationTable()
    {
        var amortizationTable = new AmortizationTable
        {
            MonthlyPayment = GetMonthlyPayment(),
            YearlyPayment = GetYearlyPayment()
        };
        var beginningBalance = Amount;
        var totalInterestPaid = 0.00;
        var yearlyPayment = GetYearlyPayment();
        for (int i = 0; i < Terms; i++)
        {
            var interestPaid = beginningBalance * Rate;
            totalInterestPaid += interestPaid;
            var principalPaid = yearlyPayment - interestPaid;
            var remainingPrincipal = beginningBalance - principalPaid;
            remainingPrincipal = (remainingPrincipal < 1) ? 0 : remainingPrincipal;
            amortizationTable.AmortizationTableItems.Add(
                new AmortizationTableItem(
                    Term: i + 1,
                    PrincipalPaid: principalPaid,
                    InterestPaid: interestPaid,
                    RemainingPrincipal: remainingPrincipal,
                    BeginningBalance: beginningBalance,
                    EndingBalance: remainingPrincipal
                )
            );
            beginningBalance = remainingPrincipal;
        }
        return amortizationTable;
    }
}
