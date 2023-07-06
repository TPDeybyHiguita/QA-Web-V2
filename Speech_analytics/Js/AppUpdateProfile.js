
dataUser();

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


function loadNameInicial(data) {

    let nombreApellido = data[0]["Nombre"];
    let text = nombreApellido.split(' ');

    let primerInicial = text[0].charAt(0);
    let segundaInicial = text[1].charAt(0);

    document.getElementById('nameUserInicial').innerHTML = primerInicial + segundaInicial;
    document.getElementById('jsDateDay').innerHTML = generarFecha();


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







function dataUser() {

    var url = "LoandDataUser/User";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {

            let CCMS = document.getElementById("ccms");
            let CCMS_MANAGER = document.getElementById("ccmsmanager");
            let NOMBRES = document.getElementById("Name");
            let APELLIDOS = document.getElementById("lastName");
            let EMAIL = document.getElementById("jsemail");
            let EMAIL_ALTERNO = document.getElementById("jsemailAlterno");
            let Estado = document.getElementById("jsestado");
            let USER = document.getElementById("userRed");

            CCMS.value = data.CCMS;
            CCMS_MANAGER.value = data.CCMS_MANAGER;
            NOMBRES.value = data.NOMBRES;
            APELLIDOS.value = data.APELLIDOS;
            EMAIL.value = data.EMAIL;
            EMAIL_ALTERNO.value = data.EMAIL_ALTERNO;
            Estado.value = data.Estado;
            USER.value = data.USER;

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

}

function actualizarDataUser() {
    document.getElementById('jsSpinner_3').style.display = 'inline-block';

    var url = "actualizarDataUser/User";
    var data =
    {
        CCMSMANAGER : document.getElementById("ccmsmanager").value,
        NOMBRES : document.getElementById("Name").value,
        APELLIDOS : document.getElementById("lastName").value,
        EMAIL : document.getElementById("jsemail").value,
        EMAIL_ALTERNO: document.getElementById("jsemailAlterno").value,
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            let documento = document.getElementById("document");
            let ccmsmanager = document.getElementById("ccmsmanager");
            let estado = document.getElementById("jsestado");
            let nombre = document.getElementById("Name");
            let apellido = document.getElementById("lastName");
            let emailAlterno = document.getElementById("jsemailAlterno");
            let email = document.getElementById("jsemail");
            let area = document.getElementById("organization");

            documento.value = data[0]["CCMS"];
            ccmsmanager.value = data[0]["CCMSMANAGER"];
            estado.value = data[0]["Estado"];
            nombre.value = data[0]["NOMBRES"];
            apellido.value = data[0]["APELLIDOS"];
            email.value = data[0]["EMAIL"];
            emailAlterno.value = data[0]["EMAIL_ALTERNO"];
            area.value = data[0]["Puesto"];
            document.getElementById('jsSpinner_3').style.display = 'none';
            document.getElementById('jsMensaje').innerHTML = "Sus datos de usuario se han actualizado";
            openMensa();

        },
        error: function (request, status, error) {
            document.getElementById('jsMensaje').innerHTML = "El servidor no responde, Vuelva a intentarlo";
            openMensa();
            document.getElementById('jsSpinner_3').style.display = 'none';
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