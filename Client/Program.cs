using Microsoft.AspNetCore.SignalR.Client;
using System.Threading;
namespace Client
{
    internal class Program
    {
        static HubConnection connection;

        static void Main(string[] args)
        {
            connection = new HubConnectionBuilder()
                        .WithUrl("https://localhost:7027/kurs")
                        .Build();
            Start();
            connection.On<string>("Receive", (kurs) =>
            Console.WriteLine(kurs));
            Console.ReadLine();
        }
        private async void Button_Click()
        {
            try
            {
                await connection.InvokeAsync("Send", "");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");        
            }
        }

        public static async void Start()
        {
            try
            {
                await connection.StartAsync();
                Console.WriteLine("You connected to server");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Error");         
            }
        }

    }
}