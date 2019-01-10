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
        AccomodationController accomodationController = new AccomodationController();

        public void AddReservation(string disableDates, string startDate, string endDate, int roomId)
        {
            DateTime dateStart = DateTime.Parse(startDate);
            DateTime dateEnd = DateTime.Parse(endDate);
            DateTime currentdate = dateStart;
            int accomodationId = accomodationController.GetAccountIdByRoom(roomId);
            while (currentdate <= dateEnd)
            {
                disableDates += currentdate.Date.ToString("MM-dd-yyyy") + ",";
                currentdate = currentdate.AddDays(1);
            }
            Clients.All.addFlashMessageForReservation(disableDates, roomId);

            string text = "Ova smestajna jedinica je upravo rezervisana u periodu od " + startDate + " do " + endDate + "!";
            Clients.All.addFlashMessageForAccomodationDetails(text, accomodationId);
        }

        public void AddAccomodation(string name, string country)
        {

            int id = accomodationController.GetLastAccomodation();
            Clients.All.addFlashMessageForAccomodation(name, country, id + 1);
        }
    }
}