import React from "react";
import '../css/Header.css';


const Header = () => {
  
  return (
    <div className="Header">
        <img className="logo" src="data:image/png;base64,<?=obtenerImagen1($conexion)?>"/>
        <a className="titulo">CORRESPONDENCIA</a>
        <img className="logo" src="data:image/png;base64,<?=obtenerImagen2($conexion)?>"/>
    </div>
  );
};

export default Header;
