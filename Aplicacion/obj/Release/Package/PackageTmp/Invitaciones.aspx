<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Invitaciones.aspx.cs" Inherits="WebSistemmas.Invitaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 211px">
        
        <table><tr>
            <td class="auto-style1">
                <asp:Label ID="Label1" runat="server" Text="Invitacion para el dia "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaInvitacion" runat="server" Width="468px"></asp:TextBox>
            </td>
        </tr></table>
        <br />
        <br />
    
        <asp:Button ID="btnPasarA_SinConfirmar" runat="server" Height="49px" OnClick="btnPasarA_SinConfirmar_Click" Text="Pasar a Sin Confirmar" Width="159px" />
        &nbsp;&nbsp;&nbsp;
    
        <asp:Button ID="Button1" runat="server" Height="49px" OnClick="Button1_Click" Text="Enviar invitaciones" Width="136px" />

        <br />
        <br />

        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>

    <style type="text/css">
        a.ConfirmButton {
            border-style: solid;
            border-width : 1px 1px 1px 1px;
            text-decoration : none;
            padding : 4px;
            border-color : #000000;
            background-color: green;
            color: #FFFFFF;
        }
        a.NoVoyButton {
            border-style: solid;
            border-width : 1px 1px 1px 1px;
            text-decoration : none;
            padding : 4px;
            border-color : #000000;
            background-color: #FF0000;
            color: #FFFFFF;
        }
        a.ChatButton {
            border-style: solid;
            border-width : 1px 1px 1px 1px;
            text-decoration : none;
            padding : 4px;
            border-color : #000000;
            background-color: #000080;
            color: #FFFFFF;
        }
        .auto-style1 {
            width: 139px;
        }
    </style>