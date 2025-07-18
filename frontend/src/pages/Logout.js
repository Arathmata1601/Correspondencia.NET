import React from "react";
import { useEffect } from 'react';

const Logout = () => {
  useEffect(() => {
    // Ejecutar logout automáticamente al cargar la página
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    sessionStorage.clear();
    
    // Redirigir después de 2 segundos
    setTimeout(() => {
      window.location.href = window.location.origin;
    }, 2000);
  }, []);

  return (
    <div>
      <h2>Cerrando sesión...</h2>
      <p>Serás redirigido en unos segundos.</p>
    </div>
  );
};

export default Logout;