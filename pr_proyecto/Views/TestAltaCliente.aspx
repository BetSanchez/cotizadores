<%@ Page Title="Prueba Alta Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestAltaCliente.aspx.cs" Inherits="pr_proyecto.TestAltaCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .test-container {
            max-width: 800px;
            margin: 30px auto;
            padding: 20px;
            background: white;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        
        .test-section {
            margin-bottom: 20px;
            padding: 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }
        
        .test-button {
            background: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin: 5px;
        }
        
        .test-button:hover {
            background: #0056b3;
        }
        
        .result {
            margin-top: 10px;
            padding: 10px;
            border-radius: 5px;
        }
        
        .result.success {
            background: #d4edda;
            border: 1px solid #c3e6cb;
            color: #155724;
        }
        
        .result.error {
            background: #f8d7da;
            border: 1px solid #f5c6cb;
            color: #721c24;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="test-container">
        <h2>Prueba de Alta de Cliente</h2>
        
        <div class="test-section">
            <h3>1. Prueba de Conexión a Base de Datos</h3>
            <asp:Button ID="btnTestConexion" runat="server" Text="Probar Conexión" CssClass="test-button" OnClick="btnTestConexion_Click" />
            <asp:Panel ID="pnlResultConexion" runat="server" Visible="false" CssClass="result" />
        </div>
        
        <div class="test-section">
            <h3>2. Prueba de Registro de Empresa</h3>
            <asp:TextBox ID="txtNombreEmpresa" runat="server" placeholder="Nombre de la empresa" />
            <asp:Button ID="btnTestEmpresa" runat="server" Text="Registrar Empresa" CssClass="test-button" OnClick="btnTestEmpresa_Click" />
            <asp:Panel ID="pnlResultEmpresa" runat="server" Visible="false" CssClass="result" />
        </div>
        
        <div class="test-section">
            <h3>3. Prueba de Registro de Sucursal</h3>
            <asp:TextBox ID="txtNombreSucursal" runat="server" placeholder="Nombre comercial de la sucursal" />
            <asp:TextBox ID="txtTelefonoSucursal" runat="server" placeholder="Teléfono" />
            <asp:TextBox ID="txtCorreoSucursal" runat="server" placeholder="Correo" />
            <asp:Button ID="btnTestSucursal" runat="server" Text="Registrar Sucursal" CssClass="test-button" OnClick="btnTestSucursal_Click" />
            <asp:Panel ID="pnlResultSucursal" runat="server" Visible="false" CssClass="result" />
        </div>
        
        <div class="test-section">
            <h3>4. Prueba de Registro de Cliente Completo</h3>
            <asp:Button ID="btnTestClienteCompleto" runat="server" Text="Registrar Cliente Completo" CssClass="test-button" OnClick="btnTestClienteCompleto_Click" />
            <asp:Panel ID="pnlResultCliente" runat="server" Visible="false" CssClass="result" />
        </div>
        
        <div class="test-section">
            <h3>5. Listar Datos Registrados</h3>
            <asp:Button ID="btnListarDatos" runat="server" Text="Listar Datos" CssClass="test-button" OnClick="btnListarDatos_Click" />
            <asp:Panel ID="pnlResultListado" runat="server" Visible="false" CssClass="result" />
        </div>
    </div>
</asp:Content> 