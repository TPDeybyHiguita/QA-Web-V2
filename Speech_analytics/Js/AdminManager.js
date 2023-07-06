
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
    listAnalysts();
    loadListClientesLevel();
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
    let fecha1 = `${month}`
    let fecha2 = `${year}`

    if (id == 1) {

        return fecha1;

    } else if (id == 2) {
        return fecha2;

    } else {
        return fecha;
    }
    
}

function newAnalista() {

    var USER_RED = document.getElementById('analistaCCMS').value;


    if (USER_RED != "" ) {
        closeAlerta();
        closeForm01();

        var url = "../Admin/agregarAnalista";
        var data =
        {
            USER_RED: USER_RED
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {
                document.getElementById("analistaNombre").value = "";
                document.getElementById("analistaCorreo").value = "";

                if (data.length != 0) {
                    document.getElementById('analistaNombre').value = data[0]["NOMBRES"] + " " +data[0]["APELLIDOS"];
                    document.getElementById('analistaCorreo').value = data[0]["EMAIL"];
                    openForm01();
                    loadListClientes();

                } else {

                    var CCMS = document.getElementById('analistaCCMS').value = "";
                    openAlerta("Usuario no encontrado")
                }
            }
        });

    }
}

function openForm01() {
    document.getElementById('form-0').style.display = 'flex';
    document.getElementById('form-1').style.display = 'flex';
    document.getElementById('form-2').style.display = 'flex';
}

function closeForm01() {
    document.getElementById('form-0').style.display = 'none';
    document.getElementById('form-1').style.display = 'none';
    document.getElementById('form-2').style.display = 'none';
}

function openFormTotal(idForm) {
    document.getElementById(idForm).style.display = 'flex';
}

function closeFormTotal(idForm) {
    document.getElementById(idForm).style.display = 'none';
}


function openAlerta(mensaje) {

    document.getElementById('NuevaAlerta').innerHTML = mensaje;
    document.getElementById('NuevaAlerta').style.display = 'inline-block';
}




function closeAlerta() {

    document.getElementById('NuevaAlerta').innerHTML = "";
    document.getElementById('NuevaAlerta').style.display = 'none';
}

function closeAlerta3(idAlert) {

    document.getElementById(idAlert).innerHTML = "";
    document.getElementById(idAlert).style.display = 'none';
}

function openAlerta3(idAlert, mensaje) {

    document.getElementById(idAlert).innerHTML = mensaje;
    document.getElementById(idAlert).style.display = 'inline-block';
}

function openAlerta2(mensaje) {

    document.getElementById('NuevaAlerta2').innerHTML = mensaje;
    document.getElementById('NuevaAlerta2').style.display = 'inline-block';
}

function closeAlerta2() {

    document.getElementById('NuevaAlerta2').innerHTML = "";
    document.getElementById('NuevaAlerta2').style.display = 'none';
}




function loadListClientes() {
    var url = "LoandListCampaign/Admin";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            var result = "<option value=''>Select</option>"
            for (var i = 0; i < data.length; i++) {
                result += "<option value='" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "'>" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "</option>"
            }
            document.getElementById('jsCliente').innerHTML = result;
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

            document.getElementById("jslab").innerHTML = "";

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

function saveanAnalyst() {

    closeAlerta3("NuevaAlerta");
    var USER_RED = document.getElementById('analistaCCMS').value;
    var NOMBRE_ANALISTA = document.getElementById('analistaNombre').value;
    var EMAIL_ANALISTA = document.getElementById('analistaCorreo').value;
    var CLIENTE_ANALISTA = document.getElementById('jsCliente').value;
    var LOB_ANALISTA = document.getElementById('jslab').value;

    if (USER_RED != "") {

        var url = "../Manager/newAnalyst";
        var data =
        {
            USER_RED: USER_RED,
            NOMBRE_ANALISTA: NOMBRE_ANALISTA,
            EMAIL_ANALISTA: EMAIL_ANALISTA,
            CLIENTE_ANALISTA: CLIENTE_ANALISTA,
            LOB_ANALISTA: LOB_ANALISTA

        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data.StateExistUser == true && data.StateUpdateStateManagerAnalys == true) {

                    openAlerta("El usuario ya existe en su lista, fue activado de nuevo");
                    listAnalysts2();
                    closeForm01();

                } else if (data.StateSaveManagerAnalyst == true && data.StateSaveManagerPermissions == true) {

                    openAlerta("El usuario fue guardado en su lista");
                    
                    closeForm01();
                    listAnalysts2();

                } else {

                    var CCMS = document.getElementById('analistaCCMS').value = "";
                    openAlerta("Ocurrio un error, vuelva a intentarlo")
                    closeForm01();
                }
            }
        });

    }
}


