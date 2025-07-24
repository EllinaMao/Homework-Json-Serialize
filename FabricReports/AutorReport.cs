using FabricReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;
using System.Text.Json;

namespace FabricReports
{
    public class AutorReport : Report, IReport
    {
        public override string GenerateReportContent(List<Poetry> poems)
        {
            var grouped = poems.GroupBy(p => p.Autor);
            var builder = new StringBuilder();

            foreach (var group in grouped)
            {
                builder.AppendLine($"Автор: {group.Key} — {group.Count()} стих(ов)");
            }

            return builder.ToString();
        }
        public override void ShowReport(List<Poetry> poems) {

            Console.WriteLine(GenerateReportContent(poems));
        }
        public override void GenereateJson(List<Poetry> poems, string filename)
        {
            var grouped = poems.GroupBy(p => p.Autor)
                               .Select(g => new
                               {
                                   Автор = g.Key,
                                   КоличествоСтихов = g.Count()
                               })
                               .ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(grouped, options);
            File.WriteAllText(filename, json);
        }

    }
}

