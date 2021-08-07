using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Paybook.WebUI.UserControls
{
    public partial class DashboardCounterUserControl : System.Web.UI.UserControl
    {

        public string BgColorClass { get; set; }
        public string BgColorHoverClass { get; set; } = "fwt-hover-grey";
        public string RsSymbolColor { get; set; } = "color: #4d5f68;";
        public string Total { get; set; } = "0";
        public string Count { get; set; } = "0";
        public string CountText { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}