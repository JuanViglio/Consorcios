<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Expensas.aspx.cs" Inherits="WebSistemmas.Consorcios.Expensas" MasterPageFile ="~/Consorcios/MenuConsorcios.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1"> 

    <form id="form1" runat="server">
        <span style="color: #003399; font-size: large">
        <br />
        Expensas</span><br />
        <table>
            <tr>
                <td style="width: 652px">
                    <asp:GridView ID="grdExpensas" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdExpensas_RowCommand" style="margin-top: 0px; margin-left: 0px;" Width="595px" OnSelectedIndexChanged="grdExpensas_SelectedIndexChanged" OnRowDataBound="grdExpensas_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="Periodo" HeaderText="Periodo">
                            <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total_Gastos" HeaderText="Total">
                            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="div_parent">
                                        <asp:ImageButton ID="UnidadesFuncionales" runat="server" CausesValidation="False" CommandName="UnidadesFuncionales" ImageUrl="~/css/img/home.png" ToolTip="Unidades Funcionales" />                                   
                                        <asp:ImageButton ID="VerPagos" runat="server" CausesValidation="False" CommandName="Expensas" ImageUrl="~/css/img/ico_pesos.png" ToolTip="Expensas" />
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
                <td>
                    <div>
            <asp:GridView ID="grdUnidades" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdUnidades_RowCommand" style="margin-top: 0px; margin-left: 0px;" Width="622px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Numero">
                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Dueño" HeaderText="Dueño">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Coeficiente" HeaderText="Coeficiente">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="div_parent">
                                <asp:ImageButton ID="Modificar" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
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
        </div>
                </td>
            </tr>
            <tr>
                <td style="height: 68px">
                    <asp:Button ID="btnNuevaExpensa" runat="server" Height="35px" OnClick="btnNuevaExpensa_Click" Text="Nuevo" Width="107px" />
                </td>
            </tr>
        </table>
    </form>

</asp:Content>
