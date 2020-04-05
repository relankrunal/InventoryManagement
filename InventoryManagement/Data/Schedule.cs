using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Data
{
    public class Schedule
    {
        private int _flightNumber;
        private string _departureAirport;
        private string _arrivalAirport;
        private int _day;
        private int _boxes;
        public int FlightNumber
        {
            get
            {
                return _flightNumber;
            }
            set
            {
                _flightNumber = value;
            }
        }
        public string DepartureAirport
        {
            get
            {
                return _departureAirport;
            }
            set
            {
                _departureAirport = value;
            }
        }
        public string ArrivalAirport
        {
            get
            {
                return _arrivalAirport;
            }
            set
            {
                _arrivalAirport = value;
            }
        }
        public int Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day= value;
            }
        }
        public int Boxes
        {
            get
            {
                return _boxes;
            }
            set
            {
                _boxes = value;
            }
        }

        public void setBoxes(int boxes)
        {
            this._boxes = boxes;
        }
        public Schedule(int flightNumber, String departureAirport, String arrivalAirport, int day)
        {
            this._flightNumber = flightNumber;
            this._departureAirport = departureAirport;
            this._arrivalAirport = arrivalAirport;
            this.Day = day;
            Boxes = 20;
        }

        public override String ToString()
        {
            return "Flight: " + FlightNumber
                    + " departure: " + _departureAirport
                    + " arrival: " + ArrivalAirport
                    + " day: " + Day;
        }
    }
}
