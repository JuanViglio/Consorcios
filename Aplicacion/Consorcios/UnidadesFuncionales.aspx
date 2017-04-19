<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnidadesFuncionales.aspx.cs" Inherits="WebSistemmas.Consorcios.UnidadesFuncionales" MasterPageFile ="~/Consorcios/MenuConsorcios.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1"> 

    <form id="form1" runat="server">
        <div>
            <span style="color: #003399; font-size: large">
            <br />
            Unidades Funcionales</span><br />
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
                                <asp:ImageButton ID="Modificar" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/assets/img/ico_modificar.gif" ToolTip="Modificar" />
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
            <br />
                    <asp:Button ID="btnNuevaUnidad" runat="server" Height="35px" OnClick="btnNuevaUnidad_Click" Text="Nuevo" Width="107px" />
        </div>
    </form>

</asp:Content>
