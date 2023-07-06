/****************************************HEADER**********************************/
myfunction();

function closeSession() {
    var url = "LoginOff/App";
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
    var url = "LoandName/App";
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


function loadNameInicial(data) {

    let nombreApellido = data[0]["Nombre"];
    let text = nombreApellido.split(' ');

    let primerInicial = text[0].charAt(0);
    let segundaInicial = text[1].charAt(0);

    document.getElementById('nameUserInicial').innerHTML = primerInicial + segundaInicial;
    document.getElementById('jsDateDay').innerHTML = headergenerarFecha();


}

function headergenerarFecha() {

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

    let fecha2 = `${day}/${month}/${year}`

    return fecha2;
}

function myfunction() {
    loandNameInit();
    loadListClientes();
    loadListManager();
}


//**************************************FUNCIONES DE LA VISTA ***********************

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

function loadDataUser() {

    var CCMS = document.getElementById('jsCCMS').value;


    if (CCMS != "") {

        var url = "../AdminResultsRandom/loadDataUser";
        var data =
        {
            CCMS: CCMS
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {
                document.getElementById("jsNombre").value = "";

                if (data.length != 0) {
                    document.getElementById('jsNombre').value = data[0]["EMAIL"];

                } else {

                    var CCMS = document.getElementById('jsCCMS').value = "";
                    openMensa("El usuario no se encuentra en la base de datos, comoniquese con soporte...");
                }
            }
        });

    }
}

function openMensa(mensaje) {

    var toastLiveExample = document.getElementById('liveToast');
    document.getElementById('jsMensaje').innerHTML = mensaje;
    if (true) {

        var toast = new bootstrap.Toast(toastLiveExample)

        toast.show()

    }
}

function loadActgenerada() {

    
    ocultarHTML("form-11");
    ocultarHTML("form-12");

    var fechaInicial = document.getElementById('jsFiltroInicial').value;
    var fechaFinal = document.getElementById('jsFechaFinal').value;
    var CCMS = document.getElementById('jsCCMS').value;
    var cliente = document.getElementById('jsCliente').value;
    var lob = document.getElementById('jslab').value;

    if (fechaInicial != "" && fechaFinal != "" && cliente != "" && lob != "") {
        printCharts();
        printCharts_2();
        

        var url = "../AdminResultsRandom/dataUserRamdomCallCustoms";
        var data =
        {
            CCMS: CCMS,
            cliente: cliente,
            lob: lob,
            fechaInicial: fechaInicial,
            fechaFinal: fechaFinal
            
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {
                document.getElementById("jsMostrarTBGenerada").innerHTML = "";

                if (data.length != 0) {

                    for (var i = 0; i < data.length; i++) {

                        var id = data[i]["ID_PDF"].toString();


                        document.getElementById("jsMostrarTBGenerada").innerHTML +=
                            "<tr>" +
                            "<td>" +
                            "<strong _msthash='3656601' _msttexthash='286260' id='jsIDPDF'>" + data[i]["ID_PDF"] + "</strong>" +
                            "</td>" +
                            "<td _msthash='3656731' _msttexthash='151567'>" + data[i]["CRETE_DATETIME"] +
                            "<td> " + "<span class='badge bg-label-primary me-1' _msthash='3656601' _msttexthash='286260'>" + data[i]["Nombre_Campaña"] + "</span>" +
                            "<td> " + "<span class='badge bg-label-primary me-1' _msthash='3656601' _msttexthash='286260'>" + data[i]["Lob"] + "</span>" +
                            "<td> " +
                            "<div class='dropdown' _msthidden='2'>" +
                            "<button type='button' class='btn p-0 dropdown-toggle hide-arrow' data-bs-toggle='dropdown' aria-expanded='false'>" +
                            "<i class='bx bx-dots-vertical-rounded'></i></button >" +
                            "<div class='dropdown-menu' style='' _mstvisible='0'>" +
                            "<a class='dropdown-item' onclick='loadCumplimiento(" + JSON.stringify(id) + ");' _mstvisible='1' id='" + JSON.stringify(id) + "'>" +
                            "<i class='bx bx-edit-alt me-1' _mstvisible= '2'></i> " +
                            "<font _mstmutation='1' _msthash='5245240' _msttexthash='75699' _mstvisible='2'> VER</font>" +
                            "</a > " +
                            "</div>" +
                            "</div>" +
                            "</tr> ";
                    }

                    mostrarHTML("form-11");
                    mostrarHTML("form-12");
                } else {
                    openMensa("No hay datos para generar el informe");
                }

                
            }
        });

    } else {
        openMensa("Debes ingresar todos los campos solicitados ");
    }
}

