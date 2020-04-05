﻿using InventoryManagement.Data;
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

        private string BLANK_STRING;

        public Parser()
        {
            _ILog = Log.GetInstance;
        }

        public string parseFileToString(string path)
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

        public List<Schedule> parseStringToSchedule(String input)
        {

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



    }
}
