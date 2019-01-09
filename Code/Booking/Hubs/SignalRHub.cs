using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Booking.Controllers;

namespace Booking
{
    public class SignalRHub : Hub
    {
        ReservationController reservationController = new ReservationController();
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
        public void AddReservation(string disableDates,string startDate, string endDate)
        {
            DateTime dateStart = DateTime.Parse(startDate);
            DateTime dateEnd = DateTime.Parse(endDate);
            DateTime currentdate = dateStart;
            while (currentdate <= dateEnd)
            {
                disableDates += currentdate.Date.ToString("MM-dd-yyyy") + ",";
                currentdate = currentdate.AddDays(1);
            }
            Clients.All.addFlashMessageForReservation(disableDates);

        }
    }
}