using InventoryManagement.Common;
using InventoryManagement.Data;
using InventoryManagement.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.Common.Logger;

namespace InventoryManagement
{
    public class Program
    {
        static ILog _ILog;

        static void Main(string[] args)
        {

            Parser parser = new Parser();

            List<Schedule> scheduleList = parser.DataManager("filesystem", out List<Orders> OrderList);

            Console.WriteLine("\n------------------------User Story 1--------------------------\n");

            foreach (Schedule sch in scheduleList)
            {
                Console.WriteLine(sch.ToString());
            }

            var s = scheduleList.Where(x => x.FlightNumber == 1).Select(c => c.ArrivalAirport).ToList()[0];
          

            GetOrderFromFlight(scheduleList, OrderList, s,1);
            ParseOrders(scheduleList, OrderList);
            Console.ReadLine();
        }
        public static void ParseOrders(List<Schedule> ScheduleList, List<Orders> OrderList)
        {
            if (OrderList.Count() == 0 || OrderList == null)
            {
                Console.WriteLine("\n------------------------User Story 2--------------------------\n");
                Console.WriteLine("No Data to print.Please check if there is any error in Debug folder.");
            }
            else
            {
                OrderList.Sort((x, y) => x.OrderID.CompareTo(y.OrderID));

                Dictionary<String, List<Schedule>> scheduleMap = new Dictionary<string, List<Schedule>>();

                List<Schedule> temp = new List<Schedule>();

                try
                {
                    Console.WriteLine("\n------------------------User Story 2--------------------------\n");
                    foreach (var sch in ScheduleList)
                    {
                        var airportPresent = scheduleMap.TryGetValue(sch.ArrivalAirport, out temp);
                        if (!airportPresent)
                        {
                            temp = new List<Schedule>();
                        }
                        else
                        {
                            scheduleMap.Remove(sch.ArrivalAirport);
                        }
                        temp.Add(sch);
                        scheduleMap.Add(sch.ArrivalAirport, temp);
                    }

                    foreach (Orders ord in OrderList)
                    {
                        String destination = ord.Destination;

                        List<Schedule> sch = new List<Schedule>();
                        bool schdefound = scheduleMap.TryGetValue(destination, out sch);

                        if (sch != null && sch.Count() > 0 && sch[0].Boxes == 0)
                        {
                            sch.RemoveAt(0);
                        }
                        if (sch == null || sch.Count() == 0)
                        {
                            Console.WriteLine($"order: {ord.OrderID} flightNumber: not scheduled");
                        }
                        else
                        {
                            Schedule current = sch[0];
                            current.setBoxes(current.Boxes - 1);

                            Console.WriteLine($"order: {ord.OrderID}  " +
                                     $"flightNumber: {current.FlightNumber} " +
                                     $"departure: { current.DepartureAirport} " +
                                     $"arrival: { current.ArrivalAirport} " +
                                     $"day: { current.Day }");
                        }


                    }
                }
                catch (Exception ex)
                {
                    _ILog.LogException(ex.Message + ex.StackTrace);
                }
            }

        }


        public static void GetOrderFromFlight(List<Schedule> ScheduleList, List<Orders> OrderList, string Destination, int  flightNumber)
        {

            if (OrderList.Count() == 0 || OrderList == null)
            {
                Console.WriteLine("\n------------------------User Story 3--------------------------\n");
                Console.WriteLine("No Data to print.Please check if there is any error in Debug folder.");
            }
            else
            {
                OrderList.Sort((x, y) => x.OrderID.CompareTo(y.OrderID));

                Dictionary<String, List<Schedule>> scheduleMap = new Dictionary<string, List<Schedule>>();

                List<Schedule> temp = new List<Schedule>();

                try
                {
                    Console.WriteLine("\n------------------------User Story 3--------------------------\n");
                    foreach (var sch in ScheduleList)
                    {
                        var airportPresent = scheduleMap.TryGetValue(sch.ArrivalAirport, out temp);
                        if (!airportPresent)
                        {
                            temp = new List<Schedule>();
                        }
                        else
                        {
                            scheduleMap.Remove(sch.ArrivalAirport);
                        }
                        if (Destination.Equals(sch.ArrivalAirport))
                        {
                            temp.Add(sch);
                            scheduleMap.Add(sch.ArrivalAirport, temp);
                        }

                    }

                    foreach (Orders ord in OrderList)
                    {
                        //   String destination = ord.Destination;

                        List<Schedule> sch = new List<Schedule>();
                        bool schdefound = scheduleMap.TryGetValue(Destination, out sch);

                        if (sch != null && sch.Count() > 0 && sch[0].Boxes == 0)
                        {
                            sch.RemoveAt(0);
                        }

                        if (sch == null || sch.Count() == 0 && sch[0].FlightNumber == flightNumber)
                        {
                            Console.WriteLine($"order: {ord.OrderID} flightNumber: not scheduled");
                        }
                        else
                        {
                            if (sch[0].FlightNumber == flightNumber)
                            {
                                Schedule current = sch[0];
                                current.setBoxes(current.Boxes - 1);

                                Console.WriteLine($"order: {ord.OrderID}  " +
                                         $"flightNumber: {current.FlightNumber} " +
                                         $"departure: { current.DepartureAirport} " +
                                         $"arrival: { current.ArrivalAirport} " +
                                         $"day: { current.Day }");
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    _ILog.LogException(ex.Message + ex.StackTrace);
                }
            }

        }
    }
}
