<%@ Page Title="Gestión de Empresas y Sucursales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerEmpresas.aspx.cs" Inherits="pr_proyecto.Views.VerEmpresas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fas fa-building text-primary"></i> Gestion de Empresas y Sucursales</h2>
            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalNuevaEmpresa">
                <i class="fas fa-plus"></i> Nueva Empresa
            </button>
        </div>

        <!-- Filtros -->
        <div class="card mb-4">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtBuscarEmpresa" runat="server" CssClass="form-control" placeholder="Buscar empresa por nombre..."></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnBuscarEmpresa" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarEmpresa_Click" />
                        <asp:Button ID="btnLimpiarEmpresa" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiarEmpresa_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Tabs para Empresas y Sucursales -->
        <ul class="nav nav-tabs" id="myTab" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="empresas-tab" data-bs-toggle="tab" data-bs-target="#empresas" type="button" role="tab">
                    <i class="fas fa-building"></i> Empresas
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="sucursales-tab" data-bs-toggle="tab" data-bs-target="#sucursales" type="button" role="tab">
                    <i class="fas fa-store"></i> Sucursales
                </button>
            </li>
        </ul>

        <div class="tab-content" id="myTabContent">
            <!-- Tab Empresas -->
            <div class="tab-pane fade show active" id="empresas" role="tabpanel">
                <div class="card">
                    <div class="card-body">
                        <asp:GridView ID="gvEmpresas" runat="server" CssClass="table table-striped table-hover" 
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" 
                            OnPageIndexChanging="gvEmpresas_PageIndexChanging" ShowHeader="true"
                            EmptyDataText="No se encontraron empresas.">
                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <span class="badge bg-secondary"><%# Eval("IdEmpresa") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <strong><%# Eval("Nombre") %></strong>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sucursales">
                                    <ItemTemplate>
                                        <span class="badge bg-info"><%# GetCantidadSucursales(Eval("IdEmpresa")) %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" Text='<%# GetBotonesEmpresa(Eval("IdEmpresa")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- Tab Sucursales -->
            <div class="tab-pane fade" id="sucursales" role="tabpanel">
                <div class="card">
                    <div class="card-header d-flex justify-content-between align-items-center">
                        <h5>Lista de Sucursales</h5>
                        <button type="button" class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modalNuevaSucursal">
                            <i class="fas fa-plus"></i> Nueva Sucursal
                        </button>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvSucursales" runat="server" CssClass="table table-striped table-hover" 
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" 
                            OnPageIndexChanging="gvSucursales_PageIndexChanging" ShowHeader="true"
                            EmptyDataText="No se encontraron sucursales.">
                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <span class="badge bg-secondary"><%# Eval("IdSucursal") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Comercial">
                                    <ItemTemplate>
                                        <strong><%# Eval("NomComercial") %></strong>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empresa">
                                    <ItemTemplate>
                                        <%# Eval("Empresa.Nombre") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Teléfono">
                            <ItemTemplate>
                                <%# Eval("Telefono") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Correo">
                            <ItemTemplate>
                                <%# Eval("Correo") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dirección">
                            <ItemTemplate>
                                <small><%# Eval("Direccion") %></small>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" Text='<%# GetBotonesSucursal(Eval("IdSucursal")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <!-- Campos ocultos para operaciones -->
        <asp:HiddenField ID="hdnAccion" runat="server" />
        <asp:HiddenField ID="hdnIdEmpresa" runat="server" />
        <asp:HiddenField ID="hdnIdSucursal" runat="server" />
        <asp:Button ID="btnAccionOculto" runat="server" style="display:none;" OnClick="btnAccionOculto_Click" />
    </div>

    <!-- Modal Nueva/Editar Empresa -->
    <div class="modal fade" id="modalNuevaEmpresa" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEmpresaLabel">Nueva Empresa</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtNombreEmpresa">Nombre *</asp:Label>
                            <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombreEmpresa" 
                                CssClass="text-danger" ErrorMessage="El nombre es requerido." ValidationGroup="GuardarEmpresa" />
                        </div>
                        <div class="col-md-6">
                            <!-- Campo RFC no existe en BD, se elimina -->
                        </div>
                    </div>
                    <!-- Campos Teléfono y Email no existen en BD para empresas, se eliminan -->
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEmpresa" runat="server" Text="Guardar" CssClass="btn btn-primary" 
                        OnClick="btnGuardarEmpresa_Click" ValidationGroup="GuardarEmpresa" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Nueva/Editar Sucursal -->
    <div class="modal fade" id="modalNuevaSucursal" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalSucursalLabel">Nueva Sucursal</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="ddlEmpresaSucursal">Empresa *</asp:Label>
                            <asp:DropDownList ID="ddlEmpresaSucursal" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">Seleccione una empresa</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEmpresaSucursal" 
                                CssClass="text-danger" ErrorMessage="Debe seleccionar una empresa." ValidationGroup="GuardarSucursal" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtNomComercial">Nombre Comercial *</asp:Label>
                            <asp:TextBox ID="txtNomComercial" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNomComercial" 
                                CssClass="text-danger" ErrorMessage="El nombre comercial es requerido." ValidationGroup="GuardarSucursal" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtTelefonoSucursal">Teléfono *</asp:Label>
                            <asp:TextBox ID="txtTelefonoSucursal" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelefonoSucursal" 
                                CssClass="text-danger" ErrorMessage="El teléfono es requerido." ValidationGroup="GuardarSucursal" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtCorreoSucursal">Correo *</asp:Label>
                            <asp:TextBox ID="txtCorreoSucursal" runat="server" CssClass="form-control" TextMode="Email" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCorreoSucursal" 
                                CssClass="text-danger" ErrorMessage="El correo es requerido." ValidationGroup="GuardarSucursal" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <asp:Label runat="server" AssociatedControlID="txtDireccionSucursal">Dirección *</asp:Label>
                            <asp:TextBox ID="txtDireccionSucursal" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDireccionSucursal" 
                                CssClass="text-danger" ErrorMessage="La dirección es requerida." ValidationGroup="GuardarSucursal" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarSucursal" runat="server" Text="Guardar" CssClass="btn btn-primary" 
                        OnClick="btnGuardarSucursal_Click" ValidationGroup="GuardarSucursal" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function editarEmpresa(idEmpresa) {
            document.getElementById('<%= hdnAccion.ClientID %>').value = 'cargar_editar_empresa';
            document.getElementById('<%= hdnIdEmpresa.ClientID %>').value = idEmpresa;
            document.getElementById('<%= btnAccionOculto.ClientID %>').click();
        }

        function eliminarEmpresa(idEmpresa, nombreEmpresa) {
            if (confirm('¿Está seguro de que desea eliminar la empresa "' + nombreEmpresa + '"?\\n\\nEsta acción no se puede deshacer.')) {
                document.getElementById('<%= hdnAccion.ClientID %>').value = 'eliminar_empresa';
                document.getElementById('<%= hdnIdEmpresa.ClientID %>').value = idEmpresa;
                document.getElementById('<%= btnAccionOculto.ClientID %>').click();
            }
        }

        function editarSucursal(idSucursal) {
            document.getElementById('<%= hdnAccion.ClientID %>').value = 'cargar_editar_sucursal';
            document.getElementById('<%= hdnIdSucursal.ClientID %>').value = idSucursal;
            document.getElementById('<%= btnAccionOculto.ClientID %>').click();
        }

        function eliminarSucursal(idSucursal, nombreSucursal) {
            if (confirm('¿Está seguro de que desea eliminar la sucursal "' + nombreSucursal + '"?\\n\\nEsta acción no se puede deshacer.')) {
                document.getElementById('<%= hdnAccion.ClientID %>').value = 'eliminar_sucursal';
                document.getElementById('<%= hdnIdSucursal.ClientID %>').value = idSucursal;
                document.getElementById('<%= btnAccionOculto.ClientID %>').click();
            }
        }

        function limpiarModalEmpresa() {
            document.getElementById('<%= txtNombreEmpresa.ClientID %>').value = '';

            document.getElementById('<%= hdnIdEmpresa.ClientID %>').value = '';
            document.querySelector('#modalEmpresaLabel').textContent = 'Nueva Empresa';
        }

        function limpiarModalSucursal() {
            document.getElementById('<%= ddlEmpresaSucursal.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= txtNomComercial.ClientID %>').value = '';
            document.getElementById('<%= txtTelefonoSucursal.ClientID %>').value = '';

            document.getElementById('<%= hdnIdSucursal.ClientID %>').value = '';
            document.querySelector('#modalSucursalLabel').textContent = 'Nueva Sucursal';
        }

        // Limpiar modales al abrirlos
        document.addEventListener('DOMContentLoaded', function() {
            var modalEmpresa = document.getElementById('modalNuevaEmpresa');
            if (modalEmpresa) {
                modalEmpresa.addEventListener('show.bs.modal', function(event) {
                    if (document.getElementById('<%= hdnAccion.ClientID %>').value !== 'editar_empresa') {
                        limpiarModalEmpresa();
                    }
                });
            }

            var modalSucursal = document.getElementById('modalNuevaSucursal');
            if (modalSucursal) {
                modalSucursal.addEventListener('show.bs.modal', function(event) {
                    if (document.getElementById('<%= hdnAccion.ClientID %>').value !== 'editar_sucursal') {
                        limpiarModalSucursal();
                    }
                });
            }
        });
    </script>
</asp:Content>