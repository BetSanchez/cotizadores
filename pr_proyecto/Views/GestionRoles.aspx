<%@ Page Title="Gestión de Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionRoles.aspx.cs" Inherits="pr_proyecto.Views.GestionRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h2><i class="fas fa-user-shield"></i> Gestión de Roles y Permisos</h2>
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalNuevoRol">
                        <i class="fas fa-plus"></i> Nuevo Rol
                    </button>
                </div>
            </div>
        </div>

        <!-- Tabs para Roles y Permisos -->
        <ul class="nav nav-tabs" id="myTab" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="roles-tab" data-bs-toggle="tab" data-bs-target="#roles" type="button" role="tab">
                    <i class="fas fa-users"></i> Roles
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="permisos-tab" data-bs-toggle="tab" data-bs-target="#permisos" type="button" role="tab">
                    <i class="fas fa-key"></i> Permisos
                </button>
            </li>
        </ul>

        <div class="tab-content" id="myTabContent">
            <!-- Tab Roles -->
            <div class="tab-pane fade show active" id="roles" role="tabpanel">
                <div class="card">
                    <div class="card-body">
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <asp:TextBox ID="txtBuscarRol" runat="server" CssClass="form-control" placeholder="Buscar rol..."></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnBuscarRol" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarRol_Click" />
                                <asp:Button ID="btnLimpiarRol" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiarRol_Click" />
                            </div>
                        </div>

                        <asp:GridView ID="gvRoles" runat="server" CssClass="table table-striped table-hover" 
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" 
                            OnPageIndexChanging="gvRoles_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="IdRol" HeaderText="ID" />
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <strong><%# Eval("Nombre") %></strong>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Permisos">
                                    <ItemTemplate>
                                        <span class="badge bg-info"><%# GetCantidadPermisos(Eval("IdRol")) %> permisos</span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Usuarios">
                                    <ItemTemplate>
                                        <span class="badge bg-success"><%# GetCantidadUsuarios(Eval("IdRol")) %> usuarios</span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" Text='<%# GetBotonesRol(Eval("IdRol")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- Tab Permisos -->
            <div class="tab-pane fade" id="permisos" role="tabpanel">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card">
                                    <div class="card-header">
                                        <h5>Crear Nuevo Permiso</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="mb-3">
                                            <asp:Label runat="server" AssociatedControlID="txtNombrePermiso">Nombre del Permiso *</asp:Label>
                                            <asp:TextBox ID="txtNombrePermiso" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombrePermiso" 
                                                CssClass="text-danger" ErrorMessage="El nombre es requerido." ValidationGroup="GuardarPermiso" />
                                        </div>
                                        <div class="mb-3">
                                            <asp:Label runat="server" AssociatedControlID="txtCodigoPermiso">Código Numérico *</asp:Label>
                                            <asp:TextBox ID="txtCodigoPermiso" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCodigoPermiso" 
                                                CssClass="text-danger" ErrorMessage="El código es requerido." ValidationGroup="GuardarPermiso" />
                                        </div>
                                        <asp:Button ID="btnGuardarPermiso" runat="server" Text="Guardar Permiso" CssClass="btn btn-success w-100" 
                                            OnClick="btnGuardarPermiso_Click" ValidationGroup="GuardarPermiso" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <asp:GridView ID="gvPermisos" runat="server" CssClass="table table-striped" 
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                                    <Columns>
                                        <asp:BoundField DataField="IdPrivilegio" HeaderText="ID" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="Valor" HeaderText="Código" />
                                        <asp:TemplateField HeaderText="Acciones">
                                            <ItemTemplate>
                                                <asp:Literal runat="server" Text='<%# GetBotonesPermiso(Eval("IdPrivilegio")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Campos ocultos para operaciones -->
        <asp:HiddenField ID="hdnAccion" runat="server" />
        <asp:HiddenField ID="hdnIdRol" runat="server" />
        <asp:HiddenField ID="hdnIdPermiso" runat="server" />
        <asp:Button ID="btnAccionOculta" runat="server" style="display: none;" OnClick="btnAccionOculta_Click" />

        <!-- Modal Nuevo/Editar Rol -->
        <div class="modal fade" id="modalNuevoRol" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalRolLabel">Nuevo Rol</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label runat="server" AssociatedControlID="txtNombreRol">Nombre del Rol *</asp:Label>
                                <asp:TextBox ID="txtNombreRol" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombreRol" 
                                    CssClass="text-danger" ErrorMessage="El nombre del rol es requerido." ValidationGroup="GuardarRol" />
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-12">
                                <h6>Asignar Permisos:</h6>
                                <asp:CheckBoxList ID="cblPermisos" runat="server" CssClass="form-check" RepeatLayout="UnorderedList" 
                                    RepeatDirection="Vertical" CellPadding="5">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnGuardarRol" runat="server" Text="Guardar" CssClass="btn btn-primary" 
                            OnClick="btnGuardarRol_Click" ValidationGroup="GuardarRol" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function editarRol(idRol) {
            document.getElementById('<%= hdnAccion.ClientID %>').value = 'editar_rol';
            document.getElementById('<%= hdnIdRol.ClientID %>').value = idRol;
            document.getElementById('<%= btnAccionOculta.ClientID %>').click();
        }

        function eliminarRol(idRol) {
            if (confirm('¿Está seguro de que desea eliminar este rol? Esta acción no se puede deshacer.')) {
                document.getElementById('<%= hdnAccion.ClientID %>').value = 'eliminar_rol';
                document.getElementById('<%= hdnIdRol.ClientID %>').value = idRol;
                document.getElementById('<%= btnAccionOculta.ClientID %>').click();
            }
        }

        function eliminarPermiso(idPermiso) {
            if (confirm('¿Está seguro de que desea eliminar este permiso?')) {
                document.getElementById('<%= hdnAccion.ClientID %>').value = 'eliminar_permiso';
                document.getElementById('<%= hdnIdPermiso.ClientID %>').value = idPermiso;
                document.getElementById('<%= btnAccionOculta.ClientID %>').click();
            }
        }

        function limpiarFormularioRol() {
            document.getElementById('<%= txtNombreRol.ClientID %>').value = '';
            var checkboxes = document.querySelectorAll('#<%= cblPermisos.ClientID %> input[type="checkbox"]');
            checkboxes.forEach(function(checkbox) {
                checkbox.checked = false;
            });
            document.querySelector('#modalRolLabel').textContent = 'Nuevo Rol';
        }

        document.addEventListener('DOMContentLoaded', function() {
            var modal = document.getElementById('modalNuevoRol');
            if (modal) {
                modal.addEventListener('show.bs.modal', function() {
                    if (document.getElementById('<%= hdnAccion.ClientID %>').value !== 'editar_rol') {
                        limpiarFormularioRol();
                    }
                });
            }
        });
    </script>
</asp:Content>