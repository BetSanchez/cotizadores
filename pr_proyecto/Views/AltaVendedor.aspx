<%@ Page Title="Alta de Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="pr_proyecto.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
            background: linear-gradient(135deg, #f8fafc 0%, #e2e8f0 100%);
            min-height: 100vh;
        }

        /* Sobrescribir el container del master page para páginas de formulario */
        .body-content {
            background: linear-gradient(135deg, #f8fafc 0%, #e2e8f0 100%);
            min-height: calc(100vh - 200px);
            padding: 0;
        }

        /* Ocultar el hr y footer en páginas de formulario */
        .body-content hr,
        .body-content footer {
            display: none;
        }

        .form-container {
            max-width: 1600px; /* Aumentado a 1600px para mayor amplitud */
            margin: 30px auto;
            background: white;
            border-radius: 16px;
            box-shadow: 0 4px 25px rgba(0, 0, 0, 0.05);
            overflow: hidden;
            animation: slideUp 0.6s ease-out;
        }

        @keyframes slideUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .form-content {
            padding: 50px 80px; /* Aumentado el padding horizontal a 80px */
        }

        .navbar-brand {
            font-size: 1.1rem;
            font-weight: 600;
        }
        
        .page-header {
            background: linear-gradient(135deg, #3b82f6 0%, #1d4ed8 100%);
            color: white;
            padding: 20px 0;
            margin-bottom: 30px;
            text-align: center;
            box-shadow: 0 2px 10px rgba(59, 130, 246, 0.2);
        }
        
        .page-header h1 {
            font-size: 2rem;
            font-weight: 600;
            margin: 0;
        }

        .form-grid {
            display: grid;
            grid-template-columns: 1fr 1fr 1fr; /* 3 columnas */
            gap: 50px; /* Aumentado el gap a 50px para mayor separación */
            margin-bottom: 40px;
        }

        .form-group {
            position: relative;
        }

        .form-group.full-width {
            grid-column: 1 / -1; /* Ocupa todas las columnas disponibles */
        }

        .form-group.half-width {
            grid-column: span 2; /* Ocupa 2 de las 3 columnas */
        }

        .form-group label {
            display: block;
            font-weight: 500;
            color: #374151;
            margin-bottom: 10px; /* Aumentado de 8px a 10px */
            font-size: 0.875rem;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .form-control {
            width: 100%;
            padding: 11px 28px; /* Aumentado el padding interno significativamente */
            border: 2px solid #e5e7eb;
            border-radius: 12px;
            font-size: 1.1rem; /* Aumentado el tamaño de fuente */
            transition: all 0.3s ease;
            background: #fafafa;
            min-width: 60px;
            min-height: 10px; /* Aumentado la altura mínima */
            box-sizing: border-box;
        }

        .form-control:focus {
            outline: none;
            border-color: #3b82f6;
            background: white;
            box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
            transform: translateY(-1px);
        }

        .form-control:hover {
            border-color: #d1d5db;
            background: white;
        }

        .btn-container {
            display: flex;
            justify-content: center;
            gap: 20px; /* Aumentado de 16px a 20px */
            padding-top: 30px; /* Aumentado de 24px a 30px */
            border-top: 1px solid #e5e7eb;
        }

        .btn {
            padding: 18px 40px; /* Aumentado el padding */
            border: none;
            border-radius: 12px;
            font-size: 1rem;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            min-width: 160px; /* Aumentado de 140px a 160px */
        }

        .btn-primary {
            background: linear-gradient(135deg, #3b82f6 0%, #1d4ed8 100%);
            color: white;
            box-shadow: 0 4px 15px rgba(59, 130, 246, 0.3);
        }

        .btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 8px 25px rgba(59, 130, 246, 0.4);
        }

        .text-danger {
            background: #fef2f2;
            border: 1px solid #fecaca;
            border-radius: 8px;
            padding: 16px;
            margin-bottom: 24px;
            color: #dc2626;
            font-size: 0.875rem;
        }

        .form-section {
            margin-bottom: 40px; /* Aumentado de 32px a 40px */
        }

        .section-title {
            font-size: 1.25rem; /* Aumentado de 1.125rem a 1.25rem */
            font-weight: 600;
            color: #1f2937;
            margin-bottom: 25px; /* Aumentado de 20px a 25px */
            padding-bottom: 10px; /* Aumentado de 8px a 10px */
            border-bottom: 2px solid #e5e7eb;
        }

        /* Media queries actualizadas */
        @media (max-width: 1400px) {
            .form-container {
                max-width: 1300px;
                margin: 20px;
            }
            
            .form-content {
                padding: 40px 60px;
            }
        }

        @media (max-width: 1200px) {
            .form-container {
                max-width: 1100px;
                margin: 20px;
            }
            
            .form-content {
                padding: 40px 50px;
            }
            
            .form-grid {
                gap: 40px;
            }
        }

        @media (max-width: 992px) {
            .form-grid {
                grid-template-columns: 1fr 1fr; /* Volver a 2 columnas en tablets */
                gap: 35px;
            }
            
            .form-content {
                padding: 35px 40px;
            }
            
            .form-control {
                padding: 20px 24px;
                font-size: 1rem;
                min-height: 60px;
            }
        }

        @media (max-width: 768px) {
            .form-grid {
                grid-template-columns: 1fr; /* 1 columna en móvil */
                gap: 30px;
            }
            
            .form-content {
                padding: 30px 24px;
            }
            
            .page-header h1 {
                font-size: 1.5rem;
            }
            
            .btn-container {
                flex-direction: column;
                gap: 12px;
            }
            
            .btn {
                padding: 14px 24px;
                font-size: 0.9rem;
            }
            
            .section-title {
                font-size: 1rem;
            }
            
            .form-control {
                padding: 18px 20px;
                font-size: 1rem;
                min-height: 56px;
            }
        }

        @media (max-width: 480px) {
            .form-container {
                margin: 10px;
                border-radius: 12px;
            }
            
            .form-content {
                padding: 20px;
            }
            
            .page-header h1 {
                font-size: 1.25rem;
            }
            
            .form-control {
                padding: 14px 16px;
                font-size: 0.9rem;
                min-height: 48px;
            }
            
            .form-group label {
                font-size: 0.8rem;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Header dinámico de la página -->
    <div class="page-header">
        <div class="container">
            <h1><%: Page.Title %></h1>
        </div>
    </div>

    <div class="container">
        <div class="form-container">
            <div class="form-content">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

            <div class="form-section">
                <h3 class="section-title">Información Personal</h3>
                <div class="form-grid">
                    <div class="form-group">
                        <label for="txtNombre">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                            ControlToValidate="txtNombre" 
                            ErrorMessage="El nombre es requerido" 
                            Display="None" />
                    </div>

                    <div class="form-group">
                        <label for="txtApPaterno">Apellido Paterno</label>
                        <asp:TextBox ID="txtApPaterno" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvApPaterno" runat="server" 
                            ControlToValidate="txtApPaterno" 
                            ErrorMessage="El apellido paterno es requerido" 
                            Display="None" />
                    </div>

                    <div class="form-group">
                        <label for="txtApMaterno">Apellido Materno</label>
                        <asp:TextBox ID="txtApMaterno" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group">
                        <label for="txtCorreo">Correo Electrónico</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" 
                            ControlToValidate="txtCorreo" 
                            ErrorMessage="El correo es requerido" 
                            Display="None" />
                        <asp:RegularExpressionValidator ID="revCorreo" runat="server" 
                            ControlToValidate="txtCorreo" 
                            ErrorMessage="Formato de correo inválido" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            Display="None" />
                    </div>

                    <div class="form-group">
                        <label for="txtTelefono">Teléfono Personal</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" 
                            ControlToValidate="txtTelefono" 
                            ErrorMessage="El teléfono es requerido" 
                            Display="None" />
                    </div>

                    <div class="form-group">
                        <label for="txtTelEmpresa">Teléfono Empresa</label>
                        <asp:TextBox ID="txtTelEmpresa" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group">
                        <label for="txtCarrera">Carrera/Profesión</label>
                        <asp:TextBox ID="txtCarrera" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group half-width">
                        <label for="txtDireccion">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>

            <div class="form-section">
                <h3 class="section-title">Fechas del Sistema</h3>
                <div class="form-grid">
                    <div class="form-group">
                        <label for="txtFechaActivacion">Fecha de Activación</label>
                        <asp:TextBox ID="txtFechaActivacion" runat="server" CssClass="form-control" TextMode="Date" />
                        <asp:RequiredFieldValidator ID="rfvFechaActivacion" runat="server" 
                            ControlToValidate="txtFechaActivacion" 
                            ErrorMessage="La fecha de activación es requerida" 
                            Display="None" />
                    </div>

                    <div class="form-group">
                        <label for="txtFechaIngreso">Fecha de Ingreso</label>
                        <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>

                    <div class="form-group">
                        <label for="txtFechaTermino">Fecha de Término</label>
                        <asp:TextBox ID="txtFechaTermino" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                </div>
            </div>

            <div class="form-section">
                <h3 class="section-title">Credenciales de Acceso</h3>
                <div class="form-grid">
                    <div class="form-group">
                        <label for="txtNomUsuario">Nombre de Usuario</label>
                        <asp:TextBox ID="txtNomUsuario" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNomUsuario" runat="server" 
                            ControlToValidate="txtNomUsuario" 
                            ErrorMessage="El nombre de usuario es requerido" 
                            Display="None" />
                    </div>

                    <div class="form-group">
                        <label for="txtContrasena">Contraseña</label>
                        <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rfvContrasena" runat="server" 
                            ControlToValidate="txtContrasena" 
                            ErrorMessage="La contraseña es requerida" 
                            Display="None" />
                    </div>

                    <div class="form-group">
                        <label for="txtConfirmarContrasena">Confirmar Contraseña</label>
                        <asp:TextBox ID="txtConfirmarContrasena" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rfvConfirmarContrasena" runat="server" 
                            ControlToValidate="txtConfirmarContrasena" 
                            ErrorMessage="Debe confirmar la contraseña" 
                            Display="None" />
                        <asp:CompareValidator ID="cvContrasenas" runat="server" 
                            ControlToValidate="txtConfirmarContrasena" 
                            ControlToCompare="txtContrasena" 
                            ErrorMessage="Las contraseñas no coinciden" 
                            Display="None" />
                    </div>

                    <div class="form-group">
                        <label for="DropDownList1">Estatus</label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvEstatus" runat="server" 
                            ControlToValidate="DropDownList1" 
                            ErrorMessage="Seleccione un estatus" 
                            Display="None" />
                    </div>

                    <div class="form-group">
                        <label for="ddlRol">Rol del Usuario</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRol" runat="server" 
                            ControlToValidate="ddlRol" 
                            ErrorMessage="Seleccione un rol" 
                            Display="None" />
                    </div>
                </div>
            </div>

            <div class="btn-container">
                <asp:Button 
                    ID="btnGuardar" 
                    runat="server" 
                    Text="Guardar Cliente" 
                    CssClass="btn btn-primary"
                    OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>
    </div>
    <script type="text/javascript">
        // Establecer fecha actual en campo de activación al cargar
        document.addEventListener('DOMContentLoaded', function() {
            var today = new Date().toISOString().split('T')[0];
            var fechaActivacion = document.getElementById('<%= txtFechaActivacion.ClientID %>');
            if (fechaActivacion && !fechaActivacion.value) {
                fechaActivacion.value = today;
            }
        });

        // Efectos visuales para inputs
        document.addEventListener('DOMContentLoaded', function() {
            var inputs = document.querySelectorAll('.form-control');
            inputs.forEach(function(input) {
                input.addEventListener('focus', function() {
                    this.parentElement.style.transform = 'scale(1.02)';
                    this.parentElement.style.transition = 'transform 0.2s ease';
                });
                
                input.addEventListener('blur', function() {
                    this.parentElement.style.transform = 'scale(1)';
                });
            });
        });
    </script>
</asp:Content>