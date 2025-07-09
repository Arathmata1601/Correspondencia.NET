import React from 'react';
import { Navigate } from 'react-router-dom';

const PrivateRoute = ({ children, allowedRole }) => {
  const token = localStorage.getItem('token');
  const rol = localStorage.getItem('rol');

  if (!token || rol !== allowedRole) {
    return <Navigate to="/" />;
  }

  return children;
};

export default PrivateRoute;
