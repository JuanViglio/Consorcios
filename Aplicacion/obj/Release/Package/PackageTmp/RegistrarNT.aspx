<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrarNT.aspx.cs" Inherits="ServempWeb.RegistrarNT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Ingreso de datos de usuario</title>
  <link href="css/demos.css" rel="stylesheet" type="text/css" />  
  <link href="css/Default.css" rel="stylesheet" type="text/css" />
  <link href="css/styleLogin.css" rel="stylesheet" type="text/css" />
  
    </head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
      <asp:ScriptReference Path="~/js/jquery-1.2.6.min.js" />
      <asp:ScriptReference Path="~/js/jquery.blockUI.js" />
      <asp:ScriptReference Path="~/js/Default.js" />
    </Scripts>
  </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
          <asp:Label ID="lblError" runat="server" Font-Size="Large" 
    ForeColor="Yellow"></asp:Label>
      </ContentTemplate>
  </asp:UpdatePanel>
  <br />
<div id="cerceve">
    <div class="formbodyRegistrar">
    <asp:TextBox class="text" ID="txtUsuario" placeholder="Ingrese su Mail" runat="server" style="background:url(images/mail.png) no-repeat;"></asp:TextBox>    
    <asp:TextBox class="text" ID="txtConfirm" placeholder="Confirme su Mail" runat="server" style="background:url(images/mail.png) no-repeat;"></asp:TextBox>
    <asp:TextBox class="text" ID="txtNombre" placeholder="Ingrese su Nombre" runat="server" style="background:url(images/username.png) no-repeat;"></asp:TextBox>

    <asp:Button class="submit"  runat="server" ID="Save" OnClick="Save_Click" Text="Guardar" UseSubmitBehavior="false"/>
    <asp:Button class="submit"  runat="server" ID="Cancelar" Text="Cancelar" UseSubmitBehavior="false" OnClick="Cancelar_Click"/>
    </div>
</div> 
  
</form>
</body>
</html>

<script type="text/javascript">
    $("button").click(function () {
        $("p").slideToggle("slow");
    });
</script>

