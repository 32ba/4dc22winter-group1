using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _32ba
{
    public class CsvReader
    {
        public static bool Read(TextAsset csvFile, List<string[]> list, char separator)
        {
            try
            {
                StringReader reader = new StringReader(csvFile.text);
                while (reader.Peek() != -1)
                {
                    string line = reader.ReadLine();
                    if (line.Contains("\\n")) line = line.Replace(@"\n", Environment.NewLine);
                    list.Add(line.Split(separator));
                }
            }
            catch (FileNotFoundException e)
            {
                Debug.LogError(e);
                return false;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }

            return true;
        }
    }
}

