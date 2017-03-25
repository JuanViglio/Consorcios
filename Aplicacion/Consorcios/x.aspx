<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="x.aspx.cs" Inherits="Websistemmas.ConsorciosLista" MasterPageFile ="~/Consorcios/MenuConsorcios.Master"  %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1"> 

<script src="scripts/Alquileres.js"></script>

<form id="form1" runat="server">

<body>
    <table>
        <tr>
            <td style="width: 717px" valign="top">
                <asp:GridView ID="grdAlquileres" runat="server" AutoGenerateColumns="False"  Height="150px" style="margin-top: 47px; margin-left: 13px;" Width="642px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdAlquileres_RowCommand" OnRowDataBound="grdAlquileres_RowDataBound" OnSelectedIndexChanged="grdAlquileres_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" >
                            <ItemStyle Font-Bold="False" Font-Names="Calibri" ForeColor="#8888A5" Font-Size="Large"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" SortExpression="FechaInicio" >
                            <ItemStyle Font-Bold="False" Font-Names="Calibri" ForeColor="#8888A5" Font-Size="Large"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" SortExpression="FechaFin" >
                            <ItemStyle Font-Bold="False" Font-Names="Calibri" ForeColor="#8888A5" Font-Size="Large"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Inquilino" HeaderText="Inquilino" >
                            <ItemStyle Font-Bold="False" Font-Names="Calibri" ForeColor="#8888A5" Font-Size="Large"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="InteresDiario" HeaderText="Interes" >                        
                            <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Names="Calibri" ForeColor="#8888A5" Font-Size="Large"/>                        
                        </asp:BoundField>
                        <asp:BoundField DataField="UltimoDiaPago" HeaderText="Ultimo dia de Pago">
                            <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" ForeColor="#8888A5" Font-Size="Large"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="ID_Alquiler" HeaderText="ID" >
                            <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" ForeColor="#8888A5" Font-Size="Large"/>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="div_parent">
                                <asp:ImageButton ID="VerPagos" runat="server" CausesValidation="False" 
                                    CommandName="Ver Pagos" ImageUrl= "~/assets/img/ico_pesos.png" 
                                    ToolTip="Ver Pagos" />
                                <asp:ImageButton ID="Eliminar" runat="server" CausesValidation="False" 
                                    CommandName="Eliminar" ImageUrl="~/assets/img/ico_eliminar.png" 
                                    ToolTip="Eliminar" />   
                                </div>                  
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>                         
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </td>            
        </tr>
    </table>

    
    <br />

    
</body>

        <asp:Label ID="lblError" runat="server" Font-Size="Large" ForeColor="#FF6600" style="margin-left: 13px;"></asp:Label>

</form>

</asp:Content>
