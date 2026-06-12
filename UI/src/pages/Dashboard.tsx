import React from "react";
import { DashboardLayout } from "../components/Layout/DashboardLayout";
import { StatCard } from "../components/Cards/StatCard";


export const Dashboard : React.FC = () => {
    return(
<div className="grid grid-cols-1 md:grid-cols-4 gap-6">
      <StatCard title="Total Products" value="120" />
      <StatCard title="Orders Today" value="35" />
      <StatCard title="Revenue" value="₹45,000" />
      <StatCard title="Customers" value="1,250" />
    </div>
    )
}