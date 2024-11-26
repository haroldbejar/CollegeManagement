import React, { useState, useContext, useEffect } from "react";
import Form from "../components/Form";
import Table from "../components/Table";
import endPoints from "../endpoints/enpoint";
import { FetchContext } from "../context/fecth.context";
import { UserContext } from "../context/user.context";

const AlumnosPorGrado = () => {
  const { post, get, put, remove, data, setData } = useContext(FetchContext);
  const { token } = useContext(UserContext);
  const [editingAlumGrados, setEditingAlumGrados] = useState(null);
  const [isEdit, setIsEdit] = useState(false);

  const formFields = [
    {
      name: "alumnoId",
      label: "Alumno",
      placeholder: "Seleccione un alumno",
      type: "select",
      options: (data.alumnos || []).map((alumno) => ({
        label: alumno.nombre,
        value: alumno.id,
      })),
    },
    {
      name: "gradoId",
      label: "Grado",
      placeholder: "Seleccione un grado",
      type: "select",
      options: (data.grados || []).map((grado) => ({
        label: grado.nombre,
        value: grado.id,
      })),
    },
    {
      name: "grupo",
      label: "Grupo",
      placeholder: "Ingrese el grupo",
      type: "text",
    },
  ];

  const tableHeaders = ["alumno", "grado", "grupo"];

  const handleAddAsignacion = async (formData) => {
    try {
      if (editingAlumGrados) {
        await put(
          `${endPoints.alumnosGrados.base}/${editingAlumGrados.id}`,
          formData,
          token,
          "alumnosGrados"
        );
      } else {
        await post(
          endPoints.alumnosGrados.base,
          formData,
          token,
          "alumnosGrados"
        );
      }
      setEditingAlumGrados(null);
      await get(`${endPoints.alumnosGrados.list}/1/15`, token, "alumnosGrados");
    } catch (error) {
      console.error(error);
    }
  };

  const handleEditAlumGrados = async (asignacion) => {
    setEditingAlumGrados(asignacion);
    setIsEdit(true);
  };

  const handleDeleteAsignacion = async (asignacion) => {
    try {
      await remove(
        `${endPoints.alumnosGrados.base}/${asignacion.id}`,
        token,
        "alumnosGrados",
        asignacion.id
      );
    } catch (error) {
      console.error(error);
    }
  };

  const fetchAsignacion = () => {
    try {
      get(`${endPoints.alumnosGrados.list}/1/15`, token, "alumnosGrados");
    } catch (error) {
      console.error("Error al obtener asignaciones", error);
    }
  };

  const fetchAlumnos = () => {
    try {
      get(`${endPoints.alumnos.list}/1/15`, token, "alumnos");
    } catch (error) {
      console.error("Error al obtener alumnos", error);
    }
  };

  const fetchGrados = () => {
    try {
      get(`${endPoints.grados.list}/1/15`, token, "grados");
    } catch (error) {
      console.error("Error al obtener grados", error);
    }
  };

  useEffect(() => {
    if (token) {
      fetchAsignacion();
      fetchAlumnos();
      fetchGrados();
    }
  }, [token]);

  const tableData = (data.alumnosGrados || []).map((almGrado) => {
    const alumnos = data.alumnos || [];
    const grados = data.grados || [];
    const alumno = alumnos.find(
      (a) => String(a.id) === String(almGrado.alumnoId)
    );
    const grado = grados.find((g) => String(g.id) === String(almGrado.gradoId));

    return {
      ...almGrado,
      alumno: alumno ? alumno.nombre : "",
      grado: grado ? grado.nombre : "",
    };
  });

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-semibold">Alumnos por Grado</h1>

      <Form
        fields={formFields}
        onSubmit={handleAddAsignacion}
        initialValues={editingAlumGrados || {}}
        editing={isEdit}
      />

      <Table
        headers={tableHeaders}
        datos={tableData}
        onDelete={handleDeleteAsignacion}
        onEdit={handleEditAlumGrados}
      />
    </div>
  );
};

export default AlumnosPorGrado;
