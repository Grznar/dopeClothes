import axios from "axios";
import { reactive, ref } from "vue";
export default {
  async registerUser(userObj) {
    try {
      const response = await axios.post(
        "https://dopeclothesserver.azurewebsites.net/Auth/Register",
        userObj
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async logoutUser() {
    try {
      return await axios.post(
        "https://dopeclothesserver.azurewebsites.net/Auth/Logout",
        {
          withCredentials: true,
        }
      );
    } catch (error) {
      throw error;
    }
  },
  async loginUser(userObj) {
    try {
      const response = await axios.post(
        "https://dopeclothesserver.azurewebsites.net/Auth/Login",
        userObj
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async resetPassword(oldPassword, newPassword, confirmPassword) {
    try {
      const response = await axios.post(
        "https://dopeclothesserver.azurewebsites.net/Auth/ResetPassword",
        {
          oldPassword: oldPassword,
          newPassword: newPassword,
          confirmPassword: confirmPassword,
        },
        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};
