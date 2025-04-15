import { APP_ROUTE_NAMES } from "@/constants/routeNames";
import { createRouter, createWebHistory } from "vue-router";
import Home from "@/views/others/Home.vue";
import ProductList from "@/views/Product/ProductList.vue";
import CreateProduct from "@/views/Product/CreateProduct.vue";
import DetailsProduct from "@/views/Product/DetailsProduct.vue";
import ProductsHome from "@/views/Product/ProductsHome.vue";
import Register from "@/views/Auth/RegisterForm.vue";
import Login from "@/views/Auth/LoginForm.vue";
import { useAuthStore } from "@/stores/authstore";
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: "/", component: Home, name: APP_ROUTE_NAMES.HOME },
    {
      path: "/create-product",
      component: CreateProduct,
      name: APP_ROUTE_NAMES.CREATE_PRODUCT,
      meta: { requiresAuth: true, requiresAdmin: true },
    },
    {
      path: "/product-list",
      component: ProductList,
      name: APP_ROUTE_NAMES.PRODUCT_LIST,
      meta: { requiresAuth: true, requiresAdmin: true },
    },
    {
      path: "/product-details/:id",
      component: DetailsProduct,
      name: APP_ROUTE_NAMES.DETAILS_PRODUCT,
      meta: { requiresAuth: true, requiresAdmin: true },
      props: true,
    },
    {
      path: "/product-home",
      component: ProductsHome,
      name: APP_ROUTE_NAMES.PRODUCTS_HOME,
      meta: { requiresAuth: true },
      props: true,
    },
    {
      path: "/register",
      component: Register,
      name: APP_ROUTE_NAMES.REGISTER,
      meta: { requiresGuest: true },
    },
    {
      path: "/login",
      component: Login,
      name: APP_ROUTE_NAMES.LOGIN,
      meta: { requiresGuest: true },
    },
  ],
});

router.beforeEach((to, from, next) => {
  const auth = useAuthStore();

  if (to.meta.requiresAuth && !auth.isLoggedIn) {
    next({ name: APP_ROUTE_NAMES.LOGIN });
  } else if (to.meta.requiresAdmin && auth.role !== "Admin") {
    next({ name: APP_ROUTE_NAMES.HOME });
  } else if (to.meta.requiresGuest && auth.isLoggedIn) {
    next({ name: APP_ROUTE_NAMES.HOME });
  } else {
    next();
  }
});
export default router;
