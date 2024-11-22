import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../context/user.context";
import endPoints from "../endpoints/enpoint";

const Login = () => {
  const { user, login } = useContext(UserContext);
  const [username, setUsername] = useState("");
  const [password, setPassWord] = useState("");
  const [role, setRole] = useState("");
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    const url = `${endPoints.acount.login}`;

    try {
      const response = await fetch(url, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, password, role }),
      });

      if (response.ok) {
        const dataJson = await response.json();
        const { token, userName, role } = dataJson;
        login(token, userName, role);
        navigate("/home");
      } else {
        throw new Error(`Error en la autenticación ${response.status}`);
      }
    } catch (error) {
      setError(`Las credenciales no son válidas ${error.message}`);
    }
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      {user ? (
        <div className="p-6 bg-white shadow-md rounded-md text-center">
          <h2 className="text-2xl font-semibold mb-4">
            Bienvenido, {user.name}!
          </h2>
          <p className="text-gray-700">Email: {user.email}</p>
          <p className="text-gray-700">
            Token: <span className="text-sm break-all">{user.token}</span>
          </p>
        </div>
      ) : (
        <form
          onSubmit={handleSubmit}
          className="bg-white p-8 rounded-lg shadow-lg max-w-md w-full space-y-6"
        >
          <h2 className="text-2xl front-bold text-center mb-6">
            Iniciar Sesión
          </h2>
          <div>
            <label htmlFor="name" className="block text-gray-700">
              Nombre Usuario:
            </label>
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
              className="mt-1 w-full px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-400 focus:outline-none"
            />
          </div>
          <div>
            <label htmlFor="name" className="block text-gray-700">
              Contraseña:
            </label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassWord(e.target.value)}
              required
              className="mt-1 w-full px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-400 focus:outline-none"
            />
          </div>

          {error && <p className="text-red-500 text-sm mt-2">{error}</p>}
          <button
            type="submit"
            className="w-full bg-blue-500 hover-bg-blue-600 text-white font-bold py-2 px-4 rounded-md transition duration-300"
          >
            Iniciar Sesión
          </button>
        </form>
      )}
    </div>
  );
};

export default Login;
