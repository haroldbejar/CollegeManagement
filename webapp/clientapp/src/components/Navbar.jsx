import React from "react";
import { Link } from "react-router-dom";

const Navbar = ({ username, onLogout }) => {
  return (
    <nav className="bg-gray-800 text-white w-full flex justify-between items-center px-6 py-3">
      <div className="text-2xl font-semibold">
        <Link to="/">College Management</Link>
      </div>

      <div className="flex items-center space-x-4">
        <span>{username}</span>
        <button
          onClick={onLogout}
          className="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded"
        >
          Logout
        </button>
      </div>
    </nav>
  );
};

export default Navbar;
