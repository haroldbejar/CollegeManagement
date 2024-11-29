import React, { useState, useContext, useEffect } from "react";
import Form from "../components/Form";
import Table from "../components/Table";
import endPoints from "../endpoints/enpoint";
import { FetchContext } from "../context/fecth.context";
import { UserContext } from "../context/user.context";
import ModalDialog from "../components/ModalDialog";

const Alumnos = () => {
  const { get, post, put, remove, data, error, message } =
    useContext(FetchContext);
  const { token } = useContext(UserContext);
  const [editingAlumno, setEditingAlumno] = useState(null);
  const [isEdit, setIsEdit] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [selectedAlumno, setSelectedAlumno] = useState(null);

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
      max: new Date().toISOString().split("T")[0],
    },
  ];

  const tableHeaders = ["nombreCompleto", "genero", "fechaNacimiento"];

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
    } catch (err) {
      console.error(`Error al agregar o editar alumno, ${err}`);
    }
  };

  const handleEdit = (alumno) => {
    setEditingAlumno(alumno);
    setIsEdit(true);
  };

  const openModalDelete = (alumno) => {
    setSelectedAlumno(alumno);
    setShowModal(true);
  };

  const handleDeleteAlumno = () => {
    if (selectedAlumno) {
      deleteAlumno(selectedAlumno);
    }
    setShowModal(false);
  };

  const deleteAlumno = async (alumno) => {
    try {
      await remove(
        `${endPoints.alumnos.base}/${alumno.id}`,
        token,
        "alumnos",
        alumno.id
      );
    } catch (err) {
      console.error(err);
    }
  };

  const closeModal = () => {
    setShowModal(false);
  };

  const fetchAlumnos = () => {
    try {
      get(`${endPoints.alumnos.list}/1/15`, token, "alumnos");
    } catch (err) {
      console.error("Error al obtener alumnos", err);
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
        editing={isEdit}
      />

      <Table
        headers={tableHeaders}
        datos={data.alumnos || []}
        onDelete={openModalDelete}
        onEdit={handleEdit}
      />
      {showModal && (
        <ModalDialog
          message={`¿Está seguro de eliminar a "${selectedAlumno.nombre} ${selectedAlumno.apellido}" ? `}
          onConfirm={handleDeleteAlumno}
          onCancel={closeModal}
        />
      )}
      {/* <ModalAlert error={false} message="Algo salio mal" /> */}
    </div>
  );
};

export default Alumnos;
