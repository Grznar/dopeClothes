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
                    <div class="row mb-4">
                      <div class="col-12 text-center">
                        <h2 class="h4">Change Password</h2>
                        <p>
                          Please fill out the form below to update your
                          password.
                        </p>
                      </div>
                    </div>
                    <form @submit.prevent="handleChangePassword">
                      <div class="row gy-3">
                        <div class="col-12">
                          <div class="form-floating mb-3">
                            <input
                              type="password"
                              class="form-control"
                              id="oldPassword"
                              placeholder="Old Password"
                              v-model="formData.oldPassword"
                              required
                            />
                            <label for="oldPassword">Old Password</label>
                          </div>
                        </div>
                        <div class="col-12">
                          <div class="form-floating mb-3">
                            <input
                              type="password"
                              class="form-control"
                              id="newPassword"
                              placeholder="New Password"
                              v-model="formData.newPassword"
                              required
                            />
                            <label for="newPassword">New Password</label>
                          </div>
                        </div>
                        <div class="col-12">
                          <div class="form-floating mb-3">
                            <input
                              type="password"
                              class="form-control"
                              id="confirmNewPassword"
                              placeholder="Confirm New Password"
                              v-model="formData.confirmPassword"
                              required
                            />
                            <label for="confirmNewPassword"
                              >Confirm New Password</label
                            >
                          </div>
                        </div>
                        <div class="col-12" v-if="errorList.length > 0">
                          <div class="alert alert-danger">
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
                        <div class="col-12">
                          <div class="d-grid">
                            <button class="btn btn-dark btn-lg" type="submit">
                              Update Password
                            </button>
                          </div>
                        </div>
                      </div>
                    </form>
                  </div>
                </div>
              </div>

              <div class="col-12 col-md-6">
                <img
                  class="img-fluid rounded-end w-100 h-100 object-fit-cover"
                  loading="lazy"
                  src="@/assets/images/password-reset.png"
                  alt="Password update illustration"
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
import { useSwal } from "@/utility/useSwal.js";
import { useRouter } from "vue-router";
import { useAuthStore } from "@/stores/authstore.js";
import { APP_ROUTE_NAMES } from "@/constants/routenames";
const auth = useAuthStore();
const swal = useSwal();
const router = useRouter();

const errorList = ref([]);
const formData = reactive({
  oldPassword: "",
  newPassword: "",
  confirmPassword: "",
});

async function handleChangePassword() {
  errorList.value = [];

  if (formData.newPassword !== formData.confirmPassword) {
    errorList.value.push("New Password and Confirm New Password must match.");
    return;
  }

  try {
    console.log(formData);
    const response = await auth.resetPassword(
      formData.oldPassword,
      formData.newPassword,
      formData.confirmPassword
    );
    swal.showSuccess("Password updated successfully!");
    resetForm();
    router.push({ name: APP_ROUTE_NAMES.HOME });
  } catch (error) {
    if (error.response && error.response.data) {
      const backendErrors = error.response.data;
      for (const key in backendErrors) {
        if (Array.isArray(backendErrors[key])) {
          backendErrors[key].forEach((msg) => {
            errorList.value.push(`${key}: ${msg}`);
          });
        } else {
          errorList.value.push(`${key}: ${backendErrors[key]}`);
        }
      }
    } else {
      errorList.value.push("An unexpected error occurred.");
    }
  }
}

function resetForm() {
  formData.oldPassword = "";
  formData.newPassword = "";
  formData.confirmPassword = "";
}
</script>

<style scoped></style>
