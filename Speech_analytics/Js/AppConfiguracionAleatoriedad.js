
console.error = function () { };


loadListClientes();

/***************************HEADER ************************/

loandNameInit();

function closeSession() {
    var url = "../App/LoginOff";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            location.reload();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function loandNameInit() {
    var url = "../App/LoandName";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {

            loadNameInicial(data);
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}


function escapeHtml(unsafe) {
    return unsafe.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&#039;");
}



function generarFecha() {

    let dateDay = new Date();
    let day = dateDay.getDate();
    let month = dateDay.getMonth() + 1;
    let year = dateDay.getFullYear();
    let seg = dateDay.getSeconds();
    let min = dateDay.getMinutes();
    let hora = dateDay.getHours();
    day = ('0' + day).slice(-2);
    month = ('0' + month).slice(-2);

    let fecha = `${year}-${month}-${day} ${hora}:${min}:${seg}`

    let fecha2 = `${year}-${month}-${day}`

    return fecha2;
}

function loadNameInicial(data) {

    let nombreApellido = data[0]["Nombre"];
    let text = nombreApellido.split(' ');

    let primerInicial = text[0].charAt(0);
    let segundaInicial = text[1].charAt(0);

    document.getElementById('nameUserInicial').innerHTML = primerInicial + segundaInicial;
    document.getElementById('jsDateDay').innerHTML = generarFecha();
}

function loandName() {
    var url = "../App/LoandName";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            loadNameInicial(data);
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

/**********************FUNCIONES **********************************/

function loadListClientes() {
    var url = "LoandListCampaign/App";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            var result = "<option value=''>Select</option>"
                result += "<option value='0-PLANTILLA'>MI PLANTILLA</option>"
            for (var i = 0; i < data.length; i++) {
                result += "<option value='" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "'>" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "</option>"
            }
            document.getElementById('jsCliente').innerHTML = result;

            /*loandNameInRandon();*/
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function loadccmsCliente() {

    document.getElementById('jsFiltroFechaIncial').max = generarFecha();
    document.getElementById('jsFiltroFechaFinal').max = generarFecha();

    limpiarCamposValue("jsFiltroFechaIncial");
    limpiarCamposValue("jsFiltroFechaFinal");
    limpiarCamposValue("jsFiltroMonitoreos");
    inputValueInZero("jsFiltroInicioCorta");
    inputValueInZero("jsFiltroFinCorta");
    inputValueInZero("jsFiltroInicioLarga");
    inputValueInZero("jsFiltroFinLarga");
    limpiarCamposInner("agentesEvaluar");
    limpiarCamposValue("jsFiltroMonitoreosAgente");
    limpiarCamposValue("jsFiltroCsat");
    limpiarCamposValue("jsFiltroNps");
    limpiarCamposValue("jsFiltroFcp");
    limpiarCamposValue("jsFiltroCes");
    limpiarCamposValue("jsFiltroRecontacto");
    limpiarCamposValue("jsFiltroHoldInicial");
    limpiarCamposValue("jsFiltroHoldFinal");
    limpiarCamposValue("jsFiltroAhtInicial");
    limpiarCamposValue("jsFiltroAhtFinal");
    inputValueInZero("jsFiltroTransferencia");

    var url = "loadccmsCliente/App";
    var data =
    {
        IdCliente: document.getElementById("jsCliente").value
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            document.getElementById("jsCcmsCliente").placeholder = "";

            if (data.length = 0) {

                document.getElementById('jsMensaje').innerHTML = "Edite y guarde su plantilla";
                openMensa();
            }
            else {
                document.getElementById("jsCcmsCliente").placeholder = data;

                let R = validarExisteFiltrosAleatoriedad();
                if (R = 1) {

                    whatFilter();
                } else {
                    
                    document.getElementById('jsMensaje').innerHTML = "No hay filtros asigados, llene los campos y guarde la configuracióon";
                    openMensa();

                }
            }

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}


function validarExisteFiltrosAleatoriedad() {

    var url = "validarExisteFiltrosAleatoriedad/App";
    var data =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        MES_ASIGNADO: generarFechaModelo("1"),
        AÑO_ASIGNADO: generarFechaModelo("2")
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length == 1) {

                return 1;
            }
            else {
                return 0;
            }

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}


function updateRandom() {

    var url = "updateRandom/Admin";
    var data =
    {
        ID_CLIENTE: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        SKILL_INICIAL: document.getElementById("jsFiltroSkill").value,
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data == 1) {

                document.getElementById('jsMensaje').innerHTML = "Filtros guardados con Éxito";
                openMensa();
            }
            else {
                document.getElementById('jsMensaje').innerHTML = "Error";
                openMensa();
            }

        },
        error: function (request, status, error) {
            document.getElementById('jsMensaje').innerHTML = "El servidor no responde, Vuelva a intentarl";
            openMensa();
        }
    });
}

function openMensa() {

    var toastLiveExample = document.getElementById('liveToast')
    if (true) {

        var toast = new bootstrap.Toast(toastLiveExample)

        toast.show()

    }
}

function whatFilter() {
    let cliente = document.getElementById("jsCliente").value;

    mostrarHTML("form-001")
    mostrarHTML("form-002")

    if ( cliente == "0-PLANTILLA") {
        loadMiPlantilla();
    } else {
        loadingFilter();
    }
}




function loadingFilter() {

    printCharts();
    printCharts_2();

    var url = "../configureRandom/loadingFilter";
    var data =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        MES_ASIGNADO: generarFechaModelo("1"),
        AÑO_ASIGNADO: generarFechaModelo("2")
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {
                
                insertValue("jsFiltroMonitoreos", data[0]["NUMERO_MONITOREOS"])
                insertValue("jsFiltroFechaIncial", data[0]["FECHA_INICIAL"])
                insertValue("jsFiltroFechaFinal", data[0]["FECHA_FINAL"])
                insertValue("jsFiltroInicioCorta", data[0]["INICIO_LLAMADA_CORTA"])
                insertValue("jsFiltroFinCorta", data[0]["FIN_LLAMADA_CORTA"])
                insertValue("jsFiltroInicioLarga", data[0]["INICIO_LLAMADA_LARGA"])
                insertValue("jsFiltroFinLarga", data[0]["FIN_LLAMADA_LARGA"])
                insertValue("jsFiltroRecontacto", data[0]["RECONTACTO"])
                insertValue("jsFiltroTransferencia", data[0]["TRASFERIDA"])
                insertValue("jsFiltroHoldInicial", data[0]["HOLD_INICIAL"])
                insertValue("jsFiltroHoldFinal", data[0]["HOLD_FINAL"])
                insertValue("jsFiltroAhtInicial", data[0]["AHT_INI"])
                insertValue("jsFiltroAhtFinal", data[0]["AHT_FIN"])

                insertInnerHTML("jscsat", "Filtros asignados: " + data[0]["CSAT"])
                insertInnerHTML("jsces", "Filtros asignados: " + data[0]["CES"])
                insertInnerHTML("jsfcr", "Filtros asignados: " + data[0]["FCR"])
                insertInnerHTML("jsnps", "Filtros asignados: " + data[0]["NPS"])

                loadAgentesGuardados(data[0]["AGENTE_EVALUAR"]);

                document.getElementById('jsMensaje').innerHTML = "Filtros cargados con Éxito";
                openMensa();
                mostrarHTML("form-11");
                mostrarHTML("form-12");
            }
            else {
                document.getElementById('jsMensaje').innerHTML = "No hay filtros asignados";
                mostrarHTML("jsBTNMiPlantilla");
                openMensa();
            }

        },
        error: function (request, status, error) {
            document.getElementById('jsMensaje').innerHTML = "El servidor no responde, Vuelva a intentarl";
            openMensa();
        }
    });
}

