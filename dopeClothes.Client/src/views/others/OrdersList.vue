<template>
  <div class="admin-orders-page full-page">
    <div class="container py-5">
      <h2 class="section-title text-center mb-5">All Orders</h2>
      <div class="row gy-4">
        <div
          v-for="order in orders"
          :key="order.orderId"
          class="col-md-6 col-lg-4"
        >
          <div class="card order-card h-100">
            <div class="card-body d-flex flex-column">
              <h5 class="card-title mb-3">Order #{{ order.orderId }}</h5>
              <p class="mb-1"><strong>User:</strong> {{ order.userEmail }}</p>
              <p class="mb-1">
                <strong>Date:</strong>
                {{ new Date(order.date).toLocaleDateString() }}
              </p>
              <p class="mb-3">
                <strong>Total:</strong> ${{ order.total.toFixed(2) }}
              </p>

              <div class="mt-auto">
                <div class="dropdown w-100">
                  <button
                    class="btn btn-outline-info w-100 dropdown-toggle"
                    type="button"
                    data-bs-toggle="dropdown"
                  >
                    {{ order.status }}
                  </button>
                  <ul class="dropdown-menu w-100">
                    <li
                      v-for="status in statuses"
                      :key="status"
                      @click="changeStatus(order, status)"
                    >
                      <a class="dropdown-item" href="#">{{ status }}</a>
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div v-if="orders.length === 0" class="col-12 text-center text-muted">
          No orders found.
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import orderService from "@/services/orderService.js";

const orders = ref([]);
const statuses = ["Pending", "Processing", "Shipped", "Completed", "Cancelled"];

onMounted(async () => {
  try {
    orders.value = await orderService.getAllOrders();
  } catch (e) {
    console.error(e);
  }
});

async function changeStatus(order, newStatus) {
  try {
    await orderService.updateOrderStatus(order.orderId, newStatus);
    order.status = newStatus;
  } catch (e) {
    console.error(e);
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
  font-weight: 600;
}
.order-card {
  background-color: #1a1a1a;
  border: none;
  border-radius: 8px;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}
.order-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.5);
}
.card-title {
  color: #0d6efd;
}
.card-body p {
  color: #ccc;
  font-size: 0.9rem;
}
.dropdown .btn {
  background-color: #222;
  color: #fff;
  border-radius: 4px;
}
.dropdown .dropdown-menu {
  background-color: #222;
}
.dropdown .dropdown-item {
  color: #e0e0e0;
}
.dropdown .dropdown-item:hover {
  background-color: #333;
  color: #fff;
}
</style>
