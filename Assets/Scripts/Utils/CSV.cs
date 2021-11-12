using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics;

public class CSV
{

    static CSV csv;


    public static CSV Instance
    {
        get
        {
            if (csv == null)
            {
                csv = new CSV();
            }
            return csv;
        }
    }


    private CSV() { }


    public List<string> ReadFromFile(string path, string fileName)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        StreamReader reader = null;
        string a = path;// + "/" + fileName;
        Logging.Log(a);
        //Resources.Load
        try
        {

            reader = File.OpenText(a);
        }
        catch
        {

        }

        List<string> values = new List<string>();

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            values.Add(line);
        }
        reader.Close();
        reader.Dispose();
        sw.Stop();
        Logging.Log("读取文件 " + fileName + " " + sw.ElapsedMilliseconds);
        return values;
    }

}
