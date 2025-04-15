<template>
  <header
    class="masthead d-flex align-items-center justify-content-center text-center"
  >
    <div class="overlay"></div>
    <div class="container">
      <div class="d-flex justify-content-center">
        <div class="text-center text-white z-2">
          <h1 class="display-4 fw-bold text-uppercase">
            Dress like a Gentleman
          </h1>
          <h2 class="text-white-50 mt-3 mb-4">
            Join the ranks. Timeless style, effortless class.
          </h2>
          <button
            @click="router.push({ name: APP_ROUTE_NAMES.PRODUCTS_HOME })"
            class="btn btn-outline-light btn-lg px-4 py-2"
          >
            Start Your Journey
          </button>
        </div>
      </div>
    </div>
  </header>
</template>

<script setup>
import { useRouter } from "vue-router";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";
const router = useRouter();
</script>

<style scoped>
.masthead {
  position: relative;
  width: 100%;
  height: 100vh;
  background: linear-gradient(
      to bottom,
      rgba(0, 0, 0, 0.4) 0%,
      rgba(0, 0, 0, 0.6) 75%,
      #000 100%
    ),
    url("@/assets/images/heroBG.png");
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  background-attachment: fixed;
  padding: 0;
}

.overlay {
  position: absolute;
  top: 0;
  left: 0;
  height: 100%;
  width: 100%;
  background: linear-gradient(rgba(0, 0, 0, 0.4), rgba(0, 0, 0, 0.6));
  z-index: 1;
}

.masthead h1 {
  font-size: 3rem;
  letter-spacing: 0.2rem;
  text-shadow: 0 2px 4px rgba(0, 0, 0, 0.6);
}

.masthead h2 {
  max-width: 600px;
  margin: auto;
  font-size: 1.25rem;
  font-weight: 300;
  color: #ddd;
  text-shadow: 0 1px 3px rgba(0, 0, 0, 0.4);
}

@media (min-width: 768px) {
  .masthead h1 {
    font-size: 4.5rem;
  }
  .masthead h2 {
    font-size: 1.5rem;
  }
}
</style>
