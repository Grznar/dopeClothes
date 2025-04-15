<template>
  <div class="d-flex flex-column min-vh-100">
    <Header v-if="route.name !== APP_ROUTE_NAMES.HOME" />
    <div class="flex-grow-1"><RouterView /></div>
    <Footer v-if="route.name !== APP_ROUTE_NAMES.HOME" />
  </div>
</template>

<script setup>
import { useRoute } from "vue-router";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";
import Header from "@/components/layout/Header.vue";
import Footer from "@/components/layout/Footer.vue";

const route = useRoute();
</script>
