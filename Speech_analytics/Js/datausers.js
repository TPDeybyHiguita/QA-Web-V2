let datos = [1011396579, 
                "Jhon Deyby", 
                "Higuita", 
                "jhon.piedrahitahiguita@teleperformance.com",
                "Analisis",
                3004280212,
                "Calle 100 - Medellín",
                "Antioquia",
                "Medellín"
            ];

function formdataUser (datosUsuario){  

    let documento = document.getElementById("document");
    let nombre = document.getElementById("Name");
    let apellido = document.getElementById("lastName");
    let email =document.getElementById("email");
    let area = document.getElementById("organization");
    let tel = document.getElementById("phoneNumber");
    let direccion = document.getElementById("address");
    let departamento = document.getElementById("state");
    let ciudad = document.getElementById("country");

    documento.value =       datos[0];
    nombre.value =          datos[1];
    apellido.value =        datos[2];
    email.value =           datos[3];
    area.value =            datos[4];
    tel.value =             datos[5];
    direccion.value =       datos[6];
    departamento.value =    datos[7];
    ciudad.value =          datos[8];
}

formdataUser(datos);
