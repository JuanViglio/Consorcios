﻿function SlideDivNuevoPago() {
    $('#divBotonPagoNuevo').toggle();
    $('#divNuevoPago').slideDown();
    $('#ctl00_ContentPlaceHolder1_UserControl2ID_lblError').text("");
    $('#ctl00_ContentPlaceHolder1_UserControl2ID_divError').slideUp();
}

function CerrarDivNuevoPago() {
    $('#divBotonPagoNuevo').toggle();
    $('#divNuevoPago').slideUp();
}

function SlideDivProveedorDatos() {
    $('#divProveedorNuevo').toggle();
    $('#divProveedorDatos').slideDown();
    $('#divProveedorModificar').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function MostrarDivProveedorDatos() {
    $('#divProveedorNuevo').toggle();
    $('#divProveedorDatos').toggle();
}

function CerrarDivProveedorDatos() {
    $('#divProveedorNuevo').toggle();
    $('#divProveedorDatos').slideUp();
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function CerrarDivProveedorModificar() {
    $('#divProveedorModificar').slideUp("slow");
    $('#ctl00_ContentPlaceHolder1_lblError').text("")
}

function ConfirmarBajaProveedor() {
    return confirm("¿Esta seguro que quiere Eliminar el Proveedor?")
}