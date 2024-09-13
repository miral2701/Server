using Microsoft.AspNetCore.SignalR;

namespace Server.KursValut
{
    public class Kurs:Hub
    {
        
        public async Task Send(string currencyPair,decimal rate)
        {
            await Clients.All.SendAsync("ReceiveMessage", currencyPair,rate);
        }

    }
}