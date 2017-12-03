<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Expensas.aspx.cs" Inherits="WebSistemmas.Consorcios.Expensas" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.2.3/jquery-confirm.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.2.3/jquery-confirm.min.js"></script>
    <script type="text/javascript" src="../js/Confirm.js"></script>

    <form id="form1" runat="server">
        <span style="color: #003399; font-size: large">
            <br />
            <asp:Label ID="lblTitulo" runat="server" Text="Expensas"></asp:Label>
        <br />
        </span>
        <br />
        <table>
            <tr>
                <td style="width: 652px">
                    <asp:gridview id="grdExpensas" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" onrowcommand="grdExpensas_RowCommand" style="margin-top: 0px; margin-left: 0px;" width="595px" onselectedindexchanged="grdExpensas_SelectedIndexChanged" onrowdatabound="grdExpensas_RowDataBound">
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
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="div_parent">
                                        <asp:ImageButton ID="VerPagos" runat="server" CausesValidation="False" CommandName="Expensas" ImageUrl="~/css/img/ico_pesos.png" ToolTip="Expensas" />
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="PeriodoNumerico" HeaderText="PeriodoNumerico">
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
                    </asp:gridview>
                </td>
                <td>
                    <div>
                        <asp:gridview id="grdUnidades" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" onrowcommand="grdUnidades_RowCommand" style="margin-top: 0px; margin-left: 0px;" width="622px" onrowdatabound="grdUnidades_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Numero">
                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Coeficiente" HeaderText="Coeficiente">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PagoId" HeaderText="PagoId">
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
            </asp:gridview>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 68px">
                    <asp:button id="btnNuevaExpensa" runat="server" height="35px" onclick="btnNuevaExpensa_Click" text="Nuevo" width="107px" />
                &nbsp;
                    <asp:button id="btnVolver" runat="server" height="35px" onclick="btnVolver_Click" text="Volver" width="107px" style="margin-left: 24px" />
                </td>
                <td>
                    <div id="divBotonesUF" runat="server" visible="false">
                        <table>
                            <tr style="width: 150px">
                                <td style="width: 144px">
                                    <asp:button id="btnAceptarExpensasUF" runat="server" height="35px" onclientclick="Confirm('¿Esta seguro que quiere ACEPTAR las Expensas?')" onclick="btnAceptarExpensasUF_Click" text="Aceptar Expensas" width="128px" />
                                </td>
                                <td style="width: 148px">
                                    <asp:button id="btnAnularExpensasUF" runat="server" height="35px" onclientclick="Confirm('¿Esta seguro que quiere ANULAR las Expensas?')" onclick="btnAnularExpensasUF_Click" text="Anular Expensas" width="128px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</asp:Content>

