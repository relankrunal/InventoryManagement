using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Common
{
    public abstract class Display
    {
        public virtual void DisplayData(string OrderId = "", int FlightNumber = 0, string DepartureAirport = "", string ArrivalAirport = "", int Day = 0)
        {
            Console.WriteLine($"order: { OrderId}  " +
                              $"flightNumber: {FlightNumber} " +
                              $"departure: { DepartureAirport} " +
                              $"arrival: { ArrivalAirport} " +
                              $"day: { Day }");
        }
    }
}
