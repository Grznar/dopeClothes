<template>
  <!--Hero-->
  <section
    class="hero position-relative d-flex justify-content-center align-items-center"
    id="hero"
  >
    <div class="container">
      <div class="row">
        <div class="col-md-12 pt-6 text-center">
          <button
            @click="router.push({ name: APP_ROUTE_NAMES.PRODUCTS_HOME })"
            class="btn btn-light btn-outline-dark mt-4 btn-hero"
          >
            <span clas="fw-bold">Shop now</span>
          </button>
        </div>
      </div>
    </div>
  </section>
</template>
<script setup>
import { useRouter } from "vue-router";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";
const router = useRouter();
</script>
