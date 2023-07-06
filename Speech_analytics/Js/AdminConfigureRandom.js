

loadListClientes();
loandNameInit();

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

function loadccmsCliente() {

    document.getElementById("jsFiltroSkill").value = "";
    document.getElementById("jsFiltroMonitoreos").value = "";

    var url = "loadccmsCliente/Admin";
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

            if (data == 0) {
                document.getElementById('form-0').style.display = 'none';
                document.getElementById("jsCcmsCliente").placeholder = "";
                document.getElementById('jsMensaje').innerHTML = "No puedes asignar filtros al cliente seleccionado";
                openMensa();
            }
            else {
                document.getElementById("jsCcmsCliente").placeholder = data;
                loadAnalysLob();
                document.getElementById('form-0').style.display = 'inline-block';
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




function loadingFilter() {

    var url = "loadingFilter/Admin";
    var data =
    {
        IdCliente: document.getElementById("jsCliente").value,
        Lob: document.getElementById("jslab").value

    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length > 0) {

                let date1;
                let date2;

                if (data[0]["FECHA_INICIAL"] != "") {
                    date1 = data[0]["FECHA_INICIAL"];
                    date2 = data[0]["FECHA_FINAL"];

                    let dalea = date1.split('-');
                    let daleb = date2.split('-');

                    let a = dalea[2].split('T');
                    let b = daleb[2].split('T');

                    let c = a[1].split(':');
                    let d = b[1].split(':');

                    date1 = dalea[0] + "-" + dalea[1] + "-" + a[0] + " " + c[0] + ":" + c[1];
                    date2 = daleb[0] + "-" + daleb[1] + "-" + b[0] + " " + d[0] + ":" + d[1];
                    document.getElementById("jsFiltroFechaIncial").value = date1
                    document.getElementById("jsFiltroFechaFinal").value = date2
                }
                
                document.getElementById("jsFiltroSkill").value = data[0]["SKILL_INICIAL"]
                document.getElementById("jsFiltroMonitoreos").value = data[0]["NUMERO_MONITOREOS"]

                document.getElementById('jsMensaje').innerHTML = "Filtros cargados con Éxito";
                openMensa();
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

            document.getElementById("jslab").innerHTML = "<option value='0'>Select</option>";

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
                        data[i]["NOMBRE_ANALISTA"] + "  -  " + data[i]["CCMS_ANALISTA"]+
                        "<span class='badge bg-primary' id='jsEstado'>" + data[i]["ESTADO"] +"</span>" +
                    "</li>";
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

    loadingFilter();

}
