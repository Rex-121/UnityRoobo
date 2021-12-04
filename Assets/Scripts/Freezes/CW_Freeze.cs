using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CW_Freeze : CoursewarePlayer
{

    public GameObject prefabImage;

    [ShowInInspector]
    [LabelText("定格内容容器")]
    public GameObject freezeContainer;

    [LabelText("遮罩")]
    public Transform mShadow;

    [LabelText("下一环节")]
    public Image mNextStep;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void InitGridAndData(CW_Freeze_SO.FreezeEntity freezeEntity)
    {
        if (freezeEntity != null) {
            mNextStep.gameObject.SetActive(freezeEntity.isNext);
            switch (freezeEntity.type)
            {
                case CW_Freeze_SO.FreezeEntity.Type.audio:
                    break;
                case CW_Freeze_SO.FreezeEntity.Type.audioAndImage:
                    var gameObject = Instantiate(prefabImage, freezeContainer.transform);
                    var comp = gameObject.GetComponent<FreezeImageView>();
                    comp.mShadow = mShadow;
                    comp.InitGrids(freezeEntity.images);
                    break;
                case CW_Freeze_SO.FreezeEntity.Type.noDisplay:
                    break;
            }
        }
    }

    /// <summary>
    /// 遮罩点击隐藏
    /// </summary>
    public void OnOffActiveShadow()
    {
        Debug.Log("OnOffActiveShadow =" + mShadow.childCount);
        for (int i = 0; i < mShadow.childCount; i++)
        {
            Destroy(mShadow.GetChild(i).gameObject);
        }
        mShadow.gameObject.SetActive(false);
    }
}
