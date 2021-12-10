using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System.Linq;
using UnityEditor;

[RequireComponent(typeof(CoursewareSupportList), typeof(CoursewareManager))]
public class CoursewareBasicPlaylist : SerializedMonoBehaviour
{

    [HideInInspector]
    public BehaviorSubject<CoursewarePlayer_SO> coursewareRx;

    [HideInInspector]
    public CoursewareRoundingList roundControl;


    [HideInInspector]
    public CoursewareManager cwManager;


    [LabelText("支持的课件列表"), HideInInspector]
    public CoursewareSupportList supports;

    private void Awake()
    {

        coursewareRx = new BehaviorSubject<CoursewarePlayer_SO>(null);

        roundControl = GetComponent<CoursewareRoundingList>();
        cwManager = GetComponent<CoursewareManager>();
        supports = GetComponent<CoursewareSupportList>();

    }



    RoundIsPlaying _Round;

    [HideInInspector]
    public RoundIsPlaying round
    {
        set
        {
            _Round = value;

            RoundDidChanged();
        }
        get
        {
            return _Round;
        }
    }


    private void Start()
    {
        GetComponent<CoursewareRoundingList>().round
            .Where(round => round != null)
            //.Where(round => SupportRountType(round.type))
            .Subscribe(round =>
            {
                RoundDidNeedChange(round);
                //this.round = round;
            }).AddTo(this);
    }

    public virtual void RoundDidNeedChange(RoundIsPlaying round)
    {

        if (SupportRountType(round.type))
        {
            RoundDidLoaded(round);
        }
    }


    public virtual void RoundDidLoaded(RoundIsPlaying round)
    {
        this.round = round;
    }


    public virtual bool SupportRountType(RoundIsPlaying.Type roundType) { return false; }


    public virtual void RoundDidChanged()
    {

        var all = round.process;

        /// 移除所有不支持的题型
        //all.RemoveAll(v => !supports.supports.ContainsKey(v.type));

        //if (all.Count == 0)
        //{
        //    NextRound();
        //    return;
        //}

        /// 将题型组装
        var leading = MergeRoundProcess(all);



        playlist = leading.next;

        //RoundDidLoaded(round);
        //playlist.previous = null;

    }


    [Title("正在播放的课件"), ShowInInspector, GUIColor("GetButtonColor")]
    [HideIf("@coursewareRx == null || coursewareRx.Value == null")]
    [PropertyOrder(-100)]
    public CoursewarePlayer_SO courseware
    {
        get
        {
            if (coursewareRx == null) return null;

            return coursewareRx.Value;
        }
    }

    CW_OriginContent MergeRoundProcess(List<CW_OriginContent> all)
    {
        CW_OriginContent leading = CW_OriginContent.Empty();

        var start = leading;

        all.ForEach(content =>
        {
            content.previous = start;
            start.next = content;

            start = content;
        });

        return leading;
    }

    [HideInInspector]
    public virtual string description { get; }


    public CoursewarePlayer_SO ReMakeSO(CW_OriginContent content)
    {


        Logging.Log("ReMakeSO -->" + content.type);

        if (!supports.supports.ContainsKey(content.type)) return null;

        var data = content.content;

        return supports.suppoting(content.type).ParseData(data);

    }



    public virtual void Next()
    {
        playlist = playlist.next;
    }

    public void NextRound()
    {
        ClearStage();
        coursewareRx.OnNext(null);
        roundControl.Next();
    }

    public virtual void ClearStage()
    {

    }


    CW_OriginContent _playlist;

    [Title("列表", "$description"), LabelText("课件列表"), PropertySpace(SpaceAfter = 30), ShowInInspector, ReadOnly]
    public CW_OriginContent playlist
    {
        get => _playlist;
        set
        {
            _playlist = value;

            if (_playlist == null)
            {
                NextRound();
            }
            else
            {
                PlayCoursewareAutomatic(_playlist);
            }
        }
    }

    public virtual void PlayCoursewareAutomatic(CW_OriginContent pl)
    {
        PlayCourseware(pl);
    }

    public virtual bool PlayCourseware(CW_OriginContent pl)
    {

        var so = ReMakeSO(pl);

        if (so == null)
        {
            Next();
            return false;
        }
        else
        {
            coursewareRx.OnNext(so);
            return true;
        }
    }



#if UNITY_EDITOR // Editor-related code must be excluded from builds
    private static Color GetButtonColor()
    {
        Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
        return Color.Lerp(Color.red, Color.green, Mathf.Abs(Mathf.Sin((float)EditorApplication.timeSinceStartup)));
    }
#endif
}



