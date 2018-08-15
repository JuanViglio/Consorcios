$(function () {
    $("#accordion").accordion();
});

function cambioTipoGastos()
{
    $.ajax({
        type: "POST",
        url: "ExpensaNueva.aspx/OnSubmit",
        data: "{'tipoGastoID': 2}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            var detalleGastos = result.d;

            $("#ContentPlaceHolder1_txtDetalleGastoEvOrd").autocomplete({
                source: detalleGastos
            });
        },
    });

    $.ajax({
        type: "POST",
        url: "ExpensaNueva.aspx/OnSubmit",
        data: "{'tipoGastoID': 3}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            var detalleGastos = result.d;

            $("#ContentPlaceHolder1_txtDetalleGastoEvExt").autocomplete({
                source: detalleGastos
            });
        },
    });
}

function abrirAcordion(solapa) {
    console.log(solapa);
    $("#accordion").accordion({ active: + solapa });
}

function PopUp(mensaje) {
    alert(mensaje);
}

function ConfirmarBajaGastoOrdinario() {
    return confirm("¿Esta seguro que quiere Eliminar el Gasto?")
}
