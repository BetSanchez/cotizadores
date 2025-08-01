<%@ Page Title="Alta de Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaCliente.aspx.cs" Inherits="pr_proyecto.AltaCliente" %>

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

        .body-content {
            background: linear-gradient(135deg, #f8fafc 0%, #e2e8f0 100%);
            min-height: calc(100vh - 200px);
            padding: 0;
        }

        .body-content hr,
        .body-content footer {
            display: none;
        }

        .form-container {
            max-width: 1600px;
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
            padding: 50px 80px;
        }

        .page-header {
            background: linear-gradient(135deg, #10b981 0%, #059669 100%);
            color: white;
            padding: 20px 0;
            margin-bottom: 30px;
            text-align: center;
            box-shadow: 0 2px 10px rgba(16, 185, 129, 0.2);
        }
        
        .page-header h1 {
            font-size: 2rem;
            font-weight: 600;
            margin: 0;
        }

        .form-grid {
            display: grid;
            grid-template-columns: 1fr 1fr 1fr;
            gap: 50px;
            margin-bottom: 40px;
        }

        .form-group {
            position: relative;
        }

        .form-group.full-width {
            grid-column: 1 / -1;
        }

        .form-group.half-width {
            grid-column: span 2;
        }

        .form-group label {
            display: block;
            font-weight: 600;
            color: #1f2937;
            margin-bottom: 12px;
            font-size: 0.95rem;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            position: relative;
        }

        .form-group label::after {
            content: '';
            position: absolute;
            bottom: -4px;
            left: 0;
            width: 30px;
            height: 2px;
            background: linear-gradient(135deg, #10b981 0%, #059669 100%);
            border-radius: 1px;
        }

        .form-control {
            width: 100%;
            padding: 16px 20px;
            border: 3px solid #d1d5db;
            border-radius: 12px;
            font-size: 1.1rem;
            transition: all 0.3s ease;
            background: white;
            min-width: 60px;
            min-height: 60px;
            box-sizing: border-box;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
            font-weight: 500;
            color: #374151;
        }

        .form-control:focus {
            outline: none;
            border-color: #10b981;
            background: white;
            box-shadow: 0 0 0 4px rgba(16, 185, 129, 0.15), 0 4px 12px rgba(0, 0, 0, 0.15);
            transform: translateY(-2px);
        }

        .form-control:hover {
            border-color: #10b981;
            background: white;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
        }

        .form-control::placeholder {
            color: #9ca3af;
            font-weight: 400;
        }

        .btn-container {
            display: flex;
            justify-content: center;
            gap: 20px;
            padding-top: 30px;
            border-top: 1px solid #e5e7eb;
        }

        .btn {
            padding: 18px 40px;
            border: none;
            border-radius: 12px;
            font-size: 1rem;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            min-width: 160px;
        }

        .btn-primary {
            background: linear-gradient(135deg, #10b981 0%, #059669 100%);
            color: white;
            box-shadow: 0 4px 15px rgba(16, 185, 129, 0.3);
        }

        .btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 8px 25px rgba(16, 185, 129, 0.4);
        }

        .btn-secondary {
            background: linear-gradient(135deg, #6b7280 0%, #4b5563 100%);
            color: white;
            box-shadow: 0 4px 15px rgba(107, 114, 128, 0.3);
        }

        .btn-secondary:hover {
            transform: translateY(-2px);
            box-shadow: 0 8px 25px rgba(107, 114, 128, 0.4);
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

        .alert {
            margin-bottom: 20px;
            padding: 15px;
            border-radius: 8px;
            font-size: 0.875rem;
            font-weight: 500;
            display: flex;
            align-items: center;
            animation: slideInDown 0.3s ease-out;
        }

        .alert-success {
            background-color: #d4edda;
            border: 1px solid #c3e6cb;
            color: #155724;
        }

        .alert-danger {
            background-color: #f8d7da;
            border: 1px solid #f5c6cb;
            color: #721c24;
        }

        .alert i {
            margin-right: 8px;
            font-size: 1rem;
        }

        @keyframes slideInDown {
            from {
                opacity: 0;
                transform: translateY(-10px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .form-section {
            margin-bottom: 40px;
        }

        .section-title {
            font-size: 1.25rem;
            font-weight: 600;
            color: #1f2937;
            margin-bottom: 25px;
            padding-bottom: 10px;
            border-bottom: 2px solid #e5e7eb;
        }

        .section-subtitle {
            font-size: 1rem;
            font-weight: 500;
            color: #6b7280;
            margin-bottom: 20px;
            padding-left: 10px;
            border-left: 3px solid #10b981;
        }

        .checkbox-group {
            display: flex;
            align-items: center;
            gap: 10px;
            margin: 15px 0;
        }

        .checkbox-group input[type="checkbox"] {
            width: 18px;
            height: 18px;
            accent-color: #10b981;
        }

        .checkbox-group label {
            margin: 0;
            text-transform: none;
            font-size: 1rem;
            color: #374151;
        }

        .conditional-section {
            background: #f9fafb;
            border: 1px solid #e5e7eb;
            border-radius: 12px;
            padding: 25px;
            margin-top: 20px;
            transition: all 0.3s ease;
        }

        .conditional-section.show {
            background: white;
            border-color: #10b981;
            box-shadow: 0 2px 10px rgba(16, 185, 129, 0.1);
        }

        .search-container {
            position: relative;
            margin-bottom: 20px;
        }

        .search-container .form-control {
            padding-right: 50px;
        }

        .search-icon {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            color: #6b7280;
            font-size: 1.1rem;
        }

        .dropdown-container {
            position: relative;
        }

        .dropdown-container .form-control {
            cursor: pointer;
            background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 20 20'%3e%3cpath stroke='%236b7280' stroke-linecap='round' stroke-linejoin='round' stroke-width='1.5' d='m6 8 4 4 4-4'/%3e%3c/svg%3e");
            background-position: right 12px center;
            background-repeat: no-repeat;
            background-size: 16px;
            padding-right: 40px;
        }

        .step-indicator {
            display: flex;
            justify-content: center;
            margin-bottom: 30px;
            gap: 20px;
        }

        .step {
            display: flex;
            align-items: center;
            gap: 10px;
            padding: 10px 20px;
            border-radius: 25px;
            font-weight: 600;
            font-size: 0.9rem;
            transition: all 0.3s ease;
        }

        .step.active {
            background: linear-gradient(135deg, #10b981 0%, #059669 100%);
            color: white;
            box-shadow: 0 4px 15px rgba(16, 185, 129, 0.3);
        }

        .step.inactive {
            background: #f3f4f6;
            color: #6b7280;
        }

        .step-number {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
            font-size: 1rem;
        }

        .step.active .step-number {
            background: rgba(255, 255, 255, 0.2);
        }

        .step.inactive .step-number {
            background: #d1d5db;
        }

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
                grid-template-columns: 1fr 1fr;
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

        /* Efectos adicionales para campos de texto */
        .form-group {
            position: relative;
            transition: all 0.3s ease;
        }

        .form-group:hover {
            transform: translateY(-2px);
        }

        .form-control:not(:placeholder-shown) {
            border-color: #10b981;
            background: #f0fdf4;
        }

        .form-control:not(:placeholder-shown) + label {
            color: #10b981;
        }

        @media (max-width: 768px) {
            .form-grid {
                grid-template-columns: 1fr;
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
                <!-- Indicador de pasos -->
                <div class="step-indicator">
                    <div class="step active">
                        <div class="step-number">1</div>
                        <span>Empresa</span>
                    </div>
                    <div class="step inactive">
                        <div class="step-number">2</div>
                        <span>Sucursal</span>
                    </div>
                    <div class="step inactive">
                        <div class="step-number">3</div>
                        <span>Contacto</span>
                    </div>
                </div>

                <!-- Panel para mensajes de feedback -->
                <asp:Panel ID="pnlMensajes" runat="server" Visible="false" CssClass="alert" />
                
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

                <!-- PASO 1: Información de la Empresa -->
                <div class="form-section">
                    <h3 class="section-title">Paso 1: Información de la Empresa</h3>
                    
                    <div class="checkbox-group">
                        <asp:CheckBox ID="chkNuevaEmpresa" runat="server" AutoPostBack="true" OnCheckedChanged="chkNuevaEmpresa_CheckedChanged" />
                        <label for="chkNuevaEmpresa">Registrar nueva empresa</label>
                    </div>

                    <!-- Sección para nueva empresa -->
                    <div class="conditional-section" id="empresaSection" runat="server">
                        <h4 class="section-subtitle">Datos de la Nueva Empresa</h4>
                        <div class="form-grid">
                            <div class="form-group">
                                <label for="txtNombreEmpresa">Nombre de la Empresa</label>
                                <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvNombreEmpresa" runat="server" 
                                    ControlToValidate="txtNombreEmpresa" 
                                    ErrorMessage="El nombre de la empresa es requerido" 
                                    Display="None" />
                            </div>

                            <div class="form-group">
                                <label for="txtRazonSocial">Razón Social</label>
                                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-group">
                                <label for="txtGiro">Giro</label>
                                <asp:TextBox ID="txtGiro" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>

                    <!-- Sección para empresa existente -->
                    <div class="conditional-section" id="empresaExistenteSection" runat="server">
                        <h4 class="section-subtitle">Seleccionar Empresa Existente</h4>
                        <div class="form-grid">
                            <div class="form-group search-container">
                                <label for="txtFiltroEmpresa">Buscar Empresa</label>
                                <asp:TextBox ID="txtFiltroEmpresa" runat="server" CssClass="form-control" 
                                    placeholder="Escriba para filtrar empresas..." 
                                    AutoPostBack="true" OnTextChanged="txtFiltroEmpresa_TextChanged" />
                                <i class="fas fa-search search-icon"></i>
                            </div>

                            <div class="form-group full-width dropdown-container">
                                <label for="ddlEmpresaExistente">Empresa</label>
                                <asp:DropDownList ID="ddlEmpresaExistente" runat="server" CssClass="form-control" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEmpresaExistente_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvEmpresaExistente" runat="server" 
                                    ControlToValidate="ddlEmpresaExistente" 
                                    ErrorMessage="Debe seleccionar una empresa" 
                                    Display="None" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- PASO 2: Información de la Sucursal -->
                <div class="form-section">
                    <h3 class="section-title">Paso 2: Información de la Sucursal</h3>
                    
                    <div class="checkbox-group">
                        <asp:CheckBox ID="chkNuevaSucursal" runat="server" AutoPostBack="true" OnCheckedChanged="chkNuevaSucursal_CheckedChanged" />
                        <label for="chkNuevaSucursal">Registrar nueva sucursal</label>
                    </div>

                    <!-- Sección para sucursal existente -->
                    <div class="conditional-section" id="sucursalExistenteSection" runat="server">
                        <h4 class="section-subtitle">Seleccionar Sucursal Existente</h4>
                        <div class="form-grid">
                            <div class="form-group full-width dropdown-container">
                                <label for="ddlSucursalExistente">Sucursal</label>
                                <asp:DropDownList ID="ddlSucursalExistente" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="-- Primero seleccione una empresa --" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSucursalExistente" runat="server" 
                                    ControlToValidate="ddlSucursalExistente" 
                                    ErrorMessage="Debe seleccionar una sucursal" 
                                    Display="None" />
                            </div>
                        </div>
                    </div>

                    <!-- Sección para nueva sucursal -->
                    <div class="conditional-section" id="sucursalSection" runat="server">
                        <h4 class="section-subtitle">Datos de la Nueva Sucursal</h4>
                        <div class="form-grid">
                            <div class="form-group">
                                <label for="txtNomComercial">Nombre Comercial</label>
                                <asp:TextBox ID="txtNomComercial" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvNomComercial" runat="server" 
                                    ControlToValidate="txtNomComercial" 
                                    ErrorMessage="El nombre comercial es requerido" 
                                    Display="None" />
                            </div>

                            <div class="form-group">
                                <label for="txtTelefonoSucursal">Teléfono</label>
                                <asp:TextBox ID="txtTelefonoSucursal" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvTelefonoSucursal" runat="server" 
                                    ControlToValidate="txtTelefonoSucursal" 
                                    ErrorMessage="El teléfono de la sucursal es requerido" 
                                    Display="None" />
                            </div>

                            <div class="form-group">
                                <label for="txtCorreoSucursal">Correo</label>
                                <asp:TextBox ID="txtCorreoSucursal" runat="server" CssClass="form-control" TextMode="Email" />
                                <asp:RequiredFieldValidator ID="rfvCorreoSucursal" runat="server" 
                                    ControlToValidate="txtCorreoSucursal" 
                                    ErrorMessage="El correo de la sucursal es requerido" 
                                    Display="None" />
                            </div>

                            <div class="form-group">
                                <label for="txtDireccionSucursal">Dirección</label>
                                <asp:TextBox ID="txtDireccionSucursal" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvDireccionSucursal" runat="server" 
                                    ControlToValidate="txtDireccionSucursal" 
                                    ErrorMessage="La dirección de la sucursal es requerida" 
                                    Display="None" />
                            </div>

                            <div class="form-group">
                                <label for="txtMetros">Metros Cuadrados</label>
                                <asp:TextBox ID="txtMetros" runat="server" CssClass="form-control" TextMode="Number" />
                            </div>

                            <div class="form-group">
                                <label for="ddlColonia">Colonia</label>
                                <asp:DropDownList ID="ddlColonia" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvColonia" runat="server" 
                                    ControlToValidate="ddlColonia" 
                                    ErrorMessage="Seleccione una colonia" 
                                    Display="None" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- PASO 3: Información del Contacto -->
                <div class="form-section">
                    <h3 class="section-title">Paso 3: Información del Contacto</h3>
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
                            <label for="txtPuesto">Puesto</label>
                            <asp:TextBox ID="txtPuesto" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvPuesto" runat="server" 
                                ControlToValidate="txtPuesto" 
                                ErrorMessage="El puesto es requerido" 
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
                    
                    <asp:Button 
                        ID="btnLimpiar" 
                        runat="server" 
                        Text="Limpiar Formulario" 
                        CssClass="btn btn-secondary"
                        OnClick="btnLimpiar_Click" 
                        CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        // Efectos visuales mejorados para inputs
        document.addEventListener('DOMContentLoaded', function() {
            var inputs = document.querySelectorAll('.form-control');
            inputs.forEach(function(input) {
                // Efecto de focus
                input.addEventListener('focus', function() {
                    this.parentElement.style.transform = 'scale(1.02)';
                    this.parentElement.style.transition = 'all 0.3s ease';
                    this.style.boxShadow = '0 0 0 4px rgba(16, 185, 129, 0.15), 0 8px 25px rgba(0, 0, 0, 0.15)';
                });
                
                // Efecto de blur
                input.addEventListener('blur', function() {
                    this.parentElement.style.transform = 'scale(1)';
                    this.style.boxShadow = '0 2px 8px rgba(0, 0, 0, 0.1)';
                });

                // Efecto de input con contenido
                input.addEventListener('input', function() {
                    if (this.value.length > 0) {
                        this.style.borderColor = '#10b981';
                        this.style.background = '#f0fdf4';
                    } else {
                        this.style.borderColor = '#d1d5db';
                        this.style.background = 'white';
                    }
                });
            });

            // Efectos para secciones condicionales
            var checkboxes = document.querySelectorAll('input[type="checkbox"]');
            checkboxes.forEach(function(checkbox) {
                checkbox.addEventListener('change', function() {
                    var section = this.closest('.form-section').querySelector('.conditional-section');
                    if (section) {
                        if (this.checked) {
                            section.classList.add('show');
                            section.style.animation = 'slideDown 0.3s ease-out';
                        } else {
                            section.classList.remove('show');
                            section.style.animation = 'slideUp 0.3s ease-out';
                        }
                    }
                });
            });

            // Agregar placeholders dinámicos
            var textInputs = document.querySelectorAll('input[type="text"], input[type="email"], input[type="tel"]');
            textInputs.forEach(function(input) {
                var label = input.parentElement.querySelector('label');
                if (label && !input.placeholder) {
                    input.placeholder = 'Ingrese ' + label.textContent.toLowerCase();
                }
            });

            // Efectos especiales para el filtro de empresas
            var filtroEmpresa = document.getElementById('txtFiltroEmpresa');
            if (filtroEmpresa) {
                filtroEmpresa.addEventListener('input', function() {
                    var searchIcon = this.parentElement.querySelector('.search-icon');
                    if (this.value.length > 0) {
                        searchIcon.style.color = '#10b981';
                        this.style.borderColor = '#10b981';
                    } else {
                        searchIcon.style.color = '#6b7280';
                        this.style.borderColor = '#d1d5db';
                    }
                });
            }
        });

        // Animaciones CSS adicionales
        var style = document.createElement('style');
        style.textContent = `
            @keyframes slideDown {
                from { opacity: 0; transform: translateY(-10px); }
                to { opacity: 1; transform: translateY(0); }
            }
            @keyframes slideUp {
                from { opacity: 1; transform: translateY(0); }
                to { opacity: 0; transform: translateY(-10px); }
            }
        `;
        document.head.appendChild(style);
    </script>
</asp:Content>

