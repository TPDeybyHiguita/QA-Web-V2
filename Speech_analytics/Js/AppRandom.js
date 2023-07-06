

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
}








loadListClientes();

function idPDFnew() {

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

            document.getElementById("jslab").innerHTML = "<option value='Select'> Select </option>";

            for (var i = 0; i < data.length; i++) {

                document.getElementById("jslab").innerHTML +=
                    "<option value='" + data[i]["Nombre_Cliente"] + "'>" + data[i]["Nombre_Cliente"] + "</option>";


            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}


function loandNameInRandon() {
    var url = "LoandName/App";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            document.getElementById('jsFecha').innerHTML = generarFechaLoad();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function setEmail1() {
    var url = "EmailRandom/App";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            if (date = 1) {
                alert("ENVIADO")
            }else
            {
                alert("ERROR")
            }
            
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}



function setEmailCall() {

    document.getElementById('jsSpinner_3').style.display = 'inline-block';
    document.getElementById('jsbuton_4').style.display = 'none';

    var url = "sendEmailAleatoriedad/App";
    var data =
    {
        idPDF: document.getElementById("jsCampo1").innerHTML
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (date = 1) {
                document.getElementById('jsMensaje').innerHTML = "Mensaje enviado a su correo, verifique que sus datos esten actualizados";
                openMensa();
                document.getElementById('jsSpinner_3').style.display = 'none';
                document.getElementById('jsbuton_2').style.display = 'none';
                document.getElementById('jsbuton_4').style.display = 'inline-block';
            } else {
                document.getElementById('jsMensaje').innerHTML = "No se pudo envir el mensaje";
                openMensa();
                document.getElementById('jsSpinner_3').style.display = 'none';
                document.getElementById('jsbuton_4').style.display = 'inline-block';
            }

        },
        error: function (request, status, error) {
            document.getElementById('jsMensaje').innerHTML = "Error al enviar correo";
            openMensa();
            document.getElementById('jsSpinner_3').style.display = 'none';
            document.getElementById('jsbuton_4').style.display = 'inline-block';
        }
    });


}

function setEmailSurv() {

    document.getElementById('jsSpinner_4').style.display = 'inline-block';
    document.getElementById('jsbuton_2').style.display = 'none';

    var url = "sendEmailAleatoriedadSurv/App";
    var data =
    {
        idPDF: document.getElementById("jsCampo001").innerHTML
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (date = 1) {
                document.getElementById('jsMensaje').innerHTML = "Mensaje enviado a su correo, verifique que sus datos esten actualizados";
                openMensa();
                document.getElementById('jsSpinner_4').style.display = 'none';
                document.getElementById('jsbuton_4').style.display = 'none';
                document.getElementById('jsbuton_2').style.display = 'inline-block';
            } else {
                document.getElementById('jsMensaje').innerHTML = "No se pudo envir el mensaje";
                openMensa();
                document.getElementById('jsSpinner_4').style.display = 'none';
                document.getElementById('jsbuton_2').style.display = 'inline-block';
            }

        },
        error: function (request, status, error) {
            document.getElementById('jsMensaje').innerHTML = "Error al enviar correo";
            openMensa();
            document.getElementById('jsSpinner_4').style.display = 'none';
            document.getElementById('jsbuton_2').style.display = 'inline-block';
        }
    });


}

function loadLobCliente() {

    var url = "../Random/loadLobCliente";
    var data =
    {
        skill: document.getElementById("jslab").value
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {

                document.getElementById('jsCampo8').innerHTML = data

            } else {

                document.getElementById('jsCampo8').value = "Skill no disponible"
            }

        },
        error: function (request, status, error) {
            document.getElementById('jsMensaje').innerHTML = "Error";
            openMensa();
            
        }
    });

}



