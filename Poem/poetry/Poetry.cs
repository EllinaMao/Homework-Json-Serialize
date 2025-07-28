using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poem.poetry
{/*Задание 1
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
По длине стиха.
    
  
  🔶 1. Паттерн "Repository" (Репозиторий)
Для чего:
– Отделяет логику работы с коллекцией (добавление, удаление, поиск) от остальной логики приложения.
Где применять:
– Класс PoemRepository будет управлять всеми объектами Poem.

🔶 2. Паттерн "Strategy" (Стратегия)
Для чего:
– Позволяет реализовать разные способы фильтрации/поиска стихов (по автору, названию, теме и т.д.).
Где применять:
– Вместо кучи if-else для поиска создаются отдельные классы, реализующие интерфейс ISearchStrategy.

🔶 3. Паттерн "Factory Method" (Фабричный метод)
Для чего:
– Упрощает создание различных отчетов: по теме, длине, году и т.п.
Где применять:
– Абстрактный класс Report, от которого наследуются AuthorReport, TopicReport, и т.д.
– Класс ReportFactory, создающий нужный тип отчета.

🔶 4. Паттерн "Command" (Команда)
Для чего:
– Упрощает реализацию операций "Добавить", "Удалить", "Изменить" как отдельных объектов, особенно если в будущем планируется отмена/повтор.
Где применять:
– Класс AddPoemCommand, DeletePoemCommand и т.д.
– Позволит легко логировать действия или реализовать "отмену".

🔶 5. Паттерн "Memento" (Снимок)
Для чего:
– Позволяет сохранять/восстанавливать состояние коллекции (например, до и после редактирования).
Где применять:
– При сохранении в файл или откате изменений.

🔶 6. Паттерн "Singleton"
Для чего:
– Гарантирует, что менеджер коллекции стихов (PoemManager) будет единственным в приложении.
Где применять:
– Управление центральным доступом к данным.
  */
    public class Poetry
    {
        public string Name { get; set; }
        public string Autor {  get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }
        public DateOnly YearOfCreation{get; set; }

        public Poetry()
            {
                Name = "Unknown";
                Autor = "Unknown";
                Theme = "Unknown";
                Text = "Unknown";
                YearOfCreation = DateOnly.MinValue;

            }
        public Poetry(string name, string autor, string theme, string text, string date)
        {
            Name = name;
            Autor = autor;
            Theme = theme;
            Text = text;
            try
            {
                YearOfCreation = DateOnly.Parse(date, CultureInfo.InvariantCulture);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);//не хочу выводить наружу ошибку в конструкторе
                YearOfCreation = DateOnly.MinValue;
            }
        }
        public Poetry(string name, string theme, string text, DateOnly date)
        {
            Name = name;
            Autor= theme;
            Theme = theme;
            Text = text;
            YearOfCreation = date;
        }
        public override string ToString()
        {
            return $"Название: {Name},\nАвтор: {Autor},\nТема: {Theme},\nСодержание: {Text},\nГод создания: {YearOfCreation.ToString(CultureInfo.InvariantCulture)}\n";
        }


    }
}
