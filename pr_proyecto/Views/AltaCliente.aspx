<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaCliente.aspx.cs" Inherits="pr_proyecto.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Alta de Cliente</h2>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

        <div class="form-group">
            <label for="txtNombre">Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtApPaterno">Apellido Paterno:</label>
            <asp:TextBox ID="txtApPaterno" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtApMaterno">Apellido Materno:</label>
            <asp:TextBox ID="txtApMaterno" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtCorreo">Correo:</label>
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" />
        </div>

        <div class="form-group">
            <label for="txtTelefono">Teléfono:</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtTelEmpresa">Teléfono de Empresa:</label>
            <asp:TextBox ID="txtTelEmpresa" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtCarrera">Carrera:</label>
            <asp:TextBox ID="txtCarrera" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtDireccion">Dirección:</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtFechaActivacion">Fecha de Activación:</label>
            <asp:TextBox ID="txtFechaActivacion" runat="server" CssClass="form-control" TextMode="Date" />
        </div>

        <div class="form-group">
            <label for="txtFechaIngreso">Fecha de Ingreso:</label>
            <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control" TextMode="Date" />
        </div>

        <div class="form-group">
            <label for="txtFechaTermino">Fecha de Término:</label>
            <asp:TextBox ID="txtFechaTermino" runat="server" CssClass="form-control" TextMode="Date" />
        </div>

        <div class="form-group">
            <label for="txtNomUsuario">Nombre de Usuario:</label>
            <asp:TextBox ID="txtNomUsuario" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="txtContrasena">Contraseña:</label>
            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" />
        </div>

        <div class="form-group">
            <label for="txtEstatus">Estatus:</label>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccione un estado" Value="" />
                <asp:ListItem Text="Activo" Value="Activo" />
                <asp:ListItem Text="Inactivo" Value="Inactivo" />
            </asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlRol">Rol:</label>
            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccione un rol" Value="" />
                <asp:ListItem Text="Administrador" Value="Administrador" />
                <asp:ListItem Text="Vendedor" Value="Vendedor" />
            </asp:DropDownList>
        </div>

        <asp:Button 
            ID="btnGuardar" 776
            runat="server" 
            Text="Guardar" 
            CssClass="btn btn-primary mt-3"
            OnClick="btnGuardar_Click"  
        />

    </div>
</asp:Content>

