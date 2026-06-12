import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { ProductSaveRequest, ProductSaveResponse } from "./types";

export const productService = createApi({
  reducerPath: "productService",

  baseQuery: fetchBaseQuery({
    baseUrl: "https://localhost:7151", // ⭐ your .NET API base URL
    prepareHeaders: (headers) => {
      headers.set("Content-Type", "application/json");
      return headers;
    },
  }),

  endpoints: (build) => ({
    saveProduct: build.mutation<ProductSaveResponse, ProductSaveRequest>({
      query: (productData) => ({
        url: "/shop/product/save",
        method: "POST",
        body: productData,
      }),
    }),
  }),
});

export const { useSaveProductMutation } = productService;
