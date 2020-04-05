using InventoryManagement.Data;
using InventoryManagement.DL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static InventoryManagement.Common.Logger;

namespace InventoryManagement.Common
{
    public class Parser
    {
        private ILog _ILog;
        string dataSource = string.Empty;
        public Parser()
        {
            _ILog = Log.GetInstance;
        }

        public List<Schedule> DataManager(string type, out List<Orders> orders)
        {
            List<Schedule> schedule = null;
            IData _data = null;
            try
            {
                ReadConfig readConfig = ReadConfig.GetInstance;

                orders = null;
               
                if (type.Equals("FileSystem", StringComparison.InvariantCultureIgnoreCase))
                {
                    _data = new GetDataFromFile();
                    orders = _data.GetDataFromJSON(readConfig.ReadConfigFile()
                                                             .SingleOrDefault(x => x.Key.Equals("FilePath", StringComparison.InvariantCultureIgnoreCase))
                                                             .Value);

                    schedule = _data.GetDataFromText(readConfig.ReadConfigFile()
                                                               .SingleOrDefault(x => x.Key.Equals("Flight-schedule", StringComparison.InvariantCultureIgnoreCase))
                                                               .Value);
                }

            }
            catch (Exception)
            {

                throw;
            }
            
            return schedule;
        }

       
    }
}

