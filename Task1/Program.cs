using FabricReports;
using Poem.poetry;
using System.ComponentModel;

namespace Task1
{
    /*Задание 1
Создайте приложение для работы с коллекцией стихов. Необходимо хранить такую информацию:

Название стиха;
ФИО автора;
Год написания;
Текст стиха;
Тема стиха.
Приложение должно позволять:

Добавлять стихи;
Удалять стихи;
Изменять информацию о стихах;
Искать стих по разным характеристикам;


Сохранять коллекцию стихов в файл;
Загружать коллекцию стихов из файла.


Приложение должно иметь возможность генерировать отчёты. Отчёт может быть отображён на экран или сохранён в файл. Создайте такие отчёты:

По названию стиха;
По ФИО автора;
По теме стиха;
По слову в тексте стиха;
По году написания стиха;
По длине стиха.*/
    internal class Program
    {
        static void Main(string[] args)
        {
            var filename = "repository.json";
            PoetryManager.Instance.LoadFromFile(filename);
            var results = PoetryManager.Instance.FindByAuthor("лермонтов");
        
            foreach (var p in results)
            {
                Console.WriteLine($"Название: {p.Name}, Автор: {p.Autor}");
            }

            Console.ReadLine();

            PoetryManager.Instance.AddPoem(new Poetry
            {
                Name = "Долгий путь домой",
                Autor = "Валерий Петросян",
                Theme = "Поэзия",
                Text = "Ехал Грека через реку, видит Грека в реке рак. Сунул Грека руку в реку. Рак за руку Греку ХАП",
                YearOfCreation = new DateOnly(2007, 12, 23)

            });
            var result = PoetryManager.Instance.GetAllPoems();
            PoetryManager.Instance.ShowAllPoems();
            PoetryManager.Instance.RemovePoem(result.Count-1);

            IReport autorReport = new AutorReport();
            IReport PoetryNameReport = new PoetryNameReport();
            IReport ThemeReport = new ThemeReport();
            IReport TextWordReport = new TextWordReport("в");
            IReport YearReport = new YearReport();
            IReport LenthReport = new LengthReport();

            autorReport.ShowReport(PoetryManager.Instance.GetAllPoems());
            autorReport.GenereateJson(PoetryManager.Instance.GetAllPoems(), "autorReport.json");

            PoetryNameReport.GenereateJson(PoetryManager.Instance.GetAllPoems(), "PoetryNameReport.json");

            ThemeReport.GenereateJson(PoetryManager.Instance.GetAllPoems(), "ThemeReport.json");
            TextWordReport.GenereateJson(PoetryManager.Instance.GetAllPoems(), "TextWordReport.json");
            YearReport.GenereateJson(PoetryManager.Instance.GetAllPoems(), "YearReport.json");
            LenthReport.GenereateJson(PoetryManager.Instance.GetAllPoems(), "LenthReport.json");
            PoetryManager.Instance.SaveToFile(filename);

        }
    }
}
