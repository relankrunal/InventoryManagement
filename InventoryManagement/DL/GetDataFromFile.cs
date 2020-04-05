using InventoryManagement.Common;
using InventoryManagement.Data;
using InventoryManagement.DL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static InventoryManagement.Common.Logger;

namespace InventoryManagement.DL
{
    public class GetDataFromFile : IData
    {
        private ILog _ILogger;
        private string BLANK_STRING;
        public GetDataFromFile()
        {
            _ILogger = Log.GetInstance;
        }
        public List<Orders> GetDataFromJSON(string input)
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

        public List<Schedule> GetDataFromText(string FilePath)
        {
            string input = GetDataFromTextFile(FilePath);
            
            List<Schedule> output = new List<Schedule>();

            int day = -1;
            String[] lines = input.Split('\n');

            try
            {

                foreach (String line in lines)
                {
                    String[] words = line.Split(' ');
                    if (words[0].Equals("Day", StringComparison.InvariantCultureIgnoreCase))
                    {
                        day = int.Parse(words[1].Replace(":", ""));
                    }
                    else if (words[0].Equals("Flight", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Regex regex = new Regex("\\((.*?)\\)");
                        MatchCollection regexMatcher = regex.Matches(line);
                        List<String> airport = new List<String>();
                        if (regexMatcher.Count > 0)
                        {
                            foreach (Match m in regexMatcher)
                                airport.Add(m.Groups[1].Value);
                        }
                        output.Add(new Schedule(int.Parse(words[1].Replace(":", "")), airport[0], airport[1], day));
                    }
                }
            }
            catch (Exception r)
            {
                r.Message.ToString();
            }
            return output;
        }

        public string GetDataFromTextFile(string path)
        {
            StringBuilder result = new StringBuilder();
            string temp = BLANK_STRING;
            try
            {
                var fullPath = Path.GetFullPath(path);
                path = fullPath.Replace(@"\bin", "");
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        temp = line;
                        if (!temp.Equals(BLANK_STRING))
                        {
                            result.Append(temp).Append("\n");
                        }
                    }
                }
            }
            catch (Exception r)
            {

                r.Message.ToString();
            }
            return result.ToString();
        }
    }
}
