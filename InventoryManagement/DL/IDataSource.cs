using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DL
{
    public interface IDataSource
    {
        string GetDataFromJSONFile(string input);

        string GetDataFromTextFile(string input);
       

    }
}
