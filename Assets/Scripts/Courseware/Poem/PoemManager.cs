using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Newtonsoft.Json;
using UniRx;
using Newtonsoft.Json.Linq;
using UnityEngine.EventSystems;
using System;

public enum PoemTextStatus
{
    NORMAL,
    HIGHTLIGHT,
    LOWLIGHT,
}

[RequireComponent(typeof(ContentPlayer))]
public class PoemManager : CoursewarePlayer
{
    public struct Data
    {
        public string image;
        public List<PoemBean> list;
    }

    public class PoemBean
    {
        public int id;
        public PoemTextStatus poemTextStatus = PoemTextStatus.NORMAL;
        public string audio;
        public string text;
        public string type;
        public int waiting;
    }

    private Data data;

    [Required]
    public Canvas canvas;
    [Required]
    public GameObject leftScroll, rightScroll;
    [Required]
    public GameObject scrollBg;
    [Required]
    public Image image;
    [Required]
    public List<Text> poemTexts;
    [Required]
    public Image cursor;
    [Required]
    public MicrophoneController microphoneController;
    private BehaviorSubject<MicrophoneState> microphoneStateStream = new BehaviorSubject<MicrophoneState>(MicrophoneState.DISABLE);
    private BehaviorSubject<PoemBean> poemTextStream = new BehaviorSubject<PoemBean>(null);
    private IDisposable poemTextStatusDisposable;
    private IDisposable poemPlayDisposable;
    private ContentPlayer contentPlayer;

    public void setData(Data data)
    {
        this.data = data;

        setupPoem();
        setupMicrophone();
    }

    private void setupPoem()
    {
        for (int i = 0; i < Math.Min(data.list.Count, 11); i++)
        {
            data.list[i].id = i;
            poemTexts[i].gameObject.GetComponent<PoemTextController>().setup(data.list[i], poemTextStream);
        }
        if (data.list.Count < 11)
        {
            for (int i = 10; i > data.list.Count - 1; i--)
            {
                poemTexts[i].gameObject.SetActive(false);
            }
        }
        image.GetComponent<UISpriteLoader>().setURL(data.image);
        Observable.EveryEndOfFrame().Take(1).Subscribe(_ =>
        {
            cursorMoveTo(0, false);
        }).AddTo(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        contentPlayer = GetComponent<ContentPlayer>();
        FPS.Shared.LockFrame();
        CreatePoemClickEvents();
    }

    private void setupMicrophone()
    {
        microphoneController.setStateStream(microphoneStateStream);
    }

    private void CreatePoemClickEvents()
    {

        EventTrigger trigger = canvas.gameObject.GetComponent<EventTrigger>();
        if (trigger != null)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((data) => { onPoemUp((PointerEventData)data); });
            trigger.triggers.Add(entry);
        }
    }

