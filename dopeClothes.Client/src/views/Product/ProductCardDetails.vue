<template>
  <div class="full-page bg-white">
    <div class="product-card">
      <div class="image-container">
        <img
          :src="
            productObj.imageUrl
              ? `https://dopeclothesserver.azurewebsites.net/${productObj.imageUrl}`
              : 'https://placehold.co/600x400'
          "
          :alt="productObj.name"
        />
        <div class="badge-wrapper" v-if="productObj.category">
          <span class="badge">{{ productObj.category }}</span>
        </div>
      </div>
      <div class="details-container">
        <h2 class="product-name">{{ productObj.name }}</h2>
        <p class="description">{{ productObj.description }}</p>
        <div class="price-section">
          <span v-if="!productObj.salePrice">{{ productObj.price }} $</span>
          <span v-else>
            <del>{{ productObj.price }} $</del>
            <span class="sale-price">{{ productObj.salePrice }} $</span>
          </span>
        </div>
      </div>
    </div>
    <div class="actions container">
      <div class="row">
        <div class="col-md-4 text-center">
          <button
            class="btn btn-lg btn-danger"
            @click="
              router.push({
                name: APP_ROUTE_NAMES.PRODUCTS_HOME,
              })
            "
          >
            Back to Product List
          </button>
        </div>
        <div class="col-md-4 text-center">
          <button class="btn btn-lg btn-primary" @click="HandleAddToWish()">
            Add To Whish List
          </button>
        </div>
        <div class="col-md-4 text-center" @click="HandleAddingToCart(props.id)">
          <button class="btn btn-lg btn-success">Add to Cart</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { defineProps, reactive, ref, onMounted } from "vue";
import productService from "@/services/productService.js";
import { APP_ROUTE_NAMES } from "@/constants/routenames";
import { useRouter } from "vue-router";
import whistlistService from "@/services/whishlistService.js";
import { useSwal } from "@/utility/useSwal.js";
import shoppingcartService from "@/services/shoppingcartService";
import { useCartStore } from "@/stores/cartstore";
const router = useRouter();
const swal = useSwal();
const cartStore = useCartStore();
const props = defineProps({
  id: {
    type: String,
    required: true,
  },
});
async function HandleAddingToCart(productId) {
  try {
    const response = await cartStore.insertProductToCart(productId);
    const fetch = await cartStore.fetchCartItems();
    swal.showSuccess(response.message);
  } catch (error) {
    swal.showError(error.message);
  }
}
const productObj = reactive({
  name: "",
  price: 0,
  salePrice: 0,
  category: "",
  description: "",
  imageUrl: "https://placehold.co/600x400",
});

const selectedSize = ref("");

onMounted(async () => {
  if (props.id) {
    try {
      const response = await productService.getProduct(props.id);
      productObj.name = response.name;
      productObj.price = response.price;
      productObj.salePrice = response.salePrice;
      productObj.category = response.category;
      productObj.description = response.description;
      productObj.imageUrl = response.imageUrl;
      if (response.sizes) {
        productObj.sizes = response.sizes;
      }
    } catch (error) {}
  }
});

async function HandleAddToWish() {
  try {
    const dialog = await swal.showConfirm(
      "Are you sure you want to add this product to your wishlist?"
    );
    if (dialog) {
      const response = await whistlistService.addToWishlist(props.id);

      if (response) {
        await swal.showSuccess(response.message);
      }
    } else {
    }
  } catch (error) {
    await swal.showError(error.response.data.message);
  }
}
</script>

<style scoped>
.full-page {
  min-height: 100vh;
  background-color: #f8f9fa;
  padding: 2rem;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

/* Produktová karta */
.product-card {
  background-color: #fff;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  width: 100%;
  max-width: 1200px;
  margin-bottom: 2rem;
  display: flex;
  flex-direction: column;
}

.product-card:hover {
  transform: translateY(-8px);
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.25);
}

.image-container {
  position: relative;
}

.image-container img {
  width: 100%;
  display: block;
  object-fit: cover;
  height: auto;
}

.badge-wrapper {
  position: absolute;
  top: 1rem;
  left: 1rem;
}

.badge {
  background-color: #6c757d;
  color: #fff;
  padding: 0.5rem 1rem;
  font-size: 0.9rem;
  border-radius: 20px;
  text-transform: uppercase;
}

.details-container {
  padding: 1.5rem 2rem;
}

.product-name {
  font-size: 2rem;
  margin-bottom: 0.5rem;
  color: #333;
}

.description {
  color: #555;
  font-size: 1rem;
  line-height: 1.5;
  margin-bottom: 1rem;
}

.price-section {
  font-size: 1.5rem;
  color: #333;
}

.price-section del {
  color: #888;
  margin-right: 0.5rem;
}

.sale-price {
  color: #dc3545;
  font-weight: bold;
}

@media (min-width: 768px) {
  .product-card {
    flex-direction: row;
  }
  .image-container,
  .details-container {
    flex: 1;
  }
  .image-container img {
    height: 100%;
  }
}

.actions {
  width: 100%;
  max-width: 1200px;
}

.actions .btn {
  position: relative;
  overflow: hidden;
  transition: transform 0.3s, box-shadow 0.3s;
  border-radius: 50px;
  padding: 1rem 2rem;
  font-size: 1.25rem;
  font-weight: bold;
  letter-spacing: 1px;
  border: none;
}

.actions .btn::after {
  content: "";
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%) scale(0);
  width: 200%;
  height: 200%;
  background: rgba(255, 255, 255, 0.2);
  border-radius: 50%;
  transition: transform 0.5s, opacity 0.5s;
  opacity: 0;
}

.actions .btn:hover {
  transform: scale(1.05);
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.25);
}

.actions .btn:hover::after {
  transform: translate(-50%, -50%) scale(1);
  opacity: 1;
}
</style>
