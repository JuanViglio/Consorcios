<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Expensas.aspx.cs" Inherits="WebSistemmas.Consorcios.Expensas" MasterPageFile ="~/Consorcios/MenuConsorcios.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1"> 

    <form id="form1" runat="server">
        <asp:GridView ID="grdExpensas" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdExpensas_RowCommand" style="margin-top: 47px; margin-left: 13px;" Width="622px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Periodo" HeaderText="Periodo">
                <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                </asp:BoundField>
                <asp:BoundField DataField="Total_Gastos" HeaderText="Total">
                <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="div_parent">
                            <asp:ImageButton ID="VerPagos" runat="server" CausesValidation="False" CommandName="Expensas" ImageUrl="~/assets/img/ico_pesos.png" ToolTip="Expensas" />
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
    </form>

</asp:Content>
