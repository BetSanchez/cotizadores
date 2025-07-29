<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="pr_proyecto.WebForm1" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container text-center mt-5">
        <h2 class="mb-4">Menú</h2>
        
        <div class="d-grid gap-3 col-2 mx-auto">
            <asp:Button ID="btnAltaProspecto" runat="server" CssClass="btn btn-primary" Text="Alta Prospecto"/>
            <asp:Button ID="btnAltaVendedor" runat="server" CssClass="btn btn-success" Text="Alta Vendedor"/>
            <asp:Button ID="btnCotizaciones" runat="server" CssClass="btn btn-warning" Text="Cotizaciones"/>
            <asp:Button ID="btnVentas" runat="server" CssClass="btn btn-danger" Text="Ventas" />
        </div>
    </div>
</asp:Content>
