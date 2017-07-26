function SlideDivUFDatos() {
    $('#divUFNuevo').toggle();
    $('#divUFDatos').slideDown();
    $('#divUFModificar').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function MostrarDivUFDatos() {
    $('#divUFNuevo').toggle();
    $('#divUFDatos').toggle();
}

function CerrarDivUFDatos() {
    $('#divUFNuevo').toggle();
    $('#divUFDatos').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function CerrarDivUFModificar() {
    $('#divUFModificar').slideUp("slow");
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function ConfirmarBajaConorcio() {
    return confirm("¿Esta seguro que quiere Eliminar la UF?")
}