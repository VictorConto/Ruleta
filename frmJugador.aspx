<%@ Page Title="Jugadores" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="frmJugador.aspx.cs" Inherits="WebAppRuleta.frmJugador" Theme="Theme" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/jquery-2.1.4.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row form-group" id="DivBtnCrear" runat="server">
            <div class="col-lg-2 col-md-2">
                <asp:Button ID="btnCrear" CssClass="btn btn-primary" runat="server" Text="Crear" Width="100%" OnClick="btnCrear_Click" />
            </div>
        </div>
        <div class="col-lg-8 col-md-8 col-lg-offset-2 col-md-offset-2 form-group" id="DivGrilla" runat="server">
            <asp:GridView ID="gvJugadores" CssClass="table table-striped" OnRowCommand="gvJugadores_RowCommand" DataKeyNames="Id" runat="server" AutoGenerateColumns="False" AllowPaging="True">
                <Columns>
                    <asp:BoundField DataField="Documento" HeaderText="Doc" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="CantidadDinero" HeaderText="Dinero" DataFormatString="{0:c}" />
                    <asp:TemplateField HeaderText="Evento">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id")%>' Text="Editar" CommandName="cmEditar"></asp:LinkButton>
                            <asp:LinkButton ID="lnkBorrar" runat="server" CommandArgument='<%#Eval("Id")%>' Text="Borrar" CommandName="cmBorrar"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="row" id="DivCrear" runat="server">
            <div class="col-lg-5 col-md-5">
                <div class="col-lg-4 col-md-4 form-group">
                    <asp:Label ID="lblDocumento" runat="server" Text="Documento:" Font-Bold="true" ForeColor="Blue"></asp:Label>
                </div>
                <div class="col-lg-6 col-md-6 form-group">
                    <asp:TextBox ID="txtDocumento" Width="100%" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-lg-4 col-md-4 form-group">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" Font-Bold="true" ForeColor="Blue"></asp:Label>
                </div>
                <div class="col-lg-6 col-md-6 form-group">
                    <asp:TextBox ID="txtNombre" runat="server" Width="100%" Text=""></asp:TextBox>
                </div>
                <div class="col-lg-4 col-md-4 form-group">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellido:" Font-Bold="true" ForeColor="Blue"></asp:Label>
                </div>
                <div class="col-lg-6 col-md-6 form-group">
                    <asp:TextBox ID="txtApellido" runat="server" Width="100%" Text=""></asp:TextBox>
                </div>
                <div class="col-lg-4 col-md-4 form-group">
                    <asp:Label ID="lblDinero" runat="server" Text="Dinero:" Font-Bold="true" ForeColor="Blue"></asp:Label>
                </div>
                <div class="col-lg-6 col-md-6 form-group">
                    <asp:TextBox ID="txtDinero" runat="server" Width="100%" Text="" TextMode="Number"></asp:TextBox>
                </div>
                <div class="col-lg-4 col-md-4">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" Width="100%" OnClick="btnGuardar_Click" />
                </div>
                <div class="col-lg-4 col-md-4">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" Width="100%" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="exampleModalLabel">Alerta</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblModal" runat="server"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-primary" Text="Aceptar" OnClick="btnAceptar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
