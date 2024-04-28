using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            TcpClient client = new TcpClient();
            Console.WriteLine("Подключение к серверу...");
            client.Connect("127.0.0.1", 8888); 

            Console.WriteLine("Подключено!");

            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string response;
                while ((response = reader.ReadLine()) != null)
                {
                    Console.WriteLine("Сервер: " + response);
                    if (response == "GAME_START")
                    {
                        PlayGame(reader, writer);
                    }
                    else if (response == "DISCONNECTED")
                    {
                        Console.WriteLine("Сервер отключился.");
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }

        Console.ReadLine();
    }

    static void PlayGame(StreamReader reader, StreamWriter writer)
    {
        for (int round = 1; round <= 5; round++)
        {
            Console.WriteLine($"Раунд {round}");
            Console.WriteLine("Выберите: Камень (r), Ножницы (s), Бумага (p): ");
            string choice = Console.ReadLine();
            writer.WriteLine(choice);
            writer.Flush();

            string opponentChoice = reader.ReadLine();
            Console.WriteLine($"Противник выбрал: {opponentChoice}");

            string result = reader.ReadLine();
            Console.WriteLine($"Результат раунда: {result}");
        }

        string gameResult = reader.ReadLine();
        Console.WriteLine($" {gameResult}");
    }
}
