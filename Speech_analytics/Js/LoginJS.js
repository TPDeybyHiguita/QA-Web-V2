
function datasession() {

    let user = document.getElementById("jsUser").value;
    let password = document.getElementById("jsPassword").value;
    let alerta = "";

    if (user.length == 0) {
        alerta += `Escriba su usuario <br>`;
        mensaje_1(alerta);
    }
    else if (password.length == 0) {
        alerta += `Escriba su contraseña  <br>`;
        mensaje_1(alerta);
    }
    else
    {
        validate_User();
    }
}

function mensaje_1(alerta) {

    document.getElementById('jsMostrar').style.display = 'block';
    document.getElementById('jsMostrar').innerHTML = alerta;

}

function validate_User() {

    document.getElementById('session').style.display = 'none';

    var url = "validateUser/Account";
    var data =
    {
        user: document.getElementById("jsUser").value,
        password: document.getElementById("jsPassword").value
    };
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        success: function (data) {
            if (data == 1) {
                window.location.href = "../App/Index";
            }
            else {
                mensaje_1("Credenciales incorrectas");
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}



function onSubmit(token) {
    document.getElementById("demo-form").submit();
    datasession();
}