function listAnalysts() {
    

    var url = "../Admin/listAnalysts";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {

            

            for (var i = 0; i < data.length; i++) {

                var CCMS_ANALISTA = data[i]["CCMS_ANALISTA"].toString();

                let html = 
                    "<tr>" +
                    "<td>" +
                    "<strong> " + data[i]["CCMS_ANALISTA"] + "</strong>" +
                    "</td> " +
                    "<td> " + data[i]["NOMBRE_ANALISTA"] + "</td >" +
                    "<td>" + data[i]["EMAIL_ANALISTA"] + "</td>";

                    if (data[i]["ESTADO"] == "ACTIVO") {
                            html += "<td> <span class='badge bg-label-primary' _msthash='244857' _msttexthash='133198'>" + data[i]["ESTADO"] + "</span></td>"
                    } else {
                            html +="<td> <span class='badge  bg-label-danger' _msthash='244857' _msttexthash='133198'>" + data[i]["ESTADO"] + "</span></td>"
                    }
                   
                    html += "<td>" +
                    "<div class='dropdown'>" +
                    "<button type='button' class='btn p-0 dropdown-toggle hide-arrow' data-bs-toggle='dropdown' aria-expanded='false'>" +
                    "<i class='bx bx-dots-vertical-rounded'></i></button > " +
                    "<div class='dropdown-menu' style=''>" +
                        "<a class='dropdown-item'  onclick='newLob(" + JSON.stringify(CCMS_ANALISTA) + ");'> <i class='bx bx-edit-alt me-1'></i> Asignar lob</a>" +
                        "<a class='dropdown-item'  onclick='lodAnalys(" + JSON.stringify(CCMS_ANALISTA) + ");'> <i class='bx bx-edit-alt me-1'></i> Ver lob</a>" +
                        "<a class='dropdown-item' onclick='updateStatus(" + JSON.stringify(CCMS_ANALISTA) + ");'><i class='bx bx-trash me-1'></i> Eliminar</a>" +
                    "</div>" +
                    "</div>" +
                    "</td>" +
                    " </tr >";



                document.getElementById("jstbodyAnalys").innerHTML += html;
                    
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function listAnalysts2() {

    var url = "../Admin/listAnalysts";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {

            document.getElementById("jstbodyAnalys").innerHTML = "";

            for (var i = 0; i < data.length; i++) {

                var CCMS_ANALISTA = data[i]["CCMS_ANALISTA"].toString();

                document.getElementById("jstbodyAnalys").innerHTML +=
                    "<tr>" +
                    "<td>" +
                    "<strong> " + data[i]["CCMS_ANALISTA"] + "</strong>" +
                    "</td> " +
                    "<td> " + data[i]["NOMBRE_ANALISTA"] + "</td >" +
                    "<td>" + data[i]["EMAIL_ANALISTA"] + "</td>" +
                    "<td> <span class='badge  bg-label-warning' _msthash='244857' _msttexthash='133198'' >" + data[i]["ESTADO"] + "</span></td>" +

                    "<td>" +
                    "<div class='dropdown'>" +
                    "<button type='button' class='btn p-0 dropdown-toggle hide-arrow' data-bs-toggle='dropdown' aria-expanded='false'>" +
                    "<i class='bx bx-dots-vertical-rounded'></i></button > " +
                    "<div class='dropdown-menu' style=''>" +
                    "<a class='dropdown-item'  onclick='newLob(" + JSON.stringify(CCMS_ANALISTA) + ");'> <i class='bx bx-edit-alt me-1'></i> Asignar lob</a>" +
                    "<a class='dropdown-item'  onclick='lodAnalys(" + JSON.stringify(CCMS_ANALISTA) + ");'> <i class='bx bx-edit-alt me-1'></i> Ver lob</a>" +
                    "<a class='dropdown-item' onclick='updateStatus(" + JSON.stringify(CCMS_ANALISTA) + ");'><i class='bx bx-trash me-1'></i> Eliminar</a>" +
                    "</div>" +
                    "</div>" +
                    "</td>" +
                    " </tr >";

            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function openModal() {

    var OpenModal = new bootstrap.Modal(document.getElementById('loadLob'))
    OpenModal.toggle();

}

function openModal6() {

    var OpenModal = new bootstrap.Modal(document.getElementById('newAnalista'))
    OpenModal.toggle();

}


function openModal2(idModal) {

    var OpenModal = new bootstrap.Modal(document.getElementById(idModal))
    OpenModal.toggle();

}

function closeModal() {

    var OpenModal = new bootstrap.Modal(document.getElementById('loadLob'))
    OpenModal.hide();
}

function closeModal3(idmodal) {

    let id = "#" + idmodal

    $(id).modal('hide')
}

function lodAnalys(CCMS_ANALISTA) {

        openModal();

        document.getElementById('jsCCMSAnalista').innerHTML = CCMS_ANALISTA;

        var url = "lodAnalys/Admin";
        var data =
        {
            CCMS_ANALISTA: CCMS_ANALISTA
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                let contenido = "";
                if (data.length == 0) {

                    openAlerta2("El usuario no tiene LOB designados");

                } else {
                    

                    for (var i = 0; i < data.length; i++) {

                        var ID = data[i]["ID"];

                        contenido +=
                            "<li class='list-group-item d-flex justify-content-between align-items-center'>" + data[i]["CLIENTE"] + " - " + data[i]["LOB"]

                        if (data[i]["ESTADO"] == "ACTIVO") {

                            contenido +=
                                "<span class='badge bg-primary'>" + data[i]["ESTADO"] + "</span>" +
                                "<button type='button' class='btn btn - icon btn - outline - primary'  onclick='deleteLob(" + ID + " , " + CCMS_ANALISTA + ");'>" +
                                "<span><i class='bx bx-trash bx-tada bx-rotate-90' ></i></span >" +
                                "</button >" +
                                "</li>";

                            document.getElementById('jsUlLob').innerHTML = contenido;
                        }
                        else {

                            contenido +=
                                "<span class='badge bg-primary'>" + data[i]["ESTADO"] + "</span>" +
                                "<button type='button' class='btn btn - icon btn - outline - primary'  onclick='activeLob(" + ID + " , " + CCMS_ANALISTA + ");'>" +
                                "<span><i class='bx bxs-check-square bx-tada bx-rotate-90' ></i></span >" +
                                "</button >" +
                                "</li>";

                            document.getElementById('jsUlLob').innerHTML = contenido;
                        }
                    }

                }
            }
        });

}

function closeModal() {

    var OpenModal = new bootstrap.Modal(document.getElementById('loadLob'))
    OpenModal.hide();
}

function lodAnalys2(CCMS_ANALISTA) {

    document.getElementById('jsCCMSAnalista').innerHTML = CCMS_ANALISTA;

    var url = "lodAnalys/Admin";
    var data =
    {
        CCMS_ANALISTA: CCMS_ANALISTA
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            let contenido ="";

            if (data.length == 0) {

                openAlerta2("El usuario no tiene LOB designados");

            } else {


                for (var i = 0; i < data.length; i++) {

                    var ID = data[i]["ID"];

                    contenido +=
                        "<li class='list-group-item d-flex justify-content-between align-items-center'>" + data[i]["CLIENTE"] + " - " + data[i]["LOB"]

                    if (data[i]["ESTADO"] == "ACTIVO") {

                        contenido +=
                            "<span class='badge bg-primary'>" + data[i]["ESTADO"] + "</span>" +
                            "<button type='button' class='btn btn - icon btn - outline - primary'  onclick='deleteLob(" + ID + " , " + CCMS_ANALISTA + ");'>" +
                            "<span><i class='bx bx-trash bx-tada bx-rotate-90' ></i></span >" +
                            "</button >" +
                            "</li>";

                        document.getElementById('jsUlLob').innerHTML = contenido;
                    }
                    else {

                        contenido +=
                            "<span class='badge bg-primary'>" + data[i]["ESTADO"] + "</span>" +
                            "<button type='button' class='btn btn - icon btn - outline - primary'  onclick='activeLob(" + ID + " , " + CCMS_ANALISTA + ");'>" +
                            "<span><i class='bx bxs-check-square bx-tada bx-rotate-90' ></i></span >" +
                            "</button >" +
                            "</li>";

                        document.getElementById('jsUlLob').innerHTML = contenido;
                    }


                    




                }

            }
        }
    });

}

function deleteLob(ID, CCMS_ANALISTA) {

    var url = "updateLob/Admin";
    var data =
    {
        CCMS_LOB: ID
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {


            if (data == 1) {

                openAlerta2("Actualizado");
                lodAnalys2(CCMS_ANALISTA);

            } else {

                openAlerta2("ERROR, Vuelva a intentarlo");
            }
        }
    });
  
}

function activeLob(ID, CCMS_ANALISTA) {

    var url = "activeLob/Admin";
    var data =
    {
        CCMS_LOB: ID
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {


            if (data == 1) {

                openAlerta2("Actualizado");
                lodAnalys2(CCMS_ANALISTA);

            } else {

                openAlerta2("ERROR, Vuelva a intentarlo");
            }
        }
    });

}

