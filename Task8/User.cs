using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
    public class User : IEquatable<User>
    {
        public string Username { get; set; }
        public MailAddress Email { get; set; }
        public string PhoneNumber { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User other)
        {
            if (other is null) return false;
            return Username == other.Username;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username, Email?.Address, PhoneNumber);
        }
    }
}
