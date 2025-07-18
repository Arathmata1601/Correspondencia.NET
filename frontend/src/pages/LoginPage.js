import React, { useState } from 'react';
import Header  from '../components/Header';
import '../css/Login.css'

const LoginPage = () => {
    const [usuario, setUsuario] = useState('');
    const [contraseña, setContraseña] = useState('');
    const [error, setError] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const res = await fetch('http://localhost:5200/api/auth/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ usuario, contraseña })
            });

            const data = await res.json();

            if (!res.ok) {
                setError(data || 'Error al iniciar sesión');
                return;
            }

            localStorage.setItem('token', data.token);
            localStorage.setItem('rol', data.rol);

            let redirect = "/";
            if (data.rol === "root") redirect = "/home";
            else if (data.rol === "administrador") redirect = "/home";
            else if (data.rol === "jefe") redirect = "/home";

            window.location.href = redirect;

        } catch (err) {
            setError('Error de conexión');
        }
    };

    return (
        <div className='body'>
        <Header/>
            <div style={{ maxWidth: '400px', margin: 'auto', paddingTop: '100px' }}>
                <div class="card login-card shadow-sm p-4">
                <h2>Iniciar Sesión</h2>
                {error && <div style={{ color: 'red' }}>{error}</div>}
                
                    <form onSubmit={handleLogin}>
                        <input type="text" class="form-control" placeholder="Usuario" value={usuario} onChange={(e) => setUsuario(e.target.value)} />
                        <input type="password" class="form-control" placeholder="Contraseña" value={contraseña} onChange={(e) => setContraseña(e.target.value)} />
                        <button type='submit' class="btn btn-login">Entrar</button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;
