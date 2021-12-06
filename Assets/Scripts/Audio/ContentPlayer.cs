using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

enum PlayerEvent
{
    finish
}

[RequireComponent(typeof(AudioSource))]
public class ContentPlayer: MonoBehaviour
{
    UnityAction<PlayerEvent> callback;

    AudioSource player;
    bool isPlaying = false;

    public void Start()
    {
        var bridge = FindObjectOfType<NativeBridgeListener>();
        if (bridge == null)
        {
            var go = new GameObject("NativeBridge");
            go.AddComponent<NativeBridgeListener>();
        }

        player = GetComponent<AudioSource>();
        //initSvui();
    }

    public void Update()
    {
        UpdatePlayerStatus();
    }

    //private void initSvui()
    //{
    //    SvuiBridge.Shared.initSvui("JDJ5HOluh", "e3079fc40bf7fce29a0edcbf5fa7febb", "JD:813c0b516b3db6c78da42815e90eec4f", true).TakeLast(1)
    //    .DoOnError(e =>
    //    {
    //        Logging.Log("svui init failed:" + e.Message);
    //    })
    //  .Subscribe(success =>
    //  {
    //      Logging.Log("success");
    //  });
    //}

    public void PlayContentByType(string content, string type)
    {
        if(content == null || content.Length == 0 || type == null || type.Length == 0 || type == "type")
        {
            callback?.Invoke(PlayerEvent.finish);
            return;
        }
        if ("tts" == type)
        {
            PlayTTS(content);
        } else if ("audio" == type)
        {
            PlayURL(content);
        }

    }

    public void PlayTTS(string tts)
    {
        SvuiBridge.Shared.tts(tts, SvuiBridge.Language.ENG).TakeLast(1)
          .DoOnError(e =>
          {
              Logging.Log("TTS error:" + e.Message);
          })
            .Subscribe(result =>
            {
                Logging.Log("TTS result:" + result);
                PlayURL(result);
            });
    }

    public void PlayURL(string path)
    {
        PlayParcel(new Parcel(path));
    }

    public void PlayParcel(Parcel parcel)
    {
        HttpRx.GetAudio(parcel.truePath).Take(1).Subscribe(c =>
        {
            player.clip = c;
            player.Play();
            isPlaying = true;
            Logging.Log("playing: " + parcel.truePath);
        }, (e) =>
        {
            Logging.Log("play error:" + e);
        }).AddTo(this);
    }


    void UpdatePlayerStatus()
    {
        if (isPlaying == true && player.isPlaying == false)
        {
            Logging.Log("play finish!");
            isPlaying = false;
            callback?.Invoke(PlayerEvent.finish);
        }
    }



}
