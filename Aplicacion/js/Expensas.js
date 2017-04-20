function cambioTipoGastos(ddl)
{
    $.ajax({
        type: "POST",
        url: "ExpensaNueva.aspx/OnSubmit",
        data: "{'tipoGastoID': " + ddl.value + "}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            console.log(result.d);
            var detalleGastos = result.d;

            $("#ctl00_ContentPlaceHolder1_txtDetalle").autocomplete({
                source: detalleGastos
            });
        }
    });

}
