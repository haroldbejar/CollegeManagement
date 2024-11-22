import React, { useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import { FetchContext } from "../context/fecth.context";

const Menu = () => {
  const { setData } = useContext(FetchContext);

  const cleanData = () => {
    setData([]);
  };

  return (
    <aside className="bg-gray-200 w-64 min-h-screen p-6">
      <ul className="space-y-4">
        <li>
          <Link
            to="profesores"
            className="text-lg text-gray-800 hover:text-blue-600"
            onClick={cleanData}
          >
            Profesores
          </Link>
        </li>
        <li>
          <Link
            to="alumnos"
            className="text-lg text-gray-800 hover:text-blue-600"
            onClick={cleanData}
          >
            Alumnos
          </Link>
        </li>
        <li>
          <Link
            to="grados"
            className="text-lg text-gray-800 hover:text-blue-600"
            onClick={cleanData}
          >
            Grados
          </Link>
        </li>
        <li>
          <Link
            to="alumnos-por-grado"
            className="text-lg text-gray-800 hover:text-blue-600"
            onClick={cleanData}
          >
            Alumnos por Grado
          </Link>
        </li>
      </ul>
    </aside>
  );
};

export default Menu;
