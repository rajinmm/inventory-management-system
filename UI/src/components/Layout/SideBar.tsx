import React from "react";
import { NavLink } from "react-router-dom";


export const SideBar: React.FC = () => {
  const linkClass = ({ isActive }: { isActive: boolean }) =>
    `block p-2 rounded hover:bg-gray-700 ${isActive ? "bg-gray-700" : ""}`;

  return (
    <aside className="w-60 bg-gray-800 text-white min-h-screen p-5">
      <div className="font-bold text-lg mb-6">Menu</div>

      <nav className="space-y-2">
        <NavLink to="/" className={linkClass}>
          Dashboard
        </NavLink>
        <NavLink to="/products" className={linkClass}>
          Products
        </NavLink>
        <NavLink to="/orders" className={linkClass}>
          Orders
        </NavLink>
      </nav>
    </aside>
  );
};
