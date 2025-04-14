<template>
  <!--Product List-->
  <section class="product-list mb-5 mt-7" id="product-list">
    <div class="container mt-5">
      <div class="row">
        <div class="col-md-12">
          <div class="card">
            <div class="card-title">
              <h1 class="text-center fw-bold mt-3">Product List</h1>
              <hr class="fw-bold" />
            </div>

            <div class="card-body">
              <div class="container pb-5">
                <div class="row justify-content-center">
                  <div class="col-md-6">
                    <div class="search-container">
                      <input
                        type="text"
                        id="productSearch"
                        class="form-control search-input"
                        placeholder="Search..."
                        @input="filterProducts"
                      />
                      <i class="bi bi-search search-icon"></i>
                    </div>
                  </div>
                </div>
              </div>
              <table class="table table-striped-columns">
                <thead>
                  <th></th>
                  <th>Name</th>
                  <th>Price</th>
                  <th>Sale Price</th>
                  <th>Description</th>
                  <th></th>
                  <th></th>
                </thead>
                <tbody>
                  <tr v-for="product in products" :key="product.id">
                    <td>
                      <img
                        v-if="product.imageUrl"
                        :src="`https://localhost:7072${product.imageUrl}`"
                        alt="product image"
                        width="80"
                      />
                    </td>
                    <td>{{ product.name }}</td>
                    <td>{{ product.price }}</td>
                    <td>{{ product.salePrice }}</td>
                    <td>{{ product.description }}</td>
                    <td>
                      <button
                        class="btn btn-primary"
                        @click="
                          router.push({
                            name: APP_ROUTE_NAMES.DETAILS_PRODUCT,
                            params: { id: product.id },
                          })
                        "
                      >
                        Edit
                      </button>
                    </td>
                    <td>
                      <button
                        class="btn btn-danger"
                        @click="deleteProduct(product.id)"
                      >
                        Delete
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>
              <div class="row">
                <div class="col-md-12">
                  <div v-if="errorList.length > 0">
                    <div class="alert alert-danger" role="alert">
                      <ul v-for="error in errorList" :key="error">
                        <li>
                          {{ error }}
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="row pt-5">
        <div class="col-md-6">
          <div class="text-start">
            <button
              class="btn btn-success"
              @click="router.push({ name: APP_ROUTE_NAMES.CREATE_PRODUCT })"
            >
              Create Product
            </button>
          </div>
        </div>
        <div class="col-md-6">
          <div class="text-end">
            <button
              class="btn btn-danger"
              @click="router.push({ name: APP_ROUTE_NAMES.HOME })"
            >
              Back to home
            </button>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
<script setup>
import productService from "@/services/productService.js";
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";
import { useSwal } from "@/utility/useSwal.js";
const router = useRouter();
const swal = useSwal();
const products = ref([]);
const errorList = ref([]);
const allProducts = ref([]);
onMounted(async () => {
  await refreshProducts();
});

async function refreshProducts() {
  try {
    errorList.value = [];
    const response = await productService.getAllProducts();
    allProducts.value = response;
    products.value = response;
  } catch (error) {
    errorList.value.push(error.message);
  }
}
function filterProducts(event) {
  const search = event.target.value.toLowerCase();

  products.value = allProducts.value.filter((product) => {
    return (
      product.name.toLowerCase().includes(search) ||
      product.description.toLowerCase().includes(search)
    );
  });
}

async function deleteProduct(id) {
  const confirmDelete = await swal.showConfirm(
    "Are you sure u want to delete this product?"
  );
  if (confirmDelete) {
    try {
      const response = await productService.deleteProduct(id);
      refreshProducts();
      swal.showSuccess("Product deleted successfully!");
    } catch (error) {
      errorList.value.push(error.message);
    }
  }
}
</script>
