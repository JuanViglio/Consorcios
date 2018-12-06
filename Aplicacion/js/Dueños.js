function SlideDivDueñoDatos() {
    $('#divDueñoNuevo').toggle();
    $('#divDueñoDatos').slideDown();
    $('#divDueñoModificar').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function MostrarDivDueñoDatos() {
    $('#divDueñoNuevo').toggle();
    $('#divDueñoDatos').toggle();
}

function CerrarDivDueñoDatos() {
    $('#divDueñoNuevo').toggle();
    $('#divDueñoDatos').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}