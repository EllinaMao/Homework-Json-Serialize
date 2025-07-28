using FabricReports;
using Poem.poetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FabricReports
{
    public class YearReport : IReport
    {
        sealed class YearReportEntry
        {
            [JsonPropertyName("Год стиха")]
            public string Year { get; set; } = string.Empty;

            [JsonPropertyName("КоличествоСтихов")]
            public int PoemCount { get; set; }

        }

        private string Normalize(string word)
        {
            return string.Join("", word.Where(c => !char.IsWhiteSpace(c)))
                         .ToLowerInvariant();
        }

        private IEnumerable<(string Year, int Count)> GroupReport(List<Poetry> poems)
        {
            return poems
                .GroupBy(p => new
                {
                    Normalized = Normalize(p.YearOfCreation.ToString("yyyy")),
                    Original = p.YearOfCreation.ToString("yyyy")
                })
                .GroupBy(g => g.Key.Normalized)
                .Select(g => (
                    Year: g.First().Key.Original,
                    Count: g.SelectMany(x => x).Count()
                ));
        }

        public string GenerateReportContent(List<Poetry> poems)
        {
            var grouped = GroupReport(poems);

            var builder = new StringBuilder();
            foreach (var group in grouped)
            {
                builder.AppendLine($"Год: {group.Year} — {group.Count} стих(ов)");
            }

            return builder.ToString();
        }

        public void ShowReport(List<Poetry> poems)
        {
            Console.WriteLine(GenerateReportContent(poems));
        }

        public void GenereateJson(List<Poetry> poems, string filename)
        {
            var grouped = GroupReport(poems)
                .Select(g => new
                {
                    ГодСтиха = g.Year,
                    КоличествоСтихов = g.Count
                });

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var json = JsonSerializer.Serialize(grouped, options);
            File.WriteAllText(filename, json);
        }

        public void LoadReportFromJson(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            var json = File.ReadAllText(filename);

            var report = JsonSerializer.Deserialize<List<YearReportEntry>>(json);

            if (report == null || report.Count == 0)
            {
                Console.WriteLine("Файл пустой или формат некорректен.");
                return;
            }

            Console.WriteLine("Отчёт из файла:");
            foreach (var entry in report)
            {
                Console.WriteLine($"Название стиха: {entry.Year}, Кол-во стихов: {entry.PoemCount}");
            }
        }

    }

}

