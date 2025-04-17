import axios from "axios";

const API_BASE = "https://localhost:7072";

export default {
  async getAllOrders() {
    try {
      const response = await axios.get(`${API_BASE}/Order/GetAllOrders`, {
        withCredentials: true,
      });

      return response.data;
    } catch (error) {
      console.error("getAllOrders error:", error);
      throw error;
    }
  },

  async getOrder(cartId) {
    try {
      const response = await axios.get(`${API_BASE}/Order/GetOrder/${cartId}`, {
        withCredentials: true,
      });
      return response.data.order;
    } catch (error) {
      console.error("getOrder error:", error);
      throw error;
    }
  },

  async placeOrder(orderId) {
    try {
      const response = await axios.post(
        `${API_BASE}/Order/PlaceOrder/${orderId}`,
        {},
        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      console.error("placeOrder error:", error);
      throw error;
    }
  },

  async updateOrderStatus(orderId, status) {
    try {
      const response = await axios.put(
        `${API_BASE}/Order/UpdateOrderStatus/${orderId}`,
        { status },
        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      console.error("updateOrderStatus error:", error);
      throw error;
    }
  },
  async getMyOrders() {
    try {
      const { data } = await axios.get(`${API_BASE}/Order/GetMyOrders`, {
        withCredentials: true,
      });
      return data;
    } catch (error) {
      console.error("getMyOrders error:", error);
      throw error;
    }
  },
};
