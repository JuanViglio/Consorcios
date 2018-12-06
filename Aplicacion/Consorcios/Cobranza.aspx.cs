using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

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
            ddlPeriodo.DataTextField = "PeriodoDetalle";
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
                divPagar.Visible = false;
                tituloPaginaID.CargarTitulo("Cobranza");
            }
        }
        #endregion

        #region Options Buttons
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

        #region Botones
        protected void btnAceptarUF_Click(object sender, EventArgs e)
        {
            if (ddlUF.SelectedValue.ToString() != "0" && ddlUF.SelectedValue.ToString() != "" )
            {
                List<UnidadesFuncionalesModel> ufModel = new List<UnidadesFuncionalesModel>();
                ufModel.Add(new UnidadesFuncionalesModel() 
                {
                    Direccion = ddlConsorcios.SelectedItem.ToString(),
                    UF = ddlUF.SelectedItem.ToString(), 
                    ID = ddlPeriodo.SelectedValue.ToString().ToDecimal(),
                    PeriodoDetalle = ddlPeriodo.SelectedItem.ToString() 
                });

                CargarGrillaCobrar(ufModel);
                divPagar.Visible = true;
            }
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
                        PeriodoDetalle = item.Cells[2].Text,
                        ID = item.Cells[3].Text.ToInt()
                    });
                }
            }

            if (ufModel.Count > 0)
            {
                CargarGrillaCobrar(ufModel);
                divPagar.Visible = true;
            }
        }

        #endregion
        #endregion

        #region DivPagar

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divPagar.Visible = false;
        }

        protected void btnCobrar_Click(object sender, EventArgs e)
        {
            var pagoId = "";

            foreach (GridViewRow item in grdPagar.Rows)
            {
                if (((CheckBox)item.FindControl("chkSumar")).Checked)
                {
                    pagoId = item.Cells[0].Text;
                }
            }

            _unidadesServ.PagarExpensa(txtImporte.Text.ToDecimal(), 0);
        }

        private void CargarGrillaCobrar(List<UnidadesFuncionalesModel> ufModel)
        {
            grdPagar.DataSource = ufModel;
            grdPagar.DataBind();

        }
        #endregion
    }
}