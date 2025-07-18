import React from 'react';
import { Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import Home from './pages/Home';
import Documentos from './pages/Documentos';
import Usuarios from './pages/Usuarios';
import PrivateRoute from './components/PrivateRoute';
import AccessDeniedPage from './components/Inauthorizader';
import Logout from './pages/Logout';
import NuevoDoc from './pages/NuevoDoc';

function App() {
  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
      <Route path="/logout" element={<Logout />} />
      <Route path="/nuevoDoc" element={<PrivateRoute allowedRole="root,administrador,Jefe"><NuevoDoc/></PrivateRoute>}/>
      <Route path="/home" element={<PrivateRoute allowedRole="root,administrador,Jefe"><Home /></PrivateRoute>} />
      <Route path='/Documentos' element={<PrivateRoute allowedRole="root,administrador,Jefe"><Documentos/></PrivateRoute>}/>
      <Route path='/Usuarios' element={<PrivateRoute allowedRole="root"><Usuarios/></PrivateRoute>}/>
      <Route path='/403' element={<PrivateRoute allowedRole="root,administrador,Jefe"><AccessDeniedPage/></PrivateRoute>}/>
    </Routes>
  );
}

export default App;
