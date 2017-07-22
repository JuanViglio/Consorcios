function SlideDivConsorcioDatos() {
    $('#divConsorcioNuevo').toggle();
    $('#divConsorcioDatos').slideDown();
    $('#divConsorcioModificar').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function MostrarDivConsorcioDatos() {
    $('#divConsorcioNuevo').toggle();
    $('#divConsorcioDatos').toggle();
}

function CerrarDivConsorcioDatos() {
    $('#divConsorcioNuevo').toggle();
    $('#divConsorcioDatos').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function CerrarDivConsorcioModificar() {
    $('#divConsorcioModificar').slideUp("slow");
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function ConfirmarBajaConorcio() {
    return confirm("¿Esta seguro que quiere Eliminar el Consorcio?")
}