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
        public class LengthReport : IReport
        {
            private sealed class LengthEntry
            {
                [JsonPropertyName("Категория")]
                public string Category { get; set; } = string.Empty;

                [JsonPropertyName("КоличествоСтихов")]
                public int Count { get; set; }
            }

            private string GetCategory(int length)
            {
                return length switch
                {
                    <= 100 => "Короткий",
                    <= 500 => "Средний",
                    _ => "Длинный"
                };
            }

            private IEnumerable<(string Category, int Count)> GroupReport(List<Poetry> poems)
            {
                return poems
                    .GroupBy(p => GetCategory((p.Text ?? "").Length))
                    .Select(g => (Category: g.Key, Count: g.Count()));
            }

            public string GenerateReportContent(List<Poetry> poems)
            {
                var grouped = GroupReport(poems);
                var builder = new StringBuilder();
                builder.AppendLine("Отчёт по длине стихов:\n");

                foreach (var entry in grouped)
                {
                    builder.AppendLine($"Категория: {entry.Category} — {entry.Count} стих(ов)");
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
                    .Select(g => new LengthEntry
                    {
                        Category = g.Category,
                        Count = g.Count
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
                var report = JsonSerializer.Deserialize<List<LengthEntry>>(json);

                if (report == null || report.Count == 0)
                {
                    Console.WriteLine("Файл пустой или формат некорректен.");
                    return;
                }

                Console.WriteLine("Отчёт из файла:");
                foreach (var entry in report)
                {
                    Console.WriteLine($"Категория: {entry.Category}, Кол-во стихов: {entry.Count}");
                }
            }
        }

    }

