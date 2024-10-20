export interface LoanModel {
  monthlyPayment: number;
  yearlyPayment: number;
  totalInterest: number;
  totalPrincipal: number;
  amortizationTableItems: AmortizationTableItem[];
}

export interface AmortizationTableItem {
  term: number;
  beginningBalance: number;
  interestPaid: number;
  principalPaid: number;
  remainingPrincipal: number;
}

export interface LoanFormModel {
  amount: number;
  interestRate: number;
  term: number;
  options: LoanOptions;
}

export interface LoanOptions {
  amountType: AmountType;
  termType: TermType;
}

export enum AmountType {
  Full = "Full",
  Monthly = "Monthly",
}

export enum TermType {
  Years = "Years",
  Months = "Months",
}
