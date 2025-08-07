<%@ Page Title="Prueba Simple" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestSimple.aspx.cs" Inherits="pr_proyecto.TestSimple" %>

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
            white-space: pre-wrap;
            font-family: monospace;
            font-size: 12px;
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
        <h2>Prueba Simple de Entity Framework</h2>
        
        <div class="test-section">
            <h3>1. Prueba de Conexión Básica</h3>
            <asp:Button ID="btnTestConexion" runat="server" Text="Probar Conexión" CssClass="test-button" OnClick="btnTestConexion_Click" />
            <asp:Panel ID="pnlResultConexion" runat="server" Visible="false" CssClass="result" />
        </div>
        
        <div class="test-section">
            <h3>2. Prueba de Consulta Simple</h3>
            <asp:Button ID="btnTestConsulta" runat="server" Text="Probar Consulta" CssClass="test-button" OnClick="btnTestConsulta_Click" />
            <asp:Panel ID="pnlResultConsulta" runat="server" Visible="false" CssClass="result" />
        </div>
        
        <div class="test-section">
            <h3>3. Prueba de Inserción Simple</h3>
            <asp:Button ID="btnTestInsercion" runat="server" Text="Probar Inserción" CssClass="test-button" OnClick="btnTestInsercion_Click" />
            <asp:Panel ID="pnlResultInsercion" runat="server" Visible="false" CssClass="result" />
        </div>
        
        <div class="test-section">
            <h3>4. Información de Configuración</h3>
            <asp:Button ID="btnInfoConfig" runat="server" Text="Mostrar Configuración" CssClass="test-button" OnClick="btnInfoConfig_Click" />
            <asp:Panel ID="pnlResultConfig" runat="server" Visible="false" CssClass="result" />
        </div>
    </div>
</asp:Content> 