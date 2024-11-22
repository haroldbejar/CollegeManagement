const baseUrl = "http://localhost:5246/api/";
const endPoints = {
  alumnos: {
    base: `${baseUrl}Alumno`,
    list: `${baseUrl}Alumno/getall`,
  },
  profesores: {
    base: `${baseUrl}Profesor`,
    list: `${baseUrl}Profesor/getall`,
  },
  grados: {
    base: `${baseUrl}Grado`,
    list: `${baseUrl}Grado/getall`,
  },
  alumnosGrados: {
    base: `${baseUrl}AlumnoGrado`,
    list: `${baseUrl}AlumnoGrado/getall`,
  },
  usuarios: {},
  acount: {
    login: `${baseUrl}Acount/login`,
    register: {},
  },
};

export default endPoints;
