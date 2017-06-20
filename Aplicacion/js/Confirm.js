function Confirm(mensaje) {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    confirm_value.id = "confirm_value";

    if (confirm(mensaje)) {
        confirm_value.value = "Si";
    } else {
        confirm_value.value = "No";
    }

    var child = document.getElementById("confirm_value");
    if (child != null)
    {
        child.parentNode.removeChild(child);
    }

    document.forms[0].appendChild(confirm_value);
}
