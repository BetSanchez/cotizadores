<%@ Page Title="Ver Cotizaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerCotizaciones.aspx.cs" Inherits="pr_proyecto.Views.VerCotizaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fas fa-list text-primary"></i> Mis Cotizaciones</h2>
            <a href="Crear_cotizacion.aspx" class="btn btn-success">
                <i class="fas fa-plus"></i> Nueva Cotizacion
            </a>
        </div>

        <!-- Filtros -->
        <div class="card mb-4">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-3">
                        <label for="ddlEstatus">Estado:</label>
                        <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEstatus_SelectedIndexChanged">
                            <asp:ListItem Value="">Todos</asp:ListItem>
                            <asp:ListItem Value="A">Activa</asp:ListItem>
                            <asp:ListItem Value="C">Cancelada</asp:ListItem>
                            <asp:ListItem Value="P">Pendiente</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label for="txtFechaDesde">Desde:</label>
                        <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label for="txtFechaHasta">Hasta:</label>
                        <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>&nbsp;</label>
                        <div>
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- GridView de Cotizaciones -->
        <div class="card">
            <div class="card-body">
                <asp:GridView ID="gvCotizaciones" runat="server" CssClass="table table-striped table-hover" 
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10" 
                    OnPageIndexChanging="gvCotizaciones_PageIndexChanging" ShowHeader="true"
                    EmptyDataText="No se encontraron cotizaciones.">
                    <Columns>
                        <asp:TemplateField HeaderText="Folio">
                            <ItemTemplate>
                                <strong><%# Eval("Folio") %></strong>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Emision">
                            <ItemTemplate>
                                <%# Eval("FechaEmision", "{0:dd/MM/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Vencimiento">
                            <ItemTemplate>
                                <%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sucursal">
                            <ItemTemplate>
                                <%# Eval("Sucursal.NomComercial") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <%# Eval("Total", "{0:C}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class="badge bg-<%# GetEstadoBadgeClass(Eval("Estatus")) %>">
                                    <%# GetEstadoTexto(Eval("Estatus")) %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <a href='<%# "EditarCotizacion.aspx?id=" + Eval("IdCotizacion") %>' class="btn btn-sm btn-outline-primary">
                                    <i class="fas fa-eye"></i> Ver
                                </a>
                                <a href='<%# "EditarCotizacion.aspx?id=" + Eval("IdCotizacion") %>' class="btn btn-sm btn-outline-warning">
                                    <i class="fas fa-edit"></i> Editar
                                </a>
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
                        <h5 class="card-title">Total Cotizaciones</h5>
                        <h3 class="text-primary"><asp:Label ID="lblTotalCotizaciones" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Monto Total</h5>
                        <h3 class="text-success"><asp:Label ID="lblMontoTotal" runat="server" Text="$0.00" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Activas</h5>
                        <h3 class="text-info"><asp:Label ID="lblActivas" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-center">
                    <div class="card-body">
                        <h5 class="card-title">Este Mes</h5>
                        <h3 class="text-warning"><asp:Label ID="lblEsteMes" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>