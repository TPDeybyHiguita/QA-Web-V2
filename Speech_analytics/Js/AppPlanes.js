let canon;
let administracion;



function showHint(str) {

    canon = document.getElementById('jsCanon').value;
    administracion = str;

    data1 = [canon, administracion]
    calculos(data1);

}

function showHint2(str2) {

    canon = str2;
    administracion = document.getElementById('jsAdministracion').value;

    data1 = [canon, administracion]
    calculos(data1);

}

function calculos(data1) {

    let calculo1 = parseInt(data1[0]) + parseInt(data1[1]);
    let calculo2 = parseFloat(data1[0]) * 1.19;
    let calculo3 = (parseFloat(data1[0]) * 0.9) * 1.19;
    let calculo4 = (parseFloat(calculo1) * 0.04) * 1.19;
    let calculo5 = (parseFloat(calculo1) * 0.036) * 1.19;
    let calculo6 = (parseFloat(calculo1) * 0.08) * 1.19;
    let calculo7 = (parseFloat(calculo1) * 0.072) * 1.19;
    let calculo9 = (parseFloat(data1[0]) * 0.5) * 1.19;
    let calculo10 = parseInt(data1[0]) * 1.19;
    let calculo11 = (parseFloat(data1[0]) * 0.5) * 1.19;
    let calculo12 = (parseFloat(data1[0]) * 0.45) * 1.19;
    let calculo13 = (parseFloat(calculo1) * 0.1) * 1.19;
    let calculo14 = (parseFloat(calculo1) * 0.09) * 1.19;
    let calculo18 = parseFloat(calculo1) - parseFloat(calculo2);
    let calculo19 = parseFloat(calculo1) - parseFloat(calculo3);
    let calculo20 = parseFloat(calculo1) - parseFloat(calculo4) - parseInt(data1[1]);
    let calculo21 = parseFloat(calculo1) - parseFloat(calculo5) - parseInt(data1[1]);
    let calculo22 = parseFloat(calculo1) - parseFloat(calculo6) - parseInt(data1[1]);
    let calculo23 = parseFloat(calculo1) - parseFloat(calculo7) - parseInt(data1[1]);
    let calculo27 = parseFloat(calculo1) - parseFloat(calculo11) - parseInt(data1[1]);
    let calculo28 = parseFloat(calculo1) - parseFloat(calculo12) - parseInt(data1[1]);
    let calculo29 = parseFloat(calculo1) - parseFloat(calculo13) - parseInt(data1[1]);
    let calculo30 = parseFloat(calculo1) - parseFloat(calculo14) - parseInt(data1[1]);


    document.getElementById("jscalculo1").value = Moneda(calculo1);
    document.getElementById("jscalculo2").value = Moneda(calculo2);
    document.getElementById("jscalculo3").value = (Moneda(calculo3));
    document.getElementById("jscalculo4").value = (Moneda(calculo4));
    document.getElementById("jscalculo5").value = (Moneda(calculo5));
    document.getElementById("jscalculo6").value = (Moneda(calculo6));
    document.getElementById("jscalculo7").value = (Moneda(calculo7));
    document.getElementById("jscalculo8").value = (Moneda(calculo1));
    document.getElementById("jscalculo11").value = (Moneda(calculo11));
    document.getElementById("jscalculo12").value = (Moneda(calculo12));
    document.getElementById("jscalculo13").value = (Moneda(calculo13));
    document.getElementById("jscalculo14").value = (Moneda(calculo14));
    document.getElementById("jscalculo17").value = Moneda(calculo1);
    document.getElementById("jscalculo18").value = Moneda(calculo18);
    document.getElementById("jscalculo19").value = Moneda(calculo19);
    document.getElementById("jscalculo20").value = Moneda(calculo20);
    document.getElementById("jscalculo21").value = Moneda(calculo21);
    document.getElementById("jscalculo22").value = Moneda(calculo22);
    document.getElementById("jscalculo23").value = Moneda(calculo23);
    document.getElementById("jscalculo26 ").value = Moneda(calculo1);
    document.getElementById("jscalculo27").value = Moneda(calculo27);
    document.getElementById("jscalculo28").value = Moneda(calculo28);
    document.getElementById("jscalculo29").value = Moneda(calculo29);
    document.getElementById("jscalculo30").value = Moneda(calculo30);

}



function Moneda(valor) {

    let a = valor;

    let formato = new Intl.NumberFormat(undefined, {


        maximumFractionDigits: 3,

        minimumFractionDigits: 3
    });

    let variable = formato.format(a);
    return variable;
}