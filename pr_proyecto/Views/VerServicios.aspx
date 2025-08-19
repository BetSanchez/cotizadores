<%@ Page Title="Ver Servicios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerServicios.aspx.cs" Inherits="pr_proyecto.Views.VerServicios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fas fa-tools text-primary"></i> Servicios Disponibles</h2>
            <span class="badge bg-info fs-6">Total: <asp:Label ID="lblTotalServicios" runat="server" Text="0" /></span>
        </div>

        <!-- Búsqueda -->
        <div class="card mb-4">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-8">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre, descripcion, terminos..."></asp:TextBox>
                    </div>
                    <div class="col-md-4">
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
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10" 
                    OnPageIndexChanging="gvServicios_PageIndexChanging" ShowHeader="true"
                    EmptyDataText="No se encontraron servicios.">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <span class="badge bg-secondary"><%# Eval("IdServicio") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <strong class="text-primary"><%# Eval("Nombre") %></strong>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripcion">
                            <ItemTemplate>
                                <div style="max-width: 300px; word-wrap: break-word;">
                                    <%# Eval("Descripcion") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Terminos">
                            <ItemTemplate>
                                <div style="max-width: 250px; word-wrap: break-word; font-size: 0.9em;">
                                    <%# Eval("Terminos") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Incluye">
                            <ItemTemplate>
                                <div style="max-width: 250px; word-wrap: break-word; font-size: 0.9em;">
                                    <%# Eval("Incluye") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Condiciones">
                            <ItemTemplate>
                                <div style="max-width: 250px; word-wrap: break-word; font-size: 0.9em;">
                                    <%# Eval("Condiciones") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <button type="button" class="btn btn-outline-info btn-sm" 
                                    onclick='verDetalleServicio(<%# Eval("IdServicio") %>)'>
                                    <i class="fas fa-eye"></i> Ver Detalle
                                </button>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>

        <!-- Información adicional -->
        <div class="row mt-4">
            <div class="col-12">
                <div class="alert alert-info">
                    <i class="fas fa-info-circle"></i>
                    <strong>Informacion:</strong> Esta es la lista completa de servicios disponibles para cotizar. 
                    Para crear una cotizacion con estos servicios, utiliza la opcion "Crear Cotizacion" desde el dashboard.
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para Ver Detalle del Servicio -->
    <div class="modal fade" id="modalDetalleServicio" tabindex="-1" aria-labelledby="modalDetalleServicioLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalDetalleServicioLabel">Detalle del Servicio</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12">
                            <h6 class="text-primary">Nombre:</h6>
                            <p id="detalleNombre" class="fw-bold"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <h6 class="text-primary">Descripcion:</h6>
                            <p id="detalleDescripcion"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <h6 class="text-primary">Terminos:</h6>
                            <p id="detalleTerminos"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <h6 class="text-primary">Incluye:</h6>
                            <p id="detalleIncluye"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <h6 class="text-primary">Condiciones:</h6>
                            <p id="detalleCondiciones"></p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <a href="Crear_cotizacion.aspx" class="btn btn-success">
                        <i class="fas fa-plus"></i> Crear Cotizacion
                    </a>
                </div>
            </div>
        </div>
    </div>

    <!-- Campos ocultos para operaciones -->
    <asp:HiddenField ID="hdnIdServicio" runat="server" />
    <asp:Button ID="btnCargarDetalle" runat="server" style="display:none;" OnClick="btnCargarDetalle_Click" />

    <script type="text/javascript">
        // Variables globales para almacenar datos de servicios
        var serviciosData = [];

        // Función para actualizar datos de servicios desde el servidor
        function actualizarServiciosData(data) {
            serviciosData = data;
        }

        function verDetalleServicio(idServicio) {
            // Buscar el servicio en los datos locales
            var servicio = serviciosData.find(s => s.IdServicio == idServicio);
            
            if (servicio) {
                // Llenar el modal con los datos
                document.getElementById('detalleNombre').textContent = servicio.Nombre || 'N/A';
                document.getElementById('detalleDescripcion').textContent = servicio.Descripcion || 'No especificado';
                document.getElementById('detalleTerminos').textContent = servicio.Terminos || 'No especificado';
                document.getElementById('detalleIncluye').textContent = servicio.Incluye || 'No especificado';
                document.getElementById('detalleCondiciones').textContent = servicio.Condiciones || 'No especificado';
                
                // Mostrar modal
                var modal = new bootstrap.Modal(document.getElementById('modalDetalleServicio'));
                modal.show();
            } else {
                // Fallback: cargar desde servidor
                document.getElementById('<%= hdnIdServicio.ClientID %>').value = idServicio;
                document.getElementById('<%= btnCargarDetalle.ClientID %>').click();
            }
        }
    </script>
</asp:Content>