import React from "react";
import { FiEdit, FiTrash } from "react-icons/fi";

const isDate = (value) => {
  return (
    value instanceof Date ||
    (typeof value === "string" && !isNaN(Date.parse(value)))
  );
};

const Table = ({ headers, datos, onEdit, onDelete }) => {
  return (
    <table className="w-full border-collapse border border-gray-200">
      <thead className="bg-gray-200">
        <tr>
          {headers.map((header) => (
            <th
              key={header}
              className="border border-gray-300 px-4 py-2 text-left"
            >
              {header.charAt(0).toUpperCase() + header.slice(1)}
            </th>
          ))}

          <th className="border border-gray-300 px-4 py-2 text-left">
            Acciones
          </th>
        </tr>
      </thead>
      <tbody>
        {datos &&
          Array.isArray(datos) &&
          datos.map((row, index) => (
            <tr key={index} className="even:bg-gray-100">
              {headers.map((header) => (
                <td key={header} className="border border-gray-300 px-4 py-2">
                  {isDate(row[header])
                    ? new Date(row[header]).toISOString().split("T")[0]
                    : row[header]}
                </td>
              ))}

              <td className="border border-gray-300 px-4 py-2">
                <div className="flex space-x-2">
                  <button
                    onClick={() => onEdit(row)}
                    className="text-blue-600 hover:text-blue-800"
                    title="Editar"
                  >
                    <FiEdit size={16} />
                  </button>
                  <button
                    onClick={() => onDelete(row)}
                    className="text-red-600 hover:text-red-800"
                    title="Eliminar"
                  >
                    <FiTrash size={16} />
                  </button>
                </div>
              </td>
            </tr>
          ))}
      </tbody>
    </table>
  );
};

export default Table;
