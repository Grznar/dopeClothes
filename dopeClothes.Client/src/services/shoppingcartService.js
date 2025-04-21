import axios from "axios";

export default {
  async insertProductToCart(productId) {
    try {
      const response = await axios.post(
        "https://dopeclothesserver.azurewebsites.net/ShoppingCart/InsertProductToShopCart/" +
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
        "https://dopeclothesserver.azurewebsites.net/ShoppingCart/FetchShopCart",
        { withCredentials: true }
      );

      if (response.data.items.length === 0) {
        return null;
      }
      return response.data.items;
    } catch (error) {
      throw error;
    }
  },
  async decreaseProduct(productId) {
    try {
      const response = await axios.post(
        "https://dopeclothesserver.azurewebsites.net/ShoppingCart/DecreaseProduct/" +
          productId,
        {},
        { withCredentials: true }
      );
    } catch (error) {
      throw error;
    }
  },
  async increaseProduct(productId) {
    try {
      const response = await axios.post(
        "https://dopeclothesserver.azurewebsites.net/ShoppingCart/IncreaseProduct/" +
          productId,
        {},
        { withCredentials: true }
      );
    } catch (error) {
      throw error;
    }
  },
};
