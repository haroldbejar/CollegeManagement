import React, { useState, useContext } from "react";
import Navbar from "../components/Navbar";
import Menu from "../components/Menu";
import { Outlet, useNavigate } from "react-router-dom";
import { UserContext } from "../context/user.context";

const Home = () => {
  const { user, userRole, logout } = useContext(UserContext);
  const navigate = useNavigate();

  const handleLogout = () => {
    navigate("/");
    logout();
  };

  return (
    <div className="flex flex-col h-screen">
      <Navbar username={`${user} (${userRole})`} onLogout={handleLogout} />
      <div className="flex flex-1">
        <Menu />

        <main className="flex-1 p-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default Home;
