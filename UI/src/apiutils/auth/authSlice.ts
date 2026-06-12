import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { LoginResponse } from "./types";

interface AuthState {
  user: LoginResponse | null;
}

const initialState: AuthState = {
  user: null,
};

const authSlice = createSlice({
  name: "auth",
  initialState,

  reducers: {
    setCredentials: (state, action: PayloadAction<LoginResponse>) => {
      state.user = action.payload;

      // store token
      localStorage.setItem("token", action.payload.token);
    },

    logout: (state) => {
      state.user = null;

      localStorage.removeItem("token");
    },
  },
});

export const { setCredentials, logout } = authSlice.actions;

export default authSlice.reducer;
