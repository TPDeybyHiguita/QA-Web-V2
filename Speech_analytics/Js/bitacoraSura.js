loadManagerBitacora();
loadTableBitacora();
loadTimeSendEmails();


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

function loandName() {
    var url = "../App/LoandName";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            loadNameInicial(data);
            loadMenu();
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

    let fecha2 = `${day}/${month}/${year}`

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



function openModal(id) {

    var OpenModal = new bootstrap.Modal(document.getElementById(id))
    OpenModal.toggle();

}

function modalNewManager(idModal) {

    creanValue("bitacoraCCMS");
    creanValue("bitacoraUsuarioRed");
    creanValue("bitacoraNombre");
    creanValue("bitacoraCorreoElectronico");
    notShowHTML("AlertManager");
    openModal(idModal);
}

function showHTML(id) {

    document.getElementById(id).style.display = 'inline-block';
}

function notShowHTML(id) {

    document.getElementById(id).style.display = 'none';
}

function creanValue(id) {

    document.getElementById(id).value = "";

}

function creanHTML(id) {

    document.getElementById(id).innerHTML = "";

}

function readonly(id) {

    var inputElement = document.getElementById(id);
    inputElement.ariaReadOnly = true;
}

function notReadonly(id) {

    var inputElement = document.getElementById(id);
    inputElement.ariaReadOnly = false;
}


function loadInfoManager() {

    let CCMS = loadDataValue('bitacoraCCMS');
    readonly("bitacoraCCMS");

    if (CCMS.length > 4) {

        var url = "../newManagerBitacora/loadDataUserBitacora";
        var data =
        {
            CCMS: CCMS
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {
                if (data.length != 0) {

                    aggValue("bitacoraCorreoElectronico", data.EMAIL);
                    aggValue("bitacoraNombre", data.NOMBRES);
                    aggValue("bitacoraUsuarioRed", data.USER);

                    showHTML("bitacoraForm_1");
                    showHTML("bitacoraForm_2");
                    showHTML("bitacoraForm_3");

                    aggHTML("AlertManager", "Verifique los datos")

                }
                else {
                    aggHTML("AlertManager", "Usuario no encontrado")
                    notReadonly("bitacoraCCMS");
                }

                showHTML("AlertManager");

            }
        });
    }
}


function loadDataValue(id) {

    let data = document.getElementById(id).value;
    return data;

}

function aggValue(id, data) {

    document.getElementById(id).value = data;
}
function aggHTML(id, data) {

    document.getElementById(id).innerHTML = data;
}



function newManager() { 


    notShowHTML("AlertManager");

    let CCMS = loadDataValue("bitacoraCCMS");
    let USER = loadDataValue("bitacoraUsuarioRed");
    let EMAIL = loadDataValue("bitacoraCorreoElectronico");
    let NOMBRES = loadDataValue("bitacoraNombre");

    if (CCMS != "" && USER != "" && EMAIL != "" && NOMBRES != "") {
        var url = "../newManagerBitacora/saveManager";
        var data =
        {
            CCMS: CCMS,
            USER: USER,
            EMAIL: EMAIL,
            NOMBRES: NOMBRES
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data == "1" || data == "2") {

                    aggHTML("AlertManager", "Manager guardado");
                    notShowHTML("bitacoraForm_1");
                    notShowHTML("bitacoraForm_2");
                    notShowHTML("bitacoraForm_3");

                    loadManagerBitacora();

                }
                else {
                    aggHTML("AlertManager", "Error, vuelva a intentarlo")
                    notReadonly("bitacoraCCMS");
                }

                showHTML("AlertManager");

            }
        });
    } else {
        aggHTML("AlertManager", "Error, ningun campo puede estar vacio")
        showHTML("AlertManager");
    }

    

}

