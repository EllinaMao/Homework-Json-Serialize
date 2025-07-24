using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace FabricReports
{
    internal interface IReport
    {
        public abstract string GenerateReportContent(List<Poetry> poems);
        public abstract void ShowReport(List<Poetry> poems);
        public abstract void GenereateJson(List<Poetry> poems, string filename);
    }
}
