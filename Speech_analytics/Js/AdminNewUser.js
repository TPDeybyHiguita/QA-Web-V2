
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


function openAlerta(mensaje) {

    document.getElementById('jsAlerta_1').innerHTML = mensaje;
    document.getElementById('jsAlerta_1').style.display = 'inline-block';
}

function closeAlerta() {

    document.getElementById('jsAlerta_1').innerHTML = "";
    document.getElementById('jsAlerta_1').style.display = 'none';
}

function openMensa(mensaje) {

    var toastLiveExample = document.getElementById('liveToast');
    document.getElementById('jsMensaje').innerHTML = mensaje;
    if (true) {

        var toast = new bootstrap.Toast(toastLiveExample)

        toast.show()

    }
}


function loadDataUser() {

    cleamDataNewUser();

    var USER_RED = document.getElementById('jsUserCcms').value;


    if (USER_RED.length > 4) {

        openMensa("Cargando información, Espere...")

        var url = "../Admin/loadDataUser";
        var data =
        {
            USER_RED: USER_RED
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {
                

                if (data.length != 0) {

                    document.getElementById('jsUserName').value = data.Nombre;
                    document.getElementById('jsUserLastName').value = data.Nombre;
                    document.getElementById('jsUserRed').value = data.idccms;
                    document.getElementById('jsUserEmail').value = data.email;
                    document.getElementById('jsUserEmailAlternative').value = data.email;
                    document.getElementById('jsUserCcmsManager').value = data.idmanager;

                } else {

                    var CCMS = document.getElementById('jsUserCcms').value = "";
                    openMensa("El usuario no se encuentra en la base de datos, comoniquese con soporte...");
                }
            }
        });

    }

}

function cleamDataNewUser() {

    limpiarValue("jsUserName");
    limpiarValue("jsUserEmail");
    limpiarValue("jsUserCcmsManager");
    limpiarValue("jsUserRed");
    limpiarValue("jsUserLastName");
    limpiarValue("jsUserEmailAlternative");

    limpiarCheck("administradorNewUser");
    limpiarCheck("administratorEditPermissions");
    limpiarCheck("administratorAddClient");
    limpiarCheck("managerEquipment");
    limpiarCheck("managerRandomnessResults");
    limpiarCheck("random");
    limpiarCheck("calculatorPlans");
    limpiarCheck("calculatorDays");
    limpiarCheck("logsSura");
}

function valitadeExisUser() {

}

function newUser() {

    ocultarHtml("buton_2");
    closeAlerta();

    var ccms = document.getElementById('jsUserCcms').value;
    var userRed = document.getElementById('jsUserRed').value;
    var nombres = document.getElementById('jsUserName').value;
    var apellidos = document.getElementById('jsUserLastName').value;
    var email = document.getElementById('jsUserEmail').value;
    var emailAlternate = document.getElementById('jsUserEmailAlternative').value;
    var ccmsManager = document.getElementById('jsUserCcmsManager').value;
    var createUser = document.getElementById('administradorNewUser').checked;
    var editPermissions = document.getElementById('administratorEditPermissions').checked;
    var AddCampaigns = document.getElementById('administratorAddClient').checked;
    var myTeams = document.getElementById('managerEquipment').checked;
    var randomnessResults = document.getElementById('managerRandomnessResults').checked;
    var random = document.getElementById('random').checked;
    var planCalculator = document.getElementById('calculatorPlans').checked;
    var daysCalculator = document.getElementById('calculatorDays').checked;
    var bitacoraSura = document.getElementById('logsSura').checked;

    if (createUser == true) {
        createUser = 1;
    } else {
        var createUser = 0;
    }

    if (editPermissions == true) {
        editPermissions = 1;
    } else {
        var editPermissions = 0;
    }

    if (AddCampaigns == true) {
        AddCampaigns = 1;
    } else {
        var AddCampaigns = 0;
    }

    if (myTeams == true) {
        myTeams = 1;
    } else {
        var myTeams = 0;
    }

    if (randomnessResults == true) {
        randomnessResults = 1;
    } else {
        var randomnessResults = 0;
    }

    if (random == true) {
        random = 1;
    } else {
        var random = 0;
    }

    if (planCalculator == true) {
        planCalculator = 1;
    } else {
        var planCalculator = 0;
    }

    if (daysCalculator == true) {
        daysCalculator = 1;
    } else {
        var daysCalculator = 0;
    }

    if (bitacoraSura == true) {
        bitacoraSura = 1;
    } else {
        var bitacoraSura = 0;
    }

    var url = "../newUser/addNewUser";
    var data =
    {
        CCMS: userRed,
        USER: ccms,
        CCMS_MANAGER: ccmsManager,
        NOMBRES: nombres,
        APELLIDOS: apellidos,
        EMAIL: email,
        EMAIL_ALTERNO: emailAlternate,
        Estado: 'Activo',
        PERMISOS_USERS: {
            createUser: createUser,
            editPermissions: editPermissions,
            AddCampaigns: AddCampaigns,
            myTeams: myTeams,
            randomnessResults: randomnessResults,
            random: random,
            planCalculator: planCalculator,
            daysCalculator: daysCalculator,
            bitacoraSura: bitacoraSura
        },
        USER_LOGIN: {
            ID_USER: ccms,
            USER_RED: userRed
        }
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.StateExistUser == true) {

                openMensa("El usuario ya se encuentra registrado en Qa Web");

            } else if (data.StateUserPermissions == true && data.StateUserData == true && data.StateUserLogin == true) {
                openMensa("Usuario registrado correctamente");
                cleamDataNewUser();
                mostrarHtml("buton_2");
            }
        }
    });

}

function simayuscula(cadena) {

    var numeroDeMayusculas;

    var url = "mayusculasNum/Admin";
    var data =
    {
        cadena: cadena
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {
            numeroDeMayusculas = data;
        }
    });

    return parseInt(numeroDeMayusculas);
}

function limpiarValue(id) {

    document.getElementById(id).value = "";

}

function mostrarHtml(id) {
    document.getElementById(id).style.display = 'inline-block';
}

function ocultarHtml(id) {
    document.getElementById(id).style.display = 'none';
}

function limpiarCheck(id) {

    document.getElementById(id).checked = false;

}



//var isChecked = document.getElementById('test').checked = true;
//if (isChecked) {
//    alert('checkbox esta seleccionado');
//}

function sendEmailNewUser() {

    openMensa("Espere"); 
    let userRed = document.getElementById('jsUserCcms').value;

    var url = "../newUser/sendEmailNewU";
    var data =
    {
        userRed: userRed
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data == 1) {

                openMensa("Mensaje enviado al usuario");
                ocultarHtml("buton_2");
            } else {
                openMensa("Error, Comunique con soporte");
            }
           
        }
    });

}