
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
    }else {
        return fecha;
    }

}


function loadCuotaMensual() {

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
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

    return cuotaMensual;

}

function loadCuotaMensual() {

    let cuotaDiariaCumplida;

    var url = "../configureRandom/loadDiaActividad";
    var data =
    {
        CAMPAÑA: document.getElementById("jsCliente").value,
        LOB: document.getElementById("jslab").value,
        FECHA: generarFechaModelo("3")
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {

                cuotaDiariaCumplida = data[0]["NUMERO_MONITOREOS"]
            } else {
                cuotaDiariaCumplida = 0;
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

    return loadCuotaMensual;

}

function loadCuotas() {

    let cotaDiaria = loadCuotaMensual();
    let cotaCumplida = loadCuotaMensual();
    let cotaFaltante = cotaDiaria - cotaCumplida;
    let data = [cotaDiaria, cotaCumplida, cotaFaltante]

    return data;
}





const printCharts_2 = () => {
    renderModelsChart_2()
}


const printCharts = () => {
    renderModelsChart()
}

const renderModelsChart = () => {

    
    const data = {
        labels: [
            'Cota Mensual',
            'Cota Cumplida',
            'Cota Faltante'
        ],
        datasets: [{
            label: 'Llamadas',
            data: loadCuotas(),
            backgroundColor: [
                'rgb(67, 154, 151)',
                'rgb(98, 182, 183)',
                'rgb(203, 237, 213)'
            ],
            hoverOffset: 3
        }]

    };


    new Chart('grafica_2', {
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
    })
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
//            data: [65, 59, 80, 81], backgroundColor: [
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