function newLob(CCMS_ANALISTA) {

    document.getElementById('analistaCCMSNewLob').value = CCMS_ANALISTA;
    loadListClientesNewLob();
    openModal2("newLob");
    closeAlerta3("NuevaAlerta3");
}

function loadListClientesNewLob() {
    var url = "LoandListCampaign/Admin";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            var result = "<option value=''>Select</option>"
            for (var i = 0; i < data.length; i++) {
                result += "<option value='" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "'>" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "</option>"
            }
            document.getElementById('jsClienteNewLob').innerHTML = result;

            /*loandNameInRandon();*/
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function loadListClientesLevel() {
    var url = "LoandListCampaign/Admin";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            var result = "<option value=''>Select</option>"
            for (var i = 0; i < data.length; i++) {
                result += "<option value='" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "'>" + data[i]["ID_Cliente"] + "-" + data[i]["Nombre_Campaña"] + "</option>"
            }
            document.getElementById('jsClienteLevel').innerHTML = result;

            /*loandNameInRandon();*/
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}


function loadSubClientesNewLob() {

    var url = "OnChangeLsCliente/App";
    var data =
    {
        IdCliente: document.getElementById("jsClienteNewLob").value
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            document.getElementById("jslabNewLob").innerHTML = "";

            for (var i = 0; i < data.length; i++) {

                document.getElementById("jslabNewLob").innerHTML +=
                    "<option value='" + data[i]["Skill"] + "'>" + data[i]["Nombre"] + "</option>";


            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function loadSubClientesLevel() {

    var url = "OnChangeLsCliente/App";
    var data =
    {
        IdCliente: document.getElementById("jsClienteLevel").value
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            document.getElementById("jslabLevel").innerHTML = "<option value = 'Select'> select</option >";

            for (var i = 0; i < data.length; i++) {

                document.getElementById("jslabLevel").innerHTML +=
                    "<option value='" + data[i]["Skill"] + "'>" + data[i]["Nombre"] + "</option>";


            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function asignarLob() {

    var CCMS_ANALISTA = document.getElementById('analistaCCMSNewLob').value;
    var CLIENTE_ANALISTA = document.getElementById('jsClienteNewLob').value;
    var LOB_ANALISTA = document.getElementById('jslabNewLob').value;

    if (CCMS_ANALISTA != "") {

        closeAlerta3("NuevaAlerta3");

        var url = "newLob/Admin";
        var data =
        {
            CCMS_ANALISTA: CCMS_ANALISTA,
            CLIENTE_ANALISTA: CLIENTE_ANALISTA,
            LOB_ANALISTA: LOB_ANALISTA
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data.StateExisManagerPermisos == true) {

                    openAlerta3("NuevaAlerta3","El usuario ya tiene este lob en su lista");

                } else if (data.StateSaveLobAnalys == true) {

                    openAlerta3("NuevaAlerta3","Lob asignado");

                } else if (data.StateExisManagerPermisos) {
                    openAlerta3("NuevaAlerta3", "Error al cargar el Lob en el usuario");
                }
            }
        });

    }
}

function updateStatus(CCMS_ANALISTA) {

    if (CCMS_ANALISTA != "") {

        var url = "updateStatus/Admin";
        var data =
        {
            CCMS_ANALISTA: CCMS_ANALISTA
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data == 1) {

                    listAnalysts2();

                }
                else {

                    openAlerta3("NuevaAlerta4", "Ocurrio un error, vuelva a intentarlo")
                }
            }
        });

    }
}

function dateNotActivity() {

    let mesActual = generarFechaModelo("1");
    let añoActual = generarFechaModelo("2");

}

function calculateDays() {

    closeAlerta3("NuevaAlerta5");
    let date = document.getElementById("jsNotDate").value;
    let html = document.getElementById('listDate').innerHTML;
    let numDays = document.getElementById('numDaysNotActivity').innerHTML;


    if (date != "") {

        if (html.indexOf(date) == -1) {
            document.getElementById("listDate").innerHTML +=
                "<li class='list-group-item d-flex align-items-center'>" + date + "</li >";

            document.getElementById('numDaysNotActivity').innerHTML = parseInt(numDays) + 1;
            daysActivity();
        } else {
            openAlerta3("NuevaAlerta5", "La fecha ya esta asignada");
        }
    }
    else
    {
        openAlerta3("NuevaAlerta5", "El campo no puede ser vacío");
    }
}

function daysMonth() {

    closeAlerta3("NuevaAlerta5");
    limpiarModal5();
    let mesActual = generarFechaModelo("1");
    let añoActual = generarFechaModelo("2");
    let date = añoActual + "-" + mesActual + "-01";
    document.getElementById("jsNotDate").value = date;

    var url = "numDays/Admin";
    var data =
    {
        MES: mesActual,
        AÑO: añoActual
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {

                document.getElementById('daysMonth').value = data;
                document.getElementById('daysActivity').value = data;
                openModal2("calculateDays");

            }
            else {

                openAlerta3("NuevaAlerta5", "Ocurrio un error, recarge la pagina");
            }
        }
    });
}

