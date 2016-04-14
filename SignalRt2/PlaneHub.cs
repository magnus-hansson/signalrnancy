using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SignalRt2;

namespace PlaneSeats
{
    public class PlaneHub : Hub
    {
        //just added to create dummy user Id :)
        static int userId;

        private static List<PlaneSeatsArrangement> allSeats = new List<PlaneSeatsArrangement>();

        public void CreateUser()
        {
            userId++;
            Clients.All.createUser(userId);
        }

        public void PopulateSeatData()
        {
            var returnData = JsonConvert.SerializeObject(allSeats);
            Clients.All.populateSeatData(returnData);
        }

        public void SelectSeat(int userId, int seatNumber)
        {
            //create document model
            var planeSeatsArrangement = new PlaneSeatsArrangement() { SeatNumber = seatNumber, UserId = userId };
            allSeats.Add(planeSeatsArrangement);
            var returnData = JsonConvert.SerializeObject(planeSeatsArrangement);
            Clients.All.selectSeat(returnData);
           
        }
    }

    public class CpuHub : Hub
    {
        private readonly Broadcaster broadCaster;

        public CpuHub()
            : this(Broadcaster.Instance)
        {

        }

        public CpuHub(Broadcaster broadCaster)
        {
            this.broadCaster = broadCaster;
        }
    }

    public class PlaneSeatsArrangement
    {
        [JsonProperty(PropertyName = "userid")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "seatnumber")]
        public int SeatNumber { get; set; }
    }
}