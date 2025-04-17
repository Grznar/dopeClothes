import { APP_ROUTE_NAMES } from "@/constants/routeNames";
import { createRouter, createWebHistory } from "vue-router";
import Home from "@/views/others/Home.vue";
import ProductList from "@/views/Product/ProductList.vue";
import CreateProduct from "@/views/Product/CreateProduct.vue";
import DetailsProduct from "@/views/Product/DetailsProduct.vue";
import ProductsHome from "@/views/Product/ProductsHome.vue";
import Register from "@/views/Auth/RegisterForm.vue";
import Login from "@/views/Auth/LoginForm.vue";
import Contact from "@/views/others/Contact.vue";
import ProductCardDetails from "@/views/Product/ProductCardDetails.vue";
import { useAuthStore } from "@/stores/authstore";
import Personal from "@/views/others/Personal.vue";
import ChangePassword from "@/views/Auth/ChangePassword.vue";
import ShoppingCart from "@/views/ShoppingCarts_Orders/ShoppingCart.vue";
import Order from "@/views/ShoppingCarts_Orders/Order.vue";
import OrdersList from "@/views/others/OrdersList.vue";
import MyOrder from "@/views/others/MyOrder.vue";
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: "/", component: Home, name: APP_ROUTE_NAMES.HOME },
    { path: "/contact", component: Contact, name: APP_ROUTE_NAMES.CONTACT },
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
      path: "/product-card-detials/:id",
      component: ProductCardDetails,
      name: APP_ROUTE_NAMES.PRODUCTCARDDETAILS,
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
    {
      path: "/personal",
      component: Personal,
      name: APP_ROUTE_NAMES.PERSONAL,
      meta: { requiresAuth: true },
    },
    {
      path: "/change-password",
      component: ChangePassword,
      name: APP_ROUTE_NAMES.CHANGE_PASSWORD,
      meta: { requiresAuth: true },
    },
    {
      path: "/shopping-cart",
      component: ShoppingCart,
      name: APP_ROUTE_NAMES.SHOPPING_CART,
      meta: { requiresAuth: true },
    },
    {
      path: "/order/:cartId",
      component: Order,
      name: APP_ROUTE_NAMES.ORDER,
      meta: { requiresAuth: true },
      props: true,
    },
    {
      path: "/my-order",
      component: MyOrder,
      name: APP_ROUTE_NAMES.MY_ORDER,
      meta: { requiresAuth: true },
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
