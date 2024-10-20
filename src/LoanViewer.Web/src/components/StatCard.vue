<script setup lang="ts">
defineProps<{
  statName: string;
  statValue: string;
}>();
</script>

<template>
  <div class="bg-white border-start border-4 rounded-3 border-danger">
    <div class="bg-white border border-start-0 border-1 rounded py-3 shadow-sm">
      <div class="d-flex justify-content-center">
        <div>
          <div class="text-black-50 small">{{ statName }}</div>
          <div class="fs-3">{{ statValue }}</div>
        </div>
      </div>
    </div>
  </div>
</template>
