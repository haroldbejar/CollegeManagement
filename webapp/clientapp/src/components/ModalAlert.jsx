import React, { useState, useEffect } from "react";

const ModalAlert = ({ error, message, timeout = 4000 }) => {
  const [visible, setVisible] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setVisible(false);
    }, timeout);

    return () => clearTimeout(timer);
  }, [timeout]);

  if (!visible) return null;

  return (
    <div
      className={`fixed top-1 right-1 flex items-center justify-center pl-2 pr-2 h-20 w-70 bg-opacity-1 ${
        error ? "bg-red-500 text-white" : "bg-green-500 text-white"
      }`}
    >
      {message}
    </div>
  );
};

export default ModalAlert;
