using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Data
{
   public class Orders
    {
        private string _orderID;
        private string _destination;
        public string OrderID 
        {
            get
            {
               return _orderID;
            }
            set 
            {
                _orderID = value;
            } 
        }
        public string Destination 
        {
            get 
            {
                return _destination;
            }
            set 
            {
                _destination = value;
            } 
        }

        public Orders(string orderId, string destination)
        {
            this.OrderID = orderId;
            this.Destination = destination;
        }
        public override String ToString()
        {
            return "Orders{" +
                    "orderId=" + OrderID +
                    ", destination='" + Destination + '\'' +
                    '}';
        }

    }

   
}
