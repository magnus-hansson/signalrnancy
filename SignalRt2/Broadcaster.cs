
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PlaneSeats;
using System;
using System.Net.Http;
using System.Threading;

namespace SignalRt2
{
    public class Broadcaster
    {
        private readonly static Lazy<Broadcaster> instance = new Lazy<Broadcaster>(() => new Broadcaster(GlobalHost.ConnectionManager.GetHubContext<CpuHub>().Clients));

        //private readonly static Lazy<Broadcaster> _instance2 = new Lazy<Broadcaster>(() => new Broadcaster(GlobalHost.ConnectionManager.GetHubContext<Broadcaster>().Clients));


        private readonly TimeSpan updateInterval = TimeSpan.FromMilliseconds(500);
        private readonly Timer timer;

        private Broadcaster(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            timer = new Timer(BroadcastCpuUsage, null, updateInterval, updateInterval);
        }

        public static Broadcaster Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        private void BroadcastCpuUsage(object state)
        {
            string cpu = GetCurrentCpu();

            //Clients.All.cpuInfo(Environment.MachineName, cpu.ToString());
        }

        private string GetCurrentCpu()
        {
            string currentCpu = "0";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080");

            var response = client.GetAsync("api/cpu").Result;
            if (response.IsSuccessStatusCode)
            {
                currentCpu = response.Content.ReadAsStringAsync().Result.ToString();
            }

            return currentCpu;
        }
    }
}