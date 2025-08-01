using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task2_3
{
    internal static class FilepathWork
    {
        public static void FindFiles(string path, string mask)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Путь не может быть пустым.");

            if (string.IsNullOrWhiteSpace(mask))
                throw new ArgumentException("Маска не может быть пустой.");

            string[] files = Directory.GetFiles(path, mask, SearchOption.AllDirectories);

            if (files.Length == 0)
            {
                Console.WriteLine("Файлы не найдены.");
            }
            else
            {
                DisplayFiles(files, "Найденные файлы:");
            }
        }

        public static void DeleteFiles(string path, string mask)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Путь не может быть пустым.");

            if (string.IsNullOrWhiteSpace(mask))
                throw new ArgumentException("Маска не может быть пустой.");

            string[] filesToDelete = Directory.GetFiles(path, mask, SearchOption.AllDirectories);

            if (filesToDelete.Length == 0)
            {
                Console.WriteLine("Файлы для удаления не найдены.");
                return;
            }

            DisplayFiles(filesToDelete, "Файлы, которые будут удалены:");

            Console.Write("Вы уверены, что хотите удалить эти файлы? (y/n): ");
            string confirmation = Console.ReadLine();

            if (confirmation?.ToLower() == "y")
            {
                foreach (var file in filesToDelete)
                {
                    File.Delete(file);
                }
                Console.WriteLine("Файлы успешно удалены.");
            }
            else
            {
                Console.WriteLine("Удаление отменено.");
            }
        }

        private static void DisplayFiles(string[] files, string header)
        {
            Console.WriteLine(header);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }
    }
}
