import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { LoginRequest, LoginResponse } from "./types";

export const authService = createApi({
  reducerPath: "authService",

  baseQuery: fetchBaseQuery({
    baseUrl: "https://localhost:7151/",
    prepareHeaders: (headers) => {
      const token = localStorage.getItem("token");

      if (token) {
        headers.set("Authorization", `Bearer ${token}`);
      }

      return headers;
    },
  }),

  endpoints: (builder) => ({
    loginUser: builder.mutation<LoginResponse, LoginRequest>({
      query: (data) => ({
        url: "api/auth/login",
        method: "POST",
        body: data,
      }),
    }),
  }),
});

export const { useLoginUserMutation } = authService;
