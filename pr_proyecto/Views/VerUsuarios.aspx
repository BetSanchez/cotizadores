<%@ Page Title="Gestion de Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerUsuarios.aspx.cs" Inherits="pr_proyecto.Views.VerUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fas fa-users text-primary"></i> Gestion de Usuarios</h2>
            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalNuevoUsuario">
                <i class="fas fa-plus"></i> Nuevo Usuario
            </button>
        </div>

        <!-- Filtros -->
        <div class="card mb-4">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtBuscarUsuario" runat="server" CssClass="form-control" placeholder="Buscar usuario por nombre o email..."></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnBuscarUsuario" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarUsuario_Click" />
                        <asp:Button ID="btnLimpiarUsuario" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiarUsuario_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-body">
                <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-striped table-hover"
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                    OnPageIndexChanging="gvUsuarios_PageIndexChanging" ShowHeader="true"
                    EmptyDataText="No se encontraron usuarios.">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <span class="badge bg-secondary"><%# Eval("IdUsuario") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <strong><%# Eval("Nombre") %></strong>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <%# Eval("Email") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rol">
                            <ItemTemplate>
                                <span class="badge bg-info"><%# Eval("Rol") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class="badge <%# (bool)Eval("Activo") ? "bg-success" : "bg-danger" %>">
                                    <%# (bool)Eval("Activo") ? "Activo" : "Baja" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Literal runat="server" Text='<%# GetBotonesUsuario(Eval("IdUsuario"), (bool)Eval("Activo")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Campos ocultos para operaciones -->
        <asp:HiddenField ID="hdnAccion" runat="server" />
        <asp:HiddenField ID="hdnIdUsuario" runat="server" />
        <asp:Button ID="btnAccionOculto" runat="server" style="display:none;" OnClick="btnAccionOculto_Click" />
    </div>

    <!-- Modal Nuevo/Editar Usuario -->
    <div class="modal fade" id="modalNuevoUsuario" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalUsuarioLabel">Nuevo Usuario</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtNombreUsuario">Nombre *</asp:Label>
                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombreUsuario"
                                CssClass="text-danger" ErrorMessage="El nombre es requerido." ValidationGroup="GuardarUsuario" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtEmailUsuario">Email *</asp:Label>
                            <asp:TextBox ID="txtEmailUsuario" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmailUsuario"
                                CssClass="text-danger" ErrorMessage="El email es requerido." ValidationGroup="GuardarUsuario" />
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="ddlRolUsuario">Rol *</asp:Label>
                            <asp:DropDownList ID="ddlRolUsuario" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">Seleccione un rol</asp:ListItem>
                                <asp:ListItem Value="Administrador">Administrador</asp:ListItem>
                                <asp:ListItem Value="Administrador">Administrador</asp:ListItem>
                                <asp:ListItem Value="Cliente">Cliente</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRolUsuario"
                                CssClass="text-danger" ErrorMessage="Debe seleccionar un rol." ValidationGroup="GuardarUsuario" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="chkActivo">Activo</asp:Label>
                            <asp:CheckBox ID="chkActivo" runat="server" Checked="true" CssClass="form-check-input" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" CssClass="btn btn-primary"
                        OnClick="btnGuardarUsuario_Click" ValidationGroup="GuardarUsuario" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function editarUsuario(idUsuario) {
            document.getElementById('<%= hdnAccion.ClientID %>').value = 'cargar_editar_usuario';
            document.getElementById('<%= hdnIdUsuario.ClientID %>').value = idUsuario;
            document.getElementById('<%= btnAccionOculto.ClientID %>').click();
        }
        function cambiarEstadoUsuario(idUsuario, nombreUsuario, accion) {
            let mensaje = accion === 'baja' ?
                `¿Está seguro de que desea dar de baja al usuario "${nombreUsuario}"?` :
                `¿Está seguro de que desea dar de alta al usuario "${nombreUsuario}"?`;
            if (confirm(mensaje)) {
                document.getElementById('<%= hdnAccion.ClientID %>').value = accion === 'baja' ? 'baja_usuario' : 'alta_usuario';
                document.getElementById('<%= hdnIdUsuario.ClientID %>').value = idUsuario;
                document.getElementById('<%= btnAccionOculto.ClientID %>').click();
            }
        }
    </script>
</asp:Content>
