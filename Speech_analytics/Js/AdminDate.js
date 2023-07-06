$('.registerFormFcMv').bootstrapValidator({
    live: 'enabled',
    fields: {
        newinputTratFechaIni: {
            validators: {
                date: {
                    format: 'DD/MM/YYYY',
                    message: 'ESTE VALOR NO COINCIDE CON UNA FECHA'
                },
                stringLength: {
                    min: 10,
                    max: 10,
                    message: 'LA LONGITUD MÁXIMA ES DE 10 INCLUYENDO /'
                },
                regexp: {
                    regexp: /^[0-9-/]+$/,
                    message: 'LA FECHA SOLO PUEDE TENER NÚMEROS Y /'
                }
            }
        }

    }
});

$('.datepicker3').datepicker({
    container: '.container-calendar',
    autoclose: true,
    todayHighlight: true,
    calendarWeeks: true,
    format: 'dd/mm/yyyy',
    language: 'es',
    multidate: true
});