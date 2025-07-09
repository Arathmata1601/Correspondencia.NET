import React from 'react';
import { Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import Home from './pages/Home';
//import PanelAdmin from './pages/PanelAdmin';
//import PanelJefe from './pages/PanelJefe';
import PrivateRoute from './components/PrivateRoute';

function App() {
  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
      <Route path="/home" element={<PrivateRoute allowedRole="root"><Home /></PrivateRoute>} />
      
    </Routes>
  );
}

export default App;
