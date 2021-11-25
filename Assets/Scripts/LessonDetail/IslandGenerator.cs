using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;
using UniRx;
using System;

public class IslandGenerator : IScrollLimiter
{
    [Required]
    public Transform anchor;
    [Required]
    public GameObject island1, island2, island3, island4;
    [Required]
    public GameObject floatingObject;
    [Required]
    public GameObject playerWithCameraObject;
    private float left;
    private float rightAnchor;
    private int islandCount;

    void OnEnable()
    {
        Camera.main.gameObject.AddComponent<CinemachineBrain>();
    }

    void OnDisable()
    {
        Destroy(Camera.main.gameObject.GetComponent<CinemachineBrain>());
    }

    // Start is called before the first frame update
    void Start()
    {
        doGetLessonDetails(()=>{
            GameObject floating = Instantiate(floatingObject);
            floating.GetComponent<PositionBridge>().setRight(rightAnchor);
            floating.transform.SetParent(transform);
            GameObject playerWithCamera = Instantiate(playerWithCameraObject);
            playerWithCamera.GetComponent<PositionBridge>().setRight(rightAnchor);
            playerWithCamera.transform.SetParent(transform);
        });
    }

    private void doGetLessonDetails(Action then)
    {
        HttpRx.Get<LessonDetailsBean.Data>("/pudding/teacher/v1/course/5509/lesson/6979/round/list").Subscribe(v =>
        {
            islandCount = v.list.Count;
            for (int i = 0; i <islandCount; i++)
            {
                instantiateIsland(i,v.list[i]);
            }
            then();
        }, e =>
        {
            Logging.Log((e as HttpError).message);
            Logging.Log((e as HttpError).Message);
        });
    }

    private void instantiateIsland(int index,LessonDetailsBean.ListItem item)
    {
        GameObject island = getIslandByPosition(index);
        if (index == 0)
        {
            left = anchor.transform.position.x;
        }
        else
        {
            SpriteRenderer lastIslandRenderer = getIslandByPosition(index - 1).GetComponent<SpriteRenderer>();
            Debug.Log(index + " ,width:" + lastIslandRenderer.bounds.size.x);
            left = left + lastIslandRenderer.bounds.size.x;
        }
        //计算最右侧的点
        if (index == islandCount - 1)
        {
            rightAnchor = left + island.GetComponent<SpriteRenderer>().bounds.size.x + anchor.transform.position.x;
        }
        island.transform.position = new Vector3(left, anchor.transform.position.y, 0);
        GameObject islandGameObject = Instantiate(island);
        islandGameObject.transform.SetParent(transform);
        islandGameObject.GetComponent<IslandChipGenerator>().setData(item);
    }

    private GameObject getIslandByPosition(int index)
    {
        if (index == 0)
        {
            return island1;
        }
        else if (index == islandCount - 1)
        {
            return island4;
        }
        else
        {
            if (index % 2 == 0)
            {
                return island2;
            }
            else
            {
                return island3;
            }

        }
    }

    public override float getLeftLimit()
    {
        return 0f;
    }

    public override float getRightLimit()
    {
        return rightAnchor;
    }

    public override float getTopLimit()
    {
        return 0f;
    }

    public override float getBottomLimit()
    {
        return 0f;
    }
}
