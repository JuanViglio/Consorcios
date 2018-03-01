using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.UserControls.ExpensasUF
{
    public partial class TotalesUF : System.Web.UI.UserControl
    {
        private unidadesFuncionalesServ _unidadesFuncServ;
        private expensasServ _expensasServ;
        private IPagosServ _pagosServ;

        public TotalesUF()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
            _pagosServ = new pagosServ(context);
            _unidadesFuncServ = new unidadesFuncionalesServ();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CalcularTotales(Pagos Pago)
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);
            var PagoId = Session["PagoId"].ToString();
            decimal coeficiente = Pago.Coeficiente;
            decimal gastosOrdinarios = _expensasServ.GetTotalGastosOrdinarios(expensaID);
            decimal gastosExtraordinarios = _expensasServ.GetTotalGastosExtraordinarios(expensaID);
            decimal subtotalGastoOrdinario = gastosOrdinarios * coeficiente / 100;
            decimal subtotalGastoExtraordinario = gastosExtraordinarios * coeficiente / 100;
            decimal subtotalGastoCocheraOrd = _pagosServ.GetTotalGastosEvOrdinariosUF(int.Parse(PagoId));
            decimal subtotalGastoCocheraExt = _pagosServ.GetTotalGastosEvExtUF(int.Parse(PagoId));
            decimal importeGastoParticular = Pago.ImporteGastoParticular;

            lblCoeficiente.Text = coeficiente.ToString();
            lblSubtotalGastoOrdinario.Text = subtotalGastoOrdinario.ToString("0.00");
            lblSubtotalGastoExt.Text = subtotalGastoExtraordinario.ToString("0.00");
            lblSubtotalGastoCocherarOrd.Text = subtotalGastoCocheraOrd.ToString("0.00");
            lblSubtotalGastoCocheraExt.Text = subtotalGastoCocheraExt.ToString("0.00");
            lblSubtotalGastoParicular.Text = importeGastoParticular.ToString("0.00");

            lblVencimiento1.Text = (subtotalGastoOrdinario + subtotalGastoExtraordinario + subtotalGastoCocheraOrd + subtotalGastoCocheraExt + importeGastoParticular).ToString("0.00");
        }

    }
}