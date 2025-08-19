<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="pr_proyecto.Views.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .dashboard-card {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border-radius: 15px;
            padding: 2rem;
            margin-bottom: 2rem;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
        }
        
        .welcome-section {
            background: rgba(255, 255, 255, 0.95);
            border-radius: 15px;
            padding: 2rem;
            margin-bottom: 2rem;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }
        
        .user-info {
            background: #f8f9fa;
            border-radius: 10px;
            padding: 1.5rem;
            margin-bottom: 1rem;
        }
        
        .btn-logout {
            background: linear-gradient(135deg, #ff6b6b 0%, #ee5a52 100%);
            border: none;
            border-radius: 8px;
            color: white;
            padding: 0.5rem 1.5rem;
            text-decoration: none;
            transition: all 0.3s ease;
        }
        
        .btn-logout:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(255, 107, 107, 0.3);
            color: white;
            text-decoration: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="welcome-section">
            <div class="row">
                <div class="col-md-8">
                    <h2><i class="fas fa-tachometer-alt text-primary"></i> Dashboard</h2>
                    <p class="text-muted">Bienvenido al sistema de cotizadores</p>
                </div>
                <div class="col-md-4 text-end">
                    <a href="Logout.aspx" class="btn-logout">
                        <i class="fas fa-sign-out-alt"></i> Cerrar Sesión
                    </a>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="user-info">
                    <h4><i class="fas fa-user text-primary"></i> Información del Usuario</h4>
                    <hr>
                    <div class="row">
                        <div class="col-sm-4"><strong>Usuario:</strong></div>
                        <div class="col-sm-8"><asp:Label ID="lblUsuario" runat="server" /></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4"><strong>Nombre:</strong></div>
                        <div class="col-sm-8"><asp:Label ID="lblNombre" runat="server" /></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4"><strong>Rol:</strong></div>
                        <div class="col-sm-8"><asp:Label ID="lblRol" runat="server" /></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4"><strong>ID Usuario:</strong></div>
                        <div class="col-sm-8"><asp:Label ID="lblIdUsuario" runat="server" /></div>
                    </div>
                </div>
            </div>
            
            <div class="col-md-6">
                <div class="dashboard-card">
                    <h4><i class="fas fa-chart-line"></i> Estadísticas Rápidas</h4>
                    <hr>
                    <div class="row text-center">
                        <div class="col-6">
                            <h3 class="mb-0">0</h3>
                            <small>Cotizaciones</small>
                        </div>
                        <div class="col-6">
                            <h3 class="mb-0">0</h3>
                            <small>Clientes</small>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Panel de Funciones del Vendedor -->
        <asp:Panel ID="panelVendedor" runat="server" Visible="true">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header bg-success text-white">
                            <h5><i class="fas fa-user-tie"></i> Funciones del Vendedor</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/Crear_cotizacion.aspx" class="btn btn-outline-success w-100">
                                        <i class="fas fa-file-invoice"></i><br>
                                        Crear Cotización
                                    </a>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/AltaCliente.aspx" class="btn btn-outline-primary w-100">
                                        <i class="fas fa-user-plus"></i><br>
                                        Alta Cliente
                                    </a>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/VerCotizaciones.aspx" class="btn btn-outline-info w-100">
                                        <i class="fas fa-list"></i><br>
                                        Ver Cotizaciones
                                    </a>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/VerEmpresas.aspx" class="btn btn-outline-warning w-100">
                                        <i class="fas fa-building"></i><br>
                                        Empresas/Sucursales
                                    </a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/VerContactos.aspx" class="btn btn-outline-secondary w-100">
                                        <i class="fas fa-address-book"></i><br>
                                        Ver Contactos
                                    </a>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/VerServicios.aspx" class="btn btn-outline-dark w-100">
                                        <i class="fas fa-tools"></i><br>
                                        Ver Servicios
                                    </a>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/ReporteCotizaciones.aspx" class="btn btn-outline-purple w-100">
                                        <i class="fas fa-chart-bar"></i><br>
                                        Generar Reporte
                                    </a>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/ExportarCotizaciones.aspx" class="btn btn-outline-primary w-100">
                                        <i class="fas fa-file-export"></i><br>
                                        Exportar Cotizaciones
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Panel de Funciones del Administrador -->
        <asp:Panel ID="panelAdmin" runat="server" Visible="false">
            <div class="row mt-3">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header bg-danger text-white">
                            <h5><i class="fas fa-user-shield"></i> Funciones del Administrador</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/GestionServicios.aspx" class="btn btn-outline-danger w-100">
                                        <i class="fas fa-cogs"></i><br>
                                        Gestionar Servicios
                                    </a>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/AltaVendedor.aspx" class="btn btn-outline-warning w-100">
                                        <i class="fas fa-user-plus"></i><br>
                                        Alta Vendedor
                                    </a>
                                </div>
                                <div class="col-md-3 mb-3">
                                    <a href="/Views/EstadoCotizaciones.aspx" class="btn btn-outline-info w-100">
                                        <i class="fas fa-chart-line"></i><br>
                                        Estado Cotizaciones
                                    </a>
                                </div>
                                                               <div class="col-md-3 mb-3">
                                   <a href="/Views/GestionUsuarios.aspx" class="btn btn-outline-dark w-100">
                                       <i class="fas fa-users-cog"></i><br>
                                       Gestión Usuarios
                                   </a>
                               </div>
                           </div>
                           <div class="row">
                               <div class="col-md-3 mb-3">
                                   <a href="/Views/VerUsuarios.aspx" class="btn btn-outline-primary w-100">
                                       <i class="fas fa-users"></i><br>
                                       Ver Usuarios
                                   </a>
                               </div>
                               <div class="col-md-3 mb-3">
                                   <a href="/Views/GestionRoles.aspx" class="btn btn-outline-purple w-100">
                                       <i class="fas fa-user-shield"></i><br>
                                       Gestión Roles
                                   </a>
                               </div>
                               <div class="col-md-3 mb-3">
                                   <a href="/Views/GestionPlantillas.aspx" class="btn btn-outline-success w-100">
                                       <i class="fas fa-file-alt"></i><br>
                                       Gestión Plantillas
                                   </a>
                               </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content> 