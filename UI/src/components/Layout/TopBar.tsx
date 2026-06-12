import React from "react";
import { useNavigate } from "react-router-dom";

export const TopBar: React.FC = () => {
  const navigate = useNavigate();
  const handleLogout = () => {
    // Implement logout logic here (e.g., clear auth tokens, update state)
    alert("Logout clicked");
    navigate("/login");
  };
  return (
    <header className="flex items-center justify-between bg-white shadow px-6 h-16">
      <h1 className="text-xl font-semibold text-gray-700">🛒 Shop Dashboard</h1>

      <div className="flex items-center gap-4">
        <span className="text-gray-600">Admin</span>
        <button
          className="bg-red-500 hover:bg-red-600 text-white px-4 py-1 rounded"
          onClick={handleLogout}
        >
          Logout
        </button>
      </div>
    </header>
  );
};
