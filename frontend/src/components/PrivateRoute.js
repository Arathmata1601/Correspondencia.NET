import React from 'react';
import { Navigate } from 'react-router-dom';

const PrivateRoute = ({ children, allowedRole }) => {
  const token = localStorage.getItem('token');
  const rol = localStorage.getItem('rol');
const allowedRoles = allowedRole.split(',').map(r => r.trim().toLowerCase());

if (!token || !allowedRoles.includes(rol?.toLowerCase())) {
  console.warn("Acceso denegado: redireccionando...");
  return <Navigate to="/403" />;
}

  return children;
};

export default PrivateRoute;
