import { useState } from 'react';
import PrivacyTipIcon from '@mui/icons-material/PrivacyTip';

export default function AccessDeniedPage() {
  const [hoveredButton, setHoveredButton] = useState(null);

  const handleGoBack = () => {
    window.location.href = '/home';
  };

  const handleGoHome = () => {
    window.location.href = '/';
  };

  const containerStyle = {
    minHeight: '100vh',
    background: 'linear-gradient(135deg, #fef2f2 0%, #fff7ed 50%, #fffbeb 100%)',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    padding: '20px',
    fontFamily: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif'
  };

  const cardStyle = {
    maxWidth: '600px',
    width: '100%',
    backgroundColor: 'white',
    borderRadius: '20px',
    boxShadow: '0 25px 50px -12px rgba(0, 0, 0, 0.25)',
    overflow: 'hidden'
  };

  const headerStyle = {
    background: 'linear-gradient(135deg, #ef4444 0%, #f97316 100%)',
    padding: '20px 40px',
    textAlign: 'center',
    color: 'white'
  };

  const shieldStyle = {
    width: '80px',
    height: '80px',
    backgroundColor: 'rgba(255, 255, 255, 0.2)',
    borderRadius: '50%',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    margin: '0 auto 20px',
    fontSize: '40px',
    animation: 'pulse 2s infinite'
  };

  const contentStyle = {
    padding: '40px',
    textAlign: 'center'
  };

  const errorCodeStyle = {
    fontSize: '72px',
    fontWeight: 'bold',
    color: '#ef4444',
    marginBottom: '20px',
    textShadow: '2px 2px 4px rgba(0, 0, 0, 0.1)'
  };

  const infoBoxStyle = {
    backgroundColor: '#f9fafb',
    borderRadius: '12px',
    padding: '24px',
    marginBottom: '32px',
    textAlign: 'left'
  };

  const listItemStyle = {
    display: 'flex',
    alignItems: 'flex-start',
    marginBottom: '8px',
    color: '#6b7280'
  };

  const bulletStyle = {
    color: '#ef4444',
    marginRight: '8px',
    fontSize: '16px'
  };

  const buttonContainerStyle = {
    display: 'flex',
    gap: '16px',
    justifyContent: 'center',
    flexWrap: 'wrap'
  };

  const baseButtonStyle = {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    padding: '12px 24px',
    borderRadius: '8px',
    border: 'none',
    cursor: 'pointer',
    fontSize: '16px',
    fontWeight: '500',
    transition: 'all 0.3s ease',
    textDecoration: 'none',
    minWidth: '140px'
  };

  const backButtonStyle = {
    ...baseButtonStyle,
    backgroundColor: hoveredButton === 'back' ? '#374151' : '#6b7280',
    color: 'white',
    transform: hoveredButton === 'back' ? 'translateY(-2px)' : 'translateY(0)'
  };

  const homeButtonStyle = {
    ...baseButtonStyle,
    background: hoveredButton === 'home' 
      ? 'linear-gradient(135deg, #dc2626 0%, #ea580c 100%)'
      : 'linear-gradient(135deg, #ef4444 0%, #f97316 100%)',
    color: 'white',
    transform: hoveredButton === 'home' ? 'translateY(-2px) scale(1.05)' : 'translateY(0) scale(1)'
  };

  const footerStyle = {
    textAlign: 'center',
    marginTop: '32px',
    color: '#6b7280'
  };

  const linkStyle = {
    color: '#ef4444',
    textDecoration: 'underline',
    marginLeft: '4px'
  };

  return (
    <div style={containerStyle}>
      <style>
        {`
          @keyframes pulse {
            0%, 100% { opacity: 1; }
            50% { opacity: 0.7; }
          }
        `}
      </style>
      
      <div style={cardStyle}>
        {/* Header */}
        <div style={headerStyle}>
          <div style={shieldStyle}>
            <PrivacyTipIcon/>
          </div>
          <h1 style={{ fontSize: '36px', fontWeight: 'bold', margin: '0 0 8px 0' }}>
            Acceso Denegado
          </h1>
          <p style={{ fontSize: '18px', margin: '0', opacity: '0.9' }}>
            No tienes permisos para acceder a esta página
          </p>
        </div>

        {/* Content */}
        <div style={contentStyle}>
          <div style={errorCodeStyle}>
            403
          </div>
          
          <h2 style={{ fontSize: '24px', fontWeight: '600', color: '#1f2937', marginBottom: '16px' }}>
            Permisos Insuficientes
          </h2>
          
          <p style={{ color: '#6b7280', marginBottom: '24px', lineHeight: '1.6' }}>
            Lo sentimos, pero no tienes los permisos necesarios para ver este contenido. 
            Si crees que esto es un error, contacta con el administrador del sistema.
          </p>

          {/* Info Box */}
          <div style={infoBoxStyle}>
            <h3 style={{ fontWeight: '600', color: '#1f2937', marginBottom: '12px' }}>
              Posibles razones:
            </h3>
            <div style={listItemStyle}>
              <span style={bulletStyle}>•</span>
              <span>Tu sesión ha expirado</span>
            </div>
            <div style={listItemStyle}>
              <span style={bulletStyle}>•</span>
              <span>No tienes el rol necesario para acceder</span>
            </div>
            <div style={listItemStyle}>
              <span style={bulletStyle}>•</span>
              <span>El contenido está restringido para tu nivel de usuario</span>
            </div>
            <div style={listItemStyle}>
              <span style={bulletStyle}>•</span>
              <span>Se requiere autenticación adicional</span>
            </div>
          </div>

          {/* Buttons */}
          <div style={buttonContainerStyle}>
            <button
              onClick={handleGoBack}
              style={backButtonStyle}
              onMouseEnter={() => setHoveredButton('back')}
              onMouseLeave={() => setHoveredButton(null)}
            >
              ← Regresar
            </button>
            
            <button
              onClick={handleGoHome}
              style={homeButtonStyle}
              onMouseEnter={() => setHoveredButton('home')}
              onMouseLeave={() => setHoveredButton(null)}
            >
              🏠 Ir al Inicio
            </button>
          </div>
        </div>
      </div>

      
    </div>
  );
}