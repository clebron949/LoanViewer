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
    [TestCase(50_000, 5.0, 10, 539)]
    public void GetMonthlyPaymentTest(
        double amount,
        double rate,
        int terms,
        double expectedMonthlyPayment
    )
    {
        _loanService.SetLoanDetails(amount, rate, terms);
        var monthlyPayment = _loanService.AmortizationTable.MonthlyPayment;
        Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment).Within(1));
    }

    [Test]
    [TestCase(1000, 1.0, 1, 1010)]
    [TestCase(50_000, 5.0, 10, 6_475)]
    public void GetYearlyPaymentTest(
        double amount,
        double rate,
        int terms,
        double expectedYearlyPayment
    )
    {
        _loanService.SetLoanDetails(amount, rate, terms);
        var yearlyPayment = _loanService.AmortizationTable.YearlyPayment;
        Assert.That(expectedYearlyPayment, Is.EqualTo(yearlyPayment).Within(1));
    }

     [Test]
     [TestCase(50_000, 5.0, 10, 14_752)]
    public void GetTotalInterestPaidTest(
        double amount,
        double rate,
        int terms,
        double expectedTotalInterestPaid
    )
    {
        _loanService.SetLoanDetails(amount, rate, terms);
        var totalInterestPaid = _loanService.AmortizationTable.TotalInterest;
        Assert.That(expectedTotalInterestPaid, Is.EqualTo(totalInterestPaid).Within(1));
    }

    [Test]
    [TestCase(1000, 1.0, 1, 1, 1_000)]
    [TestCase(1000, 1.0, 10, 5, 611.00)]
    [TestCase(50_000, 5.0, 10, 6, 28_034)]
    public void GetBeginningBalanceByTerm(
        double amount,
        double rate,
        int terms,
        int desiredTerm,
        double expectedBeginningBalance
    )
    {
        _loanService.SetLoanDetails(amount, rate, terms);
        var beginningBalanceByTerm = _loanService
            .AmortizationTable
            .AmortizationTableItems[desiredTerm - 1]
            .BeginningBalance;
        Assert.That(expectedBeginningBalance, Is.EqualTo(beginningBalanceByTerm).Within(1));
    }

    [Test]
    [TestCase(1000, 1.0, 1, 1, 10)]
    [TestCase(1000, 1.0, 10, 5, 6.12)]
    [TestCase(50_000, 5.0, 10, 6, 1_401)]
    public void GetInterestByTermTest(
        double amount,
        double rate,
        int terms,
        int desiredTerm,
        double expectedInterest
    )
    {
        _loanService.SetLoanDetails(amount, rate, terms);
        var interestByTerm = _loanService.AmortizationTable.AmortizationTableItems[desiredTerm - 1].InterestPaid;
        Assert.That(expectedInterest, Is.EqualTo(interestByTerm).Within(1));
    }

    [Test]
    [TestCase(50_000, 5.0, 10, 6, 5_073)]
    public void GetPrincipalPaidByTerm(
        double amount,
        double rate,
        int terms,
        int desiredTerm,
        double expectedPrincipal
    )
    {
        _loanService.SetLoanDetails(amount, rate, terms);
        var principalPaid = _loanService
            .AmortizationTable
            .AmortizationTableItems[desiredTerm - 1]
            .PrincipalPaid;
        Assert.That(expectedPrincipal, Is.EqualTo(principalPaid).Within(1));
    }

    [Test]
    [TestCase(50_000, 5.0, 10, 6, 22_960)]
    public void GetRemainingPrincipalByTerm(
        double amount,
        double rate,
        int terms,
        int desiredTerm,
        double expectedRemainingPrincipal
    )
    {
        _loanService.SetLoanDetails(amount, rate, terms);
        var remainingPrincipal = _loanService
            .AmortizationTable
            .AmortizationTableItems[desiredTerm - 1]
            .RemainingPrincipal;
        Assert.That(expectedRemainingPrincipal, Is.EqualTo(remainingPrincipal).Within(1));
    }
}
