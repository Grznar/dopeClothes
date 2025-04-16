import axios from "axios";

export default {
  async createProduct(productObj) {
    try {
      const response = await axios.post(
        "https://localhost:7072/Product/Create",
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
        "https://localhost:7072/Product/Update",
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
        "https://localhost:7072/Product/GetAll",
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
        "https://localhost:7072/Product/Get/" + id,
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
        "https://localhost:7072/Product/Delete/" + id,
        { withCredentials: true }
      );
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};