function daysActivity() {

    let daysMonth = document.getElementById('daysMonth').value;
    let daysNotActivity = document.getElementById('numDaysNotActivity').innerHTML;
    document.getElementById('daysActivity').value = daysMonth - daysNotActivity;
}

function limpiarModal5() {
    document.getElementById("listDate").innerHTML = "";
    document.getElementById("numDaysNotActivity").innerHTML = "0";
}

function saveDaysActivity() {
    let daysActivity = document.getElementById('daysActivity').value;
    document.getElementById('jsDaysNewRoom').value = daysActivity;
    calculateDays();
    closeModal3("calculateDays");
}

function saveCuatoMensual() {

    let Cliente = document.getElementById('jsClienteLevel').value;
    let IdLob = document.getElementById('jslabLevel').value;
    let NumAnalistas = document.getElementById('jsAnalysNewRoom').value;
    let PromedioActividad = document.getElementById('jsPromNewRoom').value;
    let CuotaMensual = document.getElementById('jsCuotaNewRoom').value;
    let DiasActividad = document.getElementById('jsDaysNewRoom').value;

    var url = "../Admin/saveConfManager";
    var data =
    {
        Cliente: Cliente,
        IdLob: IdLob,
        NumAnalistas: NumAnalistas,
        PromedioActividad: PromedioActividad,
        Mes: generarFechaModelo("1"),
        Año: generarFechaModelo("2"),
        CuotaMensual: CuotaMensual,
        DiasActividad: DiasActividad
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.StateVeryExistSenttingManager == true && data.StateUpdateSenttingManager == true) {

                openMensa("Información Actualizada");
            } else if (data.StateSaveSenttingManager == true) {

                openMensa("Informacón Guardada");
            }
            else {

                openMensa("Error");
            }
        }
    });
}

