import React, { useState, useContext, useEffect } from "react";
import Form from "../components/Form";
import Table from "../components/Table";
import endPoints from "../endpoints/enpoint";
import { FetchContext } from "../context/fecth.context";
import { UserContext } from "../context/user.context";

const Alumnos = () => {
  const { get, post, put, remove, data, setData } = useContext(FetchContext);
  const { token } = useContext(UserContext);
  const [editingAlumno, setEditingAlumno] = useState(null);

  const formFields = [
    {
      name: "nombre",
      label: "Nombre",
      placeholder: "Ingrese el nombre",
      type: "text",
    },
    {
      name: "apellido",
      label: "Apellido",
      placeholder: "Ingrese el apellido",
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
    {
      name: "fechaNacimiento",
      label: "Fecha de Nacimiento",
      placeholder: "",
      type: "date",
    },
  ];

  const tableHeaders = ["nombre", "apellido", "genero", "fechaNacimiento"];

  const handleAddAlumno = async (formData) => {
    try {
      if (editingAlumno) {
        await put(
          `${endPoints.alumnos.base}/${editingAlumno.id}`,
          formData,
          token,
          "alumnos"
        );
      } else {
        await post(endPoints.alumnos.base, formData, token, "alumnos");
      }
      setEditingAlumno(null);
      await get(`${endPoints.alumnos.list}/1/15`, token, "alumnos");
    } catch (error) {
      console.error("Error al agregar o editar alumno", error);
    }
  };

  const handleEdit = (alumno) => {
    setEditingAlumno(alumno);
  };

  const handleDeleteAlumno = async (alumno) => {
    try {
      await remove(
        `${endPoints.alumnos.base}/${alumno.id}`,
        token,
        "alumnos",
        alumno.id
      );
    } catch (error) {
      console.error(error);
    }
  };

  const fetchAlumnos = () => {
    try {
      get(`${endPoints.alumnos.list}/1/15`, token, "alumnos");
    } catch (error) {
      console.error("Error al obtener alumnos", error);
    }
  };

  useEffect(() => {
    if (token) fetchAlumnos();
  }, [token]);

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-semibold">Alumnos</h1>

      <Form
        fields={formFields}
        onSubmit={handleAddAlumno}
        initialValues={editingAlumno || {}}
      />

      <Table
        headers={tableHeaders}
        datos={data.alumnos || []}
        onDelete={handleDeleteAlumno}
        onEdit={handleEdit}
      />
    </div>
  );
};

export default Alumnos;