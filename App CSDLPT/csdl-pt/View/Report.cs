using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace csdl_pt.View
{
    class Report
    {
        public string Name { get; set; }
        public Page Page { get; set; }

        public Report(string name, Page page)
        {
            this.Name = name;
            this.Page = page;
        }
    }
}
