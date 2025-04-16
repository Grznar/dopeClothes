import axios from "axios";

export default {
  async insertProductToCart(productId) {
    try {
      const response = await axios.post(
        "https://localhost:7072/ShoppingCart/InsertProductToShopCart/" +
          productId,
        {},
        { withCredentials: true }
      );

      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async fetchMyShoppingCart() {
    try {
      const response = await axios.get(
        "https://localhost:7072/ShoppingCart/FetchShopCart",
        { withCredentials: true }
      );
      console.log(response);
      return response.data.items;
    } catch (error) {
      throw error;
    }
  },
};
