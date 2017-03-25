<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NuestraTierra.aspx.cs" Inherits="ServempWeb.NuestraTierra" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<script src="js/jquery.js" type="text/javascript"></script>
<script src="js/jquery.easing.1.3.js" type="text/javascript"></script>
<script type="text/javascript" src="js/json2.js"></script>
<script type="text/javascript" src="js/excanvas.pack.js"></script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery-ui.min.js"></script>
<script type="text/javascript" src="js/jquery.timers-1.2.js"></script>
<script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
<script type="text/javascript" src="js/jquery.galleryview-3.0-dev.js"></script>
<script type="text/javascript" src="js/jquery.corner.pack.js"></script>

<link type="text/css" rel="stylesheet" media="all" href="css/styles.css" />

<link href="css/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" /> 
<script src="js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
<link href="css/demos.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/tabs.js"></script>        
<link type="text/css" rel="stylesheet" href="css/jquery.galleryview-3.0-dev.css"/>
<link type="text/css" rel="stylesheet" href="css/Curvas.css" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 690px;
            height: 349px;
            margin-top: 21px;
        }
        .auto-style2 {
            width: 408px;
            height: 80px;
        }
        .auto-style3 {
            width: 241px;
            height: 80px;
        }
        #divChat {
            margin-left: 90px;
        }
        #btnPaginas {
            margin-left: 90px;
        }
        .auto-style6 {
            height: 90px;
        }
        .auto-style7 {
            height: 80px;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $('#myGallery').galleryView({
                panel_animation: 'slide',
                transition_speed: 1500,
                panel_width: 600,
                panel_height: 300,
                pan_images: true,
                show_filmstrip_nav: false,
                show_infobar: false,
                autoplay: true,
                show_filmstrip: false,
            });
        });

        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Atencion",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };
    </script>
</head>

<body>
    <form id="form1" runat="server" style="background-color: #EBF9DD">
    <div style="text-align: center; margin-left:320px">
        <table style="height: 135px; width: 950px; font-family: Courier; color: #008000;">
        <tr>
        <td class="style3"></td>
        <td class="style4">
        <ul id="myGallery">
        <li>
            <img src="Imagenes/Equipo2.JPG"/>
            <img src="Imagenes/Equipo.jpg"/>
        </li>
        </ul>
        </td></tr>    
        </table>        
        
    </div>       
    
    <div style="text-align: center;margin-top: 25px">
        <asp:Label ID="lblNombre" runat="server" Font-Names="Verdana" Font-Size="X-Large"></asp:Label>
    </div>
    <div id="divConfirmar" runat="server" style="text-align: center;margin-top: 25px">     
        <table style="margin-left: 250px">
            <tr>
            <td>
                <asp:Label ID="lblConfirmar" runat="server" Font-Names="Verdana" Font-Size="X-Large"></asp:Label>        
            </td>
            <td>
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" Height="36px" Width="84px" OnClick="btnConfirmar_Click" UseSubmitBehavior="False" /> 
            </td>
            </tr>
        </table>
    </div>
    <div id="divNoVoy" runat="server" style="text-align: center;margin-top: 10px">     
        <table style="margin-left: 320px">
            <tr>
            <td>
                <asp:Label ID="lblNoVoy" runat="server" Font-Names="Verdana" Font-Size="X-Large"></asp:Label>        
            </td>
            <td>
                <asp:Button ID="bntNoVoy" runat="server" Text="No Voy" Height="36px" Width="84px" OnClick="btnNoVoy_Click" UseSubmitBehavior="False" />                
            </td>
            </tr>
        </table>
    </div>   

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>             
    <table style="margin-top: 46px">
        <tr>
            <td class="auto-style2"><asp:Label ID="lblTotalConfirmados" runat="server" Font-Names="Verdana" ></asp:Label> </td>
            <td class="auto-style3">
                <asp:Label ID="lblChat" runat="server" Text="Ingrese el mensaje para el Chat"></asp:Label>
            </td>
            <td class="auto-style7">
                <asp:TextBox ID="txtMensajeChat" runat="server" Width="245px"></asp:TextBox>
            </td>
            <td class="auto-style7">
                <asp:Button ID="btnGuardarChat" runat="server" OnClick="btnGuardarChat_Click" Text="Guardar" Width="92px" Height="35px" style="margin-left: 21px" />
                &nbsp;</td>
        </tr>

    </table>
    </ContentTemplate>
    </asp:UpdatePanel>   

    <table><tr><td style="vertical-align:top">
    <div id="accordion" style ="width:300px">
        <div>
            <h3>
                <a href="#">Confirmados</a></h3>
            <div>
            
                <asp:DataList ID="lstConfirmados" runat="server" Width="244px" BackColor="White" BorderColor="#CCCCCC" BorderWidth="4px" CellPadding="1" ForeColor="Black" BorderStyle="None" GridLines="Horizontal">
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <ItemTemplate >                        
                        <asp:Label ID="NombreLabel" runat="server" Text='<%# Eval("Nombre") %>'/>
                        <br />
                        <br />
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
            
            </div>
        </div>
        <div>
            <h3>
                <a href="#">Sin Confirmar</a></h3>
            <div>
             
                <asp:DataList ID="lstSinConfirmar" runat="server" Width="244px" BackColor="White" BorderColor="#CCCCCC" BorderWidth="4px" CellPadding="1" ForeColor="Black" BorderStyle="None" GridLines="Horizontal">
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <ItemTemplate>                        
                        <asp:Label ID="NombreLabel" runat="server" Text='<%# Eval("Nombre") %>' />
                        <br />
                        <br />
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
            
            </div>
        </div>
        <div>
            <h3>
                <a href="#">No van</a></h3>
            <div>
            
                <asp:DataList ID="lstNoVan" runat="server" Width="244px" BackColor="White" BorderColor="#CCCCCC" BorderWidth="4px" CellPadding="1" ForeColor="Black" BorderStyle="None" GridLines="Horizontal">
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <ItemTemplate>                        
                        <asp:Label ID="NombreLabel" runat="server" Text='<%# Eval("Nombre") %>' />
                        <br />
                        <br />
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
            
            </div>
        </div>
    </div>
    </td>
    <td style="vertical-align:top; padding-left: 20px;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate> 
            <div id="divChat" runat="server"></div>

        
        <div id="btnPaginas" runat="server">
            <asp:Button ID="btnPaginaAnterior" runat="server" OnClick="btnPaginaAnterior_Click" Text="&lt;&lt;" alt="Pagina Anterior"/>
            &nbsp;&nbsp;
            <asp:Button ID="btnPaginaProxima" runat="server" OnClick="btnPaginaProxima_Click" Text="&gt;&gt;" alt="Proxima Pagina"/>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>     
    </td>
    </tr></table>

    </form>
</body>
</html>

<div id="dialog" style="display: none">
</div>