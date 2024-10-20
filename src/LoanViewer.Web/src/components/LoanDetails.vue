<script setup lang="ts">
import type { LoanModel } from "@/Models/LoanModel";
import StatCard from "./StatCard.vue";
import LoanTable from "./LoanTable.vue";
import { formatCurrencyString } from "@/utils/formatCurrencyString";

defineProps<{
  loan: LoanModel;
}>();
</script>

<template>
  <h2 class="py-4 text-center">Loan Details</h2>

  <div class="row justify-content-center gy-4">
    <div class="col-6 col-lg-3">
      <StatCard
        statName="Monthly Payment"
        :statValue="formatCurrencyString(loan.monthlyPayment)"
      />
    </div>

    <div class="col-6 col-lg-3">
      <StatCard
        statName="Yearly Payment"
        :statValue="formatCurrencyString(loan.yearlyPayment)"
      />
    </div>
    <div class="col-6 col-lg-3">
      <StatCard
        statName="Total Principal Paid"
        :statValue="formatCurrencyString(loan.totalPrincipal)"
      />
    </div>
    <div class="col-6 col-lg-3">
      <StatCard
        statName="Total Interest Paid"
        :statValue="formatCurrencyString(loan.totalInterest)"
      />
    </div>
  </div>

  <div class="py-5">
    <LoanTable :items="loan.amortizationTableItems" />
  </div>
</template>
