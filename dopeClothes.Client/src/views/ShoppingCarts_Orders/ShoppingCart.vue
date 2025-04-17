<template>
  <div class="cart-page full-page">
    <div class="container py-5">
      <div class="row mt-5">
        <div class="col-md-8 mb-4">
          <h3 class="section-title">Your Cart</h3>

          <div
            v-for="item in cartItems"
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
                <div class="quantity-control">
                  <button class="btn-qty" @click="decrease(item)">−</button>
                  <span class="count">{{ item.count }}</span>
                  <button class="btn-qty" @click="increase(item)">+</button>
                </div>
              </div>
              <span class="item-price">
                $
                {{
                  (item.product.salePrice || item.product.price) * item.count
                }}
              </span>
            </div>
          </div>
        </div>

        <div class="col-md-4 mt-5">
          <div class="cart-summary card p-4">
            <h5 class="summary-title mb-4">Summary</h5>
            <ul class="list-unstyled mb-4">
              <li class="d-flex justify-content-between mb-2">
                <span>Subtotal</span>
                <span>$ {{ subtotal }}</span>
              </li>
              <li class="d-flex justify-content-between mb-2">
                <span>Shipping</span>
                <span>$ {{ shipping }}</span>
              </li>
              <li class="d-flex justify-content-between total-line">
                <span>Total</span>
                <span>$ {{ subtotal + shipping }}</span>
              </li>
            </ul>
            <button
              class="btn btn-checkout w-100"
              :disabled="!cartItems.length"
              @click="toCheckout()"
            >
              Proceed to Checkout
            </button>
            <p v-if="!cartItems.length" class="empty-note">
              Your cart is empty.
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useCartStore } from "@/stores/cartstore";
import { APP_ROUTE_NAMES } from "@/constants/routenames";
import { useRouter } from "vue-router";
const router = useRouter();
const cart = useCartStore();
onMounted(() => {
  cart.fetchCartItems();
});

const cartItems = computed(() => cart.items);

const subtotal = computed(() =>
  cart.items.reduce(
    (sum, item) =>
      sum + (item.product.salePrice || item.product.price) * item.count,
    0
  )
);

const shipping = computed(() => (cart.items.length ? 5.0 : 0));
async function toCheckout() {
  try {
    const cartId = cartItems.value[0].shoppingCartId;
    console.log(cartId);
    router.push({
      name: APP_ROUTE_NAMES.ORDER,
      params: { cartId },
    });
  } catch (error) {
    console.log(error.message);
  }
}
async function increase(item) {
  try {
    await cart.increaseProduct(item.productId);
  } catch (error) {
    console.log(error.message);
  }
}

async function decrease(item) {
  try {
    const response = await cart.decreaseProduct(item.productId);
  } catch (error) {
    console.log(error.message);
  }
}
</script>

<style scoped>
.full-page {
  background: linear-gradient(135deg, #000, #0a0f24);
  min-height: 100vh;
  color: #e0e0e0;
}

.section-title {
  color: #fff;
  margin-bottom: 1.5rem;
  font-weight: 600;
}

.cart-item {
  background-color: #1a1a1a;
  border: none;
  border-radius: 8px;
  padding: 1rem;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}
.cart-item:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.5);
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
  margin-bottom: 0.5rem;
}

/* + / − ovládání */
.quantity-control {
  display: inline-flex;
  align-items: center;
  background: #222;
  border-radius: 4px;
  overflow: hidden;
}
.btn-qty {
  background: transparent;
  border: none;
  color: #fff;
  width: 28px;
  height: 28px;
  font-size: 1.25rem;
  line-height: 1;
  cursor: pointer;
}
.btn-qty:hover {
  background: rgba(255, 255, 255, 0.1);
}
.count {
  width: 32px;
  text-align: center;
  color: #fff;
  font-weight: 500;
}

.item-price {
  color: #0d6efd;
  font-weight: 600;
  margin-left: 1rem;
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
  transition: background-color 0.2s ease, transform 0.2s ease;
}

.btn-checkout:disabled {
  background-color: #444;
  cursor: not-allowed;
}
.btn-checkout:hover:enabled {
  background-color: #0b5ed7;
  transform: translateY(-1px);
}

.empty-note {
  color: #888;
  text-align: center;
  margin-top: 1rem;
}
</style>
