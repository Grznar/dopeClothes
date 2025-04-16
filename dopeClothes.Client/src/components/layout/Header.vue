<template>
  <nav class="navbar navbar-expand-lg fixed-top navbar-custom">
    <div class="container-fluid px-4">
      <!-- Logo -->
      <a
        class="navbar-brand text-uppercase fw-bold"
        @click.prevent="router.push({ name: APP_ROUTE_NAMES.HOME })"
        href="#"
      >
        <img
          src="/src/assets/images/navLogo.png"
          alt="logo"
          width="40"
          height="40"
          class="me-2"
        />
        DopeClothes
      </a>

      <!-- Toggler -->
      <button
        class="navbar-toggler text-white"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarResponsive"
      >
        <span class="navbar-toggler-icon"></span>
      </button>

      <!-- Menu -->
      <div class="collapse navbar-collapse" id="navbarResponsive">
        <ul class="navbar-nav ms-auto me-4">
          <li class="nav-item">
            <RouterLink class="nav-link" :to="{ name: APP_ROUTE_NAMES.HOME }">
              Home
            </RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink
              class="nav-link"
              :to="{ name: APP_ROUTE_NAMES.PRODUCTS_HOME }"
            >
              Products
            </RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink
              class="nav-link"
              :to="{ name: APP_ROUTE_NAMES.CONTACT }"
            >
              Contact
            </RouterLink>
          </li>

          <li class="nav-item" v-if="!auth.isLoggedIn">
            <RouterLink class="nav-link" :to="{ name: APP_ROUTE_NAMES.LOGIN }">
              Login
            </RouterLink>
          </li>
          <li class="nav-item" v-if="!auth.isLoggedIn">
            <RouterLink
              class="nav-link"
              :to="{ name: APP_ROUTE_NAMES.REGISTER }"
            >
              Register
            </RouterLink>
          </li>
          <li
            class="nav-item"
            v-if="cart.totalItemCount > 0 && auth.isLoggedIn"
          >
            <RouterLink
              class="nav-link"
              :to="{ name: APP_ROUTE_NAMES.SHOPPING_CART }"
            >
              <i class="bi bi-cart"></i> ({{ cart.totalItemCount }})
            </RouterLink>
          </li>

          <li class="nav-item" v-if="auth.isLoggedIn">
            <a class="nav-link" @click="auth.logout()">Logout</a>
          </li>
          <li class="nav-item" v-if="auth.isLoggedIn">
            <RouterLink
              class="nav-link"
              :to="{ name: APP_ROUTE_NAMES.PERSONAL }"
              ><i class="bi bi-person fa-3x"> </i
            ></RouterLink>
          </li>
          <li class="nav-item" v-if="auth.role === 'Admin'">
            <a
              class="nav-link"
              @click="router.push(APP_ROUTE_NAMES.PRODUCT_LIST)"
              >Product List</a
            >
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script setup>
import { onMounted, onBeforeUnmount } from "vue";
import { useRouter } from "vue-router";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";
import { useAuthStore } from "@/stores/authstore";
import { useCartStore } from "@/stores/cartstore";
const auth = useAuthStore();
const router = useRouter();
const cart = useCartStore();
console.log(cart.cartItems);
console.log(cart.totalItemCount);
function handleNavbarShadow() {
  const navbar = document.querySelector(".navbar");
  if (!navbar) return;

  if (window.scrollY > 10) {
    navbar.classList.add("scrolled-navbar");
  } else {
    navbar.classList.remove("scrolled-navbar");
  }
}

onMounted(() => {
  window.addEventListener("scroll", handleNavbarShadow);
  cart.fetchCartItems();
});

onBeforeUnmount(() => {
  window.removeEventListener("scroll", handleNavbarShadow);
});
</script>
<style scoped>
.navbar-custom {
  background-color: transparent;
  transition: background-color 0.5s ease, box-shadow 0.5s ease;
  padding: 1rem 0;
  font-weight: 500;
  z-index: 1000;
}

.navbar-custom .nav-link {
  color: #fff;
  margin-right: 1rem;
  font-size: 1rem;
  transition: color 0.3s ease;
}

.navbar-custom .nav-link:hover {
  color: #f8f9fa;
  text-decoration: underline;
}

.navbar-custom .navbar-toggler {
  border: none;
  background-color: transparent;
}

.navbar-custom .navbar-brand {
  color: #fff;
  font-size: 1.25rem;
  letter-spacing: 1px;
}

.scrolled-navbar {
  background-color: white !important;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: background-color 0.4s ease, box-shadow 0.4s ease;
}

.scrolled-navbar .nav-link {
  color: #000 !important;
}

.scrolled-navbar .navbar-brand {
  color: #000 !important;
}
</style>
