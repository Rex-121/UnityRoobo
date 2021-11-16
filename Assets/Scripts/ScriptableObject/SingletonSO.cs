using UnityEngine;
using Sirenix.OdinInspector;

using System.Diagnostics;

public class SingletonSO<T> : SerializedScriptableObject where T : SingletonSO<T>
{

    private static T instance;

    public static T Shared
    {
        get
        {

            if (instance == null)
            {

                Stopwatch sw = new Stopwatch();

                sw.Start();

                T[] assets = Resources.LoadAll<T>("");

                if (assets == null || assets.Length < 1)
                {
                    throw new System.Exception("没有此单例！");
                }
                else if (assets.Length > 1)
                {
                    Logging.Log("多个单例");
                }
                instance = assets[0];

                sw.Stop();

                Logging.Log("单例耗时" + instance + " " + sw.ElapsedMilliseconds);
            }

            return instance;
        }
    }


    public static T Instance(string path)
    {
        if (instance == null)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            T assets = Resources.Load<T>("Singleton/" + path);
            instance = assets;

            sw.Stop();

            Logging.Log("Ins单例耗时" + path + " " + sw.ElapsedMilliseconds);
        }

        return instance;

    }



}
