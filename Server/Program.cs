
using Microsoft.AspNetCore.SignalR;
using Server.KursValut;
namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();


            var app = builder.Build();

            app.MapHub<Kurs>("/kurs");
            StartCurrencyUpdates(app.Services);
            app.Run();
        }


        private static void StartCurrencyUpdates(IServiceProvider services)
        {
            var random = new Random();

            var hubContext = services.GetService<IHubContext<Kurs>>();

            new Thread(() =>
            {
                while (true)
                {
                    var usdToEur = Math.Round(random.NextDouble() * (1.2 - 1.0) + 1.0, 4);
                    var gbpToEur = Math.Round(random.NextDouble() * (1.2 - 1.0) + 1.0, 4);

                    hubContext.Clients.All.SendAsync("ReceiveMessage", "USD/EUR:", usdToEur);
                    hubContext.Clients.All.SendAsync("ReceiveMessage", "GBP/EUR:", gbpToEur);

                    Thread.Sleep(5000);
                }
            }).Start();
           
        }

    }
}