using Negocio;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DAO;
using Servicios;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios
{
    public partial class GastosParticulares : System.Web.UI.Page
    {
        private const int col_Unidades_Pago_ID = 3;
        private const int col_Apllicar = 4;
        private IUnidadesServ unidadesServ = new unidadesFuncionalesServ();
        unidadesFuncionaesNeg unidadesNeg;
        readonly IExpensasServ _expensasServ;
        readonly IPagosServ _pagosServ;

        public GastosParticulares()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
            _pagosServ = new pagosServ(context);
            unidadesNeg = new unidadesFuncionaesNeg(unidadesServ, _pagosServ, _expensasServ);
        }

        #region Metodos Privados

        private void CargarGrillaUnidades(bool cochera)
        {
            
            var periodoNumerico = int.Parse(Session["PeriodoNumerico"].ToString());
            grdUnidades.DataSource = unidadesNeg.GetPagosConCochera(Session["idConsorcio"].ToString(), periodoNumerico, cochera);
            grdUnidades.DataBind();
        }

        private void GetTotalPorUF()
        {
            decimal importe = 0;
            int cantAplicar = GetCantAplicar();

            decimal.TryParse(txtImporte.Text, out importe);

            lblImportePorUF.Text = DividirImportePorUfChequeada(importe, cantAplicar);           
        }            

        private string DividirImportePorUfChequeada(decimal importe, int cantAplicar)
        {
            if (cantAplicar > 0)
                return (importe / cantAplicar).ToString("#.##");
            else
                return "0";
        }

        private int GetCantAplicar()
        {
            int cantAplicar = 0;

            foreach (GridViewRow getRowItems in grdUnidades.Rows)
            {
                var chkBox = (CheckBox)(getRowItems.Cells[0].FindControl("chkAplicar"));

                if (chkBox.Checked)
                {
                    cantAplicar++;
                }
            }

            return cantAplicar;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaUnidades(false);
            }
        }

        protected void grdUnidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void chkAplicar_CheckedChanged(object sender, EventArgs e)
        {
            GetTotalPorUF();
        }

        protected void btnAplicarCocheras_Click(object sender, EventArgs e)
        {
            CargarGrillaUnidades(true);
            GetTotalPorUF();
        }

        protected void grdUnidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_Unidades_Pago_ID].Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Expensas.aspx#consorcios", false); 
        }

        protected void btnGuardarGasto_Click(object sender, EventArgs e)
        {
            try
            {
                unidadesNeg.ActualizarGastosParticulares(grdUnidades.Rows, txtImporte.Text, txtDetalle.Text, lblImportePorUF.Text, ddlTipoGasto.Text);
                Response.Redirect("Expensas.aspx#consorcios",false);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            GetTotalPorUF();
        }
    }
}