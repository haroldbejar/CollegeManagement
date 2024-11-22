import React, { useState, useContext, useEffect } from "react";
import Form from "../components/Form";
import Table from "../components/Table";
import endPoints from "../endpoints/enpoint";
import { FetchContext } from "../context/fecth.context";
import { UserContext } from "../context/user.context";

const Profesores = () => {
  const { post, get, put, remove, data } = useContext(FetchContext);
  const { token } = useContext(UserContext);
  const [editingProfesor, setEditingProfesor] = useState(null);

  const formFields = [
    {
      name: "nombre",
      label: "Nombre",
      placeholder: "Ingrese el nombre",
      type: "text",
    },
    {
      name: "genero",
      label: "Género",
      placeholder: "Seleccione un género",
      type: "select",
      options: [
        { label: "Masculino", value: "Masculino" },
        { label: "Femenino", value: "Femenino" },
      ],
    },
  ];

  const tableHeaders = ["nombre", "genero"];

  const handleAddProfesor = async (formData) => {
    try {
      if (editingProfesor) {
        await put(
          `${endPoints.profesores.base}/${editingProfesor.id}`,
          formData,
          token,
          "profesores"
        );
      } else {
        await post(endPoints.profesores.base, formData, token, "profesores");
      }
      setEditingProfesor(null);
      get(`${endPoints.profesores.list}/1/15`, token, "profesores");
    } catch (error) {
      console.error("Error al agregar profesor", error);
    }
  };

  const handleEditProfesor = (profesor) => {
    setEditingProfesor(profesor);
  };

  const handleDeleteProfesor = async (profesor) => {
    try {
      await remove(
        `${endPoints.profesores.base}/${profesor.id}`,
        token,
        "profesores",
        profesor.id
      );
    } catch (error) {
      console.error("Error al eliminar profesor", error);
    }
  };

  const fetchProfesores = () => {
    try {
      get(`${endPoints.profesores.list}/1/15`, token, "profesores");
    } catch (error) {
      console.error("Error al obtener profesores", error);
    }
  };

  useEffect(() => {
    if (token) fetchProfesores();
  }, [token]);

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-semibold">Profesores</h1>

      <Form
        fields={formFields}
        onSubmit={handleAddProfesor}
        initialValues={editingProfesor || {}}
      />

      <Table
        headers={tableHeaders}
        datos={data.profesores || []}
        onEdit={handleEditProfesor}
        onDelete={handleDeleteProfesor}
      />
    </div>
  );
};

export default Profesores;