function loanDataCliente() {

    let fecha = generarFecha();
    var url = "LoandDataUser/App";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            document.getElementById('jsCampo1').innerHTML = "CALL_" + data[0]["idfiscal"] + "_" + fecha;
            document.getElementById('jsCampo2').innerHTML = data[0]["idccms"];
            document.getElementById('jsCampo3').innerHTML = data[0]["Nombre"];
            document.getElementById('jsCampo4').innerHTML = data[0]["idfiscal"];
            document.getElementById('jsCampo5').innerHTML = data[0]["Rol"];
            document.getElementById('jsCampo9').innerHTML = "CALL_" + data[0]["idfiscal"] + "_" + fecha;
            document.getElementById('jsCampo10').innerHTML = data[0]["idccms"];
            document.getElementById('jsCampo11').innerHTML = data[0]["Nombre"];
            document.getElementById('jsCampo12').innerHTML = data[0]["idfiscal"];
            document.getElementById('jsCampo13').innerHTML = data[0]["Rol"];

            document.getElementById('jsCampo001').innerHTML = "SURVET_" + data[0]["idfiscal"] + "_" + fecha;
            document.getElementById('jsCampo002').innerHTML = data[0]["idccms"];
            document.getElementById('jsCampo003').innerHTML = data[0]["Nombre"];
            document.getElementById('jsCampo004').innerHTML = data[0]["idfiscal"];
            document.getElementById('jsCampo005').innerHTML = data[0]["Rol"];
            document.getElementById('jsCampo09').innerHTML = "SURVET_" + data[0]["idfiscal"] + "_" + fecha;
            document.getElementById('jsCampo010').innerHTML = data[0]["idccms"];
            document.getElementById('jsCampo011').innerHTML = data[0]["Nombre"];
            document.getElementById('jsCampo012').innerHTML = data[0]["idfiscal"];
            document.getElementById('jsCampo013').innerHTML = data[0]["Rol"];

            loadLobCliente();
            validarFiltrosAsignados();
            /*loandAleatorioSurveys();*/

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}




function loandAleatorioSurveys() {

    var url = "LoandRamdomClienteSurveys/App";
    var data =
    {
        IdCliente: document.getElementById("jsCliente").value
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {
            document.getElementById("tbodytbaleatorio2").innerHTML = "";

            if (data.length < 1) {
                document.getElementById('jsMensaje').innerHTML = "No existe información del día anterior para generar el Aleatorio de ENCUESTAS";
                openMensa();
                document.getElementById('jsSpinner_2').style.display = 'none';
                document.getElementById('jsbuton_1').style.display = 'inline-block';

            } else {

                for (var i = 0; i < data.length; i++) {

                    document.getElementById("tbodytbaleatorio2").innerHTML +=
                        "<tr>" +
                        "<td> " + (i + 1) +
                        "</td> <td>" + data[i]["Campaña"] +
                        "</td> <td>" + data[i]["UCID"] +
                        "</td> <td> " + data[i]["ID_Encuesta"] +
                        "</td> <td> " + data[i]["STRPregunta"] +
                        "</td> <td> " + data[i]["Respuesta"] +
                        "</td> <td> " + data[i]["Split"] +
                        "</td> <td> " + data[i]["Fecha_Fin"] +
                        "</td> <td> " + data[i]["Login"] +
                        "</td> <td> " + data[i]["ANI"] +
                        "</td></tr> ";

                }

                document.getElementById('jsSpinner_2').style.display = 'none';
                document.getElementById('jsbuton_1').style.display = 'inline-block';
                document.getElementById('jsbuton_3').style.display = 'inline-block';

            }

        },
        error: function (request, status, error) {

            document.getElementById('jsMensaje').innerHTML = "El servidor no responde, Vuelva a intentarlo";
            openMensa();
            document.getElementById('jsSpinner_2').style.display = 'none';
            document.getElementById('jsbuton_1').style.display = 'inline-block';

        }
    });



}

function insertDataRandom() {

    document.getElementById('jsSpinner_3').style.display = 'inline-block';

    var url = "SaveNewDataForPDF_DataBasic/App";
    var data =
    {
        ID_PDF: document.getElementById("jsCampo1").innerHTML,
        CCMS_EVALUATOR: document.getElementById("jsCampo2").innerHTML,
        CRETE_DATETIME: document.getElementById("jsCampo6").innerHTML,
        ID_CLIENTE: document.getElementById("jsCampo7").innerHTML,
        LOB: document.getElementById("jsCampo8").innerHTML
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data == 1) {
                openModal();
                document.getElementById('jsSpinner_3').style.display = 'none';
                document.getElementById('jsbuton_2').style.display = 'inline-block';
                document.getElementById('jsbuton_1').style.display = 'none';

            }

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

}

