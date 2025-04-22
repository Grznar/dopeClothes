import { defineStore } from "pinia";
import shoppingcartService from "@/services/shoppingcartService";

export const useCartStore = defineStore("cart", {
  state: () => ({
    items: [],
  }),
  getters: {
    totalItemCount: (state) =>
      state.items.reduce((acc, item) => acc + item.count, 0),

    getItemByProductId: (state) => (productId) =>
      state.items.find((item) => item.productId === productId),
  },
  actions: {
    async fetchCartItems() {
      try {
        const response = await shoppingcartService.fetchMyShoppingCart();

        if (response.length > 0) {
          this.items = response;
        } else this.items = null;
      } catch (error) {
        throw error;
      }
    },
    async insertProductToCart(productId) {
      try {
        const response = await shoppingcartService.insertProductToCart(
          productId
        );
        this.fetchCartItems();
        return response;
      } catch (error) {
        throw error;
      }
    },
    async decreaseProduct(productId) {
      try {
        const response = await shoppingcartService.decreaseProduct(productId);
        this.fetchCartItems();
        return response;
      } catch (error) {
        throw error;
      }
    },
    async increaseProduct(productId) {
      try {
        const response = await shoppingcartService.increaseProduct(productId);
        this.fetchCartItems();
        return response;
      } catch (error) {
        throw error;
      }
    },
  },
});