function loadManagerBitacora() {

    creanHTML("bitacoraManagers");
    var url = "../newManagerBitacora/loadListManager";

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {

            if (data.length != 0) {

                creanHTML("bitacoraManagers");

                for (var i = 0; i < data.length; i++) {

                    var IDCCMS = data[i]["IdCcms"].toString();

                    document.getElementById("bitacoraManagers").innerHTML +=
                        "<tr>" +
                        "<td>" +
                        "<strong> " + data[i]["IdCcms"] + "</strong>" +
                        "</td> " +
                        "<td> " + data[i]["Nombre"] + "</td >" +
                        "<td>" + data[i]["CorreoElectronico"] + "</td>" +
                        "<td> <span class='badge  bg-label-warning' _msthash='244857' _msttexthash='133198'' >" + data[i]["Estado"] + "</span></td>" +

                        "<td>" +
                        "<div class='dropdown'>" +
                        "<button type='button' class='btn p-0 dropdown-toggle hide-arrow' data-bs-toggle='dropdown' aria-expanded='false'>" +
                        "<i class='bx bx-dots-vertical-rounded'></i></button > " +
                        "<div class='dropdown-menu' style=''>" +
                        "<a class='dropdown-item' onclick='updateStatusManager(" + JSON.stringify(IDCCMS) + ");'><i class='bx bx-trash me-1'></i> Inactivar</a>" +
                        "</div>" +
                        "</div>" +
                        "</td>" +
                        " </tr >";

                }

            }
            else {
                aggHTML("AlertManager", "Error, vuelva a intentarlo")
                notReadonly("bitacoraCCMS");
            }

            showHTML("AlertManager");

        }
    });

}

function updateStatusManager(CCMS) {

    if (CCMS != "") {

        var url = "../newManagerBitacora/updateStatusManagerBitacora";
        var data =
        {
            CCMS: CCMS
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data == "True") {

                    loadManagerBitacora();

                }
                else {

                    alert("Ocurrio un error, vuelva a intentarlo")
                }
            }
        });

    }
}

function loadTableBitacora() {

    creanHTML("bitacoraTable");
    var url = "../newManagerBitacora/loadListManagerBitacora";

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {

            if (data.length != 0) {

                creanHTML("bitacoraTable");

                for (var i = 0; i < data.length; i++) {

                    document.getElementById("bitacoraTable").innerHTML +=
                        "<tr>" +
                        "<td>" +
                        "<strong> " + data[i]["ID_CASO"] + "</strong>" +
                        "</td> " +
                        "<td> " + data[i]["NOMBRE_CLIENTE"] + "</td >" +
                        "<td>" + data[i]["MOTIVO_1"] + "</td>" +
                        "<td> " +
                        "<div class='input - group input-group-merge'>" +
                        "<textarea id='basic-icon-default-message' class='form-control' aria-describedby='basic-icon-default-message2' _mstplaceholder='806338' _msthash='95' _mstaria-label='806338' style='height: 60px;' readonly> " + data[i]["COMENTARIO"] +"</textarea>" +
                        "</div>" +
                        "</td>" +
                        "</td>" +
                        " </tr>";
                }

            }
            else {
                alert("Error")
            }

        }
    });

}


function sendEmailBitacora() {

    var url = "../newManagerBitacora/sendEmailBitacora";

    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            if (data == 1) {
                document.getElementById('jsMensaje').innerHTML = "Mensajes enviados";
                openMensa();
            }
            else {
                document.getElementById('jsMensaje').innerHTML = "Error, no se enviaron los mensajes";
                openMensa();
            }
            
        }
    });
}

function openMensa() {

    var toastLiveExample = document.getElementById('AlertaNotificaciones')
    if (true) {

        var toast = new bootstrap.Toast(toastLiveExample)

        toast.show()

    }
}

function loadTimeSendEmails() {

    var url = "../newManagerBitacora/loadTimeEmail";

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {


            if (data != 1) {
                document.getElementById('bitacoraTimeSendEmails').value = data;
            }
            else {
                document.getElementById('jsMensaje').innerHTML = "Error, no fecha";
                openMensa();
            }

        }
    });
}


function updateTimeSendEmail() {

    let timeSendEmail = document.getElementById('bitacoraTimeSendEmails').value;

    if (timeSendEmail != "") {

        var url = "../newManagerBitacora/updateTimeSendEmailBitacoraSura";
        var data =
        {
            timeSendEmail: timeSendEmail
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data == "True") {

                    document.getElementById('jsMensaje').innerHTML = "Actualizado";
                    openMensa();

                }
                else {

                    document.getElementById('jsMensaje').innerHTML = "Error";
                    openMensa();
                }
            }
        });

    }
}