function insertValue(id,contenido) {

    document.getElementById(id).value = contenido;

}

function insertInnerHTML(id, contenido) {

    document.getElementById(id).innerHTML = contenido;

}



function loadAgentesGuardados(idAgentesEvaluar) {

    let nombreAnalista;
    var url = "../configureRandom/loadAgentesEvaluar";
    var data =
    {
        idAgentesEvaluar: idAgentesEvaluar
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {

                for (var i = 0; i < data.length; i++) {

                    contador++;
                    numFilasTable++;
                    nombreAnalista = data[i]["tbl_factempleados"]["Nombre"];
                    let HTML =
                        " <tr id='" + contador + "'>\
                                    <td _msthash='3589521' _msttexthash='151567'>"+ data[i]["LOGIN_AGENTE"] + "</td>\
                                    <td _msthash='3589521' _msttexthash='151567'>"+ data[i]["CCMS_AGENTE"] + "</td>\
                                    <td _msthash='3589521' _msttexthash='151567'>"+ nombreAnalista.toUpperCase() + "</td>\
                                    <td _msthash = '3589521' _msttexthash = '151567' >"+ data[i]["NUMERO_MONITOREOS"] + "</td>\
                                    <td _msthidden='2'>\
                                        <div class='dropdown' _msthidden = '2' >\
                                            <button type='button' class='btn p-0 dropdown-toggle hide-arrow' data-bs-toggle='dropdown'><i class='bx bx-dots-vertical-rounded'></i></button>\
                                            <div class='dropdown-menu' _msthidden='2'>\
                                                <a class='dropdown-item' href='javascript:void(0);' _msthidden='1' onclick='EliminarElementoFila("+ contador + ");'>\
                                                    <i class='bx bx-trash me-1'></i>\
                                                    <font _mstmutation='1' _msthash='5168371' _msttexthash='74802' _msthidden='1'> Eliminar</font>\
                                                </a>\
                                            </div>\
                                        </div>\
                                    </td>\
                                </tr>";

                    document.getElementById('agentesEvaluar').innerHTML += HTML;
                }
                loadNumeroMonitoreosAgente();

            } else {
                
                document.getElementById('jsMensaje').innerHTML = "No hay filtros asigados, llene los campos y guarde la configuracióon";
                openMensa();
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

    



}

function loadSubClientes() {

    var url = "OnChangeLsCliente/App";
    var data =
    {
        IdCliente: document.getElementById("jsCliente").value
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {
                document.getElementById("jslab").innerHTML = "<option value='0'>Select</option>";

                for (var i = 0; i < data.length; i++) {

                    document.getElementById("jslab").innerHTML +=
                        "<option value='" + data[i]["Skill"] + "'>" + data[i]["Nombre"] + "</option>";


                }

            } else {
                document.getElementById("jslab").innerHTML = "<option value='Select'>Select</option>";
                document.getElementById("jslab").innerHTML += "<option value='0-PLANTILLA'>MI PLANTILLA</option>";
                document.getElementById('jsMensaje').innerHTML = "No hay filtros asigados, llene los campos y guarde la configuracióon";
                openMensa();
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function loadAnalistas() {

    var url = "loadAnalistas/Admin";

    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {

            document.getElementById("jsAnalistas").innerHTML = "";

            for (var i = 0; i < data.length; i++) {

                document.getElementById("jsAnalistas").innerHTML +=
                    "<option value='" + data[i]["CCMS"] + "'>" + data[i]["CCMS"] + "</option>";
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function loadAnalysLob() {

    document.getElementById('lisAnalys').style.display = 'flex';
    document.getElementById('jsFiltroCuota').value = "";

    var url = "loadAnalysLob/Admin";
    var data =
    {
        CLIENTE: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            document.getElementById("listAnalys").innerHTML = "";

            let numAnalys = data.length;
            document.getElementById('jsFiltroCuota').value = numAnalys;


            for (var i = 0; i < data.length; i++) {

                document.getElementById("listAnalys").innerHTML +=
                    "<li class='list-group-item d-flex justify-content-between align-items-center'>" +
                    data[i]["NOMBRE_ANALISTA"] + "  -  " + data[i]["CCMS_ANALISTA"] +
                    "<span class='badge bg-primary' id='jsEstado'>" + data[i]["ESTADO"] + "</span>" +
                    "</li>";
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

    loadingFilter();

}

function LoadAgentesCliente() {

    limpiarCamposInner('jsAgente');
    limpiarCamposInner('jsClienteSeleccionado');

    let cliente = document.getElementById('jsCliente').value;
    let filtro = document.getElementById('jsFiltroAgentes').value;

    document.getElementById('jsClienteSeleccionado').innerHTML = "Filtro de agentes para el cliente: " + escapeHtml(cliente);

    if (filtro.length >= 3) {

        var url = "agentesFinales/App";
        var data =
        {
            cliente: cliente,
            filtro: filtro
        }
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data.length != 0) {

                    document.getElementById("jsAgente").innerHTML = "<option value='0'>Select</option>";

                    for (var i = 0; i < data.length; i++) {

                        var call = data[i]["TBL_FactEmpleados"]["Nombre"];
                        var idCcms = data[i]["TBL_FactEmpleados"]["idccms"];

                        document.getElementById("jsAgente").innerHTML +=
                            "<option value='"+ call.toUpperCase() +"-" +data[i]["AGENTE_FINAL"]+ "-" + idCcms +"'>" + call.toUpperCase() + "--- LOGIN: " + data[i]["AGENTE_FINAL"] + "</option>";

                        
                    }
                    closeAlerta('NuevaAlerta-1');


                } else {
                    openAlerta("No se encuentran agentes para el filtro")
                }


            },
            error: function (request, status, error) {
                openAlerta("Tiempo de espera superado, vuelva a intentarlo")
            }
        });
        
    } else {
        openAlerta("El capo filtro debe de contener más caracteres")
    }

    
}

function loadFormNumeroMonitoreos() {
    document.getElementById('jsNumeroMonitoreos').style.display = 'none';
    let agente = document.getElementById('jsAgente').value;
    if (agente != 0) {

        document.getElementById('jsNumeroMonitoreos').style.display = 'flex';
    } else {
        openAlerta("Seleccione un agente")
    }
}

function loadBtnGuardarFiltro() {
    document.getElementById('jsBtnGuardar').style.display = 'none';
    let numeroMonitoreos = document.getElementById('jsFiltroNumeroMonitoreos').value;

    if (numeroMonitoreos != 0) {
        document.getElementById('jsBtnGuardar').style.display = 'block';
    } else {
        openAlerta("Asigne número de monitoreo")
    }
}

function limpiarCamposValue(id) {

    document.getElementById(id).value = '';
}

function inputValueInZero(id) {

    document.getElementById(id).value = 0;
}

function limpiarCamposInner(id) {

    document.getElementById(id).innerHTML = '';
}



function openModal() {

    limpiarCamposValue('jsFiltroAgentes');
    limpiarCamposValue('jsAgente');
    let cliente = document.getElementById('jsCliente').value;

    document.getElementById('jsClienteSeleccionado').innerHTML = "Filtro de agentes para el cliente: " + escapeHtml(cliente);

    var OpenModal = new bootstrap.Modal(document.getElementById("jsModalAgente"))
    OpenModal.toggle();

}

function openAlerta(mensaje) {

    document.getElementById('NuevaAlerta-1').innerHTML = mensaje;
    document.getElementById('NuevaAlerta-1').style.display = 'inline-block';
}

function closeAlerta(id) {

    document.getElementById(id).style.display = 'none';
}

//********************ID DEL TR **********************

let contador = 0;
let numFilasTable = 0;

function loadAnalistHTML() {
    contador++;
    numFilasTable++;
    let agente = document.getElementById('jsAgente').value;
    let agenteNombreLogin = agente.split('-');
    let numeroMonitoreosAgente = document.getElementById('jsFiltroMonitoreosAgente').value;

    if (numeroMonitoreosAgente == "") {
        numeroMonitoreosAgente = 0;
    }
    let nombreAgente = agenteNombreLogin[0];
    let loginAgente = agenteNombreLogin[1];
    let idCcmsAgente = agenteNombreLogin[2];
    let numeroMonitoreos = document.getElementById('jsFiltroNumeroMonitoreos').value;

    // Crear los elementos y nodos de texto
    let tr = document.createElement('tr');
    tr.id = contador;

    let tdLogin = document.createElement('td');
    tdLogin.textContent = loginAgente;
    tr.appendChild(tdLogin);

    let tdIdCcms = document.createElement('td');
    tdIdCcms.textContent = idCcmsAgente;
    tr.appendChild(tdIdCcms);

    let tdNombre = document.createElement('td');
    tdNombre.textContent = nombreAgente;
    tr.appendChild(tdNombre);

    let tdMonitoreos = document.createElement('td');
    tdMonitoreos.textContent = numeroMonitoreos;
    tr.appendChild(tdMonitoreos);

    let tdDropdown = document.createElement('td');
    tdDropdown.setAttribute('msthidden', '2');
    let dropdownDiv = document.createElement('div');
    dropdownDiv.className = 'dropdown';
    dropdownDiv.setAttribute('msthidden', '2');
    let dropdownButton = document.createElement('button');
    dropdownButton.type = 'button';
    dropdownButton.className = 'btn p-0 dropdown-toggle hide-arrow';
    dropdownButton.setAttribute('data-bs-toggle', 'dropdown');
    let dropdownIcon = document.createElement('i');
    dropdownIcon.className = 'bx bx-dots-vertical-rounded';
    dropdownButton.appendChild(dropdownIcon);
    dropdownDiv.appendChild(dropdownButton);
    let dropdownMenu = document.createElement('div');
    dropdownMenu.className = 'dropdown-menu';
    dropdownMenu.setAttribute('msthidden', '2');
    let dropdownItem = document.createElement('a');
    dropdownItem.className = 'dropdown-item';
    dropdownItem.href = 'javascript:void(0);';
    dropdownItem.setAttribute('msthidden', '1');
    dropdownItem.setAttribute('onclick', 'EliminarElementoFila(' + contador + ');');
    let trashIcon = document.createElement('i');
    trashIcon.className = 'bx bx-trash me-1';
    dropdownItem.appendChild(trashIcon);
    let dropdownText = document.createTextNode('Eliminar');
    dropdownItem.appendChild(dropdownText);
    dropdownMenu.appendChild(dropdownItem);
    dropdownDiv.appendChild(dropdownMenu);
    tdDropdown.appendChild(dropdownDiv);
    tr.appendChild(tdDropdown);

    // Agregar la fila al elemento con id "agentesEvaluar"
    document.getElementById('agentesEvaluar').appendChild(tr);

    loadNumeroMonitoreosAgente();
}


function loadSelectMultiple(id) {


    let documento = document.getElementsByClassName(id);
    const questions = new Array();
    const question = new Array();
    let CES = "0";
    let FCR = "0";
    let NPS = "0";
    let CSAT = "0";
    let listCES;
    let listCSAT;
    let listNPS;
    let listFCR;

    for (var i = 0; i < documento.length; i++) {

        questions.push(documento[i].innerText);
    }

    for (var i = 0; i < questions.length; i++) {

        if (questions[i].includes('CES = ')) {

            listCES = questions[i].split('=');
            CES += " -"+(listCES[1])
        }

        if (questions[i].includes('CSAT = ')) {

            listCSAT = questions[i].split('=');
            CSAT += " -" + (listCSAT[1])
        }

        if (questions[i].includes('NPS = ')) {

            listNPS = questions[i].split('=');
            NPS += " -" + (listNPS[1])
        }

        if (questions[i].includes('FCR = ')) {

            listFCR = questions[i].split('=');
            FCR += " -" + (listFCR[1])
        }

    }

    question.push(CES, CSAT, NPS, FCR)
    return(question)
}



function loadAgentesEvaluar(idAgentesEvaluar) {
    
    for (var i = 0; i <= contador; i++) {

        try {

            let obtenerFila = document.getElementById(i);
            let elementosFila = obtenerFila.getElementsByTagName("td");
            let ID_AGENTES = idAgentesEvaluar;
            let NUMERO_AGENTES = numFilasTable;
            let LOGIN_AGENTE = elementosFila[0].innerHTML;
            let CCMS_AGENTE = elementosFila[1].innerHTML;
            let NOMBRE_AGENTE = elementosFila[2].innerHTML;
            let NUMERO_MONITOREOS = elementosFila[3].innerHTML;

            var url = "../configureRandom/saveConfiguracionAgentesEvaluar";
            var data =
            {
                ID_AGENTES: ID_AGENTES,
                CCMS_AGENTE: CCMS_AGENTE,
                NUMERO_AGENTES: NUMERO_AGENTES,
                LOGIN_AGENTE: LOGIN_AGENTE,
                NOMBRE_AGENTE: NOMBRE_AGENTE,
                NUMERO_MONITOREOS: NUMERO_MONITOREOS
            }
            $.ajax({
                url: url,
                type: 'POST',
                data: data,
                success: function (data) {

                    if (data = "true") {

                        document.getElementById('jsMensaje').innerHTML = "Agente " + LOGIN_AGENTE +" guardado";
                        openMensa();

                    } else {
                        document.getElementById('jsMensaje').innerHTML = "Error al guardar el Agente " + LOGIN_AGENTE;
                        openMensa();
                    }

                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });

            


        } catch (e) {

        }

    }

}

function loadNumeroMonitoreosAgente() {

    let numeromonitoreos = 0;

    for (var i = 0; i <= contador; i++) {

        try {

            let obtenerFila = document.getElementById(i);
            let elementosFila = obtenerFila.getElementsByTagName("td");
            let NUMERO_MONITOREOS = elementosFila[3].innerHTML;

            if (NUMERO_MONITOREOS == "") {
                NUMERO_MONITOREOS = 0;
            }
            numeromonitoreos = parseInt(NUMERO_MONITOREOS) + parseInt(numeromonitoreos);

        } catch (ex) {

        }

    }

    document.getElementById('jsFiltroMonitoreosAgente').value = numeromonitoreos; 

}

function EliminarElementoFila(id) {

    $("#" + id).remove();
    numFilasTable = numFilasTable - 1;
    loadNumeroMonitoreosAgente();

}

function mostrarHTML(id) {

    document.getElementById(id).style.display = 'inline-block';
}

function mostrarSpiner(id) {

    document.getElementById(id).style.display = 'flex';
    document.getElementById(id).style.flexDirection = 'column';
}
function mostrarAlert(id, mensaje) {
    let html = "<div class='alert alert-dark alert - dismissible mb - 0' role='alert'>\
                    <font _mstmutation = '1' _msthash = '2891525' _msttexthash = '2202746'>"+mensaje+"</font >\
                    <button type = 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Cerrar' _mstaria-label='76414' >\
                    </button>\
                </div >";
    document.getElementById(id).innerHTML += html ;
    document.getElementById(id).style.display = '';
}

function ocultarAlert(id) {
    let html = "";
    document.getElementById(id).innerHTML = html;
    document.getElementById(id).style.display = 'block';
}

function ocultarSpiner(id) {

    document.getElementById(id).style.display = 'none';
}



function ocultarHTML(id) {

    document.getElementById(id).style.display = 'none';
}


function generarFechaModelo(id) {

    let dateDay = new Date();
    let day = dateDay.getDate();
    let month = dateDay.getMonth() + 1;
    let year = dateDay.getFullYear();
    let seg = dateDay.getSeconds();
    let min = dateDay.getMinutes();
    let hora = dateDay.getHours();
    day = ('0' + day).slice(-2);
    month = ('0' + month).slice(-2);

    let fecha = `${year}-${month}-${day} ${hora}:${min}:${seg}`
    let fecha3 = `${year}-${month}`
    let fecha4 = `${year}-${month}-${day}`
    let fecha1 = `${month}`
    let fecha2 = `${year}`
    let fecha5 = `${day}`

    if (id == 1) {
        return fecha1;
    }

    if (id == 2) {
        return fecha2;
    }

    if (id == 3) {
        return fecha3;
    }

    if (id == 4) {
        return fecha4;
    }

    if (id == 5) {
        return fecha5;
    }

    

}

function loadCuotasDiarias() {

    var url = "loadCuotaAleatoriedad/App";
    var data =
    {
        Cliente: document.getElementById("jsCliente").value,
        IdLob: document.getElementById("jslab").value,
        Mes: generarFechaModelo("1"),
        Año: generarFechaModelo("2")
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {

                if (data == 0) {
                    document.getElementById("jsFiltroMonitoreos").value = 0;
                    document.getElementById('jsMensaje').innerHTML = "No existe cuota cargada por el Manager";
                    openMensa();
                } else {
                    document.getElementById("jsFiltroMonitoreos").value = data;
                }
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function saveDataConfiguracionAleatoriedad() {

    document.getElementById('jsAlerta').innerHTML = "";
    ocultarAlert("jsAlerta");
    mostrarSpiner("jsSpinner_4");

        let cont = 0;
        let CAMPAÑA= document.getElementById("jsCliente").value;
        let FECHA_INICIAL= document.getElementById("jsFiltroFechaIncial").value;
        let FECHA_FINAL= document.getElementById("jsFiltroFechaFinal").value;
        let NUMERO_MONITOREOS= document.getElementById("jsFiltroMonitoreos").value;
        let NUMERO_MONITOREOS_AGENTE = document.getElementById("jsFiltroMonitoreosAgente").value;
        let LOB= document.getElementById("jslab").value;
        let INICIO_LLAMADA_CORTA= document.getElementById("jsFiltroInicioCorta").value;
        let FIN_LLAMADA_CORTA= document.getElementById("jsFiltroFinCorta").value;
        let INICIO_LLAMADA_LARGA= document.getElementById("jsFiltroInicioLarga").value;
        let FIN_LLAMADA_LARGA= document.getElementById("jsFiltroFinLarga").value;
        let MES_ASIGNADO= generarFechaModelo("1");
        let AÑO_ASIGNADO = generarFechaModelo("2");
        const question = loadSelectMultiple('item-container');
        let RECONTACTO = document.getElementById("jsFiltroRecontacto").value;
        let TRASFERIDA = document.getElementById("jsFiltroTransferencia").value;
        let CSAT = question[1];
        let NPS = question[2];
        let FCR = question[3];
        let CES = question[0];
        let HOLD_INICIAL = document.getElementById("jsFiltroHoldInicial").value;
        let HOLD_FINAL = document.getElementById("jsFiltroHoldFinal").value;
        let AHT_INI = document.getElementById("jsFiltroAhtInicial").value;
        let AHT_FIN = document.getElementById("jsFiltroAhtFinal").value;


    if (NUMERO_MONITOREOS == 0) {

        mostrarAlert("jsAlerta", "No olvide cargar la cuota mensual ")
    }
        
    if (FECHA_INICIAL == '') {
        mostrarAlert("jsAlerta", "Seleccione la fecha inicial ")

        cont++
    }

    if (FECHA_FINAL == '') {
        mostrarAlert("jsAlerta", "Seleccione la fecha final ")

        cont++
    }
 

    if (NUMERO_MONITOREOS_AGENTE != NUMERO_MONITOREOS) {
        mostrarAlert("jsAlerta", "Los campos NUMERO MONITOREOS AGENTE y NUMERO MONITOREOS AGENTE deben de coincidir o NUMERO MONITOREOS AGENTE debe ser cero (0)")

        cont++

        if (NUMERO_MONITOREOS_AGENTE == 0) {
            cont = cont - 1;
        }
        
    }


    if (cont == 0) {
        var url = "../configureRandom/saveDataConfiguracionAleatoriedad";
        var data =
        {
            CAMPAÑA: CAMPAÑA,
            FECHA_INICIAL: FECHA_INICIAL,
            FECHA_FINAL: FECHA_FINAL,
            NUMERO_MONITOREOS: NUMERO_MONITOREOS,
            LOB: LOB,
            INICIO_LLAMADA_CORTA: INICIO_LLAMADA_CORTA,
            FIN_LLAMADA_CORTA: FIN_LLAMADA_CORTA,
            INICIO_LLAMADA_LARGA: INICIO_LLAMADA_LARGA,
            FIN_LLAMADA_LARGA: FIN_LLAMADA_LARGA,
            MES_ASIGNADO: MES_ASIGNADO,
            AÑO_ASIGNADO: AÑO_ASIGNADO,            
            RECONTACTO : RECONTACTO,
            TRASFERIDA : TRASFERIDA,
            CSAT : CSAT,
            NPS : NPS,
            FCR : FCR,
            CES : CES,
            HOLD_INICIAL : HOLD_INICIAL,
            HOLD_FINAL : HOLD_FINAL,
            AHT_INI: AHT_INI,
            AHT_FIN: AHT_FIN
        }
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data.legend != 0) {

                    /***************ID DE LA LISTA DE AGENTES ****************/
                  
                    loadAgentesEvaluar(data);

                }
                else {

                    document.getElementById('jsMensaje').innerHTML = "Hubo un error, Intente de nuevo";
                    openMensa();
                }
                ocultarSpiner("jsSpinner_4");
            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });
        
    } else {
        ocultarSpiner("jsSpinner_4");
    }

    
}


function fechasMaxMin() {
    document.getElementById('jsFiltroFechaFinal').min = document.getElementById('jsFiltroFechaIncial').value;
}

function loadMiPlantilla() {

    document.getElementById('jsAlerta').innerHTML = "";
    ocultarAlert("jsAlerta")
    document.getElementById("jsFiltroMonitoreos").value = 0;

    var url = "../configureRandom/loadingFilterMiPlantilla";
    var data =
    {
        CAMPAÑA: "0-PLANTILLA",
        LOB: "0-PLANTILLA"
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {


                insertValue("jsFiltroMonitoreos", data[0]["NUMERO_MONITOREOS"])
                insertValue("jsFiltroFechaIncial", data[0]["FECHA_INICIAL"])
                insertValue("jsFiltroFechaFinal", data[0]["FECHA_FINAL"])
                insertValue("jsFiltroInicioCorta", data[0]["INICIO_LLAMADA_CORTA"])
                insertValue("jsFiltroFinCorta", data[0]["FIN_LLAMADA_CORTA"])
                insertValue("jsFiltroInicioLarga", data[0]["INICIO_LLAMADA_LARGA"])
                insertValue("jsFiltroFinLarga", data[0]["FIN_LLAMADA_LARGA"])
                insertValue("jsFiltroRecontacto", data[0]["RECONTACTO"])
                insertValue("jsFiltroTransferencia", data[0]["TRASFERIDA"])
                insertValue("jsFiltroHoldInicial", data[0]["HOLD_INICIAL"])
                insertValue("jsFiltroHoldFinal", data[0]["HOLD_FINAL"])
                insertValue("jsFiltroAhtInicial", data[0]["AHT_INI"])
                insertValue("jsFiltroAhtFinal", data[0]["AHT_FIN"])

                insertInnerHTML("jscsat", "Filtros asignados: " + data[0]["CSAT"])
                insertInnerHTML("jsces", "Filtros asignados: " + data[0]["CES"])
                insertInnerHTML("jsfcr", "Filtros asignados: " + data[0]["FCR"])
                insertInnerHTML("jsnps", "Filtros asignados: " + data[0]["NPS"])

                document.getElementById('jsMensaje').innerHTML = "Filtros cargados con Éxito";
                openMensa();
                ocultarHTML("jsBTNMiPlantilla");
            }
            else {
                document.getElementById('jsMensaje').innerHTML = "No hay filtros asignados";
                openMensa();
            }

            
        },
        error: function (request, status, error) {
            document.getElementById('jsMensaje').innerHTML = "El servidor no responde, Vuelva a intentarl";
            openMensa();
        }
    });
}



function generarFechaModelo(id) {

    let dateDay = new Date();
    let day = dateDay.getDate();
    let month = dateDay.getMonth() + 1;
    let year = dateDay.getFullYear();
    let seg = dateDay.getSeconds();
    let min = dateDay.getMinutes();
    let hora = dateDay.getHours();
    day = ('0' + day).slice(-2);
    month = ('0' + month).slice(-2);

    let fecha = `${year}-${month}-${day} ${hora}:${min}:${seg}`
    let fecha3 = `${year}-${month}-${day}`
    let fecha1 = `${month}`
    let fecha2 = `${year}`

    if (id == 1) {

        return fecha1;

    } else if (id == 2) {
        return fecha2;

    } else if (id == 3) {
        return fecha3;
    } else {
        return fecha;
    }

}


function loadCuotas() {

    let cuotaMensual;

    var url = "../configureRandom/loadMesActividad";
    var data =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        MES_ASIGNADO: generarFechaModelo("1"),
        AÑO_ASIGNADO: generarFechaModelo("2")
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {

                cuotaMensual = data[0]["NUMERO_MONITOREOS"]
            } else {
                cuotaMensual = 0;
            }
            alert(cuotaMensual)
            return cuotaMensual;
            
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
    
}



const printCharts_2 = () => {
    renderModelsChart_2()
}


const printCharts = () => {
    renderModelsChart()
}

//const renderModelsChart = () => {
    
//    removeData("grafica_2");
//    var url = "../configureRandom/loadCuotas";
//    var dataAjax =
//    {
//        CAMPAÑA: document.getElementById("jsCliente").value,
//        LOB: document.getElementById("jslab").value,
//        MES_ASIGNADO: generarFechaModelo("1"),
//        AÑO_ASIGNADO: generarFechaModelo("2")
//    }
//    $.ajax({
//        url: url,
//        type: 'POST',
//        data: dataAjax,
//        success: function (cuotas) {
//            let jsCuotas = [cuotas[0], cuotas[1], cuotas[2]]

//            const data = {
//                labels: [
//                    'Cota diaria',
//                    'Cota Cumplida',
//                    'Cota Faltante'
//                ],
//                datasets: [{
//                    label: 'Llamadas',
//                    data: jsCuotas,
//                    backgroundColor: [
//                        'rgb(67, 154, 151)',
//                        'rgb(98, 182, 183)',
//                        'rgb(203, 237, 213)'
//                    ],
//                    hoverOffset: 3
//                }]

//            };


//            new Chart('grafica_2', {
//                type: 'doughnut',
//                data: data,
//                options: {
//                    responsive: true,
//                    plugins: {
//                        legend: {
//                            position: 'top',
//                        },
//                        customCanvasBackgroundColor: {
//                            color: '#0000',
//                        }
//                    }
//                },
//            })
//        },
//        error: function (request, status, error) {
//            alert(request.responseText);
//        }
//    });

    
//}

const renderModelsChart = () => {

    var url = "../configureRandom/loadCuotas";
    var dataAjax =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        MES_ASIGNADO: generarFechaModelo("1"),
        FECHA: generarFechaModelo("4"),
        AÑO_ASIGNADO: generarFechaModelo("2")
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: dataAjax,
        success: function (cuotas) {
          
            let jsCuotas = [cuotas[0], cuotas[1], cuotas[2]];

            const data = {
                labels: [
                    'Cota Diaria',
                    'Cota Cumplida',
                    'Cota Faltante'
                ],
                datasets: [{
                    label: 'Llamadas',
                    data: jsCuotas,
                    backgroundColor: [
                        'rgb(67, 154, 151)',
                        'rgb(98, 182, 183)',
                        'rgb(203, 237, 213)'
                    ],
                    hoverOffset: 3
                }]

            };

            var ctx2 = document.getElementById('grafica_2').getContext('2d');

            if (window.grafica) {
                window.grafica.clear();
                window.grafica.destroy();
            }

            window.grafica = new Chart(ctx2, {

                type: 'doughnut',
                data: data,
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        customCanvasBackgroundColor: {
                            color: '#0000',
                        }
                    }
                },

            });

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });


}

const renderModelsChart_2 = () => {

 
    var url = "../configureRandom/loadCuotasMensual";
    var dataAjax =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        MES_ASIGNADO: generarFechaModelo("1"),
        FECHA: generarFechaModelo("3"),
        AÑO_ASIGNADO: generarFechaModelo("2")
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: dataAjax,
        success: function (cuotas) {

            let jsCuotas = [cuotas[0], cuotas[1], cuotas[2]];

            const data2 = {
                labels: [
                    'Cota Mensual',
                    'Cota Cumplida',
                    'Cota Faltante'
                ],
                datasets: [{
                    label: 'Llamadas',
                    data: jsCuotas,
                    backgroundColor: [
                        'rgb(13, 76, 146)',
                        'rgb(89, 193, 189)',
                        'rgb(160, 228, 203)'
                    ],
                    hoverOffset: 3
                }]

            };

            var ctx3 = document.getElementById('grafica_3').getContext('2d');

            if (window.grafica3) {
                window.grafica3.clear();
                window.grafica3.destroy();
            }

            window.grafica3 = new Chart(ctx3, {

                type: 'doughnut',
                data: data2,
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        customCanvasBackgroundColor: {
                            color: '#0000',
                        }
                    }
                },

            });

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });


}

function addData(chart, label, data) {
    chart.data.labels.push(label);
    chart.data.datasets.forEach((dataset) => {
        dataset.data.push(data);
    });
    chart.update();
}


//const renderModelsChart_2 = () => {


//    const data2 = {
//        labels: [
//            'Cota Mensual',
//            'Cota Mensual',
//            'Cota Mensual',
//            'Cota Mensual'
//        ],
//        datasets: [{
//            label: 'My First Dataset',
//            data: [65, 59, 80, 81],
//            backgroundColor: [
//                'rgb(13, 76, 146)',
//                'rgb(89, 193, 189)',
//                'rgb(160, 228, 203)',
//                'rgb(207, 245, 231)'
//            ],
//            fill: false,
//            tension: 0.1
//        }]

//    };


//    new Chart('grafica_3', {
//        type: 'doughnut',
//        data: data2,
//    })
//}


function removeData(chart) {
    chart.data.labels.pop();
    chart.data.datasets.forEach((dataset) => {
        dataset.data.pop();
    });
    chart.update();
}