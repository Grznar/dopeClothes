<template>
  <!-- Registration 8 - Bootstrap Brain Component -->
  <section class="bg-light p-3 p-md-4 p-xl-5 bg-white">
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-12 col-xxl-11">
          <div class="card border-light-subtle shadow-sm">
            <div class="row g-0">
              <div class="col-12 col-md-6">
                <img
                  class="img-fluid rounded-start w-100 h-100 object-fit-cover"
                  loading="lazy"
                  src="@/assets/images/register.png"
                  alt="Welcome back you've been missed!"
                />
              </div>
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
                              />
                            </a>
                          </div>
                          <h2 class="h4 text-center">Registration</h2>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col-12">
                        <div class="d-flex gap-3 flex-column">
                          <a href="#!" class="btn btn-lg btn-outline-dark">
                            <svg
                              xmlns="http://www.w3.org/2000/svg"
                              width="16"
                              height="16"
                              fill="currentColor"
                              class="bi bi-google"
                              viewBox="0 0 16 16"
                            >
                              <path
                                d="M15.545 6.558a9.42 9.42 0 0 1 .139 1.626c0 2.434-.87 4.492-2.384 5.885h.002C11.978 15.292 10.158 16 8 16A8 8 0 1 1 8 0a7.689 7.689 0 0 1 5.352 2.082l-2.284 2.284A4.347 4.347 0 0 0 8 3.166c-2.087 0-3.86 1.408-4.492 3.304a4.792 4.792 0 0 0 0 3.063h.003c.635 1.893 2.405 3.301 4.492 3.301 1.078 0 2.004-.276 2.722-.764h-.003a3.702 3.702 0 0 0 1.599-2.431H8v-3.08h7.545z"
                              />
                            </svg>
                            <span class="ms-2 fs-6">Log in with Google</span>
                          </a>
                        </div>
                        <p class="text-center mt-4 mb-5">
                          Or enter your details to register
                        </p>
                      </div>
                    </div>
                    <form v-on:submit.prevent="HandleSubmit">
                      <div class="row gy-3 overflow-hidden">
                        <div class="col-12">
                          <div class="form-floating mb-3">
                            <input
                              type="text"
                              class="form-control"
                              name="Name"
                              id="Name"
                              placeholder="Name"
                              v-model="formData.name"
                              required
                            />
                            <label for="Name" class="form-label">Name</label>
                          </div>
                        </div>
                        <div class="col-12">
                          <div class="form-floating mb-3">
                            <input
                              type="Email"
                              class="form-control"
                              name="Email"
                              id="Email"
                              placeholder="Email"
                              v-model="formData.email"
                              required
                            />
                            <label for="Email" class="form-label">Email</label>
                          </div>
                        </div>
                        <div class="col-12">
                          <div class="form-floating mb-3">
                            <input
                              type="password"
                              class="form-control"
                              name="Password"
                              id="Password"
                              placeholder="name@example.com"
                              required
                              v-model="formData.password"
                            />
                            <label for="Password" class="form-label"
                              >Password</label
                            >
                          </div>
                        </div>
                        <div class="col-12">
                          <div class="form-floating mb-3">
                            <input
                              type="password"
                              class="form-control"
                              name="confirmPassword"
                              id="confirmPassword"
                              value=""
                              placeholder="Confirm Password"
                              required
                              v-model="formData.confirmPassword"
                            />
                            <label for="confirmPassword" class="form-label"
                              >Confirm Password</label
                            >
                          </div>
                        </div>
                        <div class="col-12">
                          <div class="form-check">
                            <input
                              class="form-check-input"
                              type="checkbox"
                              value=""
                              name="iAgree"
                              id="iAgree"
                              v-model="formData.iAgree"
                              required
                            />
                            <label class="form-check-label" for="iAgree">
                              I agree to the
                              <a class="link-primary text-decoration-none"
                                >terms and conditions</a
                              >
                            </label>
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
                            <button class="btn btn-dark btn-lg" type="submit">
                              Register
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
                            :to="{ name: APP_ROUTE_NAMES.LOGIN }"
                          >
                            Already have an account?
                          </RouterLink>
                        </p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
<script setup>
import { useRouter, useRoute } from "vue-router";
import { APP_ROUTE_NAMES } from "@/constants/routenames.js";
import { reactive, ref, computed } from "vue";
import { useSwal } from "@/utility/useSwal.js";
import { useAuthStore } from "@/stores/authstore.js";
const auth = useAuthStore();
const router = useRouter();
const swal = useSwal();
const formData = reactive({
  name: "",
  email: "",
  password: "",
  confirmPassword: "",
  iAgree: false,
});
const errorList = ref([]);

async function HandleSubmit() {
  errorList.value = [];
  if (formData.name.length < 6) {
    errorList.value.push("Name must be at least 6 characters long.");
  } else if (formData.password !== formData.confirmPassword) {
    errorList.value.push("Passwords do not match.");
  } else if (!formData.iAgree) {
    errorList.value.push("You must agree to the terms and conditions.");
  } else if (formData.password.length < 6) {
    errorList.value.push("Password must be at least 6 characters long.");
  }
  if (errorList.value.length === 0) {
    const dataForDb = {
      userName: formData.name,
      email: formData.email,
      password: formData.password,
      confirmPassword: formData.confirmPassword,
    };

    try {
      const response = await auth.register(dataForDb);
      swal.showSuccess("Registration successful!");
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
}
async function resetForm() {
  formData.name = "";
  formData.email = "";
  formData.password = "";
  formData.confirmPassword = "";
  formData.iAgree = false;
}
</script>
