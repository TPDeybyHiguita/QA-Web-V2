/**************************HEADER **************************/



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

function bodyloandNameInit() {
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

function openMensa(mensaje) {

    var toastLiveExample = document.getElementById('liveToast');
    document.getElementById('jsMensaje').innerHTML = mensaje;
    if (true) {

        var toast = new bootstrap.Toast(toastLiveExample)

        toast.show()

    }
}

function limpiarValue(id) {

    document.getElementById(id).value = "";

}

function limpiarinner(id) {

    document.getElementById(id).innerHTML = "";

}

function limpiarCheck(id) {

    document.getElementById(id).checked = false;

}

function onclicCheck(id) {

    document.getElementById(id).checked = true;

}

bodyloandNameInit();

/************************************funciones vistas *******************/


function loadDataUser() {

    limpiarValue("jsccms");
    limpiarValue("jsuserRed");
    limpiarValue("jsname");
    limpiarValue("jslastName");
    limpiarValue("jsemail");
    limpiarValue("jsemailAlternative");
    limpiarValue("jsccmsManager");
    limpiarCheck("jsstate");

    limpiarCheck("administradorNewUser");
    limpiarCheck("administratorEditPermissions");
    limpiarCheck("administratorAddClient");
    limpiarCheck("managerEquipment");
    limpiarCheck("managerRandomnessResults");
    limpiarCheck("random");
    limpiarCheck("calculatorPlans");
    limpiarCheck("calculatorDays");
    limpiarCheck("logsSura");

    var userRed = document.getElementById('user').value;


    if (userRed.length > 5) {

        openMensa("Cargando información, Espere...")

        var url = "../Admin/loadDataUserByUserRed";
        var data =
        {
            userRed: userRed
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {

                if (data.length != 0) {

                    document.getElementById('jsccms').value = data.CCMS;
                    document.getElementById('jsuserRed').value = data.USER;
                    document.getElementById('jsname').value = data.NOMBRES;
                    document.getElementById('jslastName').value = data.APELLIDOS;
                    document.getElementById('jsemail').value = data.EMAIL;
                    document.getElementById('jsemailAlternative').value = data.EMAIL_ALTERNO;
                    document.getElementById('jsccmsManager').value = data.CCMS_MANAGER;

                    if (data.Estado == "Active") {
                        onclicCheck("jsstate");
                        document.getElementById('bodydataUser').style.display = 'flex';
                    }

                } else {

                    document.getElementById('user').value = 0;
                    openMensa("El usuario no se encuentra en la base de datos, comoniquese con soporte...");
                }
            }
        });

        loadPermissions(userRed);

    }

}

function loadPermissions(userRed) {


    var url = "loadUserPermissions/Admin";
    var data =
    {
        userRed: userRed
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data.length != 0) {

                if (data.createUser == 1) {
                    onclicCheck("administradorNewUser");
                }

                if (data.editPermissions == 1) {
                    onclicCheck("administratorEditPermissions");
                }

                if (data.AddCampaigns == 1) {
                    onclicCheck("administratorAddClient");
                }

                if (data.myTeams == 1) {
                    onclicCheck("managerEquipment");
                }

                if (data.randomnessResults == 1) {
                    onclicCheck("managerRandomnessResults");
                }

                if (data.random == 1) {
                    onclicCheck("random");
                }

                if (data.planCalculator == 1) {
                    onclicCheck("calculatorPlans");
                }

                if (data.daysCalculator == 1) {
                    onclicCheck("calculatorDays");
                }

                if (data.bitacoraSura == 1) {
                    onclicCheck("logsSura");
                }


            } else {

                var CCMS = document.getElementById('userCCMS').value = "";
                openMensa("El usuario no se encuentra en la base de datos, comoniquese con soporte...");
            }
        }
    });

}

function eddPermissions() {

    document.getElementById('jsLoading').style.display = 'inline-block';

    let userRed = document.getElementById('user').value;
    let createUser = document.getElementById('administradorNewUser').checked;
    let editPermissions = document.getElementById('administratorEditPermissions').checked;
    let AddCampaigns = document.getElementById('administratorAddClient').checked;
    let myTeams = document.getElementById('managerEquipment').checked;
    let randomnessResults = document.getElementById('managerRandomnessResults').checked;
    let random = document.getElementById('random').checked;
    let planCalculator = document.getElementById('calculatorPlans').checked;
    let daysCalculator = document.getElementById('calculatorDays').checked;
    let bitacoraSura = document.getElementById('logsSura').checked;

    if (createUser == true) {
        createUser = 1;
    } else {
        createUser = 0;
    }

    if (editPermissions == true) {
        editPermissions = 1;
    } else {
        editPermissions = 0;
    }

    if (AddCampaigns == true) {
        AddCampaigns = 1;
    } else {
        AddCampaigns = 0;
    }

    if (myTeams == true) {
        myTeams = 1;
    } else {
        myTeams = 0;
    }

    if (randomnessResults == true) {
        randomnessResults = 1;
    } else {
        randomnessResults = 0;
    }

    if (random == true) {
        random = 1;
    } else {
        random = 0;
    }

    if (planCalculator == true) {
        planCalculator = 1;
    } else {
        planCalculator = 0;
    }

    if (daysCalculator == true) {
        daysCalculator = 1;
    } else {
        daysCalculator = 0;
    }

    if (bitacoraSura == true) {
        bitacoraSura = 1;
    } else {
        bitacoraSura = 0;
    }

    var url = "../Admin/updatePermissionsUser";
    var data = {
        userRed: userRed,
        createUser: createUser,
        editPermissions: editPermissions,
        AddCampaigns: AddCampaigns,
        myTeams: myTeams,
        randomnessResults: randomnessResults,
        random: random,
        planCalculator: planCalculator,
        daysCalculator: daysCalculator,
        bitacoraSura: bitacoraSura
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data == true) {

                openMensa("Permisos actualizados correctamente");

            } else {

                openMensa("El usuario no se encuentra en la base de datos, comoniquese con soporte...");
            }
            document.getElementById('jsLoading').style.display = 'none';
        }
    });



}

function updateUserData() {

    document.getElementById('jsLoadingUserData').style.display = 'inline-block';

    let CCMS = document.getElementById('jsccms').value;
    let USER = document.getElementById('user').value;
    let CCMS_MANAGER = document.getElementById('jsccmsManager').value;
    let NOMBRES = document.getElementById('jsname').value;
    let APELLIDOS = document.getElementById('jslastName').value;
    let EMAIL = document.getElementById('jsemail').value;
    let EMAIL_ALTERNO = document.getElementById('jsemailAlternative').value;
    let Estado = document.getElementById('jsstate').checked;


    if (Estado == true) {
        Estado = 'Active';
    } else {
        Estado = 'Inactive';
    }

    var url = "../Admin/updateUserData";
    var data = {
        CCMS: CCMS,
        USER: USER,
        CCMS_MANAGER: CCMS_MANAGER,
        NOMBRES: NOMBRES,
        APELLIDOS: APELLIDOS,
        EMAIL: EMAIL,
        EMAIL_ALTERNO: EMAIL_ALTERNO,
        Estado: Estado
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {

            if (data == true) {

                openMensa("Datos actualizados correctamente");

            } else {

                openMensa("El usuario no se encuentra en la base de datos, comoniquese con soporte...");
            }

            document.getElementById('jsLoadingUserData').style.display = 'none';
        }
    });



}