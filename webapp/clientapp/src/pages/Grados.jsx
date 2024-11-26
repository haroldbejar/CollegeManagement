import React, { useState, useContext, useEffect } from "react";
import Form from "../components/Form";
import Table from "../components/Table";
import endPoints from "../endpoints/enpoint";
import { FetchContext } from "../context/fecth.context";
import { UserContext } from "../context/user.context";

const Grados = () => {
  const { post, get, put, remove, data, setData } = useContext(FetchContext);
  const { token } = useContext(UserContext);
  const [editingGrado, setEditingGrado] = useState(null);
  const [isEdit, setIsEdit] = useState(false);

  const formFields = [
    {
      name: "nombre",
      label: "Nombre",
      placeholder: "Ingrese el nombre del grado",
      type: "text",
    },
    {
      name: "profesorId",
      label: "Profesor",
      placeholder: "Seleccione un profesor",
      type: "select",
      options: (data.profesores || []).map((profesor) => ({
        label: profesor.nombre,
        value: profesor.id,
      })),
    },
  ];

  const tableHeaders = ["nombre", "profesor"];

  const handleAddGrado = async (formData) => {
    try {
      if (editingGrado) {
        await put(
          `${endPoints.grados.base}/${editingGrado.id}`,
          formData,
          token,
          "grados"
        );
      } else {
        await post(endPoints.grados.base, formData, token, "grados");
      }
      setEditingGrado(null);
      get(`${endPoints.grados.list}/1/15`, token, "grados");
    } catch (error) {
      console.error("Error al agregar grado", error);
    }
  };

  const handleEditGrado = (grado) => {
    setEditingGrado(grado);
    setIsEdit(true);
  };

  const handleDeleteGrado = async (grado) => {
    try {
      await remove(
        `${endPoints.grados.base}/${grado.id}`,
        token,
        "grados",
        grado.id
      );
    } catch (error) {
      console.error(error);
    }
  };

  const fetchGrados = () => {
    try {
      get(`${endPoints.grados.list}/1/15`, token, "grados");
    } catch (error) {
      console.error("Error al obtener grados", error);
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
    if (token) {
      fetchGrados();
      fetchProfesores();
    }
  }, [token]);

  const tableData = (data.grados || []).map((grado) => {
    const profesor = (data.profesores || []).find(
      (p) => p.id === grado.profesorId
    );
    return {
      ...grado,
      profesor: profesor ? profesor.nombre : "No asignado",
    };
  });

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-semibold">Grados</h1>

      <Form
        fields={formFields}
        onSubmit={handleAddGrado}
        initialValues={editingGrado || {}}
        editing={isEdit}
      />

      <Table
        headers={tableHeaders}
        datos={tableData}
        onEdit={handleEditGrado}
        onDelete={handleDeleteGrado}
      />
    </div>
  );
};

export default Grados;
