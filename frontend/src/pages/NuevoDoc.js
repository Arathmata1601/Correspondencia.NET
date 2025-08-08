import React, { useState, useEffect,useCallback } from "react";
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

import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { CKEditor } from '@ckeditor/ckeditor5-react';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../css/nvoDoc.css';

const NuevoDoc = () => {
    // ESTADOS PARA VALIDACIONES DEL FORMULARIO
    const [formData, setFormData] = useState({
        nombre: '',
        memo: ''
    });

    const [errors, setErrors] = useState({
        nombre: false,
        memo: false
    });

    const [helperTexts, setHelperTexts] = useState({
        nombre: '',
        memo: ''
    });

    // ESTADOS PARA SELECTORES
    const [areas, setAreas] = useState([]);
    const [areaSeleccionada, setAreaSeleccionada] = useState(null);
    const [atencionSeleccionada, setAtencionSeleccionada] = useState(null);
    const [atencionChecked, setAtencionChecked] = useState(false);

    // ESTADOS PARA CCP (Con Copia Para)
    const [conCopiaChecked, setConCopiaChecked] = useState(false);
    const [usuariosSeleccionados, setUsuariosSeleccionados] = useState([]);
    const [otroChecked, setOtroChecked] = useState(false);
    const [otroNombre, setOtroNombre] = useState('');
    const [otroPuesto, setOtroPuesto] = useState('');

    // ESTADOS PARA INTRODUCCIONES Y DESPEDIDAS
    const [introducciones, setIntroducciones] = useState([]);
    const [despedidas, setDespedidas] = useState([]);
    const [introSeleccionada, setIntroSeleccionada] = useState(null);
    const [despedidaSeleccionada, setDespedidaSeleccionada] = useState(null);

    // ESTADO PARA EL CONTENIDO
    const [contenido, setContenido] = useState("");

    // FUNCIÓN HELPER PARA OBTENER TOKEN
    const getToken = () => {
        const token = localStorage.getItem('token');
        if (!token) {
            console.error('No se encontró token');
            return null;
        }
        return token;
    };

    // FUNCIÓN GENÉRICA PARA HACER PETICIONES A LA API
    const fetchData = useCallback(async (url, setter, mapFunction) => {
        const token = getToken();
        if (!token) return;

        try {
            const response = await fetch(url, {
                method: "GET",
                headers: { Authorization: `Bearer ${token}` }
            });

            if (!response.ok) {
                throw new Error(`Error ${response.status}: ${response.statusText}`);
            }

            const data = await response.json();
            const processedData = mapFunction ? data.map(mapFunction) : data;
            setter(processedData);
        } catch (error) {
            console.error(`Error al cargar datos de ${url}:`, error);
        }
    }, []);

    // CARGAR DATOS AL MONTAR EL COMPONENTE
    useEffect(() => {
        // Cargar áreas
        fetchData(
            "/api/areas", 
            setAreas, 
            (item) => ({ ...item, id: item.id_area })
        );

        // Cargar introducciones
        fetchData(
            "/api/complementos/introducciones", 
            setIntroducciones, 
            (item) => ({ ...item, id: item.idIntro })
        );

        // Cargar despedidas
        fetchData(
            "/api/complementos/despedidas", 
            setDespedidas, 
            (item) => ({ ...item, id: item.id_des })
        );
    }, [fetchData]);

    // MANEJADORES DE VALIDACIÓN
    const validateField = (fieldName, value) => {
        const isEmpty = value.trim() === '';
        const errorMessages = {
            nombre: 'El nombre es obligatorio',
            memo: 'El número de memo es obligatorio'
        };

        setErrors(prev => ({ ...prev, [fieldName]: isEmpty }));
        setHelperTexts(prev => ({ 
            ...prev, 
            [fieldName]: isEmpty ? errorMessages[fieldName] : '' 
        }));

        return !isEmpty;
    };

    const handleInputChange = (fieldName) => (e) => {
        const value = e.target.value;
        setFormData(prev => ({ ...prev, [fieldName]: value }));
        validateField(fieldName, value);
    };

    // MANEJADORES PARA CHECKBOXES Y SELECTORES
    const handleAtencionCheckbox = (event) => {
        setAtencionChecked(event.target.checked);
        if (!event.target.checked) {
            setAtencionSeleccionada(null);
        }
    };

    const handleConCopiaCheckbox = (event) => {
        setConCopiaChecked(event.target.checked);
        if (!event.target.checked) {
            setUsuariosSeleccionados([]);
            setOtroChecked(false);
            setOtroNombre('');
            setOtroPuesto('');
        }
    };

    const handleUsuarioCheckbox = (id_area) => {
        setUsuariosSeleccionados(prev =>
            prev.includes(id_area)
                ? prev.filter(id => id !== id_area)
                : [...prev, id_area]
        );
    };

    const handleOtroCheckbox = () => {
        setOtroChecked(prev => {
            if (prev) {
                setOtroNombre('');
                setOtroPuesto('');
            }
            return !prev;
        });
    };

    // VALIDACIÓN COMPLETA DEL FORMULARIO
    const validateForm = () => {
        const errors = [];

        if (!formData.memo.trim()) errors.push('El número de memo es obligatorio');
        if (!formData.nombre.trim()) errors.push('El nombre/asunto es obligatorio');
        if (!areaSeleccionada) errors.push('Debe seleccionar un área');
        if (!contenido.trim()) errors.push('El contenido es obligatorio');
        if (atencionChecked && !atencionSeleccionada) {
            errors.push('Debe seleccionar un área de atención');
        }
        if (conCopiaChecked && usuariosSeleccionados.length === 0 && !otroChecked) {
            errors.push('Debe seleccionar al menos un usuario para copia o agregar otro destinatario');
        }
        if (otroChecked && (!otroNombre.trim() || !otroPuesto.trim())) {
            errors.push('Debe completar el nombre y puesto del otro destinatario');
        }

        return errors;
    };

    // ENVÍO DEL FORMULARIO
    const handleSubmit = async (e) => {
        e.preventDefault();

        console.log("=== INICIO DE ENVÍO ===");
        console.log("FormData:", formData);
        console.log("Area seleccionada:", areaSeleccionada);
        console.log("Atencion seleccionada:", atencionSeleccionada);
        console.log("Contenido:", contenido);

        const validationErrors = validateForm();
        if (validationErrors.length > 0) {
            console.error("Errores de validación:", validationErrors);
            alert('Errores en el formulario:\n' + validationErrors.join('\n'));
            return;
        }

        const dataToSend = {
            noMemo: parseInt(formData.memo), // Convertir a int
            area_rec: areaSeleccionada?.nombre_area?.toString(), // Convertir a string si es necesario
            atencion: atencionChecked ? atencionSeleccionada?.responsable?.toString() : "",
            fechaDoc: new Date().toISOString(), // Formato DateTime completo
            asuntoDoc: formData.nombre,
            descripcion: contenido, // contenido → descripcion
            introduccion: introSeleccionada?.intro || "", // El texto, no el ID
            despedida: despedidaSeleccionada?.despedida || "", // El texto, no el ID
            elaborado: "arath.mata", // Agregar campo elaborado (quizás usuario actual?)
            conCopiaResponsablesIds: conCopiaChecked ? usuariosSeleccionados : [], // Cambio de nombre
            nombre_otro: (conCopiaChecked && otroChecked) ? otroNombre : null,
            puesto_otro: (conCopiaChecked && otroChecked) ? otroPuesto : null
        };

        console.log("=== DATOS A ENVIAR ===");
        console.log("Objeto completo:", dataToSend);
        console.log("JSON stringificado:", JSON.stringify(dataToSend, null, 2));

        try {
            const token = getToken();
            if (!token) {
                console.error("No hay token disponible");
                alert("Token no disponible. Por favor, inicie sesión nuevamente.");
                return;
            }

            console.log("Token encontrado:", token.substring(0, 20) + "...");
            console.log("URL de envío:", "/api/documents/addDocument");

            const response = await fetch("/api/documents/addDocument", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify(dataToSend)
            });

            console.log("=== RESPUESTA DEL SERVIDOR ===");
            console.log("Status:", response.status);
            console.log("Status Text:", response.statusText);
            console.log("Headers:", Object.fromEntries(response.headers.entries()));

            // Intentar leer la respuesta como texto primero
            const responseText = await response.text();
            console.log("Respuesta como texto:", responseText);

            let result;
            try {
                result = JSON.parse(responseText);
                console.log("Respuesta parseada como JSON:", result);
            } catch (parseError) {
                console.error("Error al parsear JSON:", parseError);
                console.log("La respuesta no es JSON válido");
                result = { message: responseText };
            }

            if (response.ok) {
                console.log("✅ Envío exitoso");
                alert("Documento enviado correctamente");
                // Resetear formulario
                setFormData({ nombre: '', memo: '' });
                setAreaSeleccionada(null);
                setAtencionSeleccionada(null);
                setAtencionChecked(false);
                setConCopiaChecked(false);
                setUsuariosSeleccionados([]);
                setOtroChecked(false);
                setOtroNombre('');
                setOtroPuesto('');
                setIntroSeleccionada(null);
                setDespedidaSeleccionada(null);
                setContenido('');
                setErrors({ nombre: false, memo: false });
                setHelperTexts({ nombre: '', memo: '' });
                window.location("/Documentos"); // Recargar la página para limpiar el estado
            } else {
                console.error("❌ Error del servidor");
                console.error("Status:", response.status);
                console.error("Mensaje:", result.message || result);
                
                // Mensajes de error más específicos
                let errorMessage = `Error ${response.status}: `;
                if (response.status === 400) {
                    errorMessage += "Datos inválidos. ";
                } else if (response.status === 401) {
                    errorMessage += "No autorizado. Verifique su sesión. ";
                } else if (response.status === 403) {
                    errorMessage += "Permisos insuficientes. ";
                } else if (response.status === 404) {
                    errorMessage += "Endpoint no encontrado. ";
                } else if (response.status === 500) {
                    errorMessage += "Error interno del servidor. ";
                }
                
                errorMessage += result.message || result || 'Error desconocido';
                alert(errorMessage);
            }
        } catch (error) {
            console.error("=== ERROR DE RED O FETCH ===");
            console.error("Tipo de error:", error.name);
            console.error("Mensaje:", error.message);
            console.error("Stack trace:", error.stack);
            
            if (error.name === 'TypeError' && error.message.includes('fetch')) {
                alert("Error de conexión. Verifique su conexión a internet y que el servidor esté disponible.");
            } else {
                alert("Error inesperado: " + error.message);
            }
        }

        console.log("=== FIN DE ENVÍO ===");
    };

    return (
        <div className="page-container">
            <Header />
            <Nav />
            <main className='content-wrap'>
                <h4> Nuevo Documento</h4>
                <div className="form-doc">
                    <div className="row">
                        <form onSubmit={handleSubmit}>
                            <div className="col-md-6">
                                <div className="row">
                                    <TextField
                                        label="No. Memo"
                                        variant="outlined"
                                        fullWidth
                                        margin="normal"
                                        value={formData.memo}
                                        onChange={handleInputChange('memo')}
                                        error={errors.memo}
                                        helperText={helperTexts.memo}
                                    />
                                </div>
                                <div className="row">
                                    <Autocomplete
                                        fullWidth
                                        options={areas}
                                        value={areaSeleccionada}
                                        onChange={(event, newValue) => {
                                            setAreaSeleccionada(newValue);
                                        }}
                                        getOptionLabel={(option) => option.nombre_area || ''}
                                        renderInput={(params) => <TextField {...params} label="Área" variant="outlined" />}
                                    />
                                </div>
                                <div className="row">
                                    <FormControl component="fieldset">
                                        <FormGroup aria-label="position" row>
                                            <FormControlLabel
                                                id="ateOk"
                                                value=""
                                                control={
                                                    <Checkbox
                                                        color="primary"
                                                        checked={atencionChecked}
                                                        onChange={handleAtencionCheckbox}
                                                    />
                                                }
                                                label="Atención"
                                                labelPlacement="start"
                                            />
                                        </FormGroup>
                                    </FormControl>
                                </div>

                                <div className="row">
                                    <Autocomplete
                                        id='atencion'
                                        disabled={!atencionChecked}
                                        fullWidth
                                        options={areas}
                                        value={atencionSeleccionada}
                                        onChange={(e, newValue) => setAtencionSeleccionada(newValue)}
                                        getOptionLabel={(option) => option.nombre_area || ''}
                                        renderInput={(params) => <TextField {...params} label="Atención" variant="outlined" />}
                                    />
                                </div>
                                <div className="row">
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
                                <div className="row">
                                    <TextField
                                        label="Asunto"
                                        variant="outlined"
                                        fullWidth
                                        margin="normal"
                                        value={formData.nombre}
                                        onChange={handleInputChange('nombre')}
                                        error={errors.nombre}
                                        helperText={helperTexts.nombre}
                                    />
                                </div>
                                <div className="row">
                                    <Box sx={{ mt: 2 }}>
                                        <FormControlLabel
                                            control={
                                                <Checkbox
                                                    checked={conCopiaChecked}
                                                    onChange={handleConCopiaCheckbox}
                                                />
                                            }
                                            label="Con copia a:"
                                        />

                                        {conCopiaChecked && (
                                            <Box sx={{ maxHeight: 200, overflowY: 'auto', mt: 1, mb: 2 }}>
                                                <Paper variant="outlined" sx={{ p: 2 }}>
                                                    <FormGroup>
                                                        {areas.map((area) => (
                                                            <FormControlLabel
                                                                key={area.id_area}
                                                                control={
                                                                    <Checkbox
                                                                        checked={usuariosSeleccionados.includes(area.id_area)}
                                                                        onChange={() => handleUsuarioCheckbox(area.id_area)}
                                                                    />
                                                                }
                                                                label={area.responsable}
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
                            <div className="col-md-6">
                                <div className="row">
                                    <Autocomplete
                                        fullWidth
                                        options={introducciones}
                                        value={introSeleccionada}
                                        onChange={(event, newValue) => {
                                            setIntroSeleccionada(newValue);
                                        }}
                                        getOptionLabel={(option) => option.intro || ''}
                                        renderInput={(params) => <TextField {...params} label="Introducción" variant="outlined" />}
                                    />
                                </div>
                                <div className="row">
                                    <div className="w-100">
                                        <div className="editor-wrapper">
                                            <CKEditor
                                                className="editor"
                                                editor={ClassicEditor}
                                                data={contenido}
                                                config={{
                                                    language: 'es',
                                                    toolbar: [
                                                        'heading', '|',
                                                        'bold', 'italic', 'underline', 'strikethrough', '|',
                                                        'fontSize', 'fontFamily', 'fontColor', 'fontBackgroundColor', '|',
                                                        'alignment', '|',
                                                        'bulletedList', '|',
                                                        'link', 'blockQuote', 'insertTable', 'imageUpload', '|',
                                                        'undo', 'redo', '|',
                                                        'code', 'codeBlock', '|',
                                                        'horizontalLine', 'removeFormat', '|',
                                                        'subscript', 'superscript', '|',
                                                        'specialCharacters', 'highlight'
                                                    ],
                                                    image: {
                                                        toolbar: ['imageTextAlternative', 'imageStyle:full', 'imageStyle:side']
                                                    },
                                                    table: {
                                                        contentToolbar: ['tableColumn', 'tableRow', 'mergeTableCells']
                                                    },
                                                    mediaEmbed: {
                                                        previewsInData: true
                                                    }
                                                }}
                                                onChange={(event, editor) => {
                                                    const data = editor.getData();
                                                    setContenido(data);
                                                }}
                                            />
                                        </div>
                                    </div>
                                </div>

                                <div className="row">
                                    <Autocomplete
                                        fullWidth
                                        options={despedidas}
                                        value={despedidaSeleccionada}
                                        onChange={(event, newValue) => {
                                            setDespedidaSeleccionada(newValue);
                                        }}
                                        getOptionLabel={(option) => option.despedida || ''}
                                        renderInput={(params) => <TextField {...params} label="Despedida" variant="outlined" />}
                                    />
                                </div>
                            </div>

                            <div className="d-flex justify-content-end mt-3">
                                <Button type="submit" variant="contained" color="primary">
                                    Enviar
                                </Button>
                            </div>
                        </form>
                    </div>
                </div>
            </main >
            <Footer />
        </div >
    );
};

export default NuevoDoc;