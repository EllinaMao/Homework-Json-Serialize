using Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Task4
{/*Задание 4
Пользователь вводит с клавиатуры название ресторана. +

Напишите регулярное выражение для проверки названия ресторана. В названии не может быть символов %, &, ), (.

Задание 5
Пользователь вводит с клавиатуры адрес ресторана.+

Напишите регулярное выражение для проверки адреса ресторана. В названии могут быть только буквы английского алфавита и цифры.

Задание 6
Пользователь вводит с клавиатуры название кухни ресторана.

Напишите регулярное выражение для проверки названия кухни ресторана. В названии могут быть только буквы английского алфавита в любом регистре. Пример названия кухни: italian, ukrainian, georgian, jewish и т.д.

Задание 7
Пользователь вводит с клавиатуры оценку работы ресторана. Напишите регулярное выражение для проверки оценки. Оценка может варьироваться от 1 до 12.

*/
    public class Restorant
    {
        private static string Validate(string value, string pattern, string errorMessage)
        {
            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException(errorMessage);
            return value;
        }
        private string restaurantName;
        private string adress;
        private string kitchenName;
        private string phonenumber;

        public string RestaurantName
        {
            get => restaurantName;
            set => restaurantName = Validate(
                value,
                @"^[^%&()]+$",
                "Название ресторана содержит запрещённые символы (% & ( ))."
            );
        }
        public string Adress
        {
            get => adress;
            set => adress = Validate(
                value,
                @"^[A-Za-z0-9\s]+$",
                "Улица записана некорректно"
            );
        }

        public string KitchenName
        {
            get => kitchenName;
            set => kitchenName = Validate(
                value,
                @"^[A-Za-z]+$",
                "Некорректное название кухни ресторана"
            );
        }

        public string Phonenumber
        {
            get => phonenumber;

            set
            {
                var pattern = new Regex(
                    @"(?<=^|\D)\+?3?8?0\D*(\d{2})\D*(\d{3})\D*(\d{2})\D*(\d{2})\b"
                );

                phonenumber = pattern.Replace(value, "+380-$1-$2-$3-$4");// шаблон для замены
            }
        }


        public Restorant()
        {
            restaurantName = string.Empty;
            adress = string.Empty;
            kitchenName = string.Empty;
            phonenumber = "380123456789";
        }
        public void SetUserRestaurant()
        {
            SetRestaurantName();
        }
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
        public void SetRestaurant()
        {
            SetRestaurantName();
            SetRestaurantAdress();
            SetRestaurantKitchen();
            SetRestaurantPhone();
        }

        public void SetRestaurantName()
        {
            SetPropertyWithValidation("Введите название ресторана:", value => RestaurantName = value);
        }

        public void SetRestaurantAdress()
        {
            SetPropertyWithValidation("Введите адрес ресторана:", value => Adress = value);
        }
        public void SetRestaurantKitchen()
        {
            SetPropertyWithValidation("Введите кухню ресторана:", value => KitchenName = value);
        }
        public void SetRestaurantPhone()
        {
            SetPropertyWithValidation("Введите телефон ресторана:", value => Phonenumber = value);
        }









    }
}
