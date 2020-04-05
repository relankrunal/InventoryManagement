using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Common
{

    public class Logger
    {
        public sealed class Log : ILog
        {
            private Log()
            {
            }

            private static readonly object obj = new object();

            private static Log instance = null;
            public static Log GetInstance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (obj)
                        {
                            if (instance == null)
                                instance = new Log();
                        }
                    }
                    return instance;
                }
            }

            public void LogException(string message)
            {
                string fileName = string.Format("{0}_{1}.log", "Exception", DateTime.Now.ToString("dd MMMM yyyy"));
                string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                               
                    // string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("----------------------------------------");
                sb.AppendLine(DateTime.Now.ToString());
                sb.AppendLine(message);
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.Write(sb.ToString());
                    writer.Flush();
                }
            }

        }

    }
}
