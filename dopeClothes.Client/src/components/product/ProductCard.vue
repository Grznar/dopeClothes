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
              <span class="badge bg-primary ms-2">{{ product.category }}</span>
            </h5>
          </div>
        </div>
      </a>
    </div>

    <div class="card-body text-center">
      <h3 class="card-title mb-3 text-decoration-none">{{ product.name }}</h3>
      <p class="mb-3">{{ product.category }}</p>
      <h6 class="mb-3" v-if="!product.salePrice">$ {{ product.price }}</h6>
      <h6 class="mb-3" v-else>
        <span class="text-decoration-line-through">$ {{ product.price }}</span>
        <span class="text-danger ps-2">$ {{ product.salePrice }}</span>
      </h6>
      <p class="text-muted">{{ product.description }}</p>
    </div>
  </div>
</template>

<script setup>
import { defineProps } from "vue";

const props = defineProps({
  product: {
    type: Object,
    required: true,
  },
});
</script>

<style scoped>
.product-card {
  height: 400px;
  display: flex;
  flex-direction: column;
}

.card-img-top {
  height: 200px;
  overflow: hidden;
  position: relative;
}

.card-img-top img {
  height: 100%;
  width: 100%;
  object-fit: cover;
}

.card-body {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.mask {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.3);
  opacity: 1;
  transition: opacity 0.3s;
}

.card-img-top:hover .mask {
  opacity: 1;
}
</style>
