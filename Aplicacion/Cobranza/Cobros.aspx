<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cobros.aspx.cs" Inherits="WebSistemmas.Cobranza.Cobros" MasterPageFile ="~/Cobranza/MenuCobranza.Master"  %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder2"> 

    <script src="../js/jquery.js" type="text/javascript"></script>
<script src="../js/jquery.easing.1.3.js" type="text/javascript"></script>
<link href="../css/styles.css" type="text/css" rel="stylesheet" media="all"/>
<link href="../css/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" /> 
<script src="../js/tabs.js" type="text/javascript"></script>        
<script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
<script type ="text/javascript" src="../js/Cobranza.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">        
    .auto-style2 {
        width: 132px;
    }
    .auto-style4 {
        width: 140px;
    }

        .auto-style5 {
            width: 91px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 573px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
    <div id="tabs" style="width : 552px">
	    <ul>
		    <li id="liNombre"><a href="#tabs-2">Propietario</a></li>
		    <li id="liDireccion"><a href="#tabs-3">Direccion</a></li>
	    </ul>
	    <div id="tabs-2">
            <table>
                <tr>
                    <td class="auto-style5">
                        <asp:Label ID="Label2" runat="server" Text="Nombre"></asp:Label>
                    &nbsp;del Propietario</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtNombre" runat="server" Width="164px"></asp:TextBox>
                    </td>
                    <td class="auto-style2">
                        <asp:Button ID="btnBuscarNombre" runat="server" Height="30px" OnClick="btnBuscarNombre_Click" Text="Buscar" Width="90px" style="margin-left: 10px"/>
                    </td>
                </tr> 
            </table>
	    </div>
	    <div id="tabs-3">
            <table>
                <tr>
                    <td class="auto-style5">
                        <asp:Label ID="Label3" runat="server" Text="Direccion"></asp:Label>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtDireccion" runat="server" Width="163px"></asp:TextBox>
                    </td>
                    <td class="auto-style2">
                        <asp:Button ID="btnBuscarDireccion" runat="server" Height="30px" OnClick="btnBuscarDireccion_Click" Text="Buscar" Width="90px" style="margin-left: 10px"/>
                    </td>
                </tr>
            </table>
	    </div>
	</div>
                                        
    <asp:GridView ID="grdAlquileres" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" OnRowCommand="grdAlquileres_RowCommand" OnRowDataBound="grdAlquileres_RowDataBound" OnSelectedIndexChanged="grdAlquileres_SelectedIndexChanged" PageSize="4" style="margin-top: 20px;" Width="550px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion">
            <ItemStyle Width="45%" />
            </asp:BoundField>
            <asp:BoundField DataField="Dueño" HeaderText="Nombre" SortExpression="Nombre">
            <ItemStyle Width="45%" />
            </asp:BoundField>
            <asp:BoundField DataField="UF" HeaderText="UF" />
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="div_parent">
                        <asp:ImageButton ID="VerPagos" runat="server" CausesValidation="False" CommandName="Ver Pagos" ImageUrl="~/css/img/ico_pesos.png" ToolTip="Ver Pagos" />
                    </div>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
<%--        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
    </asp:GridView>
    <asp:GridView ID="grdPagos" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" OnRowCommand="grdPagos_RowCommand" OnRowDataBound="grdPagos_RowDataBound" OnSelectedIndexChanged="grdPagos_SelectedIndexChanged" PageSize="4" style="margin-top: 20px;" Width="552px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Periodo" HeaderText="Periodo" SortExpression="Periodo">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="40%" />
            </asp:BoundField>
            <asp:BoundField DataField="Importe" HeaderText="Importe" SortExpression="Importe">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="40%" />
            </asp:BoundField>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="div_parent">
                        <asp:ImageButton ID="Cobrar" runat="server" CausesValidation="False" CommandName="Cobrar" ImageUrl="~/css/img/ico_pesos.png" ToolTip="Cobrar" />
                    </div>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

    <br />
    <asp:Label ID="lblError" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
    <br />

    <div id="divImprimirTotalInmobiliaria" runat="server" visible="false" >
        <br />
        <asp:Button ID="btnImprimir" runat="server" OnClick="btnImprimir_Click" Text="Imprimir" Width="94px" Height="33px" />
    </div>

    <br />
    </div>
    </form>
</body>
</html>

</asp:Content>
