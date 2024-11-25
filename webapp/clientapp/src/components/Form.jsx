import React, { useState, useEffect } from "react";

const Form = ({ fields, onSubmit, initialValues = {}, editing }) => {
  const [formValues, setFormValues] = useState(initialValues);
  const [isEdit, setIsEdit] = useState(false);

  useEffect(() => {
    setFormValues(initialValues);
    setIsEdit(editing ? true : false);
  }, [initialValues]);

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormValues({
      ...formValues,
      [name]: value,
    });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    onSubmit(formValues);
  };

  const handleClean = () => {
    setFormValues((initialValues = {}));
    setIsEdit(false);
  };

  const formatValue = (field) => {
    if (field.type === "date" && formValues[field.name]) {
      return new Date(formValues[field.name]).toISOString().split("T")[0];
    }
    return formValues[field.name] || "";
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="space-y-4 p-4 border rounded bg-gray-100"
    >
      {fields.map((field) => (
        <div key={field.name} className="flex flex-col">
          <label htmlFor={field.name} className="font-semibold">
            {field.label}
          </label>
          {field.type === "select" ? (
            <select
              name={field.name}
              id={field.name}
              className="border px-4 py-2 rounded focus:outline-none focus:ring focus:ring-blue-300"
              value={formatValue(field)}
              onChange={handleChange}
              required
            >
              <option value="" disabled>
                {field.placeholder || "Seleccione una opción"}
              </option>
              {field.options.map((option) => (
                <option key={option.value} value={option.value}>
                  {option.label}
                </option>
              ))}
            </select>
          ) : (
            <input
              type={field.type || "text"}
              name={field.name}
              id={field.name}
              placeholder={field.placeholder || ""}
              value={formatValue(field)}
              onChange={handleChange}
              className="border px-4 py-2 rounded focus:outline-none focus:ring focus:ring-blue-300"
              required
            />
          )}
        </div>
      ))}
      <button
        type="submit"
        className="bg-blue-500 text-white px-4 py-2 mr-4 rounded hover:bg-blue-600"
      >
        {isEdit ? "Actualizar" : "Guardar"}
      </button>
      {isEdit ? (
        <button
          type="submit"
          onClick={handleClean}
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
        >
          Nuevo
        </button>
      ) : (
        <></>
      )}
    </form>
  );
};

export default Form;
