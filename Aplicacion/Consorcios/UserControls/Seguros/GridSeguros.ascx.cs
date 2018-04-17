﻿using DAO;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.UserControls.Seguros
{
    public partial class GridSeguros : System.Web.UI.UserControl
    {
        private ISegurosNeg _segurosNeg;
        private ISegurosServ _segurosServ;

        #region Metodos Privados
        private void MostrarError(string error)
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControl2ID");
            Error errorUc = (Error)control;

            errorUc.MostrarError(error);
        }

        private void LlenarGrillaSeguros()
        {
            grdSeguros.DataSource = _segurosNeg.GetSeguros();
            grdSeguros.DataBind();
        }

        #endregion

        public GridSeguros()
        {
            ExpensasEntities context = new ExpensasEntities();
            _segurosServ = new segurosServ(context);
            _segurosNeg = new SegurosNeg(_segurosServ);

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    LlenarGrillaSeguros();
                }
                catch (Exception ex)
                {
                    MostrarError(ex.Message);
                }
            }
        }

    }
}