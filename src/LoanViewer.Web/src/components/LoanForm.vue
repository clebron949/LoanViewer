<script setup lang="ts">
import { ref } from "vue";
import {
  AmountType,
  TermType,
  type LoanFormModel,
  type LoanOptions,
} from "@/Models/LoanModel";

const emit = defineEmits(["submitLoanForm"]);

const loanModel = ref<{
  amount: string;
  loanRate: string;
  loanTerms: string;
  isFullAmount: boolean;
  isYearlyTerms: boolean;
}>({
  amount: "",
  loanRate: "",
  loanTerms: "",
  isFullAmount: true,
  isYearlyTerms: true,
});

function submitForm() {
  const areInputsValid = ValidateInputs();

  if (areInputsValid === false) {
    alert("Please fill in all the fields");
    return;
  }

  console.log("Loan Form Model: ", loanModel.value);

  const opts: LoanOptions = {
    amountType: loanModel.value.isFullAmount
      ? AmountType.Full
      : AmountType.Monthly,
    termType: loanModel.value.isYearlyTerms ? TermType.Years : TermType.Months,
  };

  const loanFormModel: LoanFormModel = {
    amount: parseFloat(parseLoanAmount(loanModel.value.amount)),
    interestRate: parseFloat(loanModel.value.loanRate),
    term: parseFloat(loanModel.value.loanTerms),
    options: opts,
  };

  console.log("Submited Loan: ", loanFormModel);

  //const navbarToggleBtn = document.getElementById("navbar-toggler-btn");
  //navbarToggleBtn?.click();

  emit("submitLoanForm", loanFormModel);
}

function ValidateInputs(): boolean {
  return (
    loanModel.value.amount.trim() !== "" &&
    loanModel.value.loanRate.trim() !== "" &&
    loanModel.value.loanTerms.trim() !== ""
  );
}

function parseLoanAmount(value: string): string {
  return value.replace(",", "");
}
</script>

<template>
  <nav class="navbar navbar-dark bg-dark">
    <div class="container">
      <div class="d-flex align-items-center gap-3">
        <button
          id="navbar-toggler-btn"
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarToggleExternalContent"
          aria-controls="navbarToggleExternalContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <span class="text-white fs-4 fw-semibold">Loan Viewer</span>
      </div>
    </div>
  </nav>
  <div class="collapse" id="navbarToggleExternalContent" data-bs-theme="dark">
    <div class="bg-dark p-4">
      <form @submit.prevent="submitForm" class="container">
        <div class="row text-white mb-3">
          <div class="d-flex gap-3 flex-wrap">
            <div class="form-check">
              <input
                class="form-check-input"
                type="radio"
                name="optLoanAmount"
                id="optLoanAmount1"
                v-bind:value="true"
                v-model="loanModel.isFullAmount"
              />
              <label class="form-check-label" for="optLoanAmount1">
                Full Amount
              </label>
            </div>

            <div class="form-check">
              <input
                class="form-check-input"
                type="radio"
                name="optLoanAmount"
                id="optLoanAmount2"
                v-bind:value="false"
                v-model="loanModel.isFullAmount"
              />
              <label class="form-check-label" for="optLoanAmount2">
                Monthly Amount
              </label>
            </div>

            <div class="form-check">
              <input
                class="form-check-input"
                type="radio"
                name="optLoanTerms"
                id="optLoanTerms1"
                v-bind:value="true"
                v-model="loanModel.isYearlyTerms"
              />
              <label class="form-check-label" for="optLoanTerms1">
                Yearly Terms
              </label>
            </div>

            <div class="form-check">
              <input
                class="form-check-input"
                type="radio"
                name="optLoanTerms"
                id="optLoanTerms2"
                v-bind:value="false"
                v-model="loanModel.isYearlyTerms"
              />
              <label class="form-check-label" for="optLoanTerms2">
                Monthly Terms
              </label>
            </div>
          </div>
        </div>
        <div class="row align-items-end gy-3">
          <div class="col-md-3">
            <label for="loanAmount" class="form-label text-white">Amount</label>
            <input
              type="text"
              class="form-control"
              id="loanAmount"
              v-model="loanModel.amount"
            />
          </div>
          <div class="col-md-3">
            <label for="loanInterestRate" class="form-label text-white"
              >Interest Rate</label
            >
            <input
              type="text"
              class="form-control"
              id="loanInterestRate"
              v-model="loanModel.loanRate"
            />
          </div>
          <div class="col-md-3">
            <label for="loanTerms" class="form-label text-white">Terms</label>
            <input
              type="text"
              class="form-control"
              id="loanTerms"
              v-model="loanModel.loanTerms"
            />
          </div>
          <div class="col-md-2">
            <button type="submit" class="btn btn-danger w-100">Submit</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
