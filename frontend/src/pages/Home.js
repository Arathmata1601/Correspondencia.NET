import React from 'react';
import { Link } from 'react-router-dom';
import Header from '../components/Header';
import Nav from '../components/Nav';
import Footer from '../components/Footer';
import '../css/Home.css';
import '../css/Styles.css';

const Home = () => {
  return (
    <div className='page-container'>
      <Header />
      <div className='navbar'>
        <Nav />
      </div>
      <main className="content-wrap">
        <div className='botones-container'>
          <div class="row">
            <div class="col-md-6">
              <button type='button' className='boton-ini'>
                <Link to="/Documentos" className="link-boton">
                  <i class="bi-file-earmark-text-fill">Documentos</i>
                </Link>
              </button>
            </div>
            <div class="col-md-6">
              <button type='button' className='boton-ini'>
                <i class="bi-archive-fill">Recibidos</i>
              </button>
            </div>
          </div>

          <div class="row">
            <div class="col-md-6">
              <button type='button' className='boton-ini'>
                <i class="bi-calendar-date">Calendario</i>
              </button>
            </div>
            <div class="col-md-6">
              <button type='button' className='boton-ini'>
                <i class="bi-telephone-fill">Recibidos</i>
              </button>
            </div>
          </div>
        </div>
      </main>
     
        <Footer />

    </div>

  );
};

export default Home;
