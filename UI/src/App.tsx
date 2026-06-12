import React from "react";
import "./App.css";
import { ProductForm } from "./components/ProductForm/ProductForm";
import { Dashboard } from "./pages/Dashboard";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { DashboardLayout } from "./components/Layout/DashboardLayout";
import { Login } from "./pages/login/Login";
import { useSelector } from "react-redux";
import { RootState } from "./apiutils/Store";

const RequireAuth = ({ children }: any) => {
  const user = useSelector((state: RootState) => state.auth.user);

  if (!user) {
    return <Navigate to="/login" />;
  }

  return children;
};

function App() {
  return (
    <BrowserRouter>
      <Routes>

        {/* Default Page */}
        <Route path="/" element={<Navigate to="/login" />} />

        {/* Login Page */}
        <Route path="/login" element={<Login />} />

        {/* Protected Routes */}
        <Route
          element={
            //<RequireAuth>
              <DashboardLayout />
            //</RequireAuth>
          }
        >
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/products" element={<ProductForm />} />
        </Route>

      </Routes>
    </BrowserRouter>
  );
}

export default App;

