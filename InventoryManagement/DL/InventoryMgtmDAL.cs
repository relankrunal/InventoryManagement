using InventoryManagement.Common;
using InventoryManagement.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public static class InventoryMgtm : IDataSource
    {

        public static IDataSource CreateDataSource(string dataSource)
        {
            if (dataSource.Equals("JSON"))
            {
                return new GetDataFromText();
            }
            else if (dataSource.Equals("text"))
            {
                return new GetDataFromText();
            }
            else
                return null;
        }

        public string GetDataFromJSONFile(string input)
        {
            throw new NotImplementedException();
        }

        public string GetDataFromTextFile(string input)
        {
            throw new NotImplementedException();
        }
    }
}
