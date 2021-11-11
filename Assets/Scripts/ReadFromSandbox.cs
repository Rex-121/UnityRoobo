using UnityEngine;
using System.IO;
using System.Diagnostics;

using UniRx;

public class ReadFromSandbox : MonoBehaviour
{
    void Start()
    {


        WebReqeust.GetAudio("", (clip) =>
        {


        }, (e) => {
            Logging.Log("errorororororororo");
            Logging.Log(e);
        });


        Observable.Timer(System.TimeSpan.FromSeconds(20)).Subscribe(_ => {
            K();
        });

        //var b = Path.Combine("file://" + Application.persistentDataPath, "aa/abc.png");

        //WebReqeust.GetTexture(b, (sp) =>
        //{

        //    Sprite s = Sprite.Create(sp, new Rect(Vector2.zero, new Vector2(sp.width, sp.height)), new Vector2(0.5f, 0.5f));

        //    sprite.sprite = s;
        //}, (e) => { });
    }



    void K()
    {
        var path = Application.persistentDataPath;
        Logging.Log("---------------------------");
        Logging.Log(path);
        Logging.Log("---------------------------");

        var a = Path.Combine("file://" + Application.persistentDataPath, "aa/abc.wav");

        Logging.Log(a);

        Stopwatch sw = new Stopwatch();
        sw.Start();

        WebReqeust.GetAudio(a, (clip) =>
        {
            sw.Stop();

            UnityEngine.Debug.Log(string.Format("total: {0} ms", sw.ElapsedMilliseconds));

            GetComponent<AudioSource>().clip = clip;

            GetComponent<AudioSource>().Play();


        }, (e) => {
            Logging.Log("errorororororororo");
            Logging.Log(e);
            sw.Stop();

            UnityEngine.Debug.Log(string.Format("total: {0} ms", sw.ElapsedMilliseconds));
        });

    }

}
