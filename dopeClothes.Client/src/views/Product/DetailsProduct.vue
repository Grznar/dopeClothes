<template>
  <div class="container pt-5 pb-5">
    <div class="row">
      <div class="col-md-12">
        <h1 class="text-center fw-bold">Update Product</h1>
        <form
          @submit.prevent="HandleSubmit"
          class="bg-light rounded rounded-3 p-4 shadow-sm"
        >
          <div class="container">
            <!-- Name Field -->
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
            <!-- Price and Sale Price -->
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
                <label for="sale-price" class="form-label">Sale Price</label>
                <input
                  type="number"
                  class="form-control"
                  id="sale-price"
                  placeholder="Product sale price"
                  v-model="productOjb.salePrice"
                />
              </div>
            </div>
            <!-- Category -->
            <div class="row">
              <div class="col-md-6 offset-md-3 mb-2">
                <label for="category" class="form-label">Category</label>
                <select
                  id="category"
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
            <!-- Description -->
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
            <!-- Image and File Input -->
            <div class="row">
              <div class="col-md-6 offset-md-3 mb-2">
                <label for="image" class="form-label">Image</label>
                <div class="mb-3 mt-3">
                  <img
                    v-if="productOjb.imageUrl"
                    :src="`https://localhost:7072${productOjb.imageUrl}`"
                    alt="product image"
                    class="img-fluid"
                    width="200"
                    height="150"
                  />
                </div>
                <input
                  type="file"
                  class="form-control"
                  id="image"
                  placeholder="Product image"
                  @change="handleImageChange"
                />
              </div>
            </div>
            <!-- Error Alert -->
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
            <!-- Buttons -->
            <div class="row pt-4">
              <div class="col-md-6 offset-md-3">
                <div class="row">
                  <div class="col-md-6">
                    <router-link
                      :to="{ name: APP_ROUTE_NAMES.PRODUCT_LIST }"
                      class="btn btn-primary"
                    >
                      Back to List
                    </router-link>
                  </div>
                  <div class="col-md-6 text-end">
                    <button type="submit" class="btn btn-success">
                      <span class="fw-bold">Update</span>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- container -->
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({ id: Number });
import { ref, reactive, onMounted } from "vue";
import { useRouter } from "vue-router";
import productService from "@/services/productService.js";
import { useSwal } from "@/utility/useSwal.js";
import { PRODUCT_CATEGORIES } from "@/constants/appconstants.js";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";

const swal = useSwal();
const errorList = ref([]);
const router = useRouter();

// Reaktivní objekt pro uchování produktových dat
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

  // Jednoduchá validace
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

  // Inicializace FormData pro odeslání dat včetně obrázku
  const formData = new FormData();
  formData.append("name", productOjb.name);
  formData.append("price", productOjb.price);
  formData.append("salePrice", productOjb.salePrice);
  formData.append("category", productOjb.category);
  formData.append("description", productOjb.description);
  // Pokud uživatel změnil obrázek, přidej ho do formData
  if (selectedFile.value) {
    formData.append("image", selectedFile.value);
  }
  // Předání id produktu (z props)
  formData.append("id", props.id);

  try {
    await productService.updateProduct(formData);
    await swal.showSuccess("Product updated");
    router.push({ name: APP_ROUTE_NAMES.PRODUCT_LIST });
  } catch (error) {
    errorList.value = [];
    if (error.response?.data?.errors) {
      const errors = error.response.data.errors;
      for (const key in errors) {
        errorList.value.push(...errors[key]);
      }
    } else {
      errorList.value.push("Unknown error.");
    }
  }
}

onMounted(async () => {
  if (props.id) {
    try {
      const response = await productService.getProduct(props.id);
      productOjb.name = response.name;
      productOjb.price = response.price;
      productOjb.salePrice = response.salePrice;
      productOjb.category = response.category;
      productOjb.description = response.description;
      productOjb.imageUrl = response.imageUrl;
    } catch (error) {
      console.error("❌ Chyba při načítání produktu", error);
    }
  }
});
</script>
