using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerEvent
{
    def, loading, playing, pause, resume, stop, interrupt, finish, none
}

[RequireComponent(typeof(AudioSource))]
public class ContentPlayer: MonoBehaviour
{
    public ReactiveProperty<PlayerEvent> status = new ReactiveProperty<PlayerEvent>();

    AudioSource player;
    System.IDisposable disposable;
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
        if(content == null || content.Length == 0 || type == null || type.Length == 0 || type == "none")
        {
            status.Value = PlayerEvent.none;
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
        status.Value = PlayerEvent.loading;
        SvuiBridge.Shared.tts(tts, SvuiBridge.Language.ENG).TakeLast(1)
          .DoOnError(e =>
          {
              status.Value = PlayerEvent.none;
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
        if (isPlaying)
        {
            player.Stop();
            isPlaying = false;
            disposable?.Dispose();
            status.Value = PlayerEvent.interrupt;
            Logging.Log("play interrupt!");
        }
        status.Value = PlayerEvent.loading;
        disposable = HttpRx.GetAudio(parcel.truePath).Take(1).Subscribe(c =>
        {
            player.clip = c;
            player.Play();
            isPlaying = true;
            status.Value = PlayerEvent.playing;
            Logging.Log("play start: " + parcel.truePath);
        }, (e) =>
        {
            status.Value = PlayerEvent.none;
            Logging.Log("play error:" + e);
        }).AddTo(this);
    }

    public void Stop()
    {
        if (isPlaying)
        {
            player.Stop();
            isPlaying = false;
            status.Value = PlayerEvent.stop;
            Logging.Log("play stop!");
        }
    }

    public void Pause()
    {
        if (isPlaying)
        {
            player.Pause();
            isPlaying = false;
            status.Value = PlayerEvent.pause;
            Logging.Log("play pause!");
        }

    }

    public void Resume()
    {
        if (!isPlaying && player.clip != null)
        {
            player.Play();
            isPlaying = true;
            status.Value = PlayerEvent.resume;
            Logging.Log("play resume!");
        }
    }


    void UpdatePlayerStatus()
    {
        if (isPlaying == true && player.isPlaying == false)
        {
            isPlaying = false;
            status.Value = PlayerEvent.finish;
            Logging.Log("play finish!");
        }
    }

}
