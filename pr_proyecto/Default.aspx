<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="pr_proyecto._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">Menú</h2>

        <div class="row g-4 justify-content-center">
            <!-- Opción 1 -->
            <div class="col-6 col-md-3">
                <a href="Views/AltaCliente.aspx" class="card text-center p-4 shadow-sm h-100 text-decoration-none text-dark">
                    <i class="bi bi-cash fs-1 text-primary mb-2"></i>
                    <div class="mt-2 fw-bold">Crear Cotización</div>
                </a>
            </div>

            <!-- Opción 2 -->
            <div class="col-6 col-md-3">
                <a href="Views/AltaCliente.aspx" class="card text-center p-4 shadow-sm h-100 text-decoration-none text-dark">
                    <i class="bi bi-person-plus fs-1 text-primary mb-2"></i>
                    <div class="mt-2 fw-bold">Alta Cliente</div>
                </a>
            </div>

            <!-- Opción 3 -->
            <div class="col-6 col-md-3">
                <a href="Views/AltaVendedor.aspx" class="card text-center p-4 shadow-sm h-100 text-decoration-none text-dark">
                    <i class="bi bi-person-plus-fill fs-1 text-primary mb-2"></i>
                    <div class="mt-2 fw-bold">Alta Vendedor</div>
                </a>
            </div>

            <!-- Opción 4 -->
            <div class="col-6 col-md-3">
                <a href="Views/AltaVendedor.aspx" class="card text-center p-4 shadow-sm h-100 text-decoration-none text-dark">
                    <i class="bi bi-file-earmark-text fs-1 text-primary mb-2"></i>
                    <div class="mt-2 fw-bold">Cotizaciones</div>
                </a>
            </div>

            <!-- Opción 5 -->
            <div class="col-6 col-md-3">
                <a href="AltaCliente.aspx" class="card text-center p-4 shadow-sm h-100 text-decoration-none text-dark">
                    <i class="bi bi-cash-coin fs-1 text-primary mb-2"></i>
                    <div class="mt-2 fw-bold">Ventas</div>
                </a>
            </div>

            <!-- Opción 6 -->
            <div class="col-6 col-md-3">
                <a href="AltaCliente.aspx" class="card text-center p-4 shadow-sm h-100 text-decoration-none text-dark">
                    <i class="bi bi-list-check fs-1 text-primary mb-2"></i>
                    <div class="mt-2 fw-bold">Servicios</div>
                </a>
            </div>

            <!-- Opción 7 -->
            <div class="col-6 col-md-3">
                <a href="AltaCliente.aspx" class="card text-center p-4 shadow-sm h-100 text-decoration-none text-dark">
                    <i class="bi bi-clipboard2-check fs-1 text-primary mb-2"></i>
                    <div class="mt-2 fw-bold">Reportes</div>
                </a>
            </div>

            <!-- Opción 8 -->
            <div class="col-6 col-md-3">
                <a href="AltaCliente.aspx" class="card text-center p-4 shadow-sm h-100 text-decoration-none text-dark">
                    <i class="bi bi-shield-lock fs-1 text-primary mb-2"></i>
                    <div class="mt-2 fw-bold">Permisos</div>
                </a>
            </div>

        </div>
    </div>
</asp:Content>


