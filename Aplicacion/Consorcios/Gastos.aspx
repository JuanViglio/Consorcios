<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gastos.aspx.cs" Inherits="WebSistemmas.Consorcios.Gastos" MasterPageFile ="~/Consorcios/MenuConsorcios.Master" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1"> 

    <form id="form1" runat="server">
        <p style="font-size: large">
            <br />
            <span style="font-size: large; color: #003399">
                Gastos</span></p>
        <p>
                <asp:GridView ID="grdGastos" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdGastos_RowCommand" style="margin-top: 0px; margin-left: 0px;" Width="395px">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="detalle" HeaderText="Detalle">
                        <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                        </asp:BoundField>                
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
            </p>
        <p>
                &nbsp;</p>
        <table style="width: 508px; height: 37px">
            <tr>
                <td style="width: 68px; height: 21px;">
                    <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                </td>
                <td style="width: 292px; height: 21px">
                    <asp:DropDownList ID="ddlTipoGastos" runat="server" Height="17px" Width="250px">
                    </asp:DropDownList>
                </td>
                <td>

                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" Height="32px" OnClick="btnActualizar_Click" Width="97px" />

                </td>

            </tr>
        </table>
    </form>

</asp:Content>