function loadCalculo() {

    let cuota = document.getElementById('jsCuotaNewRoom').value;
    let dias = document.getElementById('jsDaysNewRoom').value;
    let analistas = document.getElementById('jsAnalysNewRoom').value;
    let cumplida = document.getElementById('jsCumplimientoActividad').value;

    let promedio = ((cuota-cumplida) / dias) / analistas;
    document.getElementById('jsPromNewRoom').value = parseInt(promedio);

}

function analystsAssign() {

    let cliente = document.getElementById('jsClienteLevel').value;
    let IdLob = document.getElementById('jslabLevel').value;
    document.getElementById('jsCuotaNewRoom').value = "";
    document.getElementById('jsDaysNewRoom').value = "";
    document.getElementById('jsPromNewRoom').value = "";

    let result = laodDataManagerSetting(cliente, IdLob);
    loadCuotaCumplidaMes(cliente, IdLob);

    if (result == true) {
        var url = "analystsAssign/Admin";
        var data =
        {
            IdLob: IdLob,
            Cliente: cliente
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data.length != 0) {

                    document.getElementById('jsAnalysNewRoom').value = data;
                    openMensa("Actualizado");
                }
                else {

                    openMensa("Ocurrio un problema");
                }
            }
        });

        document.getElementById('form-11').style.display = 'inline-block';
        document.getElementById('form-12').style.display = 'inline-block';
        document.getElementById('form-13').style.display = 'inline-block';
        document.getElementById('form-14').style.display = 'inline-block';
        document.getElementById('form-15').style.display = 'inline-block';

    } else {

        openMensa("Ocurrio un problema");
    }
    
}

