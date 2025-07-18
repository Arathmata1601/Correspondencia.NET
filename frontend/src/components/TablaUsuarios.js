import React, { useEffect, useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { esES } from '@mui/x-data-grid/locales';
import { CircularProgress } from '@mui/material';
import '../css/TablaUsuarios.css';

const columns = [
  { field: 'id_us', headerName: 'ID', width: 200 },
  { field: 'nombre_us', headerName: 'Nombre', width: 200},
  { field: 'apellidos', headerName: 'Apellido', width: 200 },
  { field: 'usuario', headerName: 'Usuario', width: 200 },
];

export default function TablaUsuarios() {
  const [rows, setRows] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const ApiURL = "/api/usuarios";
    const token = localStorage.getItem('token');
    if (!token) { 
      console.error('No se encontró token');
      return;
    }

    const PARAMET_PETITION = {
      method: "GET",
      headers: { Authorization: `Bearer ${token}` }
    };

    fetch(ApiURL, PARAMET_PETITION)
      .then((res) => res.json())
      .then((data) => {
        const rowsConId = data.map((item) => ({
          ...item,
          id: item.id_us, // <-- necesario para que funcione correctamente
        }));
        setRows(rowsConId);
        setLoading(false);
      })
      .catch((err) => {
        console.error('Error al cargar los datos:', err);
        setLoading(false);
      });
  }, []);

  return (
    <div className="container-table" style={{ maxWidth: '800px', width: '90%' }}>
      {loading ? (
        <CircularProgress />
      ) : (
        <DataGrid
          rows={rows}
          columns={columns}
          pageSize={5}
          localeText={esES.components.MuiDataGrid.defaultProps.localeText}
          sx={{
            /*minWidth: 900, // Forzar ancho para activar scroll-x si pantalla es menor*/
            flexGrow: 1,
          }} 
        />
      )}
    </div>
  );
}
