using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace FabricReports
{
    public abstract class Report:IReport
    {
        public abstract string GenerateReportContent(List<Poetry> poems);
        public abstract void ShowReport(List<Poetry> poems);
        public abstract void GenereateJson(List<Poetry> poems, string filename);
    }
}
