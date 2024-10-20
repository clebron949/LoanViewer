<template>
  <LoanForm @submit-loan-form="onSubmitLoanForm" />
  <div class="bg-light">
    <div class="container">
      <div v-if="data">
        <LoanDetails :loan="data" />
      </div>
      <div v-else class="min-vh-100">
        <h2 class="text-center py-5">Enter Loan Details to Calculate Loan</h2>
      </div>
    </div>
  </div>
  <footer class="border-top footer text-muted">
    <div class="container text-center py-2">
      <strong>&copy; {{ new Date().getFullYear() }} - </strong>
      <i><strong>cjlebron.com</strong></i>
    </div>
  </footer>
</template>

<script setup lang="ts">
import { ref } from "vue";
import LoanForm from "./components/LoanForm.vue";
import type { LoanModel, LoanFormModel } from "./Models/LoanModel";
import LoanDetails from "./components/LoanDetails.vue";
import { LoanCalculationService } from "./services/LoanCalculationService";

const data = ref<LoanModel | undefined>(undefined);

function onSubmitLoanForm(loanModel: LoanFormModel) {
  data.value = fetchLoanData(loanModel);
}

function fetchLoanData(loan: LoanFormModel): LoanModel {
  const loanService = new LoanCalculationService(loan);
  return loanService.getAmortizationTable();
}

const LoanData: LoanModel = {
  monthlyPayment: 539.6023956894026,
  yearlyPayment: 6475.228748272831,
  totalInterest: 14752.28748272836,
  totalPrincipal: 40000,
  amortizationTableItems: [
    {
      term: 1,
      beginningBalance: 50000,
      interestPaid: 2500,
      principalPaid: 3975.2287482728307,
      remainingPrincipal: 46024.77125172717,
    },
    {
      term: 2,
      beginningBalance: 46024.77125172717,
      interestPaid: 2301.2385625863585,
      principalPaid: 4173.990185686473,
      remainingPrincipal: 41850.7810660407,
    },
    {
      term: 3,
      beginningBalance: 41850.7810660407,
      interestPaid: 2092.539053302035,
      principalPaid: 4382.689694970795,
      remainingPrincipal: 37468.0913710699,
    },
    {
      term: 4,
      beginningBalance: 37468.0913710699,
      interestPaid: 1873.4045685534952,
      principalPaid: 4601.824179719335,
      remainingPrincipal: 32866.26719135057,
    },
    {
      term: 5,
      beginningBalance: 32866.26719135057,
      interestPaid: 1643.3133595675286,
      principalPaid: 4831.915388705302,
      remainingPrincipal: 28034.351802645266,
    },
    {
      term: 6,
      beginningBalance: 28034.351802645266,
      interestPaid: 1401.7175901322635,
      principalPaid: 5073.511158140567,
      remainingPrincipal: 22960.8406445047,
    },
    {
      term: 7,
      beginningBalance: 22960.8406445047,
      interestPaid: 1148.042032225235,
      principalPaid: 5327.186716047596,
      remainingPrincipal: 17633.653928457104,
    },
    {
      term: 8,
      beginningBalance: 17633.653928457104,
      interestPaid: 881.6826964228553,
      principalPaid: 5593.546051849975,
      remainingPrincipal: 12040.10787660713,
    },
    {
      term: 9,
      beginningBalance: 12040.10787660713,
      interestPaid: 602.0053938303565,
      principalPaid: 5873.223354442474,
      remainingPrincipal: 6166.884522164655,
    },
    {
      term: 10,
      beginningBalance: 6166.884522164655,
      interestPaid: 308.34422610823276,
      principalPaid: 6166.884522164598,
      remainingPrincipal: 0,
    },
  ],
};
</script>
