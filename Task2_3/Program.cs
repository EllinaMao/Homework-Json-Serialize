using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task2_3
{
    internal class Program
    {
        /*Задание 2
Разработайте приложение для поиска файлов по маске. Пользователь вводит путь к папке и маску для поиска. Например:

D:\DataForUser
*.txt

Приложение должно отобразить все файлы с расширением txt по пути D:\DataForUser. Поиск должен происходить в папках и подпапках.

Задание 3
Разработайте приложение для удаления файлов по маске. Пользователь вводит путь к папке и маску для поиска удаляемых файлов. Например:

D:\DataForUser
*.txt*/
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу");
            string? Path = Console.ReadLine();
            Console.WriteLine("Введите маску");
            string? Mask = Console.ReadLine();

            if (!Directory.Exists(Path))
            {
                Console.WriteLine("Указанная папка не существует.");
                return;
            }

            try
            {

                FilepathWork.FindFiles(Path, Mask);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Введите путь к файлу");
            Path = Console.ReadLine();
            Console.WriteLine("Введите маску");
            Mask = Console.ReadLine();
            try
            {
                FilepathWork.DeleteFiles(Path, Mask);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

}
