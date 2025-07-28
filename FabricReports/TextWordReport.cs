using FabricReports;
using Poem.poetry;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
namespace FabricReports
{
    public class TextWordReport : IReport
    {
        private readonly string _searchWord;

        public TextWordReport(string searchWord)
        {
            _searchWord = searchWord;
        }

        public class TextWordReportEntry
        {
            [JsonPropertyName("Индекс")]
            public int Index { get; set; }

            [JsonPropertyName("Название стиха")]
            public string Title { get; set; } = string.Empty;

            [JsonPropertyName("Автор")]
            public string Author { get; set; } = string.Empty;

            [JsonPropertyName("Количество вхождений")]
            public int WordCount { get; set; }
        }

        public string GenerateReportContent(List<Poetry> poems)
        {
            var reportEntries = poems
                .Select((p, index) => new
                {
                    Index = index,
                    Poem = p,
                    Matches = Regex.Matches(p.Text ?? "", $@"\b{Regex.Escape(_searchWord)}\b", RegexOptions.IgnoreCase).Count
                })
                .Where(x => x.Matches > 0)
                .Select(x => new TextWordReportEntry
                {
                    Index = x.Index,
                    Title = x.Poem.Name,
                    Author = x.Poem.Autor,
                    WordCount = x.Matches
                })
                .ToList();

            var builder = new StringBuilder();
            builder.AppendLine($"Отчёт: слово «{_searchWord}» в тексте стихов\n");

            foreach (var entry in reportEntries)
            {
                builder.AppendLine($"[#{entry.Index}] {entry.Title} — {entry.Author}, вхождений: {entry.WordCount}");
            }

            builder.AppendLine($"\nВсего стихов с этим словом: {reportEntries.Count}");

            return builder.ToString();
        }

        public void ShowReport(List<Poetry> poems)
        {
            Console.WriteLine(GenerateReportContent(poems));
        }

        public void GenereateJson(List<Poetry> poems, string filename)
        {
            var reportEntries = poems
                .Select((p, index) => new
                {
                    Index = index,
                    Poem = p,
                    Matches = Regex.Matches(p.Text ?? "", $@"\b{Regex.Escape(_searchWord)}\b", RegexOptions.IgnoreCase).Count
                })
                .Where(x => x.Matches > 0)
                .Select(x => new TextWordReportEntry
                {
                    Index = x.Index,
                    Title = x.Poem.Name,
                    Author = x.Poem.Autor,
                    WordCount = x.Matches
                })
                .ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var json = JsonSerializer.Serialize(reportEntries, options);
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

            var report = JsonSerializer.Deserialize<List<TextWordReportEntry>>(json);

            if (report == null || report.Count == 0)
            {
                Console.WriteLine("Файл пустой или формат некорректен.");
            }

        }
    }
}
