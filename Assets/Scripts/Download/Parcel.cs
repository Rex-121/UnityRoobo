using System;
using UnityEngine;

public struct Parcel
{

    public string path;


    public Parcel(string p)
    {
        path = p;
    }

    public static Parcel FromLocal(string name)
    {
        return new Parcel("/" + name);
    }


    public enum ComesFrom
    {
        Cloud, Local
    }

    public string fileName
    {
        get
        {
            string[] a = path.Split(new char[]{'/'});
            if (a.Length == 0) return path;
            return a[a.Length - 1];
        }
    }

    public ComesFrom comesFrom
    {
        get
        {
            if (path.StartsWith("http://") || path.StartsWith("https://"))
            {
                return ComesFrom.Cloud;
            }
            return ComesFrom.Local;
        }
    }


    public string truePath
    {
        get
        {
            switch (comesFrom)
            {
                case ComesFrom.Cloud:
                    return path;
                case ComesFrom.Local:
                    return "file://" + Application.persistentDataPath + path;
            }
            return path;
        }
    }

}
