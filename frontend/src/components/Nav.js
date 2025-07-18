import React from "react";
import '../css/Nav.css';
import { Link } from 'react-router-dom';
import MeetingRoomIcon from '@mui/icons-material/MeetingRoom';
import AccundCircleIcon from '@mui/icons-material/AccountCircle';
import HomeIcon from '@mui/icons-material/Home';
import ApartmentIcon from '@mui/icons-material/Apartment';

const Nav = () => {
    const handleLogout = () => {
        // Limpiar datos
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        localStorage.removeItem('authToken');
        sessionStorage.clear();
        
        // Redirigir
        window.location.href = window.location.origin;
    };

    return (
        <div className="nav">
            <nav className="navbar bg-body-tertiary">
                <div className="container-fluid">
                    <a className="navbar-brand" href="#">Menú</a>
                    <button className="navbar-toggler" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasNavbar" aria-controls="offcanvasNavbar" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="offcanvas offcanvas-end" tabIndex="-1" id="offcanvasNavbar" aria-labelledby="offcanvasNavbarLabel">
                        <div className="offcanvas-header">
                            <h5 className="offcanvas-title" id="offcanvasNavbarLabel">Menú</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div className="offcanvas-body">
                            <ul className="navbar-nav justify-content-end flex-grow-1 pe-3">
                                <li className="nav-item">
                                    <Link to="/Home" className="link-boton-nav">
                                        <HomeIcon /> Inicio
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/Usuarios" className="link-boton-nav">
                                        <AccundCircleIcon/> Usuarios
                                    </Link>
                                </li>
                                <li>
                                    <Link className="link-boton-nav">
                                        <ApartmentIcon/> Areas
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <button onClick={handleLogout} className="link-boton-nav" style={{border: 'none', background: 'none', cursor: 'pointer'}}>
                                        <MeetingRoomIcon/> Cerrar Sesión
                                    </button>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </nav>
        </div>
    );
};

export default Nav;