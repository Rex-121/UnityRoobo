using UnityEngine;

public class SingletonSO<T> : ScriptableObject where T : SingletonSO<T>
{

    private static T instance;

    public static T Shared
    {
        get
        {

            if (instance == null)
            {

                
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
            }

            return instance;
        }
    }


    public static T Instance(string path)
    {
        if (instance == null)
            {
                T assets = Resources.Load<T>("Singleton/" + path);
                instance = assets;
            }

            return instance;
       
    }



}
