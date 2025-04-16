import axios from "axios";
import { reactive, ref } from "vue";
export default {
  async addToWishlist(productId) {
    try {
      const response = await axios.post(
        "https://localhost:7072/Wish/InsertProduct/" + productId,
        {},
        { withCredentials: true }
      );

      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async getAllmyWishes() {
    try {
      const response = await axios.get(
        "https://localhost:7072/Wish/GetMyWishList/",

        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async deleteFromWishlist(productId) {
    try {
      const response = await axios.post(
        "https://localhost:7072/Wish/DeleteProductFromWishList/" + productId,

        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};
