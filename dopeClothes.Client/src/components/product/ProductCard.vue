<template>
  <div class="product-card card shadow-sm border-2">
    <div class="card-img-top position-relative">
      <img
        v-if="product.imageUrl"
        :src="`https://localhost:7072/${product.imageUrl}`"
        alt="Product image"
        class="w-100"
      />
      <img
        v-else
        src="https://placehold.co/250x250"
        alt="Placeholder image"
        class="w-100"
      />
      <a>
        <div class="mask">
          <div class="d-flex justify-content-start align-items-end h-100">
            <h5 v-if="product.category" class="text-white m-0 p-2">
              <span class="badge bg-secondary ms-2">{{
                product.category
              }}</span>
            </h5>
          </div>
        </div>
      </a>
    </div>

    <div class="card-body text-center">
      <h3 class="card-title mb-3">{{ product.name }}</h3>
      <p class="mb-3">{{ product.category }}</p>
      <h6 class="mb-3" v-if="!product.salePrice">$ {{ product.price }}</h6>
      <h6 class="mb-3" v-else>
        <span class="text-decoration-line-through text-muted"
          >$ {{ product.price }}</span
        >
        <span class="text-danger ps-2">$ {{ product.salePrice }}</span>
      </h6>
      <p class="card-text">{{ product.description }}</p>
      <div class="hstack justify-content-center gap-3 mt-auto">
        <router-link
          :to="{
            name: APP_ROUTE_NAMES.PRODUCTCARDDETAILS,
            params: { id: product.id },
          }"
          class="btn btn-outline-dark"
        >
          <i class="bi bi-info-circle me-2"></i>
          <span>Details</span>
        </router-link>

        <button
          class="btn btn-dark"
          @click="HandleAddingToCart(props.product.id)"
        >
          <i class="bi bi-cart-plus me-2"></i>
          Add to cart
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { APP_ROUTE_NAMES } from "@/constants/routenames";
import { defineProps } from "vue";
import shoppingcartService from "@/services/shoppingcartService";
import { useSwal } from "@/utility/useSwal";
import { useCartStore } from "@/stores/cartstore";
const swal = useSwal();
const cart = useCartStore();
const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});
async function HandleAddingToCart(productId) {
  try {
    const response = await cart.insertProductToCart(productId);
    const fetch = await cart.fetchCartItems();
    swal.showSuccess(response.message);
  } catch (error) {
    swal.showError(error.message);
  }
}
</script>

<style scoped>
.product-card {
  height: 400px;
  display: flex;
  flex-direction: column;
  background-color: #fff; /* světlé pozadí */
  border: 1px solid #333;
  border-radius: 8px;
  overflow: hidden;
  transition: transform 0.3s;
}

.product-card:hover {
  transform: translateY(-3px);
}

.card-img-top {
  height: 200px;
  overflow: hidden;
  position: relative;
}

.card-img-top img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.mask {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.4);
  transition: background 0.3s;
}

.card-img-top:hover .mask {
  background: rgba(0, 0, 0, 0.6);
}

.card-body {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background-color: #fff; /* světlé pozadí */
  color: #333; /* tmavý text */
  padding: 1rem;
}

.card-title {
  font-size: 1.5rem;
  font-weight: bold;
  color: #333; /* tmavá barva nadpisu */
  margin-bottom: 1rem;
}

.card-text {
  color: #555;
  font-size: 0.95rem;
}

.btn {
  transition: background-color 0.3s, transform 0.3s;
}

.btn:hover {
  transform: translateY(-2px);
}

.btn i {
  font-size: 1.2rem;
}
</style>