function insertDataRandom2() {

    document.getElementById('jsSpinner_4').style.display = 'inline-block';

    var url = "SaveNewDataForPDF_DataBasic_Surveys/App";
    var data =
    {
        ID_PDF: document.getElementById("jsCampo001").innerHTML,
        CCMS_EVALUATOR: document.getElementById("jsCampo002").innerHTML,
        CRETE_DATETIME: document.getElementById("jsCampo006").innerHTML,
        ID_CLIENTE: document.getElementById("jsCampo007").innerHTML
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data == 1) {

                openModal2();
                document.getElementById('jsSpinner_4').style.display = 'none';
                document.getElementById('jsbuton_3').style.display = 'none';
                document.getElementById('jsbuton_4').style.display = 'inline-block';
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

            document.getElementById("jslab").innerHTML = "<option value='Select'>Select</option>";;

            for (var i = 0; i < data.length; i++) {

                document.getElementById("jslab").innerHTML +=
                    "<option value='" + data[i]["Skill"] + "'>" + data[i]["Nombre"] + "</option>";
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
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

    return fecha;
}

function select() {


    let data1 = document.getElementById("jsCliente").value;
    let data2 = document.getElementById("jslab").value;
    let dataSelect = [data1, data2];
    return dataSelect;

}

function userData() {


    let date = generarFecha();
    let dataSelect = select();
    let cliente = dataSelect[0];
    let lob = dataSelect[1];

    let data = [date, cliente, lob]
    return data;
}

function generarReferencia() {

    let user = userData();
    let referenciaData = `ACTIVIDAD_${user[0]}_${user[2]}_N-${user[1]}`;
    return referenciaData;

}

function openPdf() {

    document.getElementById('jsSpinner_2').style.display = 'inline-block';
    document.getElementById('jsSpinner_1').style.display = 'inline-block';

    let data = userData();

    document.getElementById('jsOpenResul').style.display = 'block';
    document.getElementById('jspdfBtn').style.display = 'block';
    document.getElementById('jspdfBtn2').style.display = 'block';
    document.getElementById('jsCampo6').innerHTML = data[0];
    document.getElementById('jsCampo7').innerHTML = data[1];
    document.getElementById('jsCampo8').innerHTML = data[2];

    document.getElementById('jsCampo006').innerHTML = data[0];
    document.getElementById('jsCampo007').innerHTML = data[1];
    document.getElementById('jsCampo008').innerHTML = data[2];


    document.getElementById('jsCampo14').innerHTML = data[0];
    document.getElementById('jsCampo15').innerHTML = data[1];
    document.getElementById('jsCampo16').innerHTML = data[2];
    document.getElementById('jsCampo014').innerHTML = data[0];
    document.getElementById('jsCampo015').innerHTML = data[1];
    document.getElementById('jsCampo016').innerHTML = data[2];
    loanDataCliente();


}

function generarPDF() {
    alert("Documento generado y enviado a su correo");
}

function loadListClientes() {
    var url = "LoandListCampaign/App";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            var result = "<option value='0'>Select</option>"
            for (var i = 0; i < data.length; i++) {
                result += "<option value='" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "'>" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "</option>"
            }
            document.getElementById('jsCliente').innerHTML = result;
            loandNameInRandon();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function loadActgenerada() {

    var fechaInicial = document.getElementById('jsFiltroInicial').value;
    var fechaFinal = document.getElementById('jsFiltroFinal').value;

    if (fechaInicial != "" && fechaFinal != "") {

        var url = "loadActGenerada/App";
        var data =
        {
            fechaInicial: fechaInicial,
            fechaFinal: fechaFinal
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {
                document.getElementById("jsMostrarTBGenerada").innerHTML = "";
                document.getElementById("jsSpanLoadCall").innerHTML = data.length;


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
                        "<a class='dropdown-item' onclick='generarPDFNuevo(" + JSON.stringify(id) + ");' _mstvisible='1' id='" + JSON.stringify(id) +"'>" +
                        "<i class='bx bx-edit-alt me-1' _mstvisible= '2'></i> " +
                        "<font _mstmutation='1' _msthash='5245240' _msttexthash='75699' _mstvisible='2'> PDF</font>" +
                        "</a > " +
                        "<a class='dropdown-item' onclick='loadCumplimiento(" + JSON.stringify(id) + ");' _mstvisible='1' id='" + JSON.stringify(id) + "'>" +
                        "<i class='bx bx-edit-alt me-1' _mstvisible= '2'></i> " +
                        "<font _mstmutation='1' _msthash='5245240' _msttexthash='75699' _mstvisible='2'> VER</font>" +
                        "</a > " +
                        "</div>" +
                        "</div>" +
                        "</tr> ";
                    

                
                }
            }
        });

    }
}

function loadActgeneradaS() {

    var fechaInicial = document.getElementById('jsFiltroInicialS').value;
    var fechaFinal = document.getElementById('jsFiltroFinalS').value;

    if (fechaInicial != "" && fechaFinal != "") {

        var url = "loadActGeneradaS/App";
        var data =
        {
            fechaInicial: fechaInicial,
            fechaFinal: fechaFinal
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {
                document.getElementById("jsMostrarTBGeneradaS").innerHTML = "";
                document.getElementById("jsSpanLoadSurv").innerHTML = data.length;

                for (var i = 0; i < data.length; i++) {

                    var id = data[i]["ID_PDF"].toString();


                    document.getElementById("jsMostrarTBGeneradaS").innerHTML +=
                        "<tr>" +
                        "<td> " + "<strong _msthash='3656601' _msttexthash='286260' id='jsIDPDF'>" + data[i]["ID_PDF"] + "</strong>" +
                        "</td> <td _msthash='3656731' _msttexthash='151567'>" + data[i]["CRETE_DATETIME"] +
                        "<td> " + "<span class='badge bg-label-primary me-1' _msthash='3656601' _msttexthash='286260'>" + data[i]["Nombre_Campaña"] + "</span>" +
                        "<td> " + "<span class='badge bg-label-primary me-1' _msthash='3656601' _msttexthash='286260'>" + data[i]["Lob"] + "</span>" +
                        "<td> " +
                        "<div class='dropdown' _msthidden='2'>" +
                        "<button type='button' class='btn p-0 dropdown-toggle hide-arrow' data-bs-toggle='dropdown' aria-expanded='false'>" +
                        "<i class='bx bx-dots-vertical-rounded'></i></button >" +
                        "<div class='dropdown-menu' style='' _mstvisible='0'>" +
                        "<a class='dropdown-item' onclick='generarPDFNuevoS(" + JSON.stringify(id) + ");' _mstvisible='1' id='" + JSON.stringify(id) + "'>" +
                        "<i class='bx bx-edit-alt me-1' _mstvisible= '2'></i> " +
                        "<font _mstmutation='1' _msthash='5245240' _msttexthash='75699' _mstvisible='2'> Ver </font>" +
                        "</a > " +
                        "</div>" +
                        "</div>" +

                        "</tr> ";



                }
            }
        });

    }
}