function loadCumplimiento(referencia) {

    document.getElementById('jsReferenciaActividad').innerHTML = referencia;

    loadDataActividad(referencia);

    var url = "../Random/actividadLlamadas";
    var data =
    {
        IDPDF: referencia
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {
            document.getElementById("tbodytbaleatorio2").innerHTML = "";

            if (data.length < 1) {
                mostrarAlerta("contenidoAlert", "Error, cierre la ventana y vuelva a intentarlo")
            } else {

                let actividadHTML = "";

                for (var i = 0; i < data.length; i++) {

                    var ID = data[i]["ID"].toString();

                    actividadHTML +=
                        "<tr>" +
                        "<td> " + data[i]["POSITION_PDF"] +
                        "</td> <td>" + data[i]["UCID"] +
                        "</td> <td>" + data[i]["DURACION"] +
                        "</td> <td> " + data[i]["TIMES_HELD"] +
                        "</td> <td> " + data[i]["AGENTE_INICIAL"] +
                        "</td> <td> " + data[i]["NOMBRE_AGENTE"] +
                        "</td> <td> " + data[i]["TALK_TIME"] +
                        "</td> <td> " + data[i]["HOLD_TIME"] +
                        "</td> <td> " + data[i]["ORIGEN_COLGADA"] +
                        "</td> <td> " + data[i]["TRANSFERIDA"] +
                        "</td> <td> ";

                    if (data[i]["CUMPLIMIENTO"] == "false") {

                        actividadHTML +=
                            "<div class='form-check form-check-dark'>\
                                <input class='form-check-input' type='checkbox' value='' id='" + data[i]["ID"] + "' readonly/>\
                                <label class='form-check-label' for='customCheckDark'></label>\
                            </div>\
                        </td> <td>\
                            <div>\
                                <textarea id='"+ data[i]["ID"] + "-observacion' class='form-control' style = 'height: 30px;' readonly>" + data[i]["OBSERVACION"] + "</textarea >\
                            </div >\
                        </td></tr>";
                    }
                    else if (data[i]["CUMPLIMIENTO"] == "true") {
                        actividadHTML +=
                            "<div class='form-check form-check-dark'>\
                                <input class='form-check-input' type='checkbox' checked value='' id='" + data[i]["ID"] + "' readonly/>\
                                <label class='form-check-label' for='customCheckDark'></label>\
                            </div>\
                        </td> <td>\
                            <div>\
                                <textarea id='"+ data[i]["ID"] + "-observacion' class='form-control' style = 'height: 30px;' readonly>" + data[i]["OBSERVACION"] + "</textarea >\
                            </div >\
                        </td></tr>";
                    } else {

                        actividadHTML +=
                            "<div class='form-check form-check-dark'>\
                                <input class='form-check-input' type='checkbox' value='' id='" + data[i]["ID"] + "' readonly/>\
                                <label class='form-check-label' for='customCheckDark'></label>\
                            </div>\
                        </td> <td>\
                            <div>\
                                <textarea id='"+ data[i]["ID"] + "-observacion' class='form-control' style = 'height: 30px;' readonly>" + data[i]["OBSERVACION"] + "</textarea >\
                            </div >\
                        </td></tr>";
                    }
                    document.getElementById("tbodytbaleatorio2").innerHTML = actividadHTML;
                }
                openModalVerActividad();
            }

        },
        error: function (request, status, error) {
            mostrarAlerta("contenidoAlert", "Error, cierre la ventana y vuelva a intentarlo")
        }
    });
}


function openModalVerActividad() {

    var OpenModal = new bootstrap.Modal(document.getElementById('fullscreenModal'))
    OpenModal.toggle()

}

function loadDataActividad(idActividad) {


    var url = "../Random/loadDataActividad";
    var data =
    {
        idActividad: idActividad
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {

                document.getElementById('jsEvaluador').innerHTML = data[0]["USERDATA"]["NOMBRES"] + data[0]["USERDATA"]["APELLIDOS"];
                document.getElementById('jsFechaGenerada').innerHTML = data[0]["CRETE_DATETIME"];
                document.getElementById('jsCLienteReferencia').innerHTML = data[0]["TBL_Info_Clientes"]["Nombre_Cliente"];
                document.getElementById('JsLobReferencia').innerHTML = data[0]["LOB"];

            }

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

}

/*******************************GRAFICAS *****************************/


const renderModelsChart = () => {

    var url = "../AdminResultsRandom/loadCuotas";
    var dataAjax =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        MES_ASIGNADO: generarFechaModelo("1"),
        FECHA: generarFechaModelo("4"),
        AÑO_ASIGNADO: generarFechaModelo("2"),
        CCMS_ANALISTA: document.getElementById("jsCCMS").value
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

    var url = "../AdminResultsRandom/loadCuotasMensual";
    var dataAjax =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        MES_ASIGNADO: generarFechaModelo("1"),
        FECHA: generarFechaModelo("3"),
        AÑO_ASIGNADO: generarFechaModelo("2"),
        CCMS_ANALISTA: document.getElementById("jsCCMS").value
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

const printCharts_2 = () => {
    renderModelsChart_2()
}


const printCharts = () => {
    renderModelsChart()
}

function mostrarHTML(id) {

    document.getElementById(id).style.display = 'inline-block';
}

function ocultarHTML(id) {

    document.getElementById(id).style.display = 'none';
}

function loadListManager() {
    var url = "../AdminResultsRandom/loadListManager";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            var result = "<option value=''>Select</option>"

            for (var i = 0; i < data.length; i++) {
                result += "<option value='" + data[i]["CCMS_ANALISTA"] + "'>" + data[i]["NOMBRE"]  + "</option>"
            }
            document.getElementById('jsCCMS').innerHTML = result;
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}