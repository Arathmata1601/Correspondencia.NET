import React from 'react';
import { Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import Home from './pages/Home';
import Documentos from './pages/Documentos';
import Usuarios from './pages/Usuarios';
import PrivateRoute from './components/PrivateRoute';

function App() {
  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
      <Route path="/home" element={<PrivateRoute allowedRole="root"><Home /></PrivateRoute>} />
      <Route path='/Documentos' element={<PrivateRoute allowedRole="root"><Documentos/></PrivateRoute>}/>
      <Route path='/Usuarios' element={<PrivateRoute allowedRole="root"><Usuarios/></PrivateRoute>}/>
    </Routes>
  );
}

export default App;
