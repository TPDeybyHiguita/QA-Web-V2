console.log = function () { };
console.warn = function () { };
console.error = function () { };



loadMenu();

function loadMenu() {


    let HTML = "";
    let createUser = "";
    let editPermissions = "";
    let AddCampaigns = "";
    let myTeams = "";
    let randomnessResults = "";
    let random = "";
    let planCalculator = "";
    let daysCalculator = "";
    let bitacoraSura = "";


    HTML += "<li class='menu-item active'>\
                    <a href = '../App/Index' class='menu-link'>\
                    <i class='menu-icon tf-icons bx bx-home-circle' ></i >\
                            <div data-i18n='Analytics'>Inicio</div>\
                        </a >\
                    </li >\
                        \
                    <!--PERMIR -->\
        \
                    <li class='menu-item'>\
                        <a href='javascript:void(0);' class='menu-link menu-toggle'>\
                            <i class='menu-icon tf-icons bx bx-layout'></i>\
                            <div data-i18n='Layouts'>Mi perfil</div>\
                        </a>\
        \
                        <ul class='menu-sub'>\
                            <li class='menu-item'>\
                                <a href='../User/UpdateProfile' class='menu-link'>\
                                    <div data-i18n='Without menu'>Editar perfil</div>\
                                </a>\
                            </li>\
                        </ul>\
                    </li>";

    createUser = "  <li class='menu-item'>\
                        <a href='../Admin/NewUser' class='menu-link'>\
                        <div data-i18n='Without menu'>Nuevo Usuario</div>\
                        </a>\
                    </li>";

    editPermissions = "<li class='menu-item'>\
                                <a href='../Admin/UserPermissions' class='menu-link'>\
                                    <div data-i18n='Without menu'>Editar Permisos</div>\
                                </a>\
                            </li>";

    AddCampaigns = "<li class='menu-item'>\
                                <a href='../Admin/Customers' class='menu-link'>\
                                    <div data-i18n='Without menu'>Campañas</div>\
                                </a>\
                            </li>";

    myTeams = "<li class='menu-item'>\
                                <a href='../Admin/Manager' class='menu-link'>\
                                    <div data-i18n='Without menu'>Manager</div>\
                                </a>\
                            </li>";

    randomnessResults = "<li class='menu-item'>\
                                <a href='../Admin/ResultsRandom' class='menu-link'>\
                                    <div data-i18n='Without menu'>Resultados Aleatorio</div>\
                                </a>\
                            </li>";

    random = "<li class='menu-item'>\
                                <a href='../App/Random' class='menu-link'>\
                                    <div data-i18n='Basic'>Generar Actividad</div>\
                                </a>\
                            </li>\
                            <li class='menu-item'>\
                                <a href='../App/configurarAleatoriedad' class='menu-link'>\
                                    <div data-i18n='Without menu'>Conf Aleatoriedad</div>\
                                </a>\
                            </li>";

    planCalculator = "<li class='menu-item '>\
                                <a href='../App/CalculadoraPLanes' class='menu-link'>\
                                    <div data-i18n='Account'>Calculadora de Planes</div>\
                                </a>\
                            </li>";


    bitacoraSura = "<li class='menu-item '>\
                                <a href='../bitacoraSura/index' class='menu-link'>\
                                    <div data-i18n='Account'>Sura</div>\
                                </a>\
                            </li>";






    let HTML_Administrador = "<li class='menu-item '>\
                        <a class='menu-link menu-toggle'>\
                            <i class='menu-icon tf-icons bx bx-layout'></i>\
                            <div data-i18n='Layouts'>Administrador</div>\
                        </a>\
        \
                        <ul class='menu-sub'>\
                            <li class='menu-item'>\
                                <a href='../Admin/NewUser' class='menu-link'>\
                                    <div data-i18n='Without menu'>Nuevo Usuario</div>\
                                </a>\
                            </li>\
                            <li class='menu-item'>\
                                <a href='../Admin/UserPermissions' class='menu-link'>\
                                    <div data-i18n='Without menu'>Editar Permisos</div>\
                                </a>\
                            </li>\
                            <li class='menu-item'>\
                                <a href='../Admin/ResultsRandom' class='menu-link'>\
                                    <div data-i18n='Without menu'>Resultados Aleatorio</div>\
                                </a>\
                            </li>\
                           \
                            <li class='menu-item'>\
                                <a href='../Admin/Manager' class='menu-link'>\
                                    <div data-i18n='Without menu'>Manager</div>\
                                </a>\
                            </li>\
                            <li class='menu-item'>\
                                <a href='../Admin/Customers' class='menu-link'>\
                                    <div data-i18n='Without menu'>Campañas</div>\
                                </a>\
                            </li>\
    \
                        </ul>\
                    </li>";



    let HTML_Calculadoras = "<li class='menu-item'>\
                        <a href='javascript:void(0);' class='menu-link menu-toggle'>\
                            <i class='menu-icon tf-icons bx bx-dock-top'></i>\
                            <div data-i18n='Account Settings'>Calculadoras</div>\
                        </a>\
                        <ul class='menu-sub'>\
                            <li class='menu-item '>\
                                <a href='../App/CalculadoraPLanes' class='menu-link'>\
                                    <div data-i18n='Account'>Calculadora de Planes</div>\
                                </a>\
                            </li>\
                        </ul>\
                    </li>";

    let HTML_Analista = "<li class='menu-header small text-uppercase'>\
                                <span class='menu-header-text'>Funciones</span>\
                            </li>\
                        <li class='menu-item'>\
                        <a href='javascript:void(0);' class='menu-link menu-toggle'>\
                            <i class='menu-icon tf-icons bx bx-dock-top'></i>\
                            <div data-i18n='Authentications'>Aleatoriedad</div>\
                        </a>\
                        <ul class='menu-sub'>\
                            <li class='menu-item'>\
                                <a href='../App/Random' class='menu-link'>\
                                    <div data-i18n='Basic'>Generar Actividad</div>\
                                </a>\
                            </li>\
                            <li class='menu-item'>\
                                <a href='../App/configurarAleatoriedad' class='menu-link'>\
                                    <div data-i18n='Without menu'>Conf Aleatoriedad</div>\
                                </a>\
                            </li>\
                        </ul>\
                    </li>";

    let HTML_informesSpeech = "<li class='menu-item'>\
                        <a href='javascript:void(0);' class='menu-link menu-toggle'>\
                            <i class='menu-icon tf-icons bx bx-dock-top'></i>\
                            <div data-i18n='Account Settings'>Speech</div>\
                        </a>\
                        <ul class='menu-sub'>\
                            <li class='menu-item '>\
                                <a href='' class='menu-link'>\
                                    <div data-i18n='Account'>Actualizaciones</div>\
                                </a>\
                            </li>\
                        </ul>\
                    </li>";

    let HTML_bitacoraSURA = "<li class='menu-item'>\
                        <a href='javascript:void(0);' class='menu-link menu-toggle'>\
                            <i class='menu-icon tf-icons bx bx-dock-top'></i>\
                            <div data-i18n='Account Settings'>Bitacoras</div>\
                        </a>\
                        <ul class='menu-sub'>\
                            <li class='menu-item '>\
                                <a href='../bitacoraSura/index' class='menu-link'>\
                                    <div data-i18n='Account'>Sura</div>\
                                </a>\
                            </li>\
                        </ul>\
                    </li>";


    var url = "../Permissions/validarPermisosUser";
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {

            if (data[0]["createUser"] == "1" || data[0]["editPermissions"] == "1" || data[0]["AddCampaigns"] == "1") {

                HTML += "<li class='menu-item '>\
                            <a class='menu-link menu-toggle'>\
                                <i class='menu-icon tf-icons bx bx-layout'></i>\
                                <div data-i18n='Layouts'>Administrador</div>\
                            </a>\
                            <ul class='menu-sub'>";

                if (data[0]["createUser"] == "1") {

                    HTML += createUser;
                }

                if (data[0]["editPermissions"] == "1") {

                    HTML += editPermissions;
                }

                if (data[0]["AddCampaigns"] == "1") {

                    HTML += AddCampaigns;
                }

                HTML += "      </ul>\
                            </li>";






            }

            if (data[0]["myTeams"] == "1" || data[0]["randomnessResults"] == "1") {
                HTML += "<li class='menu-item '>\
                            <a class='menu-link menu-toggle'>\
                                <i class='menu-icon tf-icons bx bx-layout'></i>\
                                <div data-i18n='Layouts'>Manager</div>\
                            </a>\
                            <ul class='menu-sub'>";

                if (data[0]["myTeams"] == "1") {

                    HTML += myTeams;
                }

                if (data[0]["randomnessResults"] == "1") {

                    HTML += randomnessResults;
                }

                HTML += "      </ul>\
                            </li>";

            }



            if (data[0]["planCalculator"] == "1" || data[0]["daysCalculator"] == "1") {
                HTML += "<li class='menu-item '>\
                            <a class='menu-link menu-toggle'>\
                                <i class='menu-icon tf-icons bx bx-layout'></i>\
                                <div data-i18n='Layouts'>Calculadoras</div>\
                            </a>\
                            <ul class='menu-sub'>";

                if (data[0]["planCalculator"] == "1") {

                    HTML += planCalculator;
                }

                if (data[0]["daysCalculator"] == "1") {

                    HTML += "   <li class='menu-item'>\
                                    <a href='#' class='menu-link'>\
                                        <div data-i18n='Without menu'>Calculadora de Días</div>\
                                    </a>\
                                </li>";
                }

                HTML += "      </ul>\
                            </li>";

            }

            if (data[0]["random"] == "1") {

                HTML += "<li class='menu-item '>\
                            <a class='menu-link menu-toggle'>\
                                <i class='menu-icon tf-icons bx bx-layout'></i>\
                                <div data-i18n='Layouts'>Aleatoriedad</div>\
                            </a>\
                            <ul class='menu-sub'>";

                if (data[0]["planCalculator"] == "1") {

                    HTML += random;
                }

                HTML += "      </ul>\
                            </li>";

            }

            if (data[0]["bitacoraSura"] == "1") {

                HTML += "<li class='menu-item '>\
                            <a class='menu-link menu-toggle'>\
                                <i class='menu-icon tf-icons bx bx-layout'></i>\
                                <div data-i18n='Layouts'>Bitacoras</div>\
                            </a>\
                            <ul class='menu-sub'>";

                if (data[0]["bitacoraSura"] == "1") {

                    HTML += bitacoraSura;
                }

                HTML += "      </ul>\
                            </li>";

            }


            document.getElementById('jsVerificarpermisosMenu').innerHTML = HTML;
        },
        error: function (request, status, error) {

        }
    });
}
