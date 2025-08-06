<%@ Page Title="Gestión de Servicios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionServicios.aspx.cs" Inherits="pr_proyecto.Views.GestionServicios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fas fa-cogs text-danger"></i> Gestión de Servicios</h2>
            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalNuevoServicio">
                <i class="fas fa-plus"></i> Nuevo Servicio
            </button>
        </div>

        <!-- Búsqueda -->
        <div class="card mb-4">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre o descripción..."></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                        <asp:Button ID="btnLimpiarBusqueda" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiarBusqueda_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- GridView de Servicios -->
        <div class="card">
            <div class="card-body">
                <asp:GridView ID="gvServicios" runat="server" CssClass="table table-striped table-hover" 
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="15" 
                    OnPageIndexChanging="gvServicios_PageIndexChanging" ShowHeader="true"
                    EmptyDataText="No se encontraron servicios.">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <%# Eval("IdServicio") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <strong><%# Eval("Nombre") %></strong>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripción">
                            <ItemTemplate>
                                <div style="max-width: 300px; overflow: hidden; text-overflow: ellipsis;">
                                    <%# Eval("Descripcion") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Términos">
                            <ItemTemplate>
                                <div style="max-width: 200px; overflow: hidden; text-overflow: ellipsis;">
                                    <%# Eval("Terminos") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Incluye">
                            <ItemTemplate>
                                <div style="max-width: 200px; overflow: hidden; text-overflow: ellipsis;">
                                    <%# Eval("Incluye") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Literal runat="server" Text='<%# GetBotonesAccion(Eval("IdServicio")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Campos ocultos para operaciones -->
        <asp:HiddenField ID="hdnAccion" runat="server" />
        <asp:HiddenField ID="hdnIdServicio" runat="server" />
        <asp:Button ID="btnAccionOculto" runat="server" style="display:none;" OnClick="btnAccionOculto_Click" />
    </div>

    <!-- Modal para Nuevo/Editar Servicio -->
    <div class="modal fade" id="modalNuevoServicio" tabindex="-1" aria-labelledby="modalNuevoServicioLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalNuevoServicioLabel">Nuevo Servicio</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group mb-3">
                                <asp:Label runat="server" AssociatedControlID="txtNombreServicio">Nombre del Servicio *</asp:Label>
                                <asp:TextBox ID="txtNombreServicio" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombreServicio" 
                                    CssClass="text-danger" ErrorMessage="El nombre es requerido." ValidationGroup="GuardarServicio" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group mb-3">
                                <asp:Label runat="server" AssociatedControlID="txtDescripcionServicio">Descripción</asp:Label>
                                <asp:TextBox ID="txtDescripcionServicio" runat="server" CssClass="form-control" 
                                    TextMode="MultiLine" Rows="3" MaxLength="500"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group mb-3">
                                <asp:Label runat="server" AssociatedControlID="txtTerminosServicio">Términos</asp:Label>
                                <asp:TextBox ID="txtTerminosServicio" runat="server" CssClass="form-control" 
                                    TextMode="MultiLine" Rows="3" MaxLength="1000"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group mb-3">
                                <asp:Label runat="server" AssociatedControlID="txtIncluyeServicio">Incluye</asp:Label>
                                <asp:TextBox ID="txtIncluyeServicio" runat="server" CssClass="form-control" 
                                    TextMode="MultiLine" Rows="3" MaxLength="1000"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group mb-3">
                                <asp:Label runat="server" AssociatedControlID="txtCondicionesServicio">Condiciones</asp:Label>
                                <asp:TextBox ID="txtCondicionesServicio" runat="server" CssClass="form-control" 
                                    TextMode="MultiLine" Rows="3" MaxLength="1000"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarServicio" runat="server" Text="Guardar" CssClass="btn btn-primary" 
                        OnClick="btnGuardarServicio_Click" ValidationGroup="GuardarServicio" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function editarServicio(idServicio) {
            // Configurar para edición
            document.getElementById('<%= hdnAccion.ClientID %>').value = 'cargar_editar';
            document.getElementById('<%= hdnIdServicio.ClientID %>').value = idServicio;
            
            // Trigger del botón oculto
            document.getElementById('<%= btnAccionOculto.ClientID %>').click();
        }

        function eliminarServicio(idServicio, nombreServicio) {
            if (confirm('¿Está seguro de que desea eliminar el servicio "' + nombreServicio + '"?\\n\\nEsta acción no se puede deshacer.')) {
                // Configurar para eliminación
                document.getElementById('<%= hdnAccion.ClientID %>').value = 'eliminar';
                document.getElementById('<%= hdnIdServicio.ClientID %>').value = idServicio;
                
                // Trigger del botón oculto
                document.getElementById('<%= btnAccionOculto.ClientID %>').click();
            }
        }

        function limpiarModal() {
            document.getElementById('<%= txtNombreServicio.ClientID %>').value = '';
            document.getElementById('<%= txtDescripcionServicio.ClientID %>').value = '';
            document.getElementById('<%= txtTerminosServicio.ClientID %>').value = '';
            document.getElementById('<%= txtIncluyeServicio.ClientID %>').value = '';
            document.getElementById('<%= txtCondicionesServicio.ClientID %>').value = '';
            document.getElementById('<%= hdnIdServicio.ClientID %>').value = '';
            document.querySelector('#modalNuevoServicioLabel').textContent = 'Nuevo Servicio';
        }

        // Limpiar modal al abrirlo para nuevo servicio
        document.addEventListener('DOMContentLoaded', function() {
            var modal = document.getElementById('modalNuevoServicio');
            if (modal) {
                modal.addEventListener('show.bs.modal', function(event) {
                    // Solo limpiar si no es una edición
                    if (document.getElementById('<%= hdnAccion.ClientID %>').value !== 'editar') {
                        limpiarModal();
                    }
                });
            }
        });
    </script>
</asp:Content>