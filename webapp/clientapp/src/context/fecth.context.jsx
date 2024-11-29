import { useState, createContext } from "react";
import ModalAlert from "../components/ModalAlert";

const FetchContext = createContext();

function FetchProvider(props) {
  const [data, setData] = useState({});
  const [error, setError] = useState(false);
  const [msg, setMsg] = useState(""); // TODO: delete this function
  const [modalConfig, setModalConfig] = useState({
    isVisible: false,
    isError: false,
    message: "",
  });

  const showModalAlert = (errorData) => {
    let isError = errorData ? true : false;
    setModalConfig((prev) => ({
      ...prev,
      isVisible: false,
    }));

    setTimeout(() => {
      setModalConfig({
        isVisible: true,
        isError,
        message: errorData || "Ha ocurrido un error",
      });
    });
  };

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
        const errorData = await response.json();
        showModalAlert(errorData.mensaje);
        throw new Error(`Error en la solicitud: ${response.status}`);
      }

      const contentType = response.headers.get("Content-Type");
      if (contentType && contentType.includes("application/json")) {
        const dataJson = await response.json();
        return dataJson.datos || dataJson;
      }

      return null;
    } catch (err) {
      setError(true);
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
    // TODO: change to showModalAlert
    setMsg("Registro grabado!");
  };

  const put = async (url, updateData, token) => {
    await fetchHandler({ url, method: "PUT", body: updateData, token });
    // TODO: change to showModalAlert
    setMsg("Actualizado correctamente!");
  };

  const remove = async (url, token, resourceKey, id) => {
    await fetchHandler({ url, method: "DELETE", token });
    if (resourceKey) {
      setData((prev) => ({
        ...prev,
        [resourceKey]: prev[resourceKey].filter((item) => item.id !== id),
      }));
    }
    // TODO: change to showModalAlert
    setMsg("Eliminado correctamente!");
  };

  return (
    <FetchContext.Provider
      value={{
        data,
        error,
        get,
        post,
        put,
        remove,
        setData,
      }}
    >
      {props.children}
      {modalConfig.isVisible && (
        <ModalAlert error={modalConfig.isError} message={modalConfig.message} />
      )}
    </FetchContext.Provider>
  );
}

export { FetchContext, FetchProvider };
