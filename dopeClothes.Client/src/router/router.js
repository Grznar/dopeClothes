import { APP_ROUTE_NAMES } from "@/constants/routeNames";
import { createRouter, createWebHistory } from "vue-router";
import Home from "@/views/others/Home.vue";
import ProductList from "@/views/Product/ProductList.vue";
import CreateProduct from "@/views/Product/CreateProduct.vue";
import DetailsProduct from "@/views/Product/DetailsProduct.vue";
import ProductsHome from "@/views/Product/ProductsHome.vue";
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: "/", component: Home, name: APP_ROUTE_NAMES.HOME },
    {
      path: "/create-product",
      component: CreateProduct,
      name: APP_ROUTE_NAMES.CREATE_PRODUCT,
    },
    {
      path: "/product-list",
      component: ProductList,
      name: APP_ROUTE_NAMES.PRODUCT_LIST,
    },
    {
      path: "/product-details/:id",
      component: DetailsProduct,
      name: APP_ROUTE_NAMES.DETAILS_PRODUCT,
      props: true,
    },
    {
      path: "/product-home",
      component: ProductsHome,
      name: APP_ROUTE_NAMES.PRODUCTS_HOME,
      props: true,
    },
  ],
});

export default router;
