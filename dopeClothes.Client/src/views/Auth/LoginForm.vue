<template>
  <section class="bg-light p-3 p-md-4 p-xl-5 bg-white">
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-12 col-xxl-11">
          <div class="card border-light-subtle shadow-sm">
            <div class="row g-0">
              <!-- Formulář v levém sloupci -->
              <div
                class="col-12 col-md-6 d-flex align-items-center justify-content-center"
              >
                <div class="col-12 col-lg-11 col-xl-10">
                  <div class="card-body p-3 p-md-4 p-xl-5">
                    <div class="row">
                      <div class="col-12">
                        <div class="mb-5">
                          <div class="text-center mb-4">
                            <a href="#!">
                              <img
                                src="@/assets/images/navLogo.png"
                                width="175"
                                height="175"
                                alt="Logo"
                              />
                            </a>
                          </div>
                          <h2 class="h4 text-center">Login</h2>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col-12">
                        <form @submit.prevent="handleLogin">
                          <div class="row gy-3 overflow-hidden">
                            <div class="col-12">
                              <div class="form-floating mb-3">
                                <input
                                  type="email"
                                  class="form-control"
                                  name="Email"
                                  id="Email"
                                  placeholder="Email"
                                  v-model="formData.email"
                                  required
                                />
                                <label for="Email" class="form-label"
                                  >Email</label
                                >
                              </div>
                            </div>
                            <div class="col-12">
                              <div class="form-floating mb-3">
                                <input
                                  type="password"
                                  class="form-control"
                                  name="Password"
                                  id="Password"
                                  placeholder="Password"
                                  v-model="formData.password"
                                  required
                                />
                                <label for="Password" class="form-label"
                                  >Password</label
                                >
                              </div>
                            </div>

                            <div class="row">
                              <div class="col-md-6 offset-md-3">
                                <div
                                  class="alert alert-danger pb-0"
                                  v-if="errorList.length > 0"
                                >
                                  <ul class="list-unstyled m-0 p-0">
                                    <li
                                      v-for="(error, index) in errorList"
                                      :key="index"
                                    >
                                      {{ error }}
                                    </li>
                                  </ul>
                                </div>
                              </div>
                            </div>
                            <div class="col-12">
                              <div class="d-grid">
                                <button
                                  class="btn btn-dark btn-lg"
                                  type="submit"
                                >
                                  Login
                                </button>
                              </div>
                            </div>
                          </div>
                        </form>
                        <div class="row">
                          <div class="col-12">
                            <p class="mb-0 mt-5 text-center">
                              <RouterLink
                                class="nav-link"
                                :to="{ name: APP_ROUTE_NAMES.REGISTER }"
                              >
                                Don't have an account? Register here
                              </RouterLink>
                            </p>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Obrázek v pravém sloupci -->
              <div class="col-12 col-md-6">
                <img
                  class="img-fluid rounded-end w-100 h-100 object-fit-cover"
                  loading="lazy"
                  src="@/assets/images/login.png"
                  alt="Welcome back!"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
import { reactive, ref } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "@/stores/authstore.js";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";
import { useSwal } from "@/utility/useSwal.js";

const auth = useAuthStore();
const router = useRouter();
const swal = useSwal();
const errorList = ref([]);
const formData = reactive({
  email: "",
  password: "",
});

async function handleLogin() {
  errorList.value = [];
  try {
    const loginForDb = {
      Email: formData.email,
      Password: formData.password,
    };
    const response = await auth.login(loginForDb);
    swal.showSuccess("Login successfull!");
    await resetForm();
    router.push({
      name: APP_ROUTE_NAMES.HOME,
    });
  } catch (error) {
    if (error.response && error.response.data) {
      const backendErrors = error.response.data;
      // Projdeme každý klíč objektu s chybami
      for (const key in backendErrors) {
        // Očekává se, že hodnota je pole chybových zpráv
        if (Array.isArray(backendErrors[key])) {
          backendErrors[key].forEach((msg) => {
            errorList.value.push(`${key}: ${msg}`);
          });
        } else {
          // Pokud by to nebylo pole, přidáme přímo zprávu
          errorList.value.push(`${key}: ${backendErrors[key]}`);
        }
      }
    } else {
      // Obecná chybová zpráva
      errorList.value.push("An unexpected error occurred.");
    }
  }
}
function resetForm() {
  formData.email = "";
  formData.password = "";
}
</script>

<style scoped></style>
