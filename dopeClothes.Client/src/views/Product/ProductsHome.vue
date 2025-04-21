<template>
  <div class="hero-bg"></div>

  <div class="row text-center fw-bold mt-5">
    <h1 class="xl-text">Become a Gentleman</h1>
  </div>

  <div class="row p-5">
    <div class="col-md-6 offset-md-3">
      <div class="alert alert-danger pb-0" v-if="errorList.length > 0">
        <ul class="list-unstyled m-0 p-0">
          <li v-for="(error, index) in errorList" :key="index">
            {{ error }}
          </li>
        </ul>
      </div>
    </div>
  </div>

  <div class="container mb-5">
    <div class="row">
      <div class="col-md-6 text-start">
        <div class="search-container position-relative">
          <input
            type="text"
            id="productSearch"
            class="form-control search-input"
            placeholder="Search..."
            @input="onSearchInput"
          />
          <i class="bi bi-search search-icon"></i>
        </div>
      </div>

      <div class="col-md-6 text-end">
        <div class="dropdown d-inline-block">
          <button
            class="btn btn-light border"
            type="button"
            id="dropdownMenuButton"
            data-bs-toggle="dropdown"
            aria-expanded="false"
            style="min-width: 200px; height: 40px"
          >
            {{ selectedCategory || "Choose Category" }}
          </button>
          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton">
            <li
              v-for="(category, index) in PRODUCT_CATEGORIES"
              :key="index"
              @click="selectCategory(category)"
            >
              <a class="dropdown-item" href="javascript:void(0)">{{
                category
              }}</a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>

  <div class="container">
    <div class="row">
      <div
        class="col-md-3 mb-4"
        v-for="(product, index) in filteredProducts"
        :key="index"
      >
        <ProductCard :product="product" />
      </div>
    </div>
  </div>
</template>

<style scoped>
.hero-bg {
  width: 100%;
  height: 400px;
  background: url("@/assets/images/producthome.png") no-repeat center center;
  background-size: cover;
}
.search-container {
  position: relative;
}

.search-input {
  padding-right: 2.5rem;
}

.search-icon {
  position: absolute;
  right: 0.75rem;
  top: 50%;
  transform: translateY(-50%);
  color: #aaa;
  font-size: 1.2rem;
}
</style>

<script setup>
import { ref, computed, onMounted } from "vue";
import productService from "@/services/productService.js";
import { PRODUCT_CATEGORIES } from "@/constants/appconstants.js";
import ProductCard from "@/components/product/ProductCard.vue";

const allProducts = ref([]);
const errorList = ref([]);
const searchQuery = ref("");
const selectedCategory = ref("");

onMounted(async () => {
  errorList.value = [];
  try {
    const response = await productService.getAllProducts();

    allProducts.value = response.data || response;
  } catch (error) {
    errorList.value = [error.message];
  }
});

const filteredProducts = computed(() => {
  const search = searchQuery.value.toLowerCase().trim();
  const selected = selectedCategory.value.toLowerCase().trim();

  return allProducts.value.filter((product) => {
    const name = (product.name || "").toLowerCase();
    const description = (product.description || "").toLowerCase();

    const category = (
      product.category ||
      product.Categeory ||
      ""
    ).toLowerCase();

    const matchesSearch =
      !search ||
      name.includes(search) ||
      description.includes(search) ||
      category.includes(search);

    const matchesCategory = !selected || category === selected;

    return matchesSearch && matchesCategory;
  });
});

function onSearchInput(event) {
  searchQuery.value = event.target.value;
}

function selectCategory(category) {
  if (selectedCategory.value === category) {
    selectedCategory.value = "";
  } else {
    selectedCategory.value = category;
  }
}
</script>
