using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Common
{
    public sealed class ReadConfig
    {
        private ReadConfig()
        {
        }

        private static readonly object obj = new object();

        private static ReadConfig instance = null;

        public static ReadConfig GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                            instance = new ReadConfig();
                    }
                }
                return instance;
            }
        }

        public Dictionary<string, string> ReadConfigFile()
        {

            Dictionary<string, string> appSettingData;
            try
            {

                appSettingData = new Dictionary<string, string>();
                appSettingData.Add("DataSource", ConfigurationManager.AppSettings["DataSource"]);
                appSettingData.Add("FilePath", ConfigurationManager.AppSettings["JSONFilePath"]);
                appSettingData.Add("Flight-schedule", ConfigurationManager.AppSettings["Flight-schedule"]);
            }
            catch (Exception)
            {

                throw;
            }
            return appSettingData;
        }
    }
}
