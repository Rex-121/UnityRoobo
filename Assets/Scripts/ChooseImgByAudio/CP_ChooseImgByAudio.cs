using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

/// <summary>
/// 音选图
/// </summary>
[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(AudioSource))]
public class CP_ChooseImgByAudio : CoursewarePlayer
{

    [LabelText("需要展示的模版")]
    public ChooseImgByAudioTemplate template;

    public CP_ChooseImgByAudioBridge_SO dataBridge;

    [LabelText("模版")]
    public Dictionary<ChooseImgByAudioTemplate, ChooseImgByAudioTemplateStruct> templates = new Dictionary<ChooseImgByAudioTemplate, ChooseImgByAudioTemplateStruct>();

    [LabelText("反馈音效")]
    public SoundRightWrong_SO soundEffect;


    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {

        var item = templates[template];


        var length = 4;

        var positions = item.inLinePositions.GetPositionByLength(length);

        for (int i = 0; i < length; i++)
        {
            var position = positions.positions[i];

            item.itemSO.isTheRightOption = i == 2;

            GetComponent<SpriteRenderer>().sprite = item.background;

            var gb = item.itemSO.CreateData();

            gb.transform.parent = transform;

            gb.transform.localPosition = position;

        }

    }

    private void OnEnable()
    {
        dataBridge.action += DidChooseItem;
    }

    private void OnDisable()
    {
        ClearCoursewarePlayer();
        dataBridge.action -= DidChooseItem;
    }

    void DidChooseItem(RightWrongOptionAttachment rw)
    {

        PlaySoundEffect(rw.isTheRightOption);

        if (rw.isTheRightOption)
        {
            dataBridge.didEndCourseware.Invoke(this);


            creditDelegate.PlayCreditOnScreen(credit: new Score(), endPlay: () =>
            {
                DidEndThisCourseware(this);
            }); 


        }


    }


    void PlaySoundEffect(bool isRight)
    {
        audioSource.clip = isRight ? soundEffect.right : soundEffect.wrong;

        audioSource.Play();

    }


}

public struct ChooseImgByAudioTemplateStruct
{
    [PreviewField(100), PropertyOrder(-1)]
    public Sprite background;

    [PropertyOrder(1)]
    public CP_ChooseImgByAudioItem_SO itemSO;

    [LabelText("排列位置")]
    public InLinePositions_SO inLinePositions;
}



public enum ChooseImgByAudioTemplate
{
    Shell, Farm
}