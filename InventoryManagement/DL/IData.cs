using InventoryManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DL
{
    public interface IData
    {
        List<Schedule> GetDataFromText(string Path);
        List<Orders> GetDataFromJSON(string Path);
    }
}
