<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Error.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Error" %>

<style type="text/css">
    .auto-style1 {
        height: 63px;
    }
    .auto-style2 {
        font-size: xx-small;
    }
</style>

<div id="divError" runat="server" style="color: #003399; font-size: large; " align="left" class="auto-style1">
    <span class="auto-style2"></span><br />
    <asp:label id="lblError" runat="server" forecolor="Red" style="text-align: left; font-size: large;"></asp:label>
</div>