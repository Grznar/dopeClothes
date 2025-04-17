<template>
  <div class="order-page full-page">
    <div class="container py-5">
      <!-- Order Header -->
      <div class="section-header text-center mb-5 mt-5">
        <h2 class="section-title">Order Summary</h2>
        <div class="order-info card mx-auto p-3 mb-4">
          <div class="d-flex justify-content-between">
            <span>Order #:</span><span>{{ order.id }}</span>
          </div>
          <div class="d-flex justify-content-between">
            <span>Date:</span><span>{{}}</span>
          </div>
          <div class="d-flex justify-content-between">
            <span>Status:</span
            ><span>{{ order.orderStatus || "Not placed" }}</span>
          </div>
        </div>
      </div>

      <div class="row">
        <!-- Items List -->
        <div class="col-md-8 mb-4">
          <div
            v-for="item in items"
            :key="item.productId"
            class="cart-item card mb-3"
          >
            <div class="d-flex align-items-center">
              <img
                :src="
                  `https://localhost:7072/${item.product.imageUrl}` ||
                  'https://placehold.co/60x60'
                "
                alt="Product"
                class="product-img me-3"
              />
              <div class="flex-grow-1">
                <h6 class="item-name mb-1">{{ item.product.name }}</h6>
                <p class="item-count mb-0">
                  {{ item.count }} ×
                  {{
                    (item.product.salePrice || item.product.price).toFixed(2)
                  }} $
                </p>
              </div>
              <span class="item-price">
                $
                {{
                  (
                    (item.product.salePrice || item.product.price) * item.count
                  ).toFixed(2)
                }}
              </span>
            </div>
          </div>
        </div>

        <!-- Summary -->
        <div class="col-md-4">
          <div class="cart-summary card p-4">
            <h5 class="summary-title mb-4 text-center">Summary</h5>
            <ul class="list-unstyled mb-4">
              <li class="d-flex justify-content-between mb-2">
                <span>Subtotal</span>
                <span>$ {{ subtotal.toFixed(2) }}</span>
              </li>
              <li class="d-flex justify-content-between mb-2">
                <span>Shipping</span>
                <span>$ {{ shipping.toFixed(2) }}</span>
              </li>
              <li class="d-flex justify-content-between total-line">
                <span>Total</span>
                <span>$ {{ (subtotal + shipping).toFixed(2) }}</span>
              </li>
            </ul>
            <button class="btn btn-checkout w-100" disabled>
              Thank you for your order
            </button>
          </div>
          <button
            v-if="!order.orderStatus"
            @click="placeOrder()"
            class="btn btn-lg btn-success btn-outline-white mt-3 w-100"
          >
            Place Order
          </button>
          <button
            v-else
            class="btn btn-lg btn-warning btn-outline-dark mt-3 w-100"
          >
            Order Placed
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { useRoute } from "vue-router";
import { useCartStore } from "@/stores/cartstore";
import orderService from "@/services/orderService.js";
import { defineProps } from "vue";
const route = useRoute();
const props = defineProps({
  cartId: {
    type: Number,
    required: true,
  },
});
const order = ref({ id: "", orderStatus: "", date: new Date().toISOString() });
const cart = useCartStore();
onMounted(async () => {
  try {
    console.log("Shopping cart Id:" + props.cartId);
    const resp = await orderService.getOrder(props.cartId);
    order.value = resp.order;
    console.log(order);
  } catch (e) {
    console.error(e);
  }
});
async function placeOrder() {
  try {
    const resp = await orderService.placeOrder(props.cartId);
    const fresh = await orderService.getOrder(props.cartId);
    order.value = fresh;
  } catch (error) {
    console.log(error.message);
  }
}
const items = computed(() => cart.items);

const subtotal = computed(() =>
  items.value.reduce(
    (sum, item) =>
      sum + (item.product.salePrice || item.product.price) * item.count,
    0
  )
);

const shipping = computed(() => (items.value.length ? 5.0 : 0));

const formattedDate = computed(() =>
  new Date(order.value.date).toLocaleDateString()
);
</script>

<style scoped>
.full-page {
  background: linear-gradient(135deg, #000, #0a0f24);
  min-height: 100vh;
  color: #e0e0e0;
}

.section-title {
  color: #fff;
  font-weight: 600;
  font-size: 2rem;
}

.section-header .order-info {
  max-width: 400px;
  background: #1a1a1a;
  border-radius: 8px;
}

.cart-item {
  background-color: #1a1a1a;
  border: none;
  border-radius: 8px;
  padding: 1rem;
}

.product-img {
  width: 60px;
  height: 60px;
  object-fit: cover;
  border-radius: 4px;
}

.item-name {
  color: #fff;
  font-size: 1rem;
}

.item-count {
  color: #aaa;
  font-size: 0.875rem;
}

.item-price {
  color: #0d6efd;
  font-weight: 600;
}

.cart-summary {
  background-color: #1a1a1a;
  border: none;
  border-radius: 8px;
}

.summary-title {
  color: #fff;
  font-weight: 600;
}

.cart-summary ul li {
  color: #ccc;
}

.total-line span {
  font-weight: 700;
  color: #fff;
}

.btn-checkout {
  background-color: #0d6efd;
  color: #fff;
  border: none;
  padding: 0.75rem;
  font-size: 1rem;
  border-radius: 8px;
}
</style>
