<template>
  <section class="vh-50 bg-white text-bg-black">
    <div class="container py-5 h-100 mt-5 mb-5">
      <div class="row d-flex justify-content-center align-items-center h-100">
        <div class="col col-lg-6 mb-1 mb-lg-0">
          <div class="card mb-3" style="border-radius: 0.5rem">
            <div class="row g-0">
              <div
                class="col-md-4 gradient-custom text-center text-white"
                style="
                  border-top-left-radius: 0.5rem;
                  border-bottom-left-radius: 0.5rem;
                "
              >
                <img
                  src="@/assets/images/profilepic.png"
                  alt="Avatar"
                  class="img-fluid my-5"
                  style="width: 80px"
                />
                <h3 clas="fw-bold fs-1">{{ user.name }}</h3>
                <p>Customer</p>
                <i class="far fa-edit mb-5"></i>
              </div>
              <div class="col-md-8">
                <div class="card-body p-4">
                  <h6>Information</h6>
                  <hr class="mt-0 mb-4" />
                  <div class="row pt-1">
                    <div class="col-6 mb-3">
                      <p class="text-muted">{{ user.email }}</p>
                    </div>
                  </div>

                  <hr class="mt-0 mb-4" />
                  <div class="row pt-1">
                    <div class="col-6 mb-3 mt-5">
                      <button
                        class="btn btn-danger"
                        @click="router.push(APP_ROUTE_NAMES.CHANGE_PASSWORD)"
                      >
                        Change Password
                      </button>
                    </div>
                    <div class="col-6 mb-3 mt-5">
                      <button
                        class="btn btn-success"
                        @click="router.push(APP_ROUTE_NAMES.MY_ORDER)"
                      >
                        My Orders
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="card mt-4 bg-white">
              <div class="card-body">
                <div class="card-title h5 text-center fw-bold mt-4 mb-3">
                  My Wish List
                </div>
                <table class="table align-items-center text-center">
                  <thead>
                    <tr>
                      <th>Product Name</th>
                      <th>Image</th>
                      <th>Price</th>
                      <th></th>
                    </tr>
                  </thead>
                  <tbody class="align-items-center text-center">
                    <tr v-for="(item, index) in myItems" :key="index">
                      <td>{{ item.product.name }}</td>
                      <td>
                        <img
                          :src="`https://dopeclothesserver.azurewebsites.net/${item.product.imageUrl}`"
                          alt="Product image"
                          style="width: 100px"
                        />
                      </td>
                      <td
                        v-if="item.product.salePrice > 0"
                        class="text-success"
                      >
                        {{ item.product.salePrice }} $
                        <span class="text-danger"> (SALE!)</span>
                      </td>
                      <td v-else class="text-danger">
                        {{ item.product.price }} $
                      </td>
                      <td>
                        <button
                          class="btn btn-primary btn-sm mx-2"
                          @click="
                            router.push({
                              name: APP_ROUTE_NAMES.PRODUCTCARDDETAILS,
                              params: { id: item.product.id },
                            })
                          "
                        >
                          Details
                        </button>
                        <button
                          class="btn btn-danger btn-sm"
                          @click="handleDelete(item.product.id)"
                        >
                          Delete
                        </button>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
<script setup>
import { ref, computed, onMounted, reactive } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "@/stores/authstore.js";
import { useSwal } from "@/utility/useSwal";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";
import whishlistService from "@/services/whishlistService";
const router = useRouter();
const swal = useSwal();
const auth = useAuthStore();
const myItems = ref([]);
const user = reactive({
  name: "",
  email: "",
});
async function refresh() {
  try {
    user.name = auth.user.userName;
    user.email = auth.user.email;

    const response = await whishlistService.getAllmyWishes();

    if (response) {
      myItems.value = response.listOfItems;
    }
  } catch (error) {}
}
onMounted(async () => {
  await refresh();
});
async function handleDelete(id) {
  try {
    const response = await whishlistService.deleteFromWishlist(id);
    if (response) {
      await swal.showSuccess("Wish deleted successfully!");
      await refresh();
    }
  } catch (error) {
    await swal.showError(error.message);
  }
}
</script>
