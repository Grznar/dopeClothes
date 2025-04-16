import { defineStore } from "pinia";
import authService from "@/services/authService";
import { computed } from "vue";
export const useAuthStore = defineStore("auth", {
  state: () => ({
    user: null,
    role: null,
  }),
  getters: {
    isLoggedIn: (state) => !!state.user,
    hasRole: (state) => (role) => state.role === role,
  },
  actions: {
    async register(registerData) {
      try {
        const response = await authService.registerUser(registerData);

        this.user = response.user;
        this.role = response.user.role || null;
        return response;
      } catch (error) {
        throw error;
      }
    },
    async logout() {
      try {
        await authService.logoutUser();
        this.user = null;
        this.role = null;
        router.push({ name: "LOGIN" });
        return response;
      } catch (error) {
        throw error;
      }
    },
    async login(userObj) {
      try {
        const response = await authService.loginUser(userObj);
        this.user = response.user;
        this.role = response.user.role || null;
        return response;
      } catch (error) {
        throw error;
      }
    },
    async resetPassword(oldPassword, newPassword, confirmPassword) {
      try {
        const response = await authService.resetPassword(
          oldPassword,
          newPassword,
          confirmPassword
        );
        return response;
      } catch (error) {
        throw error;
      }
    },
  },
});
