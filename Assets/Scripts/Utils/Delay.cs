using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "延迟调用", menuName = "单例SO/延迟调用")]
public class Delay : SingletonSO<Delay>
{


    private MonoBehaviour mono;



    private void Init()
    {
        if (mono == null)
        {
            var gb = new GameObject("Delay");
            mono = gb.AddComponent<SingletonMonoBehaviour>();
            DontDestroyOnLoad(gb);
        }
    }


    public Coroutine DelayToCall(float delay, Action action)
    {
        Init();
        return mono.StartCoroutine(DelayCall(action, delay));
    }


    IEnumerator DelayCall(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

}

