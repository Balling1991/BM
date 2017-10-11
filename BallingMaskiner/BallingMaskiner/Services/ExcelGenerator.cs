using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BallingMaskiner.Services
{
    public class ExcelGenerator
    {
        public void GetExcel(object data)
        {
            var grid = new GridView();
            grid.DataSource = data;
            grid.DataBind();
        }
    }
}
