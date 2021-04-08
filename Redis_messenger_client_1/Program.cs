using System;
using StackExchange.Redis;

namespace Redis_messenger_client_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ConfigurationOptions();
            options.EndPoints.Add("localhost", 6379);

            string key, message;

            var redis = ConnectionMultiplexer.Connect(options);
            var db = redis.GetDatabase(5);

            Console.WriteLine("Чтобы продолжить нажмите любую клавишу, для выхода нажмите ESC");
            var consoleKey = Console.ReadKey();
            while (consoleKey.Key != ConsoleKey.Escape)
            {
                Console.Write("Что вы хотите сделать, r - прочитать свои сообщения, w - написать сообщение пользователю: ");
                consoleKey = Console.ReadKey();
                Console.WriteLine();
                switch (consoleKey.Key)
                {
                    case ConsoleKey.W:
                    {
                        Console.Write("Кому вы хотите написать: ");
                        key = Console.ReadLine();
                        Console.Write("Что вы хотите написать: ");
                        message = Console.ReadLine();
                        db.ListRightPush(key, message);
                        break;
                    }
                    case ConsoleKey.R:
                    {
                        Console.Write("Введите имя пользователя: ");
                        key = Console.ReadLine();
                        var result = db.ListRange(key);
                        foreach (var res in result)
                        {
                            Console.WriteLine(res);
                        }
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Вы ввели не правильную букву");
                        break;
                    }
                }
                Console.WriteLine("Чтобы продолжить нажмите любую клавишу, для выхода нажмите ESC");
                consoleKey = Console.ReadKey();
            }
        }
    }
}