using DAO;
using DAO.Models;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Consorcios.UserControls;

namespace WebSistemmas.Consorcios
{
    public partial class SeguroNuevo : System.Web.UI.Page
    {
        ISegurosServ _segurosServ;
        IExpensasServ _expensasServ;
        ISegurosNeg _segurosNeg;    
        readonly IConsorciosServ _consorciosServ;
        private ExpensasEntities context = new ExpensasEntities();

        #region Metodos Privados

        private void CargarComboConsorcios()
        {
            ddlConsorcios.DataSource = _consorciosServ.GetConsorciosCombo();
            ddlConsorcios.DataTextField = "Direccion";
            ddlConsorcios.DataValueField = "Id";
            ddlConsorcios.DataBind();
        }

        private void MostrarError(string error)
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControlError");
            Error errorUc = (Error)control;

            errorUc.MostrarError(error);
        }

        #endregion

        public SeguroNuevo()
        {
            _segurosServ = new segurosServ(context);
            _consorciosServ = new consorciosServ(context);
            _expensasServ = new expensasServ(context);
            _segurosNeg = new segurosNeg(_segurosServ, _consorciosServ, _expensasServ);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.CargarTitulo("Seguro Nuevo");
                dteFechaInicio.SelectedDate = DateTime.Now;
                dteFechaFin.SelectedDate = DateTime.Now;
                CargarComboConsorcios();
                Session["Seguros"] = null;
                Session["SegurosDetalle"] = null;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Seguros.aspx#seguros");
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarError(string.Empty);

                _segurosNeg.Validar(txtCompañia.Text, txtPoliza.Text, ddlConsorcios.SelectedValue, txtCantCuotas.Text, txtCuotasDeGracia.Text, txtImporte.Text);
                gridSeguroDetalleID.ActualizarGrillaSeguro(_segurosNeg.GetSeguroDetalleModelo(txtCantCuotas.Text, dteFechaInicio.SelectedDate, txtCuotasDeGracia.Text, txtImporte.Text));

            }
            catch (Exception ex)
            {
                UserControlError.MostrarError(ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarError(string.Empty);

                var seguroModelo = _segurosNeg.GetSeguroModelo(txtCompañia.Text, txtPoliza.Text, ddlConsorcios.SelectedValue, txtCantCuotas.Text, txtCuotasDeGracia.Text, 
                    txtImporte.Text, dteFechaInicio.SelectedDate, dteFechaFin.SelectedDate, ddlTipo.SelectedValue);
                var segurosDetalleModelo = (List<SeguroDetalleModel>)Session["SegurosDetalle"];

                _segurosNeg.GuardarSeguro(seguroModelo, segurosDetalleModelo);

                Response.Redirect("Seguros.aspx#seguros");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

    }
}