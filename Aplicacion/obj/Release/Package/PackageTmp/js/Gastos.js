function SlideDivGastoDatos() {
    $('#divGastoNuevo').toggle();
    $('#divGastoDatos').slideDown();
    $('#divGastoModificar').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function MostrarDivGastoDatos() {
    $('#divGastoNuevo').toggle();
    $('#divGastoDatos').toggle();
}

function CerrarDivGastoDatos() {
    $('#divGastoNuevo').toggle();
    $('#divGastoDatos').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function CerrarDivGastoModificar() {
    $('#divGastoModificar').slideUp("slow");
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function ConfirmarBajaConorcio() {
    return confirm("¿Esta seguro que quiere Eliminar el Gasto?")
}