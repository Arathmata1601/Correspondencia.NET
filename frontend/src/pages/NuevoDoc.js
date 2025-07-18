import React, { useState, useEffect } from "react";
import Nav from "../components/Nav";
import Footer from "../components/Footer";
import Header from "../components/Header";
import '../css/Styles.css'
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { Autocomplete } from "@mui/material";
import {
    Checkbox,
    FormControlLabel,
    FormGroup,
    FormControl,
    Typography,
    Box,
    Paper
} from '@mui/material';

const NuevoDoc = () => {
    /*VALIDACIONES DEL FORMULARIO*/
    const [nombre, setNombre] = useState('');
    const [memo, setMemo] = useState('');

    const [errors, setErrors] = useState({
        nombre: false,
        memo: false
    });

    const [helperTexts, setHelperTexts] = useState({
        nombre: '',
        memo: ''
    });

    const handleChangeNombre = (e) => {
        const valor = e.target.value;
        setNombre(valor);

        if (valor.trim() === '') {
            setErrors(prev => ({ ...prev, nombre: true }));
            setHelperTexts(prev => ({ ...prev, nombre: 'El nombre es obligatorio' }));
        } else {
            setErrors(prev => ({ ...prev, nombre: false }));
            setHelperTexts(prev => ({ ...prev, nombre: '' }));
        }
    };

    const handleChangeMemo = (e) => {
        const valor = e.target.value;
        setMemo(valor);

        if (valor.trim() === '') {
            setErrors(prev => ({ ...prev, memo: true }));
            setHelperTexts(prev => ({ ...prev, memo: 'El número de memo es obligatorio' }));
        } else {
            setErrors(prev => ({ ...prev, memo: false }));
            setHelperTexts(prev => ({ ...prev, memo: '' }));
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const nombreValido = nombre.trim() !== '';
        const memoValido = memo.trim() !== '';

        if (!nombreValido || !memoValido) {
            setErrors({
                nombre: !nombreValido,
                memo: !memoValido
            });

            setHelperTexts({
                nombre: nombreValido ? '' : 'El nombre es obligatorio',
                memo: memoValido ? '' : 'El número de memo es obligatorio'
            });

            return;
        }

        alert('Formulario enviado: ' + nombre);
    };
    //
    // FUNCIONES DE SELECTOR DE AREAS
    const [areaValue, setAreaValue] = useState(null);
    const [areaInputValue, setAreaInputValue] = useState('');

    const [atencionValue, setAtencionValue] = useState(null);
    const [atencionInputValue, setAtencionInputValue] = useState('');
    /*
    const [options, setRows] = useState([]);
    const [value, setValue] = useState(null);
    const [inputValue, setInputValue] = useState('');*/

    useEffect(() => {
        const ApiURL = "/api/areas";
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
                    id: item.id_area, // <-- necesario para que funcione correctamente
                }));
                setAreaValue(rowsConId);
                //setValue(rowsConId[0]);

            })
            
            .catch((err) => {
                console.error('Error al cargar los datos:', err);

            });
    }, []);

    //
    //ACTIVAR EL SELECTOR DE ATENCIÓN

    const [atencionChecked, setAtencionChecked] = useState(false);
    const handleCheckboxChange = (event) => {
        setAtencionChecked(event.target.checked);
    };

    useEffect(() => {
        const ApiURL = "/api/areas";
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
                    id: item.id_area, // <-- necesario para que funcione correctamente
                }));
                setAtencionValue(rowsConId);
                //setValue(rowsConId[0]);

            })
            
            .catch((err) => {
                console.error('Error al cargar los datos:', err);

            });
    }, []);
    //

    //SELECTOR DE CCP

    const [conCopiaChecked, setConCopiaChecked] = useState(false);
    const [usuarios, setUsuarios] = useState([]);
    const [usuariosSeleccionados, setUsuariosSeleccionados] = useState([]);
    const [otroChecked, setOtroChecked] = useState(false);
    const [otroNombre, setOtroNombre] = useState('');
    const [otroPuesto, setOtroPuesto] = useState('');

    // Cargar usuarios desde API
    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token) {
            console.error('Token no encontrado');
            return;
        }

        fetch('/api/areas', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
            .then((res) => res.json())
            .then((data) => {
                console.log("Datos de áreas:", data);
  
                setUsuarios(data);
            })
            .catch((err) => {
                console.error('Error al obtener areas:', err);
            });
    }, []);

    const handleCheckboxUsuario = (id_area) => {
        setUsuariosSeleccionados((prev) =>
            prev.includes(id_area)
                ? prev.filter((n) => n !== id_area)
                : [...prev, id_area]
        );
    };

    const handleOtroCheckbox = () => {
        setOtroChecked((prev) => !prev);
        if (otroChecked) {
            setOtroNombre('');
            setOtroPuesto('');
        }
    };
    //


    return (
        <div className="page-container">
            <Header />
            <Nav />
            <main className='content-wrap'>
                <h4> Nuevo Documento</h4>
                <div className="form-doc">
                    <form onSubmit={handleSubmit}>
                        <div class="col-md-6">
                            <div class="row">
                                <TextField
                                    label="No. Memo"
                                    variant="outlined"
                                    fullWidth
                                    margin="normal"
                                    value={memo}
                                    onChange={handleChangeMemo}
                                    error={errors.memo}
                                    helperText={helperTexts.memo}
                                />
                            </div>
                            <div class="row">
                                <Autocomplete
                                    fullWidth
                                    options={areaValue}
                                    value={areaValue}
                                    onChange={(event, newValue) => {
                                        setAreaValue(newValue);
                                    }}
                                    inputValue={areaInputValue}
                                    onInputChange={(event, newInputValue) => {
                                        setAreaInputValue(newInputValue);
                                    }}
                                    getOptionLabel={(areaValue) => areaValue.nombre_area || ''}  // ajusta según tu propiedad
                                    renderInput={(params) => <TextField {...params} label="Área" variant="outlined" />}
                                />
                            </div>
                            <div class="row">
                                <FormControl component="fieldset">
                                    <FormGroup aria-label="position" row>

                                        <FormControlLabel
                                            id="ateOk"
                                            value=""
                                            control=
                                            {<Checkbox
                                                color="primary"
                                                checked={atencionChecked}
                                                onChange={handleCheckboxChange}
                                            />}
                                            label="Atención"
                                            labelPlacement="start"
                                        />

                                    </FormGroup>
                                </FormControl>
                            </div>

                            <div class="row">
                                <Autocomplete
                                    id='atencion'
                                    disabled={!atencionChecked}
                                    fullWidth
                                    options={atencionValue}
                                    value={atencionValue}
                                    onChange={(e, newValue) => setAtencionValue(newValue)}
                                    inputValue={atencionInputValue}
                                    onInputChange={(e, newInputValue) => setAtencionInputValue(newInputValue)}
                                    getOptionLabel={(atencionValue) => atencionValue.nombre_area || ''}
                                    renderInput={(params) => <TextField {...params} label="Atención" variant="outlined" />}
                                />
                            </div>
                            <div class="row">
                                <TextField
                                    id="date"
                                    label="Fecha"
                                    type="date"
                                    defaultValue=""
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                />
                            </div>
                            <div class="row">
                                <TextField
                                    label="Asunto"
                                    variant="outlined"
                                    //fullWidth
                                    margin="normal"
                                    value={nombre}
                                    onChange={handleChangeNombre}
                                    error={errors.nombre}
                                    helperText={helperTexts.nombre}
                                />
                            </div>
                            <div class="row">
                                <Box sx={{ mt: 2 }}>
                                    <FormControlLabel
                                        control={
                                            <Checkbox
                                                checked={conCopiaChecked}
                                                onChange={(e) => setConCopiaChecked(e.target.checked)}
                                            />
                                        }
                                        label="Con copia a:"
                                    />

                                    {conCopiaChecked && (
                                        <Box sx={{ maxHeight: 200, overflowY: 'auto', mt: 1, mb: 2 }}>
                                            <Paper variant="outlined" sx={{ p: 2 }}>
                                                <FormGroup>
                                                    {usuarios.map((usuario) => (
                                                        <FormControlLabel
                                                            key={usuario.id_area}
                                                            control={
                                                                <Checkbox
                                                                    checked={usuariosSeleccionados.includes(usuario.id_area)}
                                                                    onChange={() => handleCheckboxUsuario(usuario.responsable)}
                                                                />
                                                            }
                                                            label={usuario.responsable}
                                                        />
                                                    ))}
                                                    <FormControlLabel
                                                        control={
                                                            <Checkbox
                                                                checked={otroChecked}
                                                                onChange={handleOtroCheckbox}
                                                            />
                                                        }
                                                        label="Otro"
                                                    />
                                                </FormGroup>
                                            </Paper>
                                        </Box>
                                    )}

                                    {conCopiaChecked && otroChecked && (
                                        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
                                            <TextField
                                                label="Ingrese nombre"
                                                value={otroNombre}
                                                onChange={(e) => setOtroNombre(e.target.value)}
                                                required
                                            />
                                            <TextField
                                                label="Ingrese puesto"
                                                value={otroPuesto}
                                                onChange={(e) => setOtroPuesto(e.target.value)}
                                            />
                                        </Box>
                                    )}
                                </Box>
                            </div>
                        </div>


                        <Button type="submit" variant="contained" color="primary">
                            Enviar
                        </Button>
                    </form>
                </div>
            </main >
            <Footer />

        </div >
    );

};

export default NuevoDoc;