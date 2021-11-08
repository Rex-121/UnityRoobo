using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ReadFromSandbox : MonoBehaviour
{

    //public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {


        var path = Application.persistentDataPath;
        Debug.Log("---------------------------");
        Debug.Log(path);
        Debug.Log("---------------------------");

        var a = Path.Combine("file://" + Application.persistentDataPath, "aa/abc.wav");

        Debug.Log(a);

        //"https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-09-22/c55gk2et0gb0jnjrog3g.wav"
        WebReqeust.GetAudio(a, (clip) =>
        {
            GetComponent<AudioSource>().clip = clip;

            GetComponent<AudioSource>().Play();
        }, (e) => {
            Debug.Log("errorororororororo");
            Debug.Log(e);
        });


        //var b = Path.Combine("file://" + Application.persistentDataPath, "aa/abc.png");

        //WebReqeust.GetTexture(b, (sp) =>
        //{

        //    Sprite s = Sprite.Create(sp, new Rect(Vector2.zero, new Vector2(sp.width, sp.height)), new Vector2(0.5f, 0.5f));

        //    sprite.sprite = s;
        //}, (e) => { });
    }
    // Update is called once per frame
    void Update()
    {

    }
}
