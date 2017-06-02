using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UI.Tests.Pages.ManagePage
{
    public partial class ManagePage : BasePage
    {
        public string URL
        {
            get
            {
                return base.url + "Manage";
            }
        }
    }
}
