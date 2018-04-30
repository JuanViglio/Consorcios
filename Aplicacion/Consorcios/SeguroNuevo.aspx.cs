using DAO;
using DAO.Models;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;

namespace WebSistemmas.Consorcios
{
    public partial class SeguroNuevo : System.Web.UI.Page
    {
        readonly IConsorciosServ _consorciosServ;
        private ExpensasEntities context = new ExpensasEntities();

        #region Funciones Privadas
        private string getPeriodoNuevo(string periodoActual)
        {
            int mes = int.Parse(periodoActual.Substring(4, 2));
            int año = int.Parse(periodoActual.Substring(0, 4));
            string periodoNuevo;

            if (mes < 12)
            {
                periodoNuevo = año.ToString() + (mes + 1).ToString("D2"); 
            }
            else
            {
                periodoNuevo = (año + 1).ToString() + "01";
            }

            return periodoNuevo;
        }

        private string Validar()
        {
            if (!txtCantCuotas.Text.IsNumeric())
            {
                return "No se ingreso la Cantidad de Cuotas correctamente";
            }
            else if (!txtCuotasDeGracia.Text.IsNumeric())
            {
                return "No se ingreso la Cantidad de Cuotas de Gracia correctamente";
            }
            else if (!txtImporte.Text.IsNumeric())
            {
                return "No se ingreso el Importe correctamente";
            }

            return "";
        }

        private decimal GetImporteCuota(int cuota, int primeraCuota0)
        {
            if (cuota < primeraCuota0)
                return decimal.Parse(txtImporte.Text);
            else
                return 0;
        }

        private void CargarComboConsorcios()
        {
            ddlConsorcios.DataSource = _consorciosServ.GetConsorciosCombo();
            ddlConsorcios.DataTextField = "Direccion";
            ddlConsorcios.DataValueField = "Id";
            ddlConsorcios.DataBind();
        }
        #endregion

        public SeguroNuevo()
        {
            _consorciosServ = new consorciosServ(context);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.TituloPagina = "Seguro Nuevo";
                dteFechaInicio.SelectedDate = DateTime.Now;
                dteFechaFin.SelectedDate = DateTime.Now;
                CargarComboConsorcios();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Seguros.aspx#seguros");
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            var seguro = new SegurosModel(); 
            List<SeguroDetalleModel> detalle = null;
            string error = Validar();

            if (error == "")
            {
                detalle = new List<SeguroDetalleModel>(); 
                int cantCuotas = int.Parse(txtCantCuotas.Text);
                var periodo =  dteFechaInicio.SelectedDate.Year.ToString() + dteFechaInicio.SelectedDate.Month.ToString("D2");
                var primeraCuota0 = cantCuotas - int.Parse(txtCuotasDeGracia.Text);

                for (int i = 0; i < cantCuotas ; i++)
                {
                    detalle.Add(new SeguroDetalleModel()
                    {
                        Cuota = i + 1,
                        Periodo = int.Parse(periodo),
                        Importe = GetImporteCuota(i, primeraCuota0)
                    });
                    periodo = getPeriodoNuevo(periodo);
                }

                gridSeguroDetalleID.ActualizarGrillaSeguro(detalle);

                seguro.CantCuotas = int.Parse(txtCantCuotas.Text);
                seguro.CantCuotas0 = int.Parse(txtCuotasDeGracia.Text);
                seguro.Compañia = txtCompañia.Text;
                seguro.Poliza = txtPoliza.Text;
                seguro.Tipo = ddlTipo.SelectedItem.Text;
                seguro.FechaInicio = dteFechaInicio.SelectedDate;
                seguro.FechaFin = dteFechaFin.SelectedDate;
                seguro.Consorcio = ddlConsorcios.SelectedValue ;

                Session["Seguro"] = seguro;
            }

            UserControlError.MostrarError(error);            
        }
    }
}