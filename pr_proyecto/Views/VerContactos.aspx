<%@ Page Title="Gestión de Contactos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerContactos.aspx.cs" Inherits="pr_proyecto.Views.VerContactos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fas fa-address-book text-success"></i> Gestion de Contactos</h2>
            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalNuevoContacto">
                <i class="fas fa-user-plus"></i> Nuevo Contacto
            </button>
        </div>

        <!-- Filtros -->
        <div class="card mb-4">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBuscarContacto" runat="server" CssClass="form-control" placeholder="Buscar por nombre, apellidos o email..."></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlFiltroSucursal" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroSucursal_SelectedIndexChanged">
                            <asp:ListItem Value="">Todas las sucursales</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <!-- Filtro por tipo eliminado - campo no existe en BD -->
                    <div class="col-md-3">
                        <asp:Button ID="btnBuscarContacto" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarContacto_Click" />
                        <asp:Button ID="btnLimpiarContacto" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiarContacto_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- GridView de Contactos -->
        <div class="card">
            <div class="card-body">
                <asp:GridView ID="gvContactos" runat="server" CssClass="table table-striped table-hover" 
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="15" 
                    OnPageIndexChanging="gvContactos_PageIndexChanging" ShowHeader="true"
                    EmptyDataText="No se encontraron contactos.">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <span class="badge bg-secondary"><%# Eval("IdContacto") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre Completo">
                            <ItemTemplate>
                                <div>
                                    <strong><%# Eval("Nombre") %> <%# Eval("ApPaterno") %> <%# Eval("ApMaterno") %></strong>
                                </div>
                                <small class="text-muted"><%# Eval("Puesto") %></small>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sucursal">
                            <ItemTemplate>
                                <div>
                                    <strong><%# Eval("Sucursal.NomComercial") %></strong>
                                </div>
                                <small class="text-muted"><%# Eval("Sucursal.Empresa.Nombre") %></small>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contacto">
                            <ItemTemplate>
                                <div>
                                    <i class="fas fa-envelope"></i> <%# Eval("Correo") %>
                                </div>
                                <div>
                                    <i class="fas fa-phone"></i> <%# Eval("Telefono") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Literal runat="server" Text='<%# GetBotonesContacto(Eval("IdContacto")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>

        <!-- Resumen -->
        <div class="row mt-4">
            <div class="col-md-3">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Total Contactos</h5>
                        <h3 class="text-primary"><asp:Label ID="lblTotalContactos" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
                                <!-- Estadísticas por tipo eliminadas - campo no existe en BD -->
            <div class="col-md-3">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Este Mes</h5>
                        <h3 class="text-warning"><asp:Label ID="lblEsteMes" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
        </div>

        <!-- Campos ocultos para operaciones -->
        <asp:HiddenField ID="hdnAccion" runat="server" />
        <asp:HiddenField ID="hdnIdContacto" runat="server" />
        <asp:Button ID="btnAccionOculto" runat="server" style="display:none;" OnClick="btnAccionOculto_Click" />
    </div>

    <!-- Modal Nuevo/Editar Contacto -->
    <div class="modal fade" id="modalNuevoContacto" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalContactoLabel">Nuevo Contacto</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label runat="server" AssociatedControlID="txtNombreContacto">Nombre *</asp:Label>
                            <asp:TextBox ID="txtNombreContacto" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombreContacto" 
                                CssClass="text-danger" ErrorMessage="El nombre es requerido." ValidationGroup="GuardarContacto" />
                        </div>
                        <div class="col-md-4">
                            <asp:Label runat="server" AssociatedControlID="txtApPaternoContacto">Apellido Paterno *</asp:Label>
                            <asp:TextBox ID="txtApPaternoContacto" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApPaternoContacto" 
                                CssClass="text-danger" ErrorMessage="El apellido paterno es requerido." ValidationGroup="GuardarContacto" />
                        </div>
                        <div class="col-md-4">
                            <asp:Label runat="server" AssociatedControlID="txtApMaternoContacto">Apellido Materno</asp:Label>
                            <asp:TextBox ID="txtApMaternoContacto" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="ddlSucursalContacto">Sucursal *</asp:Label>
                            <asp:DropDownList ID="ddlSucursalContacto" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">Seleccione una sucursal</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSucursalContacto" 
                                CssClass="text-danger" ErrorMessage="Debe seleccionar una sucursal." ValidationGroup="GuardarContacto" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtPuestoContacto">Puesto</asp:Label>
                            <asp:TextBox ID="txtPuestoContacto" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label runat="server" AssociatedControlID="txtTelefonoContacto">Teléfono</asp:Label>
                            <asp:TextBox ID="txtTelefonoContacto" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label runat="server" AssociatedControlID="txtEmailContacto">Email</asp:Label>
                            <asp:TextBox ID="txtEmailContacto" runat="server" CssClass="form-control" TextMode="Email" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarContacto" runat="server" Text="Guardar" CssClass="btn btn-primary" 
                        OnClick="btnGuardarContacto_Click" ValidationGroup="GuardarContacto" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function editarContacto(idContacto) {
            document.getElementById('<%= hdnAccion.ClientID %>').value = 'cargar_editar';
            document.getElementById('<%= hdnIdContacto.ClientID %>').value = idContacto;
            document.getElementById('<%= btnAccionOculto.ClientID %>').click();
        }

        function eliminarContacto(idContacto, nombreContacto) {
            if (confirm('¿Está seguro de que desea eliminar el contacto "' + nombreContacto + '"?\\n\\nEsta acción no se puede deshacer.')) {
                document.getElementById('<%= hdnAccion.ClientID %>').value = 'eliminar';
                document.getElementById('<%= hdnIdContacto.ClientID %>').value = idContacto;
                document.getElementById('<%= btnAccionOculto.ClientID %>').click();
            }
        }

        function verDetalleContacto(idContacto) {
            alert('Funcionalidad de detalle por implementar - ID: ' + idContacto);
        }

        function limpiarModalContacto() {
            document.getElementById('<%= txtNombreContacto.ClientID %>').value = '';
            document.getElementById('<%= txtApPaternoContacto.ClientID %>').value = '';
            document.getElementById('<%= txtApMaternoContacto.ClientID %>').value = '';
            document.getElementById('<%= ddlSucursalContacto.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= txtPuestoContacto.ClientID %>').value = '';
            document.getElementById('<%= txtTelefonoContacto.ClientID %>').value = '';
            document.getElementById('<%= txtEmailContacto.ClientID %>').value = '';
            document.querySelector('#modalContactoLabel').textContent = 'Nuevo Contacto';
        }

        document.addEventListener('DOMContentLoaded', function() {
            var modal = document.getElementById('modalNuevoContacto');
            if (modal) {
                modal.addEventListener('show.bs.modal', function(event) {
                    if (document.getElementById('<%= hdnAccion.ClientID %>').value !== 'editar') {
                        limpiarModalContacto();
                    }
                });
            }
        });
    </script>
</asp:Content>