using InventoryManagement.Common;
using InventoryManagement.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.Common.Logger;

namespace InventoryManagement.DL
{
    public class GetDataFromJSON
    {
        private ILog _ILogger;
        private string BLANK_STRING;
        public GetDataFromJSON()
        {
            _ILogger = Log.GetInstance;
        }

        public List<Orders> GetDataFromJSONFile(string input)
        {
            ReadConfig readConfig = ReadConfig.GetInstance;
            
            List<Orders> items = new List<Orders>();

            StringBuilder fileData = new StringBuilder();
            try
            {
                string filePath = Path.GetFullPath(input);

                input = filePath.Replace(@"\bin", "");

                using (StreamReader r = new StreamReader(Convert.ToString(input)))
                {
                    fileData.Append(r.ReadToEnd());
                }

                dynamic deSerializeObject = JsonConvert.DeserializeObject(fileData.ToString());

                foreach (var item in deSerializeObject)
                {
                    items.Add(new Orders(item.Name, Convert.ToString(item.Value["destination"])));
                }

            }
            catch (Exception ex)
            {
                _ILogger.LogException(ex.StackTrace);
            }
            return items;
        }

    }
}