
using UnityEngine;
using UniRx;

using Michsky.LSS;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;

public class LoginManager : MonoBehaviour
{

    struct ABC
    {
        public string account;// = "50000000593";
        public string password;

        public ABC(string a, string p)
        {
            account = a;
            password = p;
        }
    }


    public LoadingScreenManager lsm;

    AsyncOperation async;


    // Start is called before the first frame update
    void Start()
    {

        var d = NativeBridge.Default;

        Logging.Log(User.Default.token.account);

        HttpRx.Post<User.Token>("pudding/manager/v1/provisional/tempAccount/login", new ABC("50000000593", "lu23t0gk5110")).Subscribe((r) =>
        {
            User.Default.token = r;
            Logging.Log(r.accessToken);
            Logging.Log("fasd");
        }, (e) =>
         {
             Logging.Log("fasd");
             Logging.Log("error");
             Logging.Log(e.Message);
         }, () => { Logging.Log("com"); }).AddTo(this);

        //StartCoroutine(LoadSceneAsync());

        //Observable.EveryEndOfFrame().Take(1).SelectMany(LoadLineAsync).Subscribe().AddTo(this);

        //Observable.EveryEndOfFrame().Take(1).SelectMany(LoadSceneAsync).Subscribe().AddTo(this);
    }

    private void OnMouseDown()
    {

        //SceneManager.LoadScene("SampleScene");
        //lsm.LoadScene("SampleScene");

        //async.allowSceneActivation = true;


        gameObject.SetActive(false);

    }



    IEnumerator LoadSceneAsync()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();
        async = SceneManager.LoadSceneAsync("SampleScene");
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
        {
            Logging.Log("加载进度" + async.progress);
            yield return new WaitForEndOfFrame();
        }

        sw.Stop();

        Logging.Log("加载完成 " + sw.ElapsedMilliseconds + "ms");
        Logging.Log("加载完成" + async.progress);
    }

}


