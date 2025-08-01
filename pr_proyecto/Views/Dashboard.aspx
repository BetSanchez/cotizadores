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

        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-header">
                        <h5><i class="fas fa-cogs text-primary"></i> Acciones Rápidas</h5>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3 mb-3">
                               <a href="/Views/AltaCliente.aspx" class="btn btn-outline-primary w-100">
    <i class="fas fa-user-plus"></i><br>
    Registrar Cliente
</a>

                            </div>
                            <div class="col-md-3 mb-3">
                                <a href="#" class="btn btn-outline-success w-100">
                                    <i class="fas fa-file-invoice"></i><br>
                                    Nueva Cotización
                                </a>
                            </div>
                            <div class="col-md-3 mb-3">
                                <a href="#" class="btn btn-outline-info w-100">
                                    <i class="fas fa-users"></i><br>
                                    Ver Clientes
                                </a>
                            </div>
                            <div class="col-md-3 mb-3">
                                <a href="#" class="btn btn-outline-warning w-100">
                                    <i class="fas fa-chart-bar"></i><br>
                                    Reportes
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content> 