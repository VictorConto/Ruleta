<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="frmRuleta.aspx.cs" Inherits="WebAppRuleta.frmRuleta" Theme="Theme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/TweenMax.min.js"></script>
    <script src="js/Winwheel.min.js"></script>
     <script src="js/jquery-1.10.2.js"></script>
    <script src="js/jquery-2.1.4.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-4 col-md-4 form-group">
        <asp:Button ID="btnRueda" runat="server" Text="Girar" CssClass="btn btn-success" OnClick="btnRueda_Click" OnClientClick="miRuleta.startAnimation();" />
        <br />
        <br />
        <canvas id="canvas" height="400" width="400"></canvas>
        <script>
            var miRuleta = new Winwheel({
                'numSegments': 37, // Número de segmentos
                'outerRadius': 170, // Radio externo
                'segments': [ // Datos de los segmentos
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#0FE826', 'text': ' ' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                    { 'fillStyle': '#E80F0F', 'text': ' ' },
                    { 'fillStyle': '#000000', 'text': '' },
                ],
                'animation': {
                    'type': 'spinToStop', // Giro y alto
                    'duration': 5, // Duración de giro
                    'callbackFinished': 'Mensaje() ', // Función para mostrar mensaje
                   // 'callbackAfter': 'dibujarIndicador() ' // Funciona de pintar indicador
                }
            });
            // Funciones complementarias
           
            function Mensaje() {
                var SegmentoSeleccionado = miRuleta.getIndicatedSegment();
               
                miRuleta.stopAnimation(false);
                miRuleta.rotationAngle = 0;
            }
        </script>
    </div>
    <div class="col-lg-8 col-md-8 form-group">
        <div class="col-lg-2 col-md-2 form-group" runat="server" id="DivBtnAgregar">
            <asp:Button ID="btnAgregarJugador" runat="server" CssClass="btn btn-primary" Text="Agregar Jugador" OnClick="btnAgregarJugador_Click" />
        </div>
        <div class="col-lg-12 col-md-12" style="padding-left: 0%" id="DivAddJugador" runat="server">
            <div class="col-lg-4 col-md-4 form-group">
                <asp:DropDownList ID="dpJugadores" runat="server" AutoPostBack="true" DataTextField="NombreCompleto" DataValueField="Id" Width="100%" OnSelectedIndexChanged="dpJugadores_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="row">
                <div class="col-lg-5 col-md-5 form-group">
                    <div class="col-lg-4 col-md-4 form-group">
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre:" Font-Bold="true" ForeColor="Blue"></asp:Label>
                    </div>
                    <div class="col-lg-8 col-md-8 form-group">
                        <asp:TextBox ID="txtNombre" runat="server" Width="100%" Text="" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-lg-4 col-md-4 form-group">
                        <asp:Label ID="lblColor" runat="server" Text="Color:" Font-Bold="true" ForeColor="Blue"></asp:Label>
                    </div>
                    <div class="col-lg-8 col-md-8 form-group">
                        <asp:DropDownList ID="dpColores" runat="server" Width="100%">
                            <asp:ListItem Text="Rojo" Value="Rojo"></asp:ListItem>
                            <asp:ListItem Text="Negro" Value="Negro"></asp:ListItem>
                            <asp:ListItem Text="Verde" Value="Verde"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-4 col-md-4 form-group">
                        <asp:Label ID="lblPorcentaje" runat="server" Text="Apuesta:" Font-Bold="true" ForeColor="Blue"></asp:Label>
                    </div>
                    <div class="col-lg-8 col-md-8 form-group">
                        <asp:DropDownList ID="dpApuesta" runat="server" Width="100%">
                            <asp:ListItem Text="8%" Value="8"></asp:ListItem>
                            <asp:ListItem Text="9%" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10%" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11%" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12%" Value="12"></asp:ListItem>
                            <asp:ListItem Text="13%" Value="12"></asp:ListItem>
                            <asp:ListItem Text="14%" Value="14"></asp:ListItem>
                            <asp:ListItem Text="15%" Value="15"></asp:ListItem>
                            <asp:ListItem Text="All In" Value="AllIn"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-4 col-md-4 form-group">
                        <asp:Label ID="lblDinero" runat="server" Text="Dinero:" Font-Bold="true" ForeColor="Blue"></asp:Label>
                    </div>
                    <div class="col-lg-8 col-md-8 form-group">
                        <asp:TextBox ID="txtDinero" runat="server" Width="100%" Text="" Enabled="false" TextMode="Number"></asp:TextBox>
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
        <div id="DivSinGrilla" style="text-align: center; margin-top: 6%;" runat="server">
            <h4>Agregar jugadores a la partida</h4>
        </div>
        <div class="col-lg-12 col-md-12 form-group" id="DivGrilla" runat="server">
            <asp:GridView ID="gvJugadoresApuest" CssClass="table table-striped" EmptyDataText="Agregar jugadores a partida" OnRowCommand="gvJugadoresApuest_RowCommand" DataKeyNames="Id" runat="server" AutoGenerateColumns="False" AllowPaging="True">
                <Columns>
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                    <asp:BoundField DataField="CantidadDinero" HeaderText="Dinero" DataFormatString="{0:c}" />
                    <asp:BoundField DataField="Color" HeaderText="Color" />
                    <asp:BoundField DataField="Apuesta" HeaderText="% Apuesta" />
                     <asp:BoundField DataField="ValorApuesta" HeaderText="Valor Apuesta" DataFormatString="{0:c}" />
                    <asp:TemplateField HeaderText="Evento">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCambiar" runat="server" CommandArgument='<%#Eval("Id")%>' Text="Cambiar apuesta" CommandName="cmCambiar"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
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
                    <asp:Label ID="lblModal" runat="server" Text="" ></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-primary" Text="Aceptar" OnClick="btnAceptar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
