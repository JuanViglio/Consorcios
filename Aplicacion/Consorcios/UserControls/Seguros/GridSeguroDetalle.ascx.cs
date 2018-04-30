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

namespace WebSistemmas.Consorcios.UserControls.Seguros
{
    public partial class GridSeguroDetalle : UserControl
    {
        ISegurosNeg _segurosNeg;
        ISegurosServ _segurosServ;
        IConsorciosServ _consorciosServ;

        public GridSeguroDetalle()
        {
            ExpensasEntities context = new ExpensasEntities();
            _segurosServ = new segurosServ(context);
            _consorciosServ = new consorciosServ(context);
            _segurosNeg = new segurosNeg(_segurosServ, _consorciosServ);
        }

        #region Metodos Privados
        private void MostrarError(string error)
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControlError");
            Error errorUc = (Error)control;

            errorUc.MostrarError(error);
        }
        #endregion

        public void ActualizarGrillaSeguro(List<SeguroDetalleModel> detalleSeguro)
        {
            grdSeguros.DataSource = detalleSeguro;
            grdSeguros.DataBind();

            Session["SegurosDetalle"] = detalleSeguro;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "GuardarSeguro", "$('#divGuardarSeguro').slideDown();", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void grdSeguros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow GridViewrow = null;
            MostrarError(string.Empty);

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton _ImgButton = (ImageButton)e.CommandSource;
                    GridViewrow = (GridViewRow)_ImgButton.NamingContainer;
                    string Tipo = e.CommandName.ToUpper();

                    switch (Tipo)
                    {
                        case "MODIFICAR":
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ImporteSeguros", "$('#divImporteSeguros').slideDown();", true);
                            txtImporte.Text = GridViewrow.Cells[2].Text;
                            Session["CuotaParaModificar"] = GridViewrow.Cells[0].Text;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarError(string.Empty);
                var detalle = (List<SeguroDetalleModel>)Session["SegurosDetalle"];
                int cuota = int.Parse(Session["CuotaParaModificar"].ToString()) - 1;
                detalle[cuota].Importe =  decimal.Parse(txtImporte.Text);

                ActualizarGrillaSeguro(detalle);
            }
            catch
            {
                MostrarError("No se pudo Modificar el Importe");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ImporteSeguros", "$('#divGuardarSeguro').slideDown();", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {            
                MostrarError(string.Empty);
                var seguroModelo = (SegurosModel)Session["Seguro"];
                var segurosDetalleModelo = (List<SeguroDetalleModel>)Session["SegurosDetalle"]; 
                           
                _segurosNeg.GuardarSeguro(seguroModelo, segurosDetalleModelo);

                Response.Redirect("Seguros.aspx#seguros");
            }
            catch (Exception)
            {
                MostrarError("No se pudo Guardar el Seguro");
            }
        }
    }
}