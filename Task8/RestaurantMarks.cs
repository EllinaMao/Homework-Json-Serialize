using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task8
{
/*Задание 8
Разработайте приложение для оценки деятельности ресторана. Пользователь вводит с клавиатуры такую информацию:

Ник;
Электронный адрес;
Номер телефона;
Название ресторана;
Адрес ресторана;
Кухня ресторана;
Контактный номер телефона ресторана;
Оценка ресторана;
Отзыв пользователя о ресторане.
Используя регулярные выражения проверьте данные, введенные в форму. Если пользователь где-то совершил ошибку при вводе, сообщите ему об этом. Если информация была введена успешно, сохраните информацию в файл*/
    internal class RestaurantMarks
    {
        private int reputationMark;
        public string Revew {  get; set; }

        private static void SetPropertyWithValidation(
            string prompt,
            Action<string> propertySetter)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                try
                {
                    var input = Input.UserInput.GetStringFromUser();
                    propertySetter(input);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message} Попробуйте ещё раз.");
                }
            }
        }
        public int ReputationMark
        {
            get => reputationMark;
            set
            {
                if (!Regex.IsMatch(value.ToString(), @"^(1[0-2]|[1-9])$"))
                    throw new ArgumentException("Оценка ресторана должна быть целым числом от 1 до 12.");
                reputationMark = value;
            }
        }
        public void SetRestaurantReputation()
        {
            SetPropertyWithValidation("Введите оценку ресторана:", value => ReputationMark = Convert.ToInt32(value));
        }
    }
}
