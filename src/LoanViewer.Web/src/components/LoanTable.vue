<script setup lang="ts">
import type { AmortizationTableItem } from "@/Models/LoanModel";
import { formatCurrencyString } from "@/utils/formatCurrencyString";

defineProps<{
  items: AmortizationTableItem[];
}>();
</script>

<template>
  <div class="table-responsive rounded shadow">
    <table class="table text-center table-hover">
      <thead>
        <tr class="table-dark">
          <th>Term</th>
          <th>Beggining Balance</th>
          <th>Interest Paid</th>
          <th>Principal Paid</th>
          <th>Remaining Principal</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" :key="item.term">
          <td>{{ item.term }}</td>
          <td>
            <span>{{ formatCurrencyString(item.beginningBalance) }}</span>
          </td>
          <td>
            <span>{{ formatCurrencyString(item.interestPaid) }}</span>
          </td>
          <td>
            <span>{{ formatCurrencyString(item.principalPaid) }}</span>
          </td>
          <td>
            <span>{{ formatCurrencyString(item.remainingPrincipal) }}</span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
