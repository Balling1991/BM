using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using BallingMaskiner.Models;

namespace BallingMaskiner.Controllers
{
    public class ExcelController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        public void Excel(string model)
        {
            GridView gv = new GridView();

            switch (model)
            {
                case "customers":
                    gv.DataSource = db.Customers.ToList();
                    break;
                case "contacts":
                    gv.DataSource = db.Contacts.ToList();
                    break;
                case "machines":
                    gv.DataSource = db.Machines.ToList();
                    break;
            }

            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=excel.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }
}