import { configureStore } from "@reduxjs/toolkit";
import { productService } from "./product/productService";
import productReducer from "./product/productSlice";
import { authService } from "./auth/authService";
import authReducer from "./auth/authSlice";

export const store = configureStore({
  reducer: {
    product: productReducer,
    auth: authReducer,
    [productService.reducerPath]: productService.reducer,
    [authService.reducerPath]: authService.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(productService.middleware)
      .concat(authService.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
