import React from 'react';
import { Link } from 'react-router-dom';
import Header from '../components/Header';
import Nav from '../components/Nav';
import Footer from '../components/Footer';
import DataTable from '../components/TablaDocumentos'
//import '../css/Home.css';
import '../css/Styles.css';



const Documentos = () => {
  
  return (
    <div className='page-container'>
      <Header />
      <div className='navbar'>
        <Nav />
      </div>
      <main className='content-wrap'>
        <h3>DOCUMENTOS</h3>
        <div className='agregar'>
          <button className="boton-add" type='button'>
            <Link><i class="bi-file-earmark-plus-fill">Agregar</i></Link>
          </button>
        </div>
        <DataTable 
      />
      
      </main>
      <Footer/>
    </div>
  );
};

export default Documentos;
