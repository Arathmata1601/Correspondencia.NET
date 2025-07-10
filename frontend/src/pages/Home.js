import React from 'react';
import Header from '../components/Header';
import Nav from '../components/Nav';
import Footer from '../components/Footer';
import '../css/Home.css';
import '../css/Styles.css';

const Home = () => {
  return (
    <div className='container'>
      <Header />
      <div className='navbar'>
        <Nav />
      </div>
      <div className='botones-container'>
        <div class="row">
          <div class="col-md-6">
              <button type='button' className='boton-ini'>
                <i class="bi-file-earmark-text-fill">Documentos</i>
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
      <div className='Footer'>
        <Footer/>
      </div>
    
    </div>
   
  );
};

export default Home;
