import axios from "axios";

export default {
  async createProduct(productObj) {
    try {
      const response = await axios.post(
        "https://dopeclothesserver.azurewebsites.net/Product/Create",
        productObj,
        {
          withCredentials: true,
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async updateProduct(productObj) {
    try {
      const response = await axios.post(
        "https://dopeclothesserver.azurewebsites.net/Product/Update",
        productObj,
        {
          withCredentials: true,
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async getAllProducts() {
    try {
      const response = await axios.get(
        "https://dopeclothesserver.azurewebsites.net/Product/GetAll",
        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async getProduct(id) {
    try {
      const response = await axios.get(
        "https://dopeclothesserver.azurewebsites.net/Product/Get/" + id,
        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async deleteProduct(id) {
    try {
      const response = await axios.delete(
        "https://dopeclothesserver.azurewebsites.net/Product/Delete/" + id,
        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};
