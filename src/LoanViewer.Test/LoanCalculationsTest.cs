using LoanViewer.Api.Services;

namespace LoanViewer.Test;

public class LoanCalculationsTest
{
    private LoanCalculationService _loanService;

    [SetUp]
    public void Setup()
    {
        _loanService = new LoanCalculationService();
    }

    [Test]
    [TestCase(1000, 1.0, 1, 84.17)]
    public void GetMonthlyPaymentsTest(
        double amount,
        double rate,
        int terms,
        double expectedMonthlyPayment
    )
    {
        _loanService.SetLoanDetails(amount, rate, terms);
        var monthlyPayment = Convert.ToDouble(_loanService.MonthlyPayment!.Replace("$", ""));
        Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment).Within(2));
    }

    [Test]
    [TestCase(1000, 1.0, 1, 1010)]
    public void GetYearlyPaymentsTest(
        double amount,
        double rate,
        int terms,
        double expectedYearlyPayment
    )
    {
        _loanService.SetLoanDetails(amount, rate, terms);
        var yearlyPayment = Convert.ToDouble(_loanService.YearlyPayment!.Replace("$", ""));
        Assert.That(yearlyPayment, Is.EqualTo(expectedYearlyPayment).Within(2));
    }
}
