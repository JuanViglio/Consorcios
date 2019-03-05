function SlideDivGastoDatos() {
    $('#divGastoDatos').slideDown();
    $('#divGastoModificar').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function MostrarDivGastoDatos() {
    $('#divGastoModificar').toggle();
    $('#divGastoDatos').toggle();
}

function CerrarDivGastoDatos() {
    $('#divGastoModificar').toggle();
    $('#divGastoDatos').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function CerrarDivGastoModificar() {
    $('#divGastoModificar').slideUp("slow");
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function ConfirmarBajaGasto() {
    return confirm("¿Esta seguro que quiere Eliminar el Gasto?")
}