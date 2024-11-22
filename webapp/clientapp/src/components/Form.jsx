import React from "react";

const Form = ({ fields, onSubmit, initialValues = {} }) => {
  const handleSubmit = (event) => {
    event.preventDefault();
    const formData = {};
    fields.forEach((field) => {
      formData[field.name] = event.target[field.name].value;
    });
    onSubmit(formData);
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
              defaultValue={initialValues[field.name] || ""}
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
              defaultValue={initialValues[field.name] || ""}
              className="border px-4 py-2 rounded focus:outline-none focus:ring focus:ring-blue-300"
            />
          )}
        </div>
      ))}
      <button
        type="submit"
        className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
      >
        Guardar
      </button>
    </form>
  );
};

export default Form;
