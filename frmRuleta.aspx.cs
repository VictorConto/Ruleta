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
    public partial class frmRuleta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<Jugador> listJugador = new List<Jugador>();
                if (!IsPostBack)
                {
                    Session["listJugadorGrilla"] = null;
                    Session["Ganador"] = null;
                    Session["listJugadorApuesta"] = null;
                    Session["AccionEd"] = null;
                    Session["SelecJugador"] = null;
                    DivBtnAgregar.Visible = true;
                    DivGrilla.Visible = true;
                    DivSinGrilla.Visible = true;
                    DivAddJugador.Visible = false;
                    BaseContext baseContext = new BaseContext();
                    listJugador = baseContext.Jugador.ToList();
                    if (listJugador != null && listJugador.Count > 0)
                    {
                        List<JugadorApuesta> listJugadorApuesta = (from d in listJugador.AsEnumerable()
                                                                   select new JugadorApuesta
                                                                   {
                                                                       Id = d.Id,
                                                                       Documento = d.Documento,
                                                                       Apellido = d.Apellido,
                                                                       CantidadDinero = d.CantidadDinero,
                                                                       Nombre = d.Nombre,
                                                                       NombreCompleto = d.Nombre + " " + d.Apellido
                                                                   }).ToList();
                        Session["listJugadorApuesta"] = listJugadorApuesta;
                        dpJugadores.DataSource = Session["listJugadorApuesta"];
                        dpJugadores.DataBind();
                        dpJugadores.Items.Insert(0, new ListItem("Seleccionar", "0"));
                    }
                }
                else
                {
                    if (Session["listJugadorGrilla"] != null)
                    {
                        gvJugadoresApuest.DataSource = Session["listJugadorGrilla"];
                        gvJugadoresApuest.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Ganador"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void gvJugadoresApuest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Int32 jugadorSeleccionado = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "cmCambiar")
                {
                    Session["AccionEd"] = "X";
                    List<JugadorApuesta> listJugadorApuest = (List<JugadorApuesta>)Session["listJugadorApuesta"];
                    JugadorApuesta jugadorApuesta = listJugadorApuest.Where(w => w.Id == jugadorSeleccionado).FirstOrDefault();
                    txtNombre.Text = jugadorApuesta.NombreCompleto;
                    txtDinero.Text = Convert.ToInt32(jugadorApuesta.CantidadDinero).ToString();
                    if (!string.IsNullOrEmpty(jugadorApuesta.Color))
                        dpColores.SelectedValue = jugadorApuesta.Color;
                    if (!string.IsNullOrEmpty(jugadorApuesta.Apuesta))
                    {
                        dpApuesta.SelectedValue = jugadorApuesta.Apuesta;
                        if (jugadorApuesta.Apuesta == "AllIn")
                            dpApuesta.Enabled = false;
                        else
                            dpApuesta.Enabled = true;
                    }
                    Session["SelecJugador"] = jugadorApuesta;
                    dpJugadores.Enabled = false;
                    DivBtnAgregar.Visible = false;
                    DivGrilla.Visible = false;
                    DivAddJugador.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Session["Ganador"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void btnAgregarJugador_Click(object sender, EventArgs e)
        {
            try
            {
                DivBtnAgregar.Visible = false;
                DivGrilla.Visible = false;
                DivAddJugador.Visible = true;
            }
            catch (Exception ex)
            {
                Session["Ganador"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void dpJugadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string jugadorSeleccionado = dpJugadores.SelectedValue;
                if (jugadorSeleccionado != "0")
                {
                    List<JugadorApuesta> listJugadorApuest = (List<JugadorApuesta>)Session["listJugadorApuesta"];
                    JugadorApuesta jugadorApuesta = listJugadorApuest.Where(w => w.Id == Convert.ToInt32(jugadorSeleccionado)).FirstOrDefault();
                    txtNombre.Text = jugadorApuesta.NombreCompleto;
                    txtDinero.Text = Convert.ToInt32(jugadorApuesta.CantidadDinero).ToString();

                    if (Convert.ToInt32(jugadorApuesta.CantidadDinero) <= 1000)
                    {
                        dpApuesta.Enabled = false;
                        dpApuesta.SelectedValue = "AllIn";
                    }
                    else
                        dpApuesta.Enabled = true;
                    Session["SelecJugador"] = jugadorApuesta;
                }
                else
                {
                    dpJugadores.Enabled = true;
                    txtDinero.Text = "";
                    txtNombre.Text = "";
                    Session["SelecJugador"] = null;
                }
            }
            catch (Exception ex)
            {
                Session["Ganador"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["SelecJugador"] == null)
                {
                    throw new Exception("Seleccionar jugador");
                }
                List<JugadorApuesta> listJugadorGrilla = new List<JugadorApuesta>();
                if (Session["listJugadorGrilla"] != null)
                    listJugadorGrilla = (List<JugadorApuesta>)Session["listJugadorGrilla"];

                JugadorApuesta jugadorApuesta = (JugadorApuesta)Session["SelecJugador"];
                jugadorApuesta.Color = dpColores.SelectedValue;
                jugadorApuesta.Apuesta = dpApuesta.SelectedValue;
                if (jugadorApuesta.Apuesta != "AllIn")
                    jugadorApuesta.ValorApuesta = Convert.ToInt32(jugadorApuesta.CantidadDinero * Convert.ToInt32(jugadorApuesta.Apuesta)) / 100;
                else
                    jugadorApuesta.ValorApuesta = Convert.ToInt32(jugadorApuesta.CantidadDinero);
                if (Session["AccionEd"] != null && Session["AccionEd"].ToString() == "X")
                    listJugadorGrilla.RemoveAll(r => r.Id == jugadorApuesta.Id);
                if (listJugadorGrilla.Exists(y => y.Id == jugadorApuesta.Id))
                    listJugadorGrilla.RemoveAll(r => r.Id == jugadorApuesta.Id);

                if (jugadorApuesta.CantidadDinero > 0)
                    listJugadorGrilla.Add(jugadorApuesta);
                else
                    throw new Exception("Seleccionar jugador con cantidad de dinero mayor a 0");
                Session["SelecJugador"] = null;
                Session["AccionEd"] = null;
                Session["listJugadorGrilla"] = listJugadorGrilla;
                gvJugadoresApuest.DataSource = Session["listJugadorGrilla"];
                gvJugadoresApuest.DataBind();
                dpJugadores.Enabled = true;
                dpJugadores.SelectedValue = "0";
                txtDinero.Text = "";
                txtNombre.Text = "";
                DivBtnAgregar.Visible = true;
                DivGrilla.Visible = true;
                DivSinGrilla.Visible = false;
                DivAddJugador.Visible = false;
            }
            catch (Exception ex)
            {
                Session["Ganador"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                txtDinero.Text = "";
                txtNombre.Text = "";
                Session["SelecJugador"] = null;
                Session["AccionEd"] = null;
                DivBtnAgregar.Visible = true;
                DivGrilla.Visible = true;
                DivAddJugador.Visible = false;
                dpJugadores.Enabled = true;
            }
            catch (Exception ex)
            {
                Session["Ganador"] = null;
                MostrarModal(ex.Message);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["Ganador"] != null)
                    if (Session["listJugadorGrilla"] != null)
                    {
                        List<JugadorApuesta> listJugadorGrilla = (List<JugadorApuesta>)Session["listJugadorGrilla"];
                        foreach (JugadorApuesta itemjugador in listJugadorGrilla)
                        {
                            Jugador jugador = new Jugador();
                            jugador.Id = itemjugador.Id;
                            jugador.Apellido = itemjugador.Apellido;
                            jugador.Nombre = itemjugador.Nombre;
                            jugador.Documento = itemjugador.Documento;

                            if (itemjugador.Color == Session["Ganador"].ToString())
                            {
                                switch (Session["Ganador"].ToString())
                                {
                                    case "Negro":
                                    case "Rojo":
                                        jugador.CantidadDinero = itemjugador.CantidadDinero + itemjugador.ValorApuesta;
                                        break;
                                    case "Verde":
                                        jugador.CantidadDinero = itemjugador.CantidadDinero + (itemjugador.ValorApuesta * 14);
                                        break;
                                }
                            }
                            else
                                jugador.CantidadDinero = itemjugador.CantidadDinero - itemjugador.ValorApuesta;
                            itemjugador.CantidadDinero = jugador.CantidadDinero;

                            if (itemjugador.Apuesta != "AllIn")
                                itemjugador.ValorApuesta = Convert.ToInt32(itemjugador.CantidadDinero * Convert.ToInt32(itemjugador.Apuesta)) / 100;
                            else
                                itemjugador.ValorApuesta = Convert.ToInt32(itemjugador.CantidadDinero);

                            BaseContext baseContext = new BaseContext();
                            baseContext.Jugador.Add(jugador);
                            baseContext.Entry(jugador).State = EntityState.Modified;
                            baseContext.SaveChanges();
                        }
                        listJugadorGrilla.RemoveAll(r => r.CantidadDinero == 0);
                        Session["listJugadorGrilla"] = listJugadorGrilla;
                        gvJugadoresApuest.DataSource = Session["listJugadorGrilla"];
                        gvJugadoresApuest.DataBind();

                        List<Jugador> listJugador = new List<Jugador>();
                        BaseContext baseContextAux = new BaseContext();
                        listJugador = baseContextAux.Jugador.ToList();
                        if (listJugador != null && listJugador.Count > 0)
                        {
                            List<JugadorApuesta> listJugadorApuesta = (from d in listJugador.AsEnumerable()
                                                                       select new JugadorApuesta
                                                                       {
                                                                           Id = d.Id,
                                                                           Documento = d.Documento,
                                                                           Apellido = d.Apellido,
                                                                           CantidadDinero = d.CantidadDinero,
                                                                           Nombre = d.Nombre,
                                                                           NombreCompleto = d.Nombre + " " + d.Apellido
                                                                       }).ToList();
                            Session["listJugadorApuesta"] = listJugadorApuesta;
                            dpJugadores.DataSource = Session["listJugadorApuesta"];
                            dpJugadores.DataBind();
                            dpJugadores.Items.Insert(0, new ListItem("Seleccionar", "0"));
                        }
                    }
            }
            catch (Exception ex)
            {
                Session["Ganador"] = null;
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

        protected void btnRueda_Click(object sender, EventArgs e)
        {
            try
            {
                var ListColores = new List<string>{
               "Rojo",
                "Negro",
               "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Verde",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                "Rojo",
                "Negro",
                };
                Random random = new Random();
                int index = random.Next(0, ListColores.Count());
                var Reul = ListColores[index];
                Session["Ganador"] = Reul;
                MostrarModal("El " + Reul + " es el color ganador");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myRueda", "miRuleta.stopAnimation(false); miRuleta.rotationAngle = 0; ", true);
            }
            catch (Exception ex)
            {
                Session["Ganador"] = null;
                MostrarModal(ex.Message);
            }
        }
    }
}