    private void onPoemUp(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerCurrentRaycast.gameObject.name.Equals("next"))
        {
            closeScroll();
        }
        else if (pointerEventData.pointerCurrentRaycast.gameObject.name.Equals("audio"))
        {
            playPoem(0);
        }
        else
        {
            onPoemClick(pointerEventData);
        }
    }

    private void onPoemClick(PointerEventData pointerEventData)
    {
        int clickIndex = getIndexByName(pointerEventData.pointerCurrentRaycast.gameObject.name);
        if (clickIndex == -1 || clickIndex > poemTexts.Count - 1)
        {
            return;
        }
        //麦克风录音动画
        microphoneStateStream.OnNext(MicrophoneState.DISABLE);
        //诗句动画
        data.list[clickIndex].poemTextStatus = PoemTextStatus.LOWLIGHT;
        poemTextStream.OnNext(data.list[clickIndex]);
        cursorMoveTo(clickIndex, true);
        if (null != poemTextStatusDisposable)
        {
            poemTextStatusDisposable.Dispose();
        }

        playPoem(clickIndex, (index) =>
        {
            //麦克风录音动画
            microphoneController.recordDuration = data.list[index].waiting;
            microphoneStateStream.OnNext(MicrophoneState.RECORDING);
            
            poemTextStatusDisposable = Observable.Timer(TimeSpan.FromSeconds(data.list[index].waiting)).Subscribe((_) =>
            {

                //诗句动画
                data.list[index].poemTextStatus = PoemTextStatus.NORMAL;
                poemTextStream.OnNext(data.list[index]);
            }).AddTo(this);
        });
    }

    //播放整诗
    private void playPoem(int start)
    {
        if (start >= data.list.Count) {
            Logging.Log("whole poem play finish~");
            //诗句动画
            data.list[0].poemTextStatus = PoemTextStatus.NORMAL;
            poemTextStream.OnNext(data.list[0]);
            cursorMoveTo(0, true);
            return;
        }

        //麦克风录音动画
        microphoneStateStream.OnNext(MicrophoneState.DISABLE);
        //诗句动画
        data.list[start].poemTextStatus = PoemTextStatus.HIGHTLIGHT;
        poemTextStream.OnNext(data.list[start]);
        cursorMoveTo(start, true);

        playPoem(start, (index)=> {
            playPoem(index+1);
        });
    }

    private void playPoem(int index, Action<int> then)
    {
        contentPlayer.PlayContentByType(data.list[index].audio, "audio");

        poemPlayDisposable?.Dispose();
        contentPlayer.status = new ReactiveProperty<PlayerEvent>();
        poemPlayDisposable=contentPlayer.status.Subscribe((status) =>
        {
            Logging.Log("content player status:" + status);
            if (status == PlayerEvent.finish)
            {
                then(index);
            }
        }).AddTo(this);
    }

    private void closeScroll()
    {
        stopAll();
        DOTweenAnimation leftScrollAnim = getCloseAnim(leftScroll.GetComponents<DOTweenAnimation>());
        if (null != leftScrollAnim) { leftScrollAnim.DOPlay(); }
        DOTweenAnimation rightScrollAnim = getCloseAnim(rightScroll.GetComponents<DOTweenAnimation>());
        if (null != rightScrollAnim) { rightScrollAnim.DOPlay(); }
        DOTweenAnimation scrollBgAnim = getCloseAnim(scrollBg.GetComponents<DOTweenAnimation>());
        if (null != scrollBgAnim) { scrollBgAnim.DOPlay(); }
        Observable.Timer(TimeSpan.FromSeconds(3)).Take(1).Subscribe((_) =>
        {
            Logging.Log("poem end!!!");
            DidEndCourseware(this);
        }).AddTo(this);
    }

     void OnDestroy()
    {
        stopAll();
    }

    private void stopAll() {
        //麦克风录音动画
        microphoneStateStream.OnNext(MicrophoneState.DISABLE);

        contentPlayer?.Stop();
        poemPlayDisposable?.Dispose();
        poemTextStatusDisposable?.Dispose();
    }

    private DOTweenAnimation getCloseAnim(DOTweenAnimation[] anims)
    {
        for (int i = 0; i < anims.Length; i++)
        {
            Logging.Log(anims[i].id);
            if (anims[i].id.Equals("close"))
            {
                return anims[i];
            }
        }
        return null;
    }

    private int getIndexByName(string name)
    {
        if (name.Equals("title"))
        {
            return 0;
        }
        else if (name.Equals("author"))
        {
            return 1;
        }
        else
        {
            string i = name.Replace("content", "");
            try
            {
                return int.Parse(i) + 1;
            }
            catch
            {
                return -1;
            }
        }
    }

    private void cursorMoveTo(int index, bool withAnim)
    {
        if (index == -1 || index > poemTexts.Count - 1)
        {
            return;
        }
        Vector3 end = new Vector3(poemTexts[index].rectTransform.position.x - poemTexts[index].rectTransform.sizeDelta.x / 2 * canvas.transform.localScale.x,
            poemTexts[index].rectTransform.position.y, cursor.transform.position.z);
        if (withAnim)
        {
            cursor.rectTransform.DOMove(end, 0.2f);
        }
        else
        {
            cursor.rectTransform.position = end;
        }
    }
}
