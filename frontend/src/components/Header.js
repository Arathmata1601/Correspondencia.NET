import React from "react";
import '../css/Header.css';


const Header = () => {
  
  return (
    <div className="Header">
        <img className="logo" src="/Images/LOGO-ESTADO.png"/>
        <a className="titulo">CORRESPONDENCIA</a>
        <img className="logo" src="/Images/SEDIF.png"/>
    </div>
  );
};

export default Header;