function loadCuotaCumplidaMes(cliente, lob) {

    var url = "loadCuotaCumplidaMes/Admin";
    var data =
    {
        CLIENTE: cliente,
        LOB: lob,
        MES_ASIGNADO: generarFechaModelo("1"),
        AÑO_ASIGNADO: generarFechaModelo("2")
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {
            if (data.length != 0) {
                document.getElementById('jsCumplimientoActividad').value = parseInt(data) ;
            }

        }
    });
    return 1;
}



function laodDataManagerSetting(Cliente, IdLob) {

    var url = "../Admin/laodDataManagerSetting";
    var data =
    {
        Cliente: Cliente,
        IdLob: IdLob,
        Mes: generarFechaModelo("1"),
        Año: generarFechaModelo("2")
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {
            if (data.CuotaMensual != null || data.CuotaMensual != 0) {
                document.getElementById('jsCuotaNewRoom').value = data.CuotaMensual;
                document.getElementById('jsDaysNewRoom').value = data.DiasActividad;
                document.getElementById('jsPromNewRoom').value = data.PromedioActividad;
            } else {
                openMensa("No hay cuota asignada");
            }
            
        }
    });

    return true;
}


function openMensa(mensaje) {

    var toastLiveExample = document.getElementById('liveToast');
    document.getElementById('jsMensaje').innerHTML = mensaje;
    if (true) {

        var toast = new bootstrap.Toast(toastLiveExample)

        toast.show()

    }
}


