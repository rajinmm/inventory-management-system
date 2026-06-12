import React from "react";
import { SideBar } from "./SideBar";
import { TopBar } from "./TopBar";
import { Outlet } from "react-router-dom";

interface Props {
  children: React.ReactNode;
}

export const DashboardLayout: React.FC = () => {
  return (
    <div className="flex min-h-screen">
      <SideBar />

      <div className="flex-1 bg-gray-100">
        <TopBar />
        <main className="p-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
};