function generarPDFNuevo(ID_PDF) {


    var url = "loadIDPDF/App";
    var data =
    {
        IDPDF: ID_PDF
    }


    $.ajax({
        url: url,
        type: 'GET',
        data: data,
        success: function () {

            var urlpag = "./PDF";
            window.open(urlpag, "Diseño Web", "width=600, height=600");

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

}

function generarPDFNuevoS(ID_PDF) {


    var url = "loadIDPDFS/App";
    var data =
    {
        IDPDF: ID_PDF
    }


    $.ajax({
        url: url,
        type: 'GET',
        data: data,
        success: function () {

            var urlpag = "./PDFSURV";
            window.open(urlpag, "Diseño Web", "width=600, height=600");

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

}

function openModal() {

    var OpenModal = new bootstrap.Modal(document.getElementById('modalToggle'))
    OpenModal.toggle()

}

function openModal2() {

    var OpenModal2 = new bootstrap.Modal(document.getElementById('modalToggleSurvey'))
    OpenModal2.toggle()

}

function sendEmailAleatorio() {

    var url = "sendEmailAleatoriedad/App";
    var data =
    {
        ID_PDF: document.getElementById("jsCampo1").innerHTML,
        CCMS_EVALUATOR: document.getElementById("jsCampo2").innerHTML,
        CRETE_DATETIME: document.getElementById("jsCampo6").innerHTML,
        ID_CLIENTE: document.getElementById("jsCampo7").innerHTML
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data == 1) {

                alert("Mensaje enviado")
            } else {
                alert("Error")
            }

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

}

function generarFechaLoad() {

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

function openMensa() {

    var toastLiveExample = document.getElementById('liveToast')
    if (true) {
        
            var toast = new bootstrap.Toast(toastLiveExample)

            toast.show()
        
    }
}


function verifyPermission() {

    var url = "verifyPermission/App";
    var data =
    {
        cliente: document.getElementById("jsCliente").value,
        lob: document.getElementById("jslab").value
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length == 0) {

                document.getElementById('jsMensaje').innerHTML = "No tienes permisos para generar esta actividad, seleccione otro cliente";
                openMensa();

            } else {

                if (data[0]["ESTADO"] == "ACTIVO") {

                    document.getElementById('jsMensaje').innerHTML = "Generando actividad, este proceso puede tardar varios minunos. ¡ESPERE!";
                    openMensa();
                    openPdf();
                    
                }
                else {

                    document.getElementById('jsMensaje').innerHTML = "El estado del permiso para el Cliente seleccionado es INACTIVO, comuniquese con su lider";
                    openMensa();
                }

            }

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

}



function openModalVerActividad() {

    var OpenModal = new bootstrap.Modal(document.getElementById('fullscreenModal'))
    OpenModal.toggle()

}

function loadCumplimiento(referencia) {

    ocultarAlerta("contenidoAlert");
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
                                <input onclick='loadEstadoActividad(" + JSON.stringify(ID) + ");' class='form-check-input' type='checkbox' value='' id='" + data[i]["ID"] + "' />\
                                <label class='form-check-label' for='customCheckDark'></label>\
                            </div>\
                        </td> <td>\
                            <div>\
                                    <select id='"+ data[i]["ID"] + "-observacion' class='form-select form-select-sm' onchange='loadEstadoActividad(" + JSON.stringify(ID) + ");'>\
                                            <option value='"+ data[i]["OBSERVACION"] + "' selected>" + data[i]["OBSERVACION"] +"</option>\
                                            <option value='SELECT'>SELECT</option>\
                                            <option value='UCID NO ENCONTRADO'>UCID NO ENCONTRADO</option>\
                                    </select>\
                            </div >\
                        </td></tr>";
                    }
                    else if (data[i]["CUMPLIMIENTO"] == "true") {
                        actividadHTML +=
                            "<div class='form-check form-check-dark'>\
                                <input onclick='loadEstadoActividad(" + JSON.stringify(ID) + ");' class='form-check-input' type='checkbox' checked value='' id='" + data[i]["ID"] + "' />\
                                <label class='form-check-label' for='customCheckDark'></label>\
                            </div>\
                        </td> <td>\
                            <div>\
                                <div>\
                                        <select id='"+ data[i]["ID"] + "-observacion' class='form-select form-select-sm' onchange='loadEstadoActividad(" + JSON.stringify(ID) + ");'>\
                                            <option value='"+ data[i]["OBSERVACION"] + "' selected>" + data[i]["OBSERVACION"] +"</option>\
                                            <option value='SELECT'>SELECT</option>\
                                            <option value='UCID NO ENCONTRADO'>UCID NO ENCONTRADO</option>\
                                        </select>\
                                    </div >\
                            </div >\
                        </td></tr>";
                    }else {

                        actividadHTML +=
                            "<div class='form-check form-check-dark'>\
                                <input onclick='loadEstadoActividad(" + JSON.stringify(ID) + ");' class='form-check-input' type='checkbox' value='' id='" + data[i]["ID"] + "' />\
                                <label class='form-check-label' for='customCheckDark'></label>\
                            </div>\
                        </td> <td>\
                            <div>\
                                <div>\
                                        <select id='"+ data[i]["ID"] + "-observacion' class='form-select form-select-sm' onchange='loadEstadoActividad(" + JSON.stringify(ID) + ");'>\
                                            <option value='SELECT' selected>SELECT</option>\
                                            <option value='UCID NO ENCONTRADO'>UCID NO ENCONTRADO</option>\
                                        </select>\
                                    </div >\
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

function mostrarAlerta(id, mensaje) {
    document.getElementById(id).innerHTML = mensaje;
    document.getElementById(id).style.display = 'block';

}


function ocultarAlerta(id) {
    document.getElementById(id).style.display = 'none';

}

/**************************CARGAR FILTRO*************************/

function limpiarCamposValue(id) {

    document.getElementById(id).value = '';
}

function InsertValue(id,contenido) {

    document.getElementById(id).value = contenido;
}

function generarFechaApp(id) {
    let dateDay = new Date();
    let day = dateDay.getDate();
    let day2 = dateDay.getDate()-01;
    let day3 = dateDay.ge;
    let month = dateDay.getMonth() + 1;
    let year = dateDay.getFullYear();
    let seg = dateDay.getSeconds();
    let min = dateDay.getMinutes();
    let hora = dateDay.getHours();
    day = ('0' + day).slice(-2);
    month = ('0' + month).slice(-2);
    let fecha;
    
    if (id == 1) {
        fecha = `${year}-${month}-${day} ${hora}:${min}:${seg}`
    }

    if (id == 2) {
        fecha = `${year}-${month}-${day}`
    }
    if (id == 3) {
        fecha = `${year}`
    }

    if (id == 4) {
        fecha = `${month}`
    }
    if (id == 5) {

        switch (day2) {
        case 1:
            day2 = '01';
            break;
        case 2:
            day2 = '02';
            break;
        case 3:
            day2 = '03';
            break;
        case 4:
            day2 = '04';
            break;
        case 5:
            day2 = '05';
            break;
        case 6:
            day2 = '06';
            break;
        case 7:
            day2 = '07';
            break
        case 8:
            day2 = '08';
            break
        case 9:
            day2 = '09';
            break;
        default: 
            day2 = day2
        }
        fecha = `${year}-${month}-${day2}`
    }

    return fecha;
}


function loadFilter() {
    filtrosSistema();
    filtrosAnalista();
}
function filtrosSistema() {


    limpiarCamposValue("FechaInicial_2");
    limpiarCamposValue("FechaFinal_2");
    let fechaFinal =  generarFechaApp(2);
    let fechaInicial = generarFechaApp(5);

    document.getElementById('FechaInicial_2').value = fechaInicial;
    document.getElementById('FechaFinal_2').value = fechaInicial;
}

function filtrosAnalista() {

    let recontacto;
    let trasferida;

    limpiarCamposValue("jsCcmsAdmin");
    limpiarCamposValue("jsfechaactualizado");
    limpiarCamposValue("jsNumCall");
    limpiarCamposValue("jsFaltantes");
    limpiarCamposValue("FechaInicial_1");
    limpiarCamposValue("FechaFinal_1");
    limpiarCamposValue("CSAT");
    limpiarCamposValue("NPS");
    limpiarCamposValue("FCR");
    limpiarCamposValue("CES");
    limpiarCamposValue("RECONTACTOS");
    limpiarCamposValue("HOLD_INICIAL");
    limpiarCamposValue("HOLD_FINAL");
    limpiarCamposValue("AHT_INICIAL");
    limpiarCamposValue("AHT_FINAL");
    limpiarCamposValue("TRASFERIDA");
    limpiarCamposValue("AGENTES");
    limpiarCamposValue("LLAMADAS_CORTAS");
    limpiarCamposValue("LLAMADAS_LARGAS");

    var url = "../Random/loadingFilter";
    var data =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        MES_ASIGNADO: generarFechaApp(4),
        AÑO_ASIGNADO: generarFechaApp(3)
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length > 0) {

                let fecha = data[0]["FECHA_ACTUALIZADO"];

                let fecha1 = fecha.split(' ');
                let fecha2 = fecha1[0].split('/');
                let fecha3 = fecha2[2] + "-" + fecha2[1] + "-" + fecha2[0];
                recontacto = data[0]["RECONTACTO"];
                trasferida = data[0]["TRASFERIDA"];

                switch (recontacto) {
                    case '0':
                        recontacto = "No";
                        break;
                    case '1':
                        recontacto = "0-24 Horas";
                        break;
                    case '2':
                        recontacto = "24-48 Horas";
                        break;
                    case '3':
                        recontacto = "48-72 Horas";
                        break;
                    case '4':
                        recontacto = "Ultimo mes";
                        break;
                    case '5':
                        recontacto = "Ultimos dos mes";
                        break;
                    case '6':
                        recontacto = "Ultimos tres mes"
                        break;
                    case '7':
                        recontacto = "Ultimos dos mes"
                        break;
                    case '8':
                        recontacto = "Ultimos tres mes"
                        break;
                    default:
                        recontacto = "No"
                }

                
                if (trasferida == 1) {
                    trasferida = "Si"
                } else {

                    trasferida = "No"
                }

                loadCuotaCumplidaDia();
                InsertValue("jsCcmsAdmin", data[0]["CCMS_ACTUALIZADO"]);
                InsertValue("jsfechaactualizado", data[0]["FECHA_ACTUALIZADO"]);
                InsertValue("jsNumCall", data[0]["NUMERO_MONITOREOS"]);
                InsertValue("FechaInicial_1", data[0]["FECHA_INICIAL"]);
                InsertValue("FechaFinal_1", data[0]["FECHA_FINAL"]);
                InsertValue("CSAT", data[0]["CSAT"]);
                InsertValue("NPS", data[0]["NPS"]);
                InsertValue("FCR", data[0]["FCR"]);
                InsertValue("CES", data[0]["CES"]);
                InsertValue("RECONTACTOS",recontacto);
                InsertValue("HOLD_INICIAL", data[0]["HOLD_INICIAL"]);
                InsertValue("HOLD_FINAL", data[0]["HOLD_FINAL"]);
                InsertValue("AHT_INICIAL", data[0]["AHT_INI"]);
                InsertValue("AHT_FINAL", data[0]["AHT_FIN"]);
                InsertValue("TRASFERIDA", trasferida);
                InsertValue("AGENTES", data[0]["AGENTE_EVALUAR"]);
                InsertValue("LLAMADAS_CORTAS", data[0]["INICIO_LLAMADA_CORTA"] + " - " + data[0]["FIN_LLAMADA_CORTA"]);
                InsertValue("LLAMADAS_LARGAS", data[0]["INICIO_LLAMADA_LARGA"] + " - " + data[0]["FIN_LLAMADA_LARGA"]);


                document.getElementById('form-1').style.display = 'inline-block';
                document.getElementById('form-2').style.display = 'inline-block';
                document.getElementById('form-3').style.display = 'inline-block';
                document.getElementById('form-4').style.display = 'inline-block';
                document.getElementById('form-5').style.display = 'inline-block';
                document.getElementById('form-6').style.display = 'inline-block';
                document.getElementById('form-7').style.display = 'inline-block';
                document.getElementById('form-8').style.display = 'inline-block';

                document.getElementById('jsMensaje').innerHTML = "Filtros cargados con Éxito";
                openMensa();
            }
            else {
                document.getElementById('jsMensaje').innerHTML = "Filtros del analista no disponibles, debes de configurar el LOB";
                openMensa();
                ocultarHTML("form-1")
                ocultarHTML("form-2")
                ocultarHTML("form-3")
                ocultarHTML("form-4")
                ocultarHTML("form-5")
                ocultarHTML("form-6")
                ocultarHTML("form-7")
                ocultarHTML("form-8")
            }

        },
        error: function (request, status, error) {
            document.getElementById('jsMensaje').innerHTML = "El servidor no responde, Vuelva a intentarlo";
            openMensa();
        }
    });
}
function ocultarHTML(id) {

    document.getElementById(id).style.display = 'none';
}

function validarFiltrosAsignados() {

    let filtroFechaAnalista = document.getElementById('jsFechasActividad_1').checked;
    let filtroFechaSistema = document.getElementById('jsFechasActividad_2').checked;
    let filtroEstrategico = document.getElementById('jsEstrategico').checked;
    let filtroTactico = document.getElementById('jsTactico').checked;
    let filtroAseguramiento = document.getElementById('jsAseguramiento').checked;
    let FiltroNivelNo = document.getElementById('jsNoFiltros').checked;
    let FiltroLlammadasCortas = document.getElementById('jsLlamadasCortas').checked;
    let FiltroLlamadasLargas = document.getElementById('jsLlamadasLargas').checked;
    let tipoLlamadas;
    let FiltroFecha;
    let filtroNivel;

    //SI FILTRO FECHA ANALISTA ES TRUE FILTRO ES = 1
    //SI FILTRO FECHA SISTEMA ES TRUE FILTRO ES = 2

    if (filtroFechaAnalista == true) {
        FiltroFecha = 1;
    } else if (filtroFechaSistema == true){
        FiltroFecha = 2;
    }

    //NIVEL ESTRATEGICO = 1
    //NIVEL TACTICO = 2
    //NIVEL ASEGURAMIENTO OPERATIVO = 3
    //NO HAY NIVEL = 4


    if (filtroEstrategico == true) {
        filtroNivel     = 1;
    } else if (filtroTactico == true) {
        filtroNivel   = 2;
    } else if (filtroAseguramiento == true) {
        filtroNivel = 3;

       /* *****************LLAMADAS CORTAS = 1 Y LLAMADAS LARGAS = 2***************/

        if (FiltroLlammadasCortas == true) {
            tipoLlamadas = 1
        } else if (FiltroLlamadasLargas == true) {
            tipoLlamadas = 2;
        }

    } else if (FiltroNivelNo == true) {
        filtroNivel     = 4;
    }


    var url = "LoandRamdomCliente/App";
    var data =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        FILTRO_NIVEL: filtroNivel,
        FILTRO_FECHA: FiltroFecha,
        MES_ASIGNADO: generarFechaApp(4),
        AÑO_ASIGNADO: generarFechaApp(3),
        TIPO_LLAMADAS: tipoLlamadas
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {
            document.getElementById("tbodytbaleatorio").innerHTML = "";

            if (data.length < 1) {
                document.getElementById('jsMensaje').innerHTML = "No existe información del día filtrado para generar el Aleatorio de LLAMADAS";
                openMensa();
                document.getElementById('jsSpinner_1').style.display = 'none';
            } else {

                for (var i = 0; i < data.length; i++) {

                    document.getElementById("tbodytbaleatorio").innerHTML +=
                        "<tr>" +
                        "<td> " + (i + 1) +
                        "</td> <td>" + data[i]["CAMPAÑA"] +
                        "</td> <td>" + data[i]["UCID"] +
                        "</td> <td> " + data[i]["DURACION"] +
                        "</td> <td> " + data[i]["AGENTE_INICIAL"] +
                        "</td> <td> " + data[i]["N_AGENTE_FINAL"] +
                        "</td> <td> " + data[i]["TALK_TIME"] +
                        "</td> <td> " + data[i]["HOLD_TIME"] +
                        "</td> <td> " + data[i]["ORIGEN_COLGADA"] +
                        "</td> <td> " + data[i]["TRANSFERIDA"] +
                        "</td></tr> ";

                    document.getElementById('jsSpinner_1').style.display = 'none';

                }
                document.getElementById('jsSpinner_2').style.display = 'none';
                document.getElementById('jsbuton_1').style.display = 'inline-block';
                document.getElementById('jsbuton_3').style.display = 'inline-block';
            }

        },
        error: function (request, status, error) {
            document.getElementById('jsMensaje').innerHTML = "LoandRamdomCliente El servidor no responde, Vuelva a intentarlo";
            openMensa();
            document.getElementById('jsSpinner_1').style.display = 'none';
        }
    });



}

function readValue(id) {

    let contenido = document.getElementById(id).value;
    return contenido;

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


function loadCuotaCumplidaDia() {
    var url = "../Random/loadCuotaCumplidaDiaria";
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
        success: function (cuota) {

            InsertValue("jsFaltantes",cuota)
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}


function loadEstadoActividad(id) {
    document.getElementById('jsSpinner_10').style.display = 'inline-block';

    let cumplimiento = document.getElementById(id).checked;
    let OBSERVACION = document.getElementById(id + "-observacion").value;
   

    var url = "../Random/actualizarEstadoMonitoreo";
    var data =
    {
        REFERENCIA: document.getElementById("jsReferenciaActividad").innerHTML,
        OBSERVACION: OBSERVACION.toUpperCase(),
        ID: id,
        ESTADO: cumplimiento
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {

                document.getElementById('jsSpinner_10').style.display = 'none';
            } 

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

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



//function loandAleatorio() {

//    var url = "LoandRamdomCliente/App";
//    var data =
//    {
//        IdCliente: document.getElementById("jsCliente").value,
//        lob: document.getElementById("jslab").value
//    }
//    $.ajax({
//        url: url,
//        type: 'POST',
//        data: data,
//        success: function (data) {
//            document.getElementById("tbodytbaleatorio").innerHTML = "";

//            if (data.length < 1) {
//                document.getElementById('jsMensaje').innerHTML = "No existe información del día filtrado para generar el Aleatorio de LLAMADAS";
//                openMensa();
//                document.getElementById('jsSpinner_1').style.display = 'none';
//            } else {

//                for (var i = 0; i < data.length; i++) {

//                    document.getElementById("tbodytbaleatorio").innerHTML +=
//                        "<tr>" +
//                        "<td> " + (i + 1) +
//                        "</td> <td>" + data[i]["CAMPAÑA"] +
//                        "</td> <td>" + data[i]["UCID"] +
//                        "</td> <td> " + data[i]["DURACION"] +
//                        "</td> <td> " + data[i]["CONCTACT_ID"] +
//                        "</td> <td> " + data[i]["AGENTE_INICIAL"] +
//                        "</td> <td> " + data[i]["TALK_TIME"] +
//                        "</td> <td> " + data[i]["HOLD_TIME"] +
//                        "</td> <td> " + data[i]["ORIGEN_COLGADA"] +
//                        "</td> <td> " + data[i]["TRANSFERIDA"] +
//                        "</td></tr> ";

//                    document.getElementById('jsSpinner_1').style.display = 'none';

//                }
//            }

//        },
//        error: function (request, status, error) {
//            document.getElementById('jsMensaje').innerHTML = "LoandRamdomCliente El servidor no responde, Vuelva a intentarlo";
//            openMensa();
//            document.getElementById('jsSpinner_1').style.display = 'none';
//        }
//    });
//}