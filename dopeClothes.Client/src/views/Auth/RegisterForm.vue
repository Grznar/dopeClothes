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
                    <div class="row"></div>
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
