using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Consorcios.UserControls;

namespace WebSistemmas.Common
{
    public static class ConstantesWeb
    {
        public static void MostrarError(string error, Page page)
        {
            ContentPlaceHolder placeHolder = page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControl2ID");
            Error errorUc = (Error)control;

            errorUc.MostrarError(error);
        }
    }
}