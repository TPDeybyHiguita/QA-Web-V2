

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
    dataCliente();
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





function loadListClientesLevel() {
    var url = "../Customers/loadListClient";
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

function saveConfCliente() {

    var url = "../Customers/saveConfCliente";
    var data =
    {
        cliente: document.getElementById("jsClienteLevel").value
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            openMensa("Actualizado");
            dataCliente();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function updateConfClienteActivar(idCliente ) {

    var url = "../Customers/updateConfClienteActivar";
    var data =
    {
        idCliente: idCliente
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            openMensa("Actualizado");
            dataCliente();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function updateConfClienteInactivar(idCliente) {

    var url = "../Customers/updateConfClienteInactivar";
    var data =
    {
        idCliente: idCliente
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            openMensa("Actualizado");
            dataCliente();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}


function dataCliente() {

    document.getElementById("jsTableClientes").innerHTML = "";

    var url = "../Customers/dataCliente";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {

            for (var i = 0; i < data.length; i++) {

                var idCliente = data[i]["id_Cliente"].toString();

                let html =
                    "<tr>" +
                    "<td>" +
                    "<strong> " + data[i]["id_Cliente"] + "</strong>" +
                    "</td> " +
                    "<td> " + data[i]["nombre"] + "</td >" +
                    "<td> " + data[i]["fecha_Actualizado"] + "</td >";

                if (data[i]["estado"] == "ACTIVO") {
                    html += "<td> <span class='badge bg-label-primary' _msthash='244857' _msttexthash='133198'>" + data[i]["estado"] + "</span></td>"
                } else {
                    html += "<td> <span class='badge  bg-label-danger' _msthash='244857' _msttexthash='133198'>" + data[i]["estado"] + "</span></td>"
                }

                html += "<td>" +
                    "<div class='dropdown'>" +
                    "<button type='button' class='btn p-0 dropdown-toggle hide-arrow' data-bs-toggle='dropdown' aria-expanded='false'>" +
                    "<i class='bx bx-dots-vertical-rounded'></i></button > " +
                    "<div class='dropdown-menu' style=''>" +
                    "<a class='dropdown-item' onclick='updateConfClienteActivar(" + JSON.stringify(idCliente) + ");'><i class='fa-solid fa-check'></i> Activar </a>" +
                    "<a class='dropdown-item' onclick='updateConfClienteInactivar(" + JSON.stringify(idCliente) + ");'><i class='bx bx-trash me-1'></i> Inactivar </a>" +
                    "</div>" +
                    "</div>" +
                    "</td>" +
                    " </tr >";



                document.getElementById("jsTableClientes").innerHTML += html;

            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function openMensa(mensaje) {

    var toastLiveExample = document.getElementById('liveToast');
    document.getElementById('jsMensaje').innerHTML = mensaje;
    if (true) {

        var toast = new bootstrap.Toast(toastLiveExample)

        toast.show()

    }
}