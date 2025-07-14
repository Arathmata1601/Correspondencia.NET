import React from 'react';
import { Link } from 'react-router-dom';
import Header from '../components/Header';
import Nav from '../components/Nav';
import Footer from '../components/Footer';
import DataTable from '../components/TablaUsuarios'
import '../css/Usuario.css';
import '../css/Styles.css';



const Usuarios = () => {
  
  return (
    <div className='page-container'>
      <Header />
      <div className='navbar'>
        <Nav />
      </div>
      <main className='content-wrap'>
        <h4>USUARIOS</h4>
        <div className='agregar'>
          <button className='boton-add' type='button'>
            <Link to = '/AddUsuario' className="link-boton"><i class = 'bi-person-fill-add'> Agregar</i></Link>
          </button>
        </div>
        <DataTable 
      />
      
      </main>
      <Footer/>
    </div>
  );
};

export default Usuarios;
