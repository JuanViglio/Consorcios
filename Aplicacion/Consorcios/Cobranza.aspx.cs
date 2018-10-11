﻿using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Cobranza : System.Web.UI.Page
    {
        private IConsorciosServ _consorciosServ;
        private IUnidadesServ _unidadesServ;
        private dueñosServ _propietariosServ;
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
            ddlUF.DataSource = _unidadesServ.GetUnidadesFuncionales(idConsorcio);
            ddlUF.DataTextField = "UF";
            ddlUF.DataValueField = "Id";
            ddlUF.DataBind();
        }

        private void CargarComboPropietarios()
        {
            ddlPropietario.DataSource = _propietariosServ.GetDueñosCombo();
            ddlPropietario.DataTextField = "Apellido_y_Nombre";
            ddlPropietario.DataValueField = "ID";
            ddlPropietario.DataBind();
        }

        private void CargarGrillaUF(decimal idPropietario)
        {
            grdUF.DataSource = _unidadesServ.GetUFByPropietarioId(idPropietario);
            grdUF.DataBind();
        }

        #endregion

        #region Constructor y Page Load
        public Cobranza()
        {
            _consorciosServ = new consorciosServ(context);
            _unidadesServ = new unidadesFuncionalesServ();
            _propietariosServ = new dueñosServ();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.CargarTitulo("Cobranza");
                CargarComboConsorcios();
                CargarComboPropietarios();
                CargarGrillaUF(ddlPropietario.SelectedValue.ToDecimal());
            }
        }
        #endregion

        protected void ddlConsorcios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboUF(ddlConsorcios.SelectedValue);
        }

        protected void ddlPropietario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaUF(ddlPropietario.SelectedValue.ToDecimal());
        }

        protected void btnAceptarPropietario_Click(object sender, EventArgs e)
        {
            List<UnidadesFuncionalesModel> ufModel = new List<UnidadesFuncionalesModel>();

            foreach (GridViewRow item in grdUF.Rows)
            {
                if (((CheckBox)item.FindControl("chkSumar")).Checked)
                {
                    ufModel.Add(new UnidadesFuncionalesModel() {
                        Direccion = item.Cells[0].Text,
                        UF = item.Cells[1].Text ,
                        ID = item.Cells[2].Text.ToInt()
                    });
                }                
            }

            grdPagar.DataSource = ufModel;
            grdPagar.DataBind();
        }
    }
}