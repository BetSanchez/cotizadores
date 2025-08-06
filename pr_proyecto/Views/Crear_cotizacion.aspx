<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crear_cotizacion.aspx.cs" Inherits="pr_proyecto.Views.Crear_cotizacion" MasterPageFile="~/Site.Master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Crear Nueva Cotización</h2>
        
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtFolio">Folio</asp:Label>
                            <asp:TextBox ID="txtFolio" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        
                        <div class="form-group mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtFechaEmision">Fecha de Emisión</asp:Label>
                            <asp:TextBox ID="txtFechaEmision" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFechaEmision" 
                                CssClass="text-danger" ErrorMessage="La fecha de emisión es requerida." ValidationGroup="GuardarCotizacion" />
                        </div>
                        
                        <div class="form-group mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtFechaVencimiento">Fecha de Vencimiento</asp:Label>
                            <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFechaVencimiento" 
                                CssClass="text-danger" ErrorMessage="La fecha de vencimiento es requerida." ValidationGroup="GuardarCotizacion" />
                        </div>
                    </div>
                    
                    <div class="col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label runat="server" AssociatedControlID="ddlSucursal">Sucursal</asp:Label>
                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSucursal" 
                                CssClass="text-danger" ErrorMessage="La sucursal es requerida." ValidationGroup="GuardarCotizacion" />
                        </div>
                        
                        <div class="form-group mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtNota">Nota</asp:Label>
                            <asp:TextBox ID="txtNota" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="card mt-4">
            <div class="card-body">
                <h4>Servicios</h4>
                <div class="row mb-3">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlServicios" runat="server" CssClass="form-control" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlServicios_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="mb-3">
                    <button type="button" class="btn btn-success" onclick="mostrarModalServicio()">
                        <i class="fas fa-plus"></i> Agregar Servicio
                    </button>
                    <asp:Button ID="btnDebug" runat="server" Text="Debug Info" CssClass="btn btn-warning ms-2" 
                        OnClick="btnDebug_Click" CausesValidation="false" />
                    <asp:Button ID="btnTestAgregar" runat="server" Text="Test Agregar" CssClass="btn btn-info ms-2" 
                        OnClick="btnTestAgregar_Click" CausesValidation="false" />
                </div>

                <asp:GridView ID="gvServicios" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                    ShowHeader="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Servicio">
                            <ItemTemplate>
                                <strong><%# Eval("Servicio.Nombre") %></strong>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripción">
                            <ItemTemplate>
                                <div style="max-width: 200px; overflow: hidden; text-overflow: ellipsis;">
                                    <%# Eval("Descripcion") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <%# Eval("Cantidad") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Unitario">
                            <ItemTemplate>
                                <%# Eval("ValorUnitario", "{0:C}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subtotal">
                            <ItemTemplate>
                                <%# Eval("Subtotal", "{0:C}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Términos">
                            <ItemTemplate>
                                <div style="max-width: 150px; overflow: hidden; text-overflow: ellipsis;">
                                    <%# Eval("Terminos") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Incluye">
                            <ItemTemplate>
                                <div style="max-width: 150px; overflow: hidden; text-overflow: ellipsis;">
                                    <%# Eval("Incluye") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Condiciones">
                            <ItemTemplate>
                                <div style="max-width: 150px; overflow: hidden; text-overflow: ellipsis;">
                                    <%# Eval("Condiciones") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Literal runat="server" Text='<%# GetBotonesAccion(Container.DataItemIndex) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div class="row mt-3">
                    <div class="col-md-6">
                        <div class="form-group mb-3">
                            <asp:Label runat="server" AssociatedControlID="txtComentario">Comentario</asp:Label>
                            <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="text-end">
                            <h5>Subtotal: <asp:Label ID="lblSubtotal" runat="server" Text="$0.00"></asp:Label></h5>
                            <h5>IVA: <asp:Label ID="lblIVA" runat="server" Text="$0.00"></asp:Label></h5>
                            <h4>Total: <asp:Label ID="lblTotal" runat="server" Text="$0.00"></asp:Label></h4>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="text-end mt-4">
            <asp:Button ID="btnVistaPrevia" runat="server" Text="Vista Previa" CssClass="btn btn-info me-2" 
                OnClick="btnVistaPrevia_Click" CausesValidation="false" />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cotización" CssClass="btn btn-primary" 
                OnClick="btnGuardar_Click" ValidationGroup="GuardarCotizacion" />
        </div>

        <!-- Campos ocultos para las operaciones con JavaScript -->
        <asp:HiddenField ID="hdnAccion" runat="server" />
        <asp:HiddenField ID="hdnIndiceServicio" runat="server" />
        <asp:Button ID="btnAccionOculto" runat="server" style="display:none;" OnClick="btnAccionOculto_Click" />
    </div>

    <!-- Modal para agregar servicio -->
    <div class="modal fade" id="modalServicio" tabindex="-1" aria-labelledby="modalServicioLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalServicioLabel">Agregar Servicio</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <!-- Información del servicio seleccionado -->
                    <div class="alert alert-info mb-3">
                        <h6>Información del Servicio:</h6>
                        <div id="servicioInfo">
                            <strong>Nombre:</strong> <span id="lblNombreServicio"></span><br />
                            <strong>Descripción Base:</strong> <span id="lblDescripcionServicio"></span><br />
                            <strong>Términos Base:</strong> <span id="lblTerminosServicio"></span><br />
                            <strong>Incluye Base:</strong> <span id="lblIncluyeServicio"></span><br />
                            <strong>Condiciones Base:</strong> <span id="lblCondicionesServicio"></span>
                        </div>
                    </div>

                    <!-- Campos editables para cotizacion_servicios -->
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtCantidad">Cantidad</asp:Label>
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number" Min="1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCantidad" 
                            CssClass="text-danger" ErrorMessage="La cantidad es requerida." ValidationGroup="AgregarServicio" />
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtValorUnitario">Valor Unitario</asp:Label>
                        <asp:TextBox ID="txtValorUnitario" runat="server" CssClass="form-control" TextMode="Number" Step="0.01" Min="0"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtValorUnitario" 
                            CssClass="text-danger" ErrorMessage="El valor unitario es requerido." ValidationGroup="AgregarServicio" />
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtDescripcionServicio">Descripción Personalizada</asp:Label>
                        <asp:TextBox ID="txtDescripcionServicio" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" 
                            placeholder="Descripción personalizada para esta cotización"></asp:TextBox>
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtTerminos">Términos Personalizados</asp:Label>
                        <asp:TextBox ID="txtTerminos" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" 
                            placeholder="Términos específicos para esta cotización"></asp:TextBox>
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtIncluye">Incluye Personalizado</asp:Label>
                        <asp:TextBox ID="txtIncluye" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" 
                            placeholder="Qué incluye específicamente en esta cotización"></asp:TextBox>
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtCondiciones">Condiciones Personalizadas</asp:Label>
                        <asp:TextBox ID="txtCondiciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" 
                            placeholder="Condiciones específicas para esta cotización"></asp:TextBox>
                    </div>
                    
                    <div class="alert alert-warning">
                        <small><i class="fas fa-info-circle"></i> Los campos personalizados sobrescribirán la información base del servicio. Déjalos vacíos para usar la información original.</small>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnAgregarServicio" runat="server" Text="Agregar" CssClass="btn btn-primary" 
                        OnClick="btnAgregarServicio_Click" ValidationGroup="AgregarServicio" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para editar servicio -->
    <div class="modal fade" id="modalEditarServicio" tabindex="-1" aria-labelledby="modalEditarServicioLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEditarServicioLabel">Editar Servicio</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <!-- Información del servicio seleccionado -->
                    <div class="alert alert-info mb-3">
                        <h6>Información del Servicio:</h6>
                        <div id="servicioInfoEditar">
                            <strong>Nombre:</strong> <span id="lblNombreServicioEditar"></span><br />
                            <strong>Descripción Base:</strong> <span id="lblDescripcionServicioEditar"></span><br />
                            <strong>Términos Base:</strong> <span id="lblTerminosServicioEditar"></span><br />
                            <strong>Incluye Base:</strong> <span id="lblIncluyeServicioEditar"></span><br />
                            <strong>Condiciones Base:</strong> <span id="lblCondicionesServicioEditar"></span>
                        </div>
                    </div>

                    <!-- Campos editables para cotizacion_servicios -->
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtCantidadEditar">Cantidad</asp:Label>
                        <asp:TextBox ID="txtCantidadEditar" runat="server" CssClass="form-control" TextMode="Number" Min="1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCantidadEditar" 
                            CssClass="text-danger" ErrorMessage="La cantidad es requerida." ValidationGroup="EditarServicio" />
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtValorUnitarioEditar">Valor Unitario</asp:Label>
                        <asp:TextBox ID="txtValorUnitarioEditar" runat="server" CssClass="form-control" TextMode="Number" Step="0.01" Min="0"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtValorUnitarioEditar" 
                            CssClass="text-danger" ErrorMessage="El valor unitario es requerido." ValidationGroup="EditarServicio" />
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtDescripcionServicioEditar">Descripción Personalizada</asp:Label>
                        <asp:TextBox ID="txtDescripcionServicioEditar" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" 
                            placeholder="Descripción personalizada para esta cotización"></asp:TextBox>
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtTerminosEditar">Términos Personalizados</asp:Label>
                        <asp:TextBox ID="txtTerminosEditar" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" 
                            placeholder="Términos específicos para esta cotización"></asp:TextBox>
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtIncluyeEditar">Incluye Personalizado</asp:Label>
                        <asp:TextBox ID="txtIncluyeEditar" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" 
                            placeholder="Qué incluye específicamente en esta cotización"></asp:TextBox>
                    </div>
                    
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtCondicionesEditar">Condiciones Personalizadas</asp:Label>
                        <asp:TextBox ID="txtCondicionesEditar" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" 
                            placeholder="Condiciones específicas para esta cotización"></asp:TextBox>
                    </div>
                    
                    <div class="alert alert-warning">
                        <small><i class="fas fa-info-circle"></i> Los campos personalizados sobrescribirán la información base del servicio. Déjalos vacíos para usar la información original.</small>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnActualizarServicio" runat="server" Text="Actualizar" CssClass="btn btn-primary" 
                        OnClick="btnActualizarServicio_Click" ValidationGroup="EditarServicio" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para vista previa de la cotización -->
    <div class="modal fade" id="modalVistaPrevia" tabindex="-1" role="dialog" aria-labelledby="modalVistaPreviaLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalVistaPreviaLabel">Vista Previa de la Cotización</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <h6>Información General:</h6>
                            <table class="table table-sm">
                                <tr><td><strong>Folio:</strong></td><td id="previewFolio"></td></tr>
                                <tr><td><strong>Fecha Emisión:</strong></td><td id="previewFechaEmision"></td></tr>
                                <tr><td><strong>Fecha Vencimiento:</strong></td><td id="previewFechaVencimiento"></td></tr>
                                <tr><td><strong>Sucursal:</strong></td><td id="previewSucursal"></td></tr>
                            </table>
                        </div>
                        <div class="col-md-6">
                            <h6>Totales:</h6>
                            <table class="table table-sm">
                                <tr><td><strong>Subtotal:</strong></td><td id="previewSubtotal"></td></tr>
                                <tr><td><strong>IVA:</strong></td><td id="previewIVA"></td></tr>
                                <tr><td><strong>Total:</strong></td><td id="previewTotal"></td></tr>
                            </table>
                        </div>
                    </div>
                    
                    <div class="row mb-3">
                        <div class="col-12">
                            <h6>Nota:</h6>
                            <p id="previewNota" class="text-muted"></p>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <h6>Servicios:</h6>
                            <div id="previewServicios"></div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <h6>Comentario:</h6>
                            <p id="previewComentario" class="text-muted"></p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        // Función para escapar caracteres especiales en JavaScript
        function escapeJavaScript(text) {
            if (!text) return '';
            return text.replace(/'/g, "\\'")
                .replace(/"/g, '\\"')
                .replace(/\n/g, '\\n')
                .replace(/\r/g, '\\r');
        }

        // Función para mostrar detalles del servicio
        function mostrarDetallesServicio(descripcion, terminos, incluye, condiciones) {
            document.getElementById('detalleDescripcion').textContent = descripcion || 'No especificado';
            document.getElementById('detalleTerminos').textContent = terminos || 'No especificado';
            document.getElementById('detalleIncluye').textContent = incluye || 'No especificado';
            document.getElementById('detalleCondiciones').textContent = condiciones || 'No especificado';
        }

        // Función para cerrar modales
        function cerrarModal(modalId) {
            var modal = document.getElementById(modalId);
            if (modal) {
                var bootstrapModal = bootstrap.Modal.getInstance(modal);
                if (bootstrapModal) {
                    bootstrapModal.hide();
                }
            }
            return false; // Prevenir submit
        }

        // Función para mostrar modal de editar
        function mostrarModalEditar() {
            var modal = document.getElementById('modalEditarServicio');
            if (modal) {
                var bootstrapModal = new bootstrap.Modal(modal);
                bootstrapModal.show();
            }
        }

        // Función para verificar si Bootstrap está disponible
        function verificarBootstrap() {
            if (typeof bootstrap === 'undefined') {
                console.error('Bootstrap no está disponible');
                return false;
            }
            return true;
        }

        // Función para mostrar modal de servicio
        function mostrarModalServicio() {
            // Verificar que se haya seleccionado un servicio
            var ddlServicios = document.getElementById('<%= ddlServicios.ClientID %>');
            if (!ddlServicios || ddlServicios.value === '') {
                alert('Debe seleccionar un servicio primero.');
                return;
            }
            
            if (!verificarBootstrap()) {
                alert('Error: Bootstrap no está disponible');
                return;
            }
            
            var modal = document.getElementById('modalServicio');
            if (modal && typeof bootstrap !== 'undefined') {
                var bootstrapModal = new bootstrap.Modal(modal);
                bootstrapModal.show();
            } else if (modal) {
                // Fallback: usar jQuery si Bootstrap no está disponible
                if (typeof $ !== 'undefined') {
                    $(modal).modal('show');
                } else {
                    // Último recurso: mostrar manualmente
                    modal.classList.add('show');
                    modal.style.display = 'block';
                    document.body.classList.add('modal-open');
                    // Crear backdrop
                    var backdrop = document.createElement('div');
                    backdrop.className = 'modal-backdrop fade show';
                    document.body.appendChild(backdrop);
                }
            } else {
                console.error('Modal no encontrado');
            }
        }

        // Inicializar Bootstrap cuando el DOM esté listo
        document.addEventListener('DOMContentLoaded', function () {
            console.log('DOM cargado, inicializando Bootstrap...');
            
            // Verificar si Bootstrap está disponible
            if (typeof bootstrap !== 'undefined') {
                console.log('Bootstrap disponible');
                // Inicializar todos los modales
                var modalElements = document.querySelectorAll('.modal');
                modalElements.forEach(function (element) {
                    new bootstrap.Modal(element);
                });
            } else {
                console.error('Bootstrap no está disponible');
            }
        });

        // Función para manejar errores de JavaScript
        window.onerror = function(msg, url, lineNo, columnNo, error) {
            console.error('Error JavaScript:', msg, 'en', url, 'línea', lineNo);
            alert('Error JavaScript: ' + msg + ' en línea ' + lineNo);
            return false;
        };

        // Variables globales para almacenar los servicios
        var serviciosData = [];

        // Función para actualizar los datos de servicios desde el servidor
        function actualizarServiciosData(data) {
            serviciosData = data;
        }

        // Función para editar servicio
        function editarServicio(indice) {
            try {
                console.log('Editando servicio en índice:', indice);
                console.log('Datos de servicios:', serviciosData);
                
                if (!serviciosData || serviciosData.length <= indice) {
                    alert('Error: No se encontraron datos del servicio.');
                    return;
                }

                var servicio = serviciosData[indice];
                console.log('Servicio a editar:', servicio);

                // Llenar los campos de la modal de editar
                document.getElementById('lblNombreServicioEditar').textContent = servicio.NombreServicio || '';
                document.getElementById('lblDescripcionServicioEditar').textContent = servicio.DescripcionBase || '';
                document.getElementById('lblTerminosServicioEditar').textContent = servicio.TerminosBase || '';
                document.getElementById('lblIncluyeServicioEditar').textContent = servicio.IncluyeBase || '';
                document.getElementById('lblCondicionesServicioEditar').textContent = servicio.CondicionesBase || '';

                // Llenar los campos editables
                document.getElementById('<%= txtCantidadEditar.ClientID %>').value = servicio.Cantidad || '';
                document.getElementById('<%= txtValorUnitarioEditar.ClientID %>').value = servicio.ValorUnitario || '';
                document.getElementById('<%= txtDescripcionServicioEditar.ClientID %>').value = servicio.Descripcion || '';
                document.getElementById('<%= txtTerminosEditar.ClientID %>').value = servicio.Terminos || '';
                document.getElementById('<%= txtIncluyeEditar.ClientID %>').value = servicio.Incluye || '';
                document.getElementById('<%= txtCondicionesEditar.ClientID %>').value = servicio.Condiciones || '';

                // Guardar el índice para usar en el botón actualizar
                document.getElementById('<%= hdnIndiceServicio.ClientID %>').value = indice;

                // Mostrar la modal
                var modal = document.getElementById('modalEditarServicio');
                if (modal) {
                    if (typeof bootstrap !== 'undefined') {
                        var bootstrapModal = new bootstrap.Modal(modal);
                        bootstrapModal.show();
                    } else if (typeof $ !== 'undefined') {
                        $(modal).modal('show');
                    } else {
                        modal.style.display = 'block';
                        modal.classList.add('show');
                    }
                }
            } catch (error) {
                console.error('Error al editar servicio:', error);
                alert('Error al editar servicio: ' + error.message);
            }
        }

        // Función para eliminar servicio
        function eliminarServicio(indice) {
            try {
                if (!serviciosData || serviciosData.length <= indice) {
                    alert('Error: No se encontraron datos del servicio.');
                    return;
                }

                var servicio = serviciosData[indice];
                var nombreServicio = servicio.NombreServicio || 'Servicio';
                
                if (confirm('¿Está seguro de que desea eliminar el servicio "' + nombreServicio + '"?')) {
                    // Configurar la acción y el índice
                    document.getElementById('<%= hdnAccion.ClientID %>').value = 'eliminar';
                    document.getElementById('<%= hdnIndiceServicio.ClientID %>').value = indice;
                    
                    // Trigger del botón oculto
                    document.getElementById('<%= btnAccionOculto.ClientID %>').click();
                }
            } catch (error) {
                console.error('Error al eliminar servicio:', error);
                alert('Error al eliminar servicio: ' + error.message);
            }
        }
    </script>
</asp:Content>