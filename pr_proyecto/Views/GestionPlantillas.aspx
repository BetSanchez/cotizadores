<%@ Page Title="Gestión de Plantillas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionPlantillas.aspx.cs" Inherits="pr_proyecto.Views.GestionPlantillas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h2><i class="fas fa-file-alt"></i> Gestion de Plantillas de Servicios</h2>
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalNuevaPlantilla">
                        <i class="fas fa-plus"></i> Nueva Plantilla
                    </button>
                </div>
            </div>
        </div>

        <!-- Filtros -->
        <div class="row mb-4">
            <div class="col-md-4">
                <asp:TextBox ID="txtBuscarPlantilla" runat="server" CssClass="form-control" placeholder="Buscar plantilla..."></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddlFiltroUsuario" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroUsuario_SelectedIndexChanged">
                    <asp:ListItem Value="">Todos los usuarios</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnBuscarPlantilla" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarPlantilla_Click" />
                <asp:Button ID="btnLimpiarPlantilla" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiarPlantilla_Click" />
            </div>
        </div>

        <!-- GridView de Plantillas -->
        <div class="card">
            <div class="card-body">
                <asp:GridView ID="gvPlantillas" runat="server" CssClass="table table-striped table-hover" 
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10" 
                    OnPageIndexChanging="gvPlantillas_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="IdPlantilla" HeaderText="ID" />
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <strong><%# Eval("Nombre") %></strong>
                                <br />
                                <small class="text-muted"><%# Eval("Descripcion") %></small>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Creador">
                            <ItemTemplate>
                                <%# Eval("Usuario.Nombre") %> <%# Eval("Usuario.ApPaterno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Servicios">
                            <ItemTemplate>
                                <span class="badge bg-info"><%# GetCantidadServicios(Eval("IdPlantilla")) %> servicios</span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Creacion">
                            <ItemTemplate>
                                <%# Eval("FechaCreacion", "{0:dd/MM/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Literal runat="server" Text='<%# GetBotonesPlantilla(Eval("IdPlantilla")) %>' />
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
                        <h5 class="card-title">Total Plantillas</h5>
                        <h3 class="text-primary"><asp:Label ID="lblTotalPlantillas" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Este Mes</h5>
                        <h3 class="text-success"><asp:Label ID="lblEsteMes" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Mis Plantillas</h5>
                        <h3 class="text-info"><asp:Label ID="lblMisPlantillas" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Servicios Unicos</h5>
                        <h3 class="text-warning"><asp:Label ID="lblServiciosUnicos" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
        </div>

        <!-- Campos ocultos para operaciones -->
        <asp:HiddenField ID="hdnAccion" runat="server" />
        <asp:HiddenField ID="hdnIdPlantilla" runat="server" />
        <asp:Button ID="btnAccionOculta" runat="server" style="display: none;" OnClick="btnAccionOculta_Click" />

        <!-- Modal Nueva/Editar Plantilla -->
        <div class="modal fade" id="modalNuevaPlantilla" tabindex="-1">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalPlantillaLabel">Nueva Plantilla</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtNombrePlantilla">Nombre de la Plantilla *</asp:Label>
                                <asp:TextBox ID="txtNombrePlantilla" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombrePlantilla" 
                                    CssClass="text-danger" ErrorMessage="El nombre es requerido." ValidationGroup="GuardarPlantilla" />
                            </div>
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtDescripcionPlantilla">Descripcion</asp:Label>
                                <asp:TextBox ID="txtDescripcionPlantilla" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-12">
                                <h6>Servicios Disponibles</h6>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h6>Seleccionar Servicios</h6>
                                            </div>
                                            <div class="card-body" style="max-height: 400px; overflow-y: auto;">
                                                <asp:CheckBoxList ID="cblServicios" runat="server" CssClass="form-check" RepeatLayout="UnorderedList" 
                                                    RepeatDirection="Vertical" CellPadding="5">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card">
                                            <div class="card-header">
                                                <h6>Servicios Seleccionados (<span id="contadorServicios">0</span>)</h6>
                                            </div>
                                            <div class="card-body" style="max-height: 400px; overflow-y: auto;">
                                                <div id="serviciosSeleccionados" class="list-group">
                                                    <!-- Se llena dinámicamente con JavaScript -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnGuardarPlantilla" runat="server" Text="Guardar Plantilla" CssClass="btn btn-primary" 
                            OnClick="btnGuardarPlantilla_Click" ValidationGroup="GuardarPlantilla" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Ver Detalles Plantilla -->
        <div class="modal fade" id="modalVerPlantilla" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Detalles de la Plantilla</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <div id="detallesPlantilla">
                            <!-- Se llena dinámicamente -->
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function editarPlantilla(idPlantilla) {
            document.getElementById('<%= hdnAccion.ClientID %>').value = 'editar';
            document.getElementById('<%= hdnIdPlantilla.ClientID %>').value = idPlantilla;
            document.getElementById('<%= btnAccionOculta.ClientID %>').click();
        }

        function verPlantilla(idPlantilla) {
            document.getElementById('<%= hdnAccion.ClientID %>').value = 'ver';
            document.getElementById('<%= hdnIdPlantilla.ClientID %>').value = idPlantilla;
            document.getElementById('<%= btnAccionOculta.ClientID %>').click();
        }

        function eliminarPlantilla(idPlantilla) {
            if (confirm('¿Está seguro de que desea eliminar esta plantilla? Esta acción no se puede deshacer.')) {
                document.getElementById('<%= hdnAccion.ClientID %>').value = 'eliminar';
                document.getElementById('<%= hdnIdPlantilla.ClientID %>').value = idPlantilla;
                document.getElementById('<%= btnAccionOculta.ClientID %>').click();
            }
        }

        function actualizarContadorServicios() {
            var checkboxes = document.querySelectorAll('#<%= cblServicios.ClientID %> input[type="checkbox"]:checked');
            var contador = checkboxes.length;
            document.getElementById('contadorServicios').textContent = contador;
            
            var container = document.getElementById('serviciosSeleccionados');
            container.innerHTML = '';
            
            checkboxes.forEach(function(checkbox) {
                var label = checkbox.parentNode.querySelector('label').textContent;
                var item = document.createElement('div');
                item.className = 'list-group-item list-group-item-success';
                item.innerHTML = '<i class="fas fa-check"></i> ' + label;
                container.appendChild(item);
            });
        }

        function limpiarFormularioPlantilla() {
            document.getElementById('<%= txtNombrePlantilla.ClientID %>').value = '';
            document.getElementById('<%= txtDescripcionPlantilla.ClientID %>').value = '';
            var checkboxes = document.querySelectorAll('#<%= cblServicios.ClientID %> input[type="checkbox"]');
            checkboxes.forEach(function(checkbox) {
                checkbox.checked = false;
            });
            actualizarContadorServicios();
            document.querySelector('#modalPlantillaLabel').textContent = 'Nueva Plantilla';
        }

        document.addEventListener('DOMContentLoaded', function() {
            // Escuchar cambios en los checkboxes
            var checkboxes = document.querySelectorAll('#<%= cblServicios.ClientID %> input[type="checkbox"]');
            checkboxes.forEach(function(checkbox) {
                checkbox.addEventListener('change', actualizarContadorServicios);
            });

            // Limpiar modal al abrirlo para nueva plantilla
            var modal = document.getElementById('modalNuevaPlantilla');
            if (modal) {
                modal.addEventListener('show.bs.modal', function() {
                    if (document.getElementById('<%= hdnAccion.ClientID %>').value !== 'editar') {
                        limpiarFormularioPlantilla();
                    }
                });
            }
        });
    </script>
</asp:Content>