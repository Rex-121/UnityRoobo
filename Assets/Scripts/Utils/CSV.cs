using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics;
using BestHTTP;

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

        StreamReader reader = new StreamReader(new MemoryStream(Resources.Load<TextAsset>("adfa").bytes));

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


        Logging.Log("读取----文件 " + fileName + " " + sw.ElapsedMilliseconds);

        KKK();
        //LLL();
        return values;
    }


    void LLL()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Logging.Log("开始读取resource");
        var a = Resources.LoadAll("");
        Logging.Log(a);
        Logging.Log("结束读取resource " + sw.ElapsedMilliseconds);
        sw.Stop();
    }


    void KKK()
    {
        

        Stopwatch sw = new Stopwatch();
        sw.Start();
        Logging.Log("开始读取resource");
        FPS.Shared.LockFrame();

        Logging.Log("结束读取resource " + sw.ElapsedMilliseconds);
        sw.Stop();
    }

}
