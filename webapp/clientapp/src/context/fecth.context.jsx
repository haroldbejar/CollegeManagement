import { useState, createContext } from "react";

const FetchContext = createContext();

function FetchProvider(props) {
  const [data, setData] = useState({});
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");

  const fetchHandler = async ({ url, method = "GET", body, token }) => {
    try {
      const headers = {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      };

      const options = { method, headers };
      if (body) options.body = JSON.stringify(body);

      const response = await fetch(url, options);
      if (!response.ok) {
        debugger;
        const errorData = await response.json();
        // TODO: crear funcion para desplegar mensajes
        alert(`error: ${errorData.mensaje}`);
        throw new Error(`Error en la solicitud: ${response.status}`);
      }

      const contentType = response.headers.get("Content-Type");
      if (contentType && contentType.includes("application/json")) {
        const dataJson = await response.json();
        setMessage(dataJson.mensaje);
        return dataJson.datos || dataJson;
      }

      return null;
    } catch (err) {
      setError(err.message);
      console.log(err.message);
      throw err;
    }
  };

  const get = async (url, token, resourceKey) => {
    const datos = await fetchHandler({ url, token });

    if (resourceKey) {
      setData((prev) => ({ ...prev, [resourceKey]: datos }));
    } else {
      setData(datos);
    }
    setMessage("Success!");
  };

  const post = async (url, bodyData, token, resourceKey) => {
    const datos = await fetchHandler({
      url,
      method: "POST",
      body: bodyData,
      token,
    });
    if (resourceKey) {
      setData((prev) => ({
        ...prev,
        [resourceKey]: [...(prev[resourceKey] || []), datos],
      }));
    } else {
      setData(datos);
    }
    setMessage("Success!");
  };

  const put = async (url, updateData, token) => {
    await fetchHandler({ url, method: "PUT", body: updateData, token });
    setMessage("Actualizado correctamente!");
  };

  const remove = async (url, token, resourceKey, id) => {
    await fetchHandler({ url, method: "DELETE", token });
    if (resourceKey) {
      setData((prev) => ({
        ...prev,
        [resourceKey]: prev[resourceKey].filter((item) => item.id !== id),
      }));
    }
    setMessage("Eliminado correctamente!");
  };

  return (
    <FetchContext.Provider
      value={{
        data,
        error,
        message,
        get,
        post,
        put,
        remove,
        setData,
      }}
    >
      {props.children}
    </FetchContext.Provider>
  );
}

export { FetchContext, FetchProvider };
