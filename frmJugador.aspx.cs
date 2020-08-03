using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppRuleta.Modelo;
using WebAppRuleta.Context;
using System.Data.Entity;

namespace WebAppRuleta
{
    public partial class frmJugador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<Jugador> listJugador = new List<Jugador>();
                if (!IsPostBack)
                {
                    DivCrear.Visible = false;
                    BaseContext baseContext = new BaseContext();
                    listJugador = baseContext.Jugador.ToList();
                    if (listJugador != null && listJugador.Count > 0)
                        Session["listJugador"] = listJugador;
                }
                else
                {
                    listJugador = (List<Jugador>)Session["listJugador"];
                }
                gvJugadores.DataSource = listJugador;
                gvJugadores.DataBind();
            }
            catch (Exception ex)
            {
                Session["AccionE"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void gvJugadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Int32 CodigoSeleccionado = Convert.ToInt32(e.CommandArgument.ToString());
                Session["CodigoSeleccionado"] = CodigoSeleccionado;
                List<Jugador> listJugadorAux = (List<Jugador>)Session["listJugador"];
                Jugador jugador = listJugadorAux.Where(w => w.Id == CodigoSeleccionado).FirstOrDefault();
                if (e.CommandName == "cmEditar")
                {
                    Session["AccionE"] = "Editar";
                    txtNombre.Text = jugador.Nombre;
                    txtDocumento.Text = jugador.Documento;
                    txtApellido.Text = jugador.Apellido;
                    txtDinero.Text = Convert.ToInt32(jugador.CantidadDinero).ToString();
                    DivCrear.Visible = true;
                    DivBtnCrear.Visible = false;
                    DivGrilla.Visible = false;
                }
                else if (e.CommandName == "cmBorrar")
                {
                    Session["AccionE"] = "Borrar";
                    string Datos = string.Format("Esta seguro que desea eliminar al jugador {0} {1}", jugador.Nombre, jugador.Apellido);
                    MostrarModal(Datos);
                }

            }
            catch (Exception ex)
            {
                Session["AccionE"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaControles();
                DivCrear.Visible = true;
                DivBtnCrear.Visible = false;
                DivGrilla.Visible = false;
                Session["AccionE"] = "Guardar";
            }
            catch (Exception ex)
            {
                Session["AccionE"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string accion = "";
                string mensaje = "";
                if (Session["AccionE"] != null)
                    accion = Session["AccionE"].ToString();
                if (string.IsNullOrEmpty(txtDocumento.Text))
                    throw new Exception("Documento");
                if (string.IsNullOrEmpty(txtNombre.Text))
                    throw new Exception("Falta Nombre");
                if (string.IsNullOrEmpty(txtApellido.Text))
                    throw new Exception("Falta Apellido");
                // se cargan datos de jugador a crear
                Jugador jugador = new Jugador();
                jugador.Documento = txtDocumento.Text;
                jugador.Nombre = txtNombre.Text;
                jugador.Apellido = txtApellido.Text;
                jugador.CantidadDinero = Convert.ToDecimal(txtDinero.Text);
                BaseContext baseContext = new BaseContext();
                baseContext.Jugador.Add(jugador);

                switch (accion)
                {
                    case "Guardar":
                        baseContext.SaveChanges();
                        mensaje = "Registro creado con exito";
                        break;
                    case "Editar":
                        jugador.Id = Convert.ToInt32(Session["CodigoSeleccionado"].ToString());
                        baseContext.Entry(jugador).State = EntityState.Modified;
                        baseContext.SaveChanges();
                        Session["CodigoSeleccionado"] = null;
                        mensaje = "Registro editado con exito";
                        break;
                }
                // se limpian los controles
                LimpiaControles();
                DivCrear.Visible = false;
                DivBtnCrear.Visible = true;
                DivGrilla.Visible = true;
                Session["AccionE"] = null;
                // se actuliza grilla
                List<Jugador> listJugador = new List<Jugador>();
                listJugador = baseContext.Jugador.ToList();
                Session["listJugador"] = listJugador;
                gvJugadores.DataSource = listJugador;
                gvJugadores.DataBind();
                // se muestra mensaje exito
                MostrarModal(mensaje);
            }
            catch (Exception ex)
            {
                Session["AccionE"] = null;
                MostrarModal(ex.Message);
            }
        }

        private void LimpiaControles()
        {
            try
            {
                txtApellido.Text = "";
                txtDinero.Text = "";
                txtDocumento.Text = "";
                txtNombre.Text = "";
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaControles();
                DivCrear.Visible = false;
                DivBtnCrear.Visible = true;
                DivGrilla.Visible = true;
            }
            catch (Exception ex)
            {
                Session["AccionE"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string accion = "";
                if (Session["AccionE"] != null)
                    accion = Session["AccionE"].ToString();

                if (!string.IsNullOrEmpty(accion) && accion == "Borrar")
                {

                    BaseContext baseContext = new BaseContext();
                    List<Jugador> listJugadorAux = (List<Jugador>)Session["listJugador"];
                    Jugador jugador = listJugadorAux.Where(w => w.Id == Convert.ToInt32(Session["CodigoSeleccionado"].ToString())).FirstOrDefault();
                    baseContext.Entry(jugador).State = EntityState.Deleted;
                    baseContext.SaveChanges();
                    // se actuliza grilla
                    Session["CodigoSeleccionado"] = null;
                    List<Jugador> listJugador = new List<Jugador>();
                    listJugador = baseContext.Jugador.ToList();
                    Session["listJugador"] = listJugador;
                    gvJugadores.DataSource = listJugador;
                    gvJugadores.DataBind();
                }
                else
                {
                    Session["AccionE"] = null;
                    lblModal.Text = "";
                }
            }
            catch (Exception ex)
            {
                Session["AccionE"] = null;
                MostrarModal(ex.Message);
            }
        }

        private void MostrarModal(string mensaje)
        {
            try
            {
                lblModal.Text = mensaje;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$(document).ready(function () {$('#exampleModal').modal();});", true);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}