



function loadInfPDF() {
    let url = "loadinfPDF/App";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {

            document.getElementById('jsCampo1').innerHTML = "Hola";
            document.getElementById('jsCampo2').innerHTML = JSON.stringify(data[0]["ID_PDF"]);
            document.getElementById('jsCampo2').innerHTML = data[0]["CCMS_EVALUATOR"];
            document.getElementById('jsCampo3').innerHTML = data[0]["Nombre"];
            document.getElementById('jsCampo4').innerHTML = data[0]["Rol"];
            document.getElementById('jsCampo5').innerHTML = data[0]["CREATE_DATATIME"];
            document.getElementById('jsCampo6').innerHTML = data[0]["Nombre_Campaña"];

        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function loadAct() {

    let url = "loadActGeneradaForIDPDF/App";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            document.getElementById("jstbodytabla").innerHTML = "";

            for (var i = 0; i < data.length; i++) {

                document.getElementById("jstbodytabla").innerHTML +=
                    "<tr>" +
                    "<td> " + (i + 1) +
                    "</td> <td>" + data[i]["CAMPAÑA"] +
                    "</td> <td>" + data[i]["UCID"] +
                    "</td> <td> " + data[i]["DURACION"] +
                    "</td> <td> " + data[i]["TIMES_HELD"] +
                    "</td> <td> " + data[i]["AGENTE_INICIAL"] +
                    "</td> <td> " + data[i]["TALK_TIME"] +
                    "</td> <td> " + data[i]["HOLD_TIME"] +
                    "</td> <td> " + data[i]["ORIGEN_COLGADA"] +
                    "</td> <td> " + data[i]["TRANSFERIDA"] +
                    "</td></tr> ";

            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function generarPDF() {
    window.location.href = "./PDF";
}

