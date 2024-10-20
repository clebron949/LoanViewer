import {
  AmountType,
  TermType,
  type LoanFormModel,
  type LoanModel,
} from "@/Models/LoanModel";

export class LoanCalculationService {
  amount: number;
  rate: number;
  terms: number;

  constructor(model: LoanFormModel) {
    if (model.interestRate > 1) {
      model.interestRate /= 100;
    }

    this.amount = model.amount;
    this.rate = model.interestRate;
    this.terms = model.term;

    if (
      model.options.amountType === AmountType.Monthly &&
      model.options.termType === TermType.Months
    ) {
      this.amount = model.amount * this.terms;
      this.rate /= 12;
    } else if (
      model.options.amountType === AmountType.Monthly &&
      model.options.termType === TermType.Years
    ) {
      this.amount = model.amount * this.terms * 12;
    } else if (
      model.options.amountType === AmountType.Full &&
      model.options.termType === TermType.Months
    ) {
      this.rate /= 12;
    }

    console.log("Loan Service", model);
  }

  get amortizationTable(): LoanModel {
    return this.getAmortizationTable();
  }

  public getMonthlyPayment(): number {
    return this.getYearlyPayment() / 12;
  }

  public getYearlyPayment(): number {
    return (
      this.amount *
      ((this.rate * Math.pow(1 + this.rate, this.terms)) /
        (Math.pow(1 + this.rate, this.terms) - 1))
    );
  }

  public getAmortizationTable(): LoanModel {
    const amortizationTable: LoanModel = {
      monthlyPayment: this.getMonthlyPayment(),
      yearlyPayment: this.getYearlyPayment(),
      totalInterest: 0.0,
      totalPrincipal: 0.0,
      amortizationTableItems: [],
    };

    let beginningBalance = this.amount;
    let totalInterestPaid = 0.0;
    let totalPrincipalPaid = 0.0;
    const yearlyPayment = this.getYearlyPayment();

    for (let i = 0; i < this.terms; i++) {
      const interestPaid = beginningBalance * this.rate;
      totalInterestPaid += interestPaid;
      const principalPaid = yearlyPayment - interestPaid;
      totalPrincipalPaid += principalPaid;
      let remainingPrincipal = beginningBalance - principalPaid;
      remainingPrincipal = remainingPrincipal < 1 ? 0 : remainingPrincipal;

      amortizationTable.amortizationTableItems.push({
        term: i + 1,
        beginningBalance: beginningBalance,
        principalPaid: principalPaid,
        interestPaid: interestPaid,
        remainingPrincipal: remainingPrincipal,
      });

      beginningBalance = remainingPrincipal;
    }

    amortizationTable.totalInterest = totalInterestPaid;
    amortizationTable.totalPrincipal = totalPrincipalPaid;

    return amortizationTable;
  }
}
