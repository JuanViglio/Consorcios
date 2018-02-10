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
        private IUnidadesServ unidadesServ = new unidadesFuncionalesServ();
        unidadesFuncionaesNeg unidadesNeg;
        readonly IExpensasServ _expensasServ;
        readonly IPagosServ _pagosServ;

        public GastosParticulares()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
            _pagosServ = new pagosServ(context);
        }

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
        }


        #region Metodos Privados
        private void CargarGrillaUnidades(bool cochera)
        {
            unidadesNeg = new unidadesFuncionaesNeg(unidadesServ);
            var periodoNumerico = int.Parse(Session["PeriodoNumerico"].ToString());
            grdUnidades.DataSource = unidadesNeg.GetPagosConCochera(Session["idConsorcio"].ToString(), periodoNumerico, cochera);
            grdUnidades.DataBind();
        }
        #endregion

        protected void btnAplicarCocheras_Click(object sender, EventArgs e)
        {
            CargarGrillaUnidades(true);
        }

        protected void grdUnidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_Unidades_Pago_ID].Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Expensas.aspx#consorcios"); 
        }

        protected void btnGuardarGasto_Click(object sender, EventArgs e)
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            foreach (GridViewRow row in grdUnidades.Rows)
            {
                CheckBox chk = row.Cells[4].Controls[1] as CheckBox;
                if (chk != null && chk.Checked)
                {
                    var idPago = int.Parse(row.Cells[3].Text);
                    _pagosServ.AddGastosEvOrdinariosEFDetalle(idPago, txtDetalle.Text.ToUpper(), Convert.ToDecimal(txtImporte.Text));
                    //_expensasServ.ActualizarTotalGastosEvOrdinarios(expensaId);
                }
            }

            txtDetalle.Text = "";
            txtImporte.Text = "";

        }
    }
}