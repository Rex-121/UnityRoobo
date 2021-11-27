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

public class PoemManager : MonoBehaviour
{
    [Required]
    public Canvas canvas;
    [Required]
    public GameObject leftScroll,rightScroll;
    [Required]
    public GameObject scrollBg;
    [Required]
    public List<Text> poemTexts;
    [Required]
    public Image cursor;

    // Start is called before the first frame update
    void Start()
    {
        FPS.Shared.LockFrame();
        CreatePoemClickEvents();

        Observable.EveryEndOfFrame().Take(1).Subscribe(_ =>
        {
            cursorMoveTo(0, false);
        }).AddTo(this);

    }

    private void CreatePoemClickEvents()
    {

        EventTrigger trigger = canvas.gameObject.GetComponent<EventTrigger>();
        if (trigger != null)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => { onPoemDown((PointerEventData)data); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((data) => { onPoemUp((PointerEventData)data); });
            trigger.triggers.Add(entry);
        }
    }

    private void onPoemDown(PointerEventData data)
    {

    }

    private void onPoemUp(PointerEventData data)
    {
        if (data.pointerCurrentRaycast.gameObject.name.Equals("next"))
        {
            closeScroll();
            return;
        }
        cursorMoveTo(getIndexByName(data.pointerCurrentRaycast.gameObject.name), true);
    }

    private void closeScroll()
    {
        DOTweenAnimation leftScrollAnim = getCloseAnim(leftScroll.GetComponents<DOTweenAnimation>());
        if (null != leftScrollAnim) { leftScrollAnim.DOPlay(); }
        DOTweenAnimation rightScrollAnim = getCloseAnim(rightScroll.GetComponents<DOTweenAnimation>());
        if (null != rightScrollAnim) { rightScrollAnim.DOPlay(); }
        DOTweenAnimation scrollBgAnim = getCloseAnim(scrollBg.GetComponents<DOTweenAnimation>());
        if (null != scrollBgAnim) { scrollBgAnim.DOPlay(); }
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
            catch (Exception e)
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
        Logging.Log("poem x:" + poemTexts[index].rectTransform.position.x + "\n" + "poem width:" + poemTexts[index].rectTransform.sizeDelta.x / 2);
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
