import React, { useEffect, useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { esES } from '@mui/x-data-grid/locales';
import { CircularProgress, IconButton } from '@mui/material';
import '../css/TablaDocumentos.css';
import EditSquareIcon from '@mui/icons-material/EditSquare';
import VisibilityIcon from '@mui/icons-material/Visibility';

const handleVer = (row) => {
  console.log("Ver:", row);
};

const handleEditar = (row) => {
  console.log("Editar:", row);
};

const handleEliminar = (row) => {
  console.log("Eliminar:", row);
};

const columns = [
  { field: 'id_doc', headerName: 'ID', width: 50 },
  { field: 'noMemo', headerName: 'No.Memo', width: 80},
  { field: 'asuntoDoc', headerName: 'Asunto', width: 300 },
  { field: 'area_rec', headerName: 'Area', width: 200 },
  { field: 'atencion', headerName: 'Atencion', width: 200 },
  { field: 'usuarioEmisor', headerName: 'Usuario', width: 200 },
  { field: 'fechaDoc', headerName: 'Fecha', width: 200 },
  {
    field: 'acciones',
    headerName: 'Acciones',
    width: 200,
    sortable: false,
    renderCell: (params) => (
      <div style={{ display: 'flex', gap: '0.5rem' }}>
        <IconButton  aria-label="Ver" color="success" onClick={() => handleVer(params.row)} ><VisibilityIcon/></IconButton>
        <IconButton  aria-label="Editar" color="primary" onClick={() => handleEditar(params.row)}><EditSquareIcon/></IconButton>
        <IconButton  aria-label="Eliminar" color="error" onClick={() => handleEliminar(params.row)}><i class="bi-trash-fill"></i></IconButton>
      </div>
    ),
  },
];

export default function TablaDocumentos() {
  const [rows, setRows] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const ApiURL = "/api/documents";
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
          id: item.id_doc, // <-- necesario para que funcione correctamente
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
    <div className="container-table" style={{ maxWidth: '1800px', width: '90%'}}>
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
