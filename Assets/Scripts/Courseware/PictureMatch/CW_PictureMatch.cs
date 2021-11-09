using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CW_PictureMatch : CoursewarePlayer
{



    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private InLinePositions_SO inLine;

    [Header("上排元素")]
    [SerializeField]
    private Transform upper;


    [Header("下排元素")]
    [SerializeField]
    private Transform bottom;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            var gb = Instantiate(itemPrefab);
            gb.transform.SetParent(upper);
            gb.transform.localPosition = inLine.positionDic[4].positions[i];
            var script = gb.GetComponent<PictureMatchItem>();
            script.key = i.ToString();
            script.DidTouchItem += UpperTouch;
        }

        for (int i = 0; i < 4; i++)
        {
            var gb = Instantiate(itemPrefab);
            gb.transform.SetParent(bottom);
            gb.transform.localPosition = inLine.positionDic[4].positions[i];
            var script = gb.GetComponent<PictureMatchItem>();
            script.key = i.ToString();
            script.upsideDown = true;
            script.DidTouchItem += DownTouch;
        }
    }


    [SerializeField]
    private PictureMatchItem latestUpperChoosen;

    void UpperTouch(PictureMatchItem item)
    {

        if (latestUpperChoosen != null)
        {
            if (latestUpperChoosen != item)
            {
                latestUpperChoosen.state = PictureMatchItem.State.normal;
            }
        }

        latestUpperChoosen = item;

        if (TryMatchItems()) return;
    }

    [SerializeField]
    private PictureMatchItem latestDownChoosen;

    void DownTouch(PictureMatchItem item)
    {

        if (latestDownChoosen != null)
        {
            if (latestDownChoosen != item)
            {
                latestDownChoosen.state = PictureMatchItem.State.normal;
            }
        }

        latestDownChoosen = item;


        if (TryMatchItems()) return;
    }

    int matched = 0;

    bool TryMatchItems()
    {
        if (latestUpperChoosen != null && latestDownChoosen != null)
        {
            bool match = latestUpperChoosen.MatchTo(latestDownChoosen);

            latestUpperChoosen = null;

            latestDownChoosen = null;

            if (match)
            {
                matched++;
            }

            if (matched == 4)
            {
                creditDelegate.PlayCreditOnScreen(new Score(), () =>
                {
                  DidEndCourseware(this);
                });
            }

            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
