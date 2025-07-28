using Poem.poetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricReports
{
    public interface IReport
    {
        string GenerateReportContent(List<Poetry> poems);
        void ShowReport(List<Poetry> poems);
        void GenereateJson(List<Poetry> poems, string filename);
        void LoadReportFromJson(string filename);

    }
}
