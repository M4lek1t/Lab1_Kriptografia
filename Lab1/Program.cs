using System;
using System.Linq;

class Program
{
    enum EncryptionType { Encode, Decode }
    const string alf = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz1234567890";

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Шифровать текст.");
            Console.WriteLine("2 - Дешифровать текст.");
            Console.WriteLine("ESC - Завершить программу.");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    MainVigener(EncryptionType.Encode);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    MainVigener(EncryptionType.Decode);
                    break;
                case ConsoleKey.Escape:
                    return;
            }
        }
    }

    //Метод взаимодествия
    static void MainVigener(EncryptionType encryptionType)
    {
        // Проверка ключа на не пустую строку и на принадлежность всех символов алфавиту.
        static bool CheckKeyString(string key)
        => !string.IsNullOrWhiteSpace(key) && key.All(symbol => alf.Contains(symbol));

        //ввод данных
        Console.Clear();
        Console.Write("Введите текст: ");
        string text = Console.ReadLine();
        Console.Write("Введите ключ: ");
        string key = Console.ReadLine();

        //вывод результата
        Console.WriteLine(CheckKeyString(key)
        ? "\r\n" + VigenerEncryption(text, key, encryptionType)
        : "\r\nВы ввели не корректный ключ!");
        Console.WriteLine("\r\nНажмите любую клавишу для выхода...");
        Console.ReadKey(true);
    }

    // Метод шифрования (decrypt:false) / дешифрования (decrypt:true)
    static string VigenerEncryption(string text, string key, EncryptionType encryptionType)
    {
        // индекс текущего символа ключа
        int keyIndex = -1;
        // получить следующий символ ключа
        char NextKeySymbol()
        => key[++keyIndex < key.Length ? keyIndex : keyIndex = 0];
        // зашифровать символ: (t + k) % aLen
        char EncryptSymbol(char symbol)
        => alf[(alf.IndexOf(symbol) + alf.IndexOf(NextKeySymbol())) % alf.Length];
        // расшифровать символ: (t + aLen - k) % aLen
        char DecryptSymbol(char symbol)
        => alf[(alf.IndexOf(symbol) + alf.Length - alf.IndexOf(NextKeySymbol())) % alf.Length];

        // пересобираем строку, выполняя операцию шифрования/дешифрования для символов входящих в алфавит (alf)
        return encryptionType switch
        {
            EncryptionType.Encode => new string(text.Select(s => alf.Contains(s) ? EncryptSymbol(s) : s).ToArray()),
            EncryptionType.Decode => new string(text.Select(s => alf.Contains(s) ? DecryptSymbol(s) : s).ToArray()),
            _ => "",
        };
    }
}
