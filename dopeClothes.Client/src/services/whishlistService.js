import axios from "axios";
import { reactive, ref } from "vue";
export default {
  async addToWishlist(productId) {
    try {
      const response = await axios.post(
        "https://dopeclothesserver.azurewebsites.net/Wish/InsertProduct/" +
          productId,
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
        "https://dopeclothesserver.azurewebsites.net/Wish/GetMyWishList/",

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
        "https://dopeclothesserver.azurewebsites.net/Wish/DeleteProductFromWishList/" +
          productId,

        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};
