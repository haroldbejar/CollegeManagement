import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import { useContext } from "react";
import { UserContext } from "./context/user.context";
import Login from "./pages/Login";
import Home from "./pages/Home";
import Profesores from "./pages/Profesores";
import Alumnos from "./pages/Alumnos";
import Grados from "./pages/Grados";
import AlumnosPorGrado from "./pages/AlumnosPorGrado";

const App = () => {
  const { token } = useContext(UserContext);
  return (
    <Router future={{ v7_startTransition: true, v7_relativeSplatPath: true }}>
      <Routes>
        <Route path="" element={token ? <Navigate to="/home" /> : <Login />} />
        <Route path="/home" element={<Home />}>
          <Route path="profesores" element={<Profesores />} />
          <Route path="alumnos" element={<Alumnos />} />
          <Route path="grados" element={<Grados />} />
          <Route path="alumnos-por-grado" element={<AlumnosPorGrado />} />
        </Route>
      </Routes>
    </Router>
  );
};

export default App;
