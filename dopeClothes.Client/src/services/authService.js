import axios from "axios";
import { reactive, ref } from "vue";
export default {
  async registerUser(userObj) {
    try {
      const response = await axios.post(
        "https://localhost:7072/Auth/Register",
        userObj
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async logoutUser() {
    try {
      return await axios.post("https://localhost:7072/Auth/Logout");
    } catch (error) {
      throw error;
    }
  },
  async loginUser(userObj) {
    try {
      const response = await axios.post(
        "https://localhost:7072/Auth/Login",
        userObj
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};
