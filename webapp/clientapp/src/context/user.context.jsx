import { createContext, useState } from "react";

const UserContext = createContext();

function UserProvider(props) {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(null);
  const [userRole, setUserRole] = useState(null);

  const login = (userToken, userName, role) => {
    setToken(userToken);
    setUser(userName);
    setUserRole(role);
  };

  const logout = () => {
    setUser(null);
    setToken(null);
    setUserRole(null);
  };

  return (
    <UserContext.Provider value={{ user, token, userRole, login, logout }}>
      {props.children}
    </UserContext.Provider>
  );
}

export { UserContext, UserProvider };
