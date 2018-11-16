using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Consorcios.UserControls.Cobranza;

namespace WebSistemmas.Consorcios
{
    public partial class Cobranza : System.Web.UI.Page
    {
        private IConsorciosServ _consorciosServ;
        private IUnidadesServ _unidadesServ;
        private dueñosServ _propietariosServ;
        private pagosServ _pagosServ;
        private ExpensasEntities context = new ExpensasEntities();

        #region Metodos Privados
        private void CargarComboConsorcios()
        {
            ddlConsorcios.DataSource = _consorciosServ.GetConsorciosCombo();
            ddlConsorcios.DataTextField = "Direccion";
            ddlConsorcios.DataValueField = "Id";
            ddlConsorcios.DataBind();
        }

        private void CargarComboUF(string idConsorcio)
        {
            ddlUF.DataSource = _unidadesServ.GetUnidadesFuncionalesCombo(idConsorcio);
            ddlUF.DataTextField = "UF";
            ddlUF.DataValueField = "Id";
            ddlUF.DataBind();
        }

        private void CargarComboPeriodos(decimal idUF)
        {
            var periodos = _pagosServ.GetPagosAdeudados(idUF);
            ddlPeriodo.DataSource = periodos;
            ddlPeriodo.DataTextField = "Periodo";
            ddlPeriodo.DataValueField = "ID";
            ddlPeriodo.DataBind();
        }

        private void CargarGrillaUF(decimal idPropietario)
        {
            grdUF.DataSource = _unidadesServ.GetUFByPropietarioId(idPropietario);
            grdUF.DataBind();
        }

        private void CargarComboPropietarios()
        {
            ddlPropietario.DataSource = _propietariosServ.GetDueñosCombo();
            ddlPropietario.DataTextField = "Apellido_y_Nombre";
            ddlPropietario.DataValueField = "ID";
            ddlPropietario.DataBind();
        }

        #endregion

        #region Constructor y Page Load
        public Cobranza()
        {
            _consorciosServ = new consorciosServ(context);
            _unidadesServ = new unidadesFuncionalesServ();
            _propietariosServ = new dueñosServ();
            _pagosServ = new pagosServ(context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divPropietario.Visible = false;
                CargarComboConsorcios();
                CargarComboPropietarios();
            }
        }
        #endregion

        #region BusquedaUF

        #region Drop Down Lists
        protected void ddlConsorcios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboUF(ddlConsorcios.SelectedValue);
        }

        protected void ddlUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboPeriodos(ddlUF.SelectedValue.ToDecimal());
        }

        #endregion

        #endregion

        #region BusquedaPropietario

        #region DropDownList
        protected void ddlPropietario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaUF(ddlPropietario.SelectedValue.ToDecimal());
        }
        #endregion

        #region Botones
        protected void btnAceptarPropietario_Click(object sender, EventArgs e)
        {
            List<UnidadesFuncionalesModel> ufModel = new List<UnidadesFuncionalesModel>();

            foreach (GridViewRow item in grdUF.Rows)
            {
                if (((CheckBox)item.FindControl("chkSumar")).Checked)
                {
                    ufModel.Add(new UnidadesFuncionalesModel()
                    {
                        Direccion = item.Cells[0].Text,
                        UF = item.Cells[1].Text,
                        ID = item.Cells[2].Text.ToInt()
                    });
                }
            }

            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("gridPagarID");
            GridPagar GridPagarUC = (GridPagar)control;

            Session["ufModel"] = ufModel;

            GridPagarUC.CargarGrillaCobrar();

            //divTest.Visible = false;
        }

        #endregion
        #endregion

        //private IConsorciosServ _consorciosServ;
        //private IUnidadesServ _unidadesServ;
        //private dueñosServ _propietariosServ;
        //private ExpensasEntities context = new ExpensasEntities();

        //public Cobranza()
        //{
        //    _consorciosServ = new consorciosServ(context);
        //    _unidadesServ = new unidadesFuncionalesServ();
        //    _propietariosServ = new dueñosServ();
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        tituloPaginaID.CargarTitulo("Cobranza");
        //        divPropietario.Visible = false;
        //    }
        //}

        //#region Botones

        //protected void btnCobrar_Click(object sender, EventArgs e)
        //{

        //}

        //#endregion

        protected void UF_CheckedChanged(object sender, EventArgs e)
        {
            divUF.Visible = true;
            divPropietario.Visible = false;
        }

        protected void Propietario_CheckedChanged(object sender, EventArgs e)
        {
            divUF.Visible = false;
            divPropietario.Visible = true;
        }
    }
}