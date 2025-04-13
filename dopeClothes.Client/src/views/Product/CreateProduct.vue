<template>
  <div class="container pt-5 pb-5">
    <div class="row">
      <div class="col-md-12">
        <h1 class="text-center fw-bold">Create Product</h1>
        <form
          v-on:submit.prevent="HandleSubmit"
          action=""
          class="bg-light rounded rounded-3 p-4 shadow-sm"
        >
          <div class="container">
            <div class="row">
              <div class="col-md-6 offset-md-3 mb-2">
                <label for="name" class="form-label">Name</label>
                <input
                  type="text"
                  class="form-control"
                  id="name"
                  placeholder="Product name"
                  v-model="productOjb.name"
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-3 offset-md-3 mb-2">
                <label for="price" class="form-label">Price</label>
                <input
                  type="number"
                  class="form-control"
                  id="price"
                  placeholder="Product price"
                  v-model="productOjb.price"
                />
              </div>
              <div class="col-md-3 mb-2">
                <label for="sale-price" class="form-label"> Sale Price</label>
                <input
                  type="number"
                  class="form-control"
                  id="sale-price"
                  placeholder="Product sale price"
                  v-model="productOjb.salePrice"
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 offset-md-3 mb-2">
                <label for="" class="form-label">Category</label>
                <select
                  name=""
                  id=""
                  class="form-select"
                  v-model="productOjb.category"
                >
                  <option
                    v-for="option in PRODUCT_CATEGORIES"
                    :key="option"
                    :value="option"
                  >
                    {{ option }}
                  </option>
                </select>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 offset-md-3 mb-2">
                <label for="description" class="form-label">Description</label>
                <textarea
                  class="form-control"
                  id="description"
                  rows="3"
                  placeholder="Product description"
                  v-model="productOjb.description"
                ></textarea>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 offset-md-3 mb-2">
                <label for="image" class="form-label">Image</label>
                <input
                  type="file"
                  class="form-control"
                  id="image"
                  placeholder="Product image"
                  @change="handleImageChange"
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 offset-md-3">
                <div
                  class="alert alert-danger pb-0"
                  v-if="errorList.length > 0"
                >
                  <ul class="list-unstyled m-0 p-0">
                    <li v-for="(error, index) in errorList" :key="index">
                      {{ error }}
                    </li>
                  </ul>
                </div>
              </div>
            </div>

            <div class="row pt-4">
              <div class="col-md-6 offset-md-3">
                <div class="row">
                  <div class="col-md-6">
                    <router-link
                      :to="{ name: APP_ROUTE_NAMES.PRODUCT_LIST }"
                      class="btn btn-primary"
                      >Back to List</router-link
                    >
                  </div>
                  <div class="col-md-6 text-end">
                    <button type="submit" class="btn btn-success">
                      <span class="fw-bold">Create</span>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
<script setup>
import { ref, reactive } from "vue";
import { useRoute, useRouter } from "vue-router";
import productService from "@/services/productService.js";
import { useSwal } from "@/utility/useSwal.js";
import { PRODUCT_CATEGORIES } from "@/constants/appconstants.js";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";
const swal = useSwal();
const errorList = ref([]);
const router = useRouter();
const productOjb = reactive({
  name: "",
  price: 0,
  salePrice: 0,
  category: "",
  description: "",
  imageUrl: "https://placehold.co/600x400",
});
const isLoading = ref(false);
const selectedFile = ref(null);

function handleImageChange(event) {
  const file = event.target.files[0];
  if (file) {
    selectedFile.value = file;
  }
}
async function HandleSubmit() {
  errorList.value = []; // reset error list
  isLoading.value = true;
  // validace
  if (productOjb.name.length < 5) {
    errorList.value.push("Name must be at least 5 characters long.");
  }
  if (productOjb.price < 0) {
    errorList.value.push("Price must be a positive number.");
  }
  if (productOjb.description.length === 0) {
    errorList.value.push("There must be a description.");
  }
  if (errorList.value.length > 0) {
    isLoading.value = false;
    return;
  }
  // inicializace formData
  const formData = new FormData();
  formData.append("name", productOjb.name);
  formData.append("price", productOjb.price);
  formData.append("salePrice", productOjb.salePrice);
  formData.append("category", productOjb.category);
  formData.append("description", productOjb.description);
  formData.append("image", selectedFile.value);
  // odeslani dat na server
  try {
    await productService.createProduct(formData);
    clearProduct();
    await swal.showSuccess("Product created");
  } catch (error) {
    errorList.value = [];

    if (error.response?.data?.errors) {
      const errors = error.response.data.errors;

      for (const key in errors) {
        errorList.value.push(...errors[key]); // vytáhne všechny chyby
      }
    } else {
      errorList.value.push("Unknow error.");
    }
  } finally {
    isLoading.value = false;
  }
}
function clearProduct() {
  productOjb.name = "";
  productOjb.price = 0;
  productOjb.salePrice = 0;
  productOjb.category = "";
  productOjb.description = "";
  productOjb.imageUrl = "https://placehold.co/600x400";
  selectedFile.value = null;
  errorList.value = [];

  const imageInput = document.getElementById("image");
  if (imageInput) {
    imageInput.value = "";
  }
}
</script>
