using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input
{
    public static class UserInput
    {
        public static int GetIntFromUser()
        {
            try
            {
                int number = Convert.ToInt32(Console.ReadLine());
                //Console.Beep(); //^-^
                return number;
            }
            catch (Exception ex)
            {
                throw new FormatException("Введено некорректное значение. Ожидалось целое число.");
            }
        }


        public static string GetStringFromUser()
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Ввод не может быть пустым или состоять только из пробелов.");
            }
            return input.Trim(); 
        }

    }


}
