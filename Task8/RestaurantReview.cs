using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task4;

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
namespace Task8
{
    internal class RestaurantReview
    {
        [JsonPropertyName("Имя пользователя")]
        public string UserName { get; set; }
        [JsonPropertyName("Эмейл")]
        public string Email { get; set; }
        private string userPhone;

        [JsonPropertyName("Название ресторана")]
        public Restorant Restaurant { get; set; } = new();
        [JsonPropertyName("Оценки ресторана")]
        public RestaurantMarks Marks { get; set; } = new();

        [JsonPropertyName("Телефон")]
        public string UserPhone
        {
            get => userPhone;
            set
            {
                var pattern = new Regex(@"(?<=^|\D)\+?3?8?0\D*(\d{2})\D*(\d{3})\D*(\d{2})\D*(\d{2})\b");
                userPhone = pattern.Replace(value, "+380-$1-$2-$3-$4");
            }
        }

        private static string GetValidatedInput(string prompt, string pattern, string error)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Input.UserInput.GetStringFromUser();
                if (Regex.IsMatch(input, pattern))
                    return input;
                Console.WriteLine($"Ошибка: {error} Попробуйте ещё раз.");
            }
        }

        public void FillFromUser()
        {
            UserName = GetValidatedInput("Введите ваш ник:", @"^\w{3,}$", "Ник должен содержать хотя бы 3 символа");
            Email = GetValidatedInput("Введите вашу почту:", @"^[^@\s]+@[^@\s]+\.[^@\s]+$", "Неверный формат почты");
            UserPhone = GetValidatedInput("Введите ваш номер телефона:", @"\d{10,}", "Неверный номер телефона");

            Restaurant.SetRestaurant();            
            Marks.SetRestaurantReputation();       
            Console.WriteLine("Введите отзыв о ресторане:");
            Marks.Revew = Input.UserInput.GetStringFromUser();
        }
        public void SaveToFile(string filename)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var data = new
            {
                UserName = this.UserName,
                Email = this.Email,
                UserPhone = this.UserPhone,
                Restaurant = new
                {
                    Name = Restaurant.RestaurantName,
                    Address = Restaurant.Adress,
                    Kitchen = Restaurant.KitchenName,
                    Phone = Restaurant.Phonenumber
                },
                Rating = Marks.ReputationMark,
                Review = Marks.Revew
            };

            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filename, json);
            Console.WriteLine($"Информация сохранена в файл: {filename}");

        }
    }
}
