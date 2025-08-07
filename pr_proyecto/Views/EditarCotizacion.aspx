<%@ Page Title="Editar Cotización" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarCotizacion.aspx.cs" Inherits="pr_proyecto.Views.EditarCotizacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fas fa-edit text-warning"></i> Editar Cotización</h2>
            <div>
                <a href="VerCotizaciones.aspx" class="btn btn-secondary">
                    <i class="fas fa-arrow-left"></i> Volver a Lista
                </a>
                <a href="Crear_cotizacion.aspx" class="btn btn-success">
                    <i class="fas fa-plus"></i> Nueva Cotización
                </a>
            </div>
        </div>

        <!-- Información de la Cotización -->
        <div class="row">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header">
                        <h5><i class="fas fa-file-invoice"></i> Información General</h5>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label for="txtFolio">Folio *</label>
                                    <asp:TextBox ID="txtFolio" runat="server" CssClass="form-control" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label for="ddlEstatus">Estado *</label>
                                    <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEstatus_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="P" Text="Pendiente" />
                                        <asp:ListItem Value="A" Text="Aceptada" />
                                        <asp:ListItem Value="C" Text="Cancelada" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label for="txtFechaEmision">Fecha de Emisión *</label>
                                    <asp:TextBox ID="txtFechaEmision" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFechaEmision" 
                                        CssClass="text-danger" ErrorMessage="La fecha de emisión es requerida." ValidationGroup="GuardarCotizacion" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label for="txtFechaVencimiento">Fecha de Vencimiento *</label>
                                    <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFechaVencimiento" 
                                        CssClass="text-danger" ErrorMessage="La fecha de vencimiento es requerida." ValidationGroup="GuardarCotizacion" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label for="ddlSucursal">Sucursal *</label>
                                    <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">Seleccione una sucursal</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSucursal" 
                                        CssClass="text-danger" ErrorMessage="Debe seleccionar una sucursal." ValidationGroup="GuardarCotizacion" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label for="txtVendedor">Vendedor</label>
                                    <asp:TextBox ID="txtVendedor" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group mb-3">
                                    <label for="txtNota">Nota</label>
                                    <asp:TextBox ID="txtNota" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="1000"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group mb-3">
                                    <label for="txtComentario">Comentario de Estado</label>
                                    <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="500"></asp:TextBox>
                                    <small class="form-text text-muted">Agregue comentarios sobre cambios de estado o actualizaciones.</small>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Panel de Estado -->
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header">
                        <h5><i class="fas fa-info-circle"></i> Estado Actual</h5>
                    </div>
                    <div class="card-body text-center">
                        <div class="mb-3">
                            <asp:Label ID="lblEstadoActual" runat="server" CssClass="badge fs-6 p-3" Text="Pendiente"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <small class="text-muted">Última actualización:</small><br>
                            <asp:Label ID="lblFechaActualizacion" runat="server" CssClass="fw-bold"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <small class="text-muted">Versión:</small><br>
                            <asp:Label ID="lblVersion" runat="server" CssClass="fw-bold"></asp:Label>
                        </div>
                    </div>
                </div>

                <!-- Panel de Totales -->
                <div class="card mt-3">
                    <div class="card-header">
                        <h5><i class="fas fa-calculator"></i> Totales</h5>
                    </div>
                    <div class="card-body">
                        <div class="d-flex justify-content-between">
                            <span>Subtotal:</span>
                            <asp:Label ID="lblSubtotal" runat="server" CssClass="fw-bold"></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span>IVA (16%):</span>
                            <asp:Label ID="lblIva" runat="server" CssClass="fw-bold"></asp:Label>
                        </div>
                        <hr>
                        <div class="d-flex justify-content-between">
                            <span><strong>Total:</strong></span>
                            <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold text-success fs-5"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Servicios de la Cotización -->
        <div class="card mt-4">
            <div class="card-header d-flex justify-content-between align-items-center">
                <h5><i class="fas fa-list"></i> Servicios Cotizados</h5>
                <span class="badge bg-info">Total: <asp:Label ID="lblTotalServicios" runat="server" Text="0"></asp:Label></span>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvServicios" runat="server" CssClass="table table-striped table-hover" 
                    AutoGenerateColumns="false" ShowHeader="true" EmptyDataText="No hay servicios en esta cotización.">
                    <Columns>
                        <asp:TemplateField HeaderText="Servicio">
                            <ItemTemplate>
                                <div>
                                    <strong><%# Eval("Descripcion") %></strong>
                                </div>
                                <small class="text-muted">ID Servicio: <%# Eval("IdServicio") %></small>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <span class="badge bg-secondary"><%# Eval("Cantidad") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Unitario">
                            <ItemTemplate>
                                <%# Eval("ValorUnitario", "{0:C}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subtotal">
                            <ItemTemplate>
                                <strong><%# Eval("Subtotal", "{0:C}") %></strong>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Términos">
                            <ItemTemplate>
                                <div style="max-width: 200px; overflow: hidden; text-overflow: ellipsis;">
                                    <%# Eval("Terminos") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Botones de Acción -->
        <div class="card mt-4">
            <div class="card-body">
                <div class="d-flex justify-content-between">
                    <div>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary btn-lg" 
                            OnClick="btnGuardar_Click" ValidationGroup="GuardarCotizacion" />
                        <button type="button" class="btn btn-info btn-lg" onclick="previewCotizacion()">
                            <i class="fas fa-eye"></i> Vista Previa
                        </button>
                    </div>
                    <div>
                        <asp:Button ID="btnMarcarPendiente" runat="server" Text="Marcar Pendiente" CssClass="btn btn-warning" 
                            OnClick="btnCambiarEstado_Click" CommandArgument="P" />
                        <asp:Button ID="btnMarcarAceptada" runat="server" Text="Marcar Aceptada" CssClass="btn btn-success" 
                            OnClick="btnCambiarEstado_Click" CommandArgument="A" />
                        <asp:Button ID="btnMarcarCancelada" runat="server" Text="Marcar Cancelada" CssClass="btn btn-danger" 
                            OnClick="btnCambiarEstado_Click" CommandArgument="C" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Historial de Cambios -->
        <asp:Panel ID="panelHistorial" runat="server" Visible="false">
            <div class="card mt-4">
                <div class="card-header">
                    <h5><i class="fas fa-history"></i> Historial de Cambios</h5>
                </div>
                <div class="card-body">
                    <asp:Repeater ID="rptHistorial" runat="server">
                        <ItemTemplate>
                            <div class="d-flex justify-content-between align-items-center border-bottom py-2">
                                <div>
                                    <strong><%# Eval("Accion") %></strong> - <%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %>
                                    <br><small class="text-muted"><%# Eval("Comentario") %></small>
                                </div>
                                <span class="badge bg-<%# GetHistorialBadgeClass(Eval("EstadoAnterior")) %>">
                                    <%# Eval("EstadoAnterior") %> → <%# Eval("EstadoNuevo") %>
                                </span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </asp:Panel>
    </div>

    <script type="text/javascript">
        function previewCotizacion() {
            // Aquí se podría implementar una vista previa de la cotización
            alert('Vista previa de cotización - Funcionalidad por implementar');
        }

        function confirmarCambioEstado(nuevoEstado, estadoTexto) {
            return confirm('¿Está seguro de que desea cambiar el estado de la cotización a "' + estadoTexto + '"?\\n\\nEste cambio se registrará en el historial.');
        }

        // Actualizar apariencia del badge según el estado
        function actualizarEstadoBadge(estado) {
            var lblEstado = document.getElementById('<%= lblEstadoActual.ClientID %>');
            if (lblEstado) {
                lblEstado.className = 'badge fs-6 p-3 ';
                switch(estado) {
                    case 'P':
                        lblEstado.className += 'bg-warning text-dark';
                        lblEstado.textContent = 'Pendiente';
                        break;
                    case 'A':
                        lblEstado.className += 'bg-success';
                        lblEstado.textContent = 'Aceptada';
                        break;
                    case 'C':
                        lblEstado.className += 'bg-danger';
                        lblEstado.textContent = 'Cancelada';
                        break;
                    default:
                        lblEstado.className += 'bg-secondary';
                        lblEstado.textContent = 'Desconocido';
                }
            }
        }

        // Actualizar fecha de modificación
        function actualizarFechaModificacion() {
            var lblFecha = document.getElementById('<%= lblFechaActualizacion.ClientID %>');
            if (lblFecha) {
                var ahora = new Date();
                lblFecha.textContent = ahora.toLocaleDateString() + ' ' + ahora.toLocaleTimeString();
            }
        }
    </script>
</asp:Content>