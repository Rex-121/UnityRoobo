using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : IScrollLimiter
{
    public Transform anchor;
    public GameObject island1, island2, island3, island4;
    private float left;
    private float rightAnchor;
    private int islandCount = 13;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < islandCount; i++)
        {
            instantiateIsland(i);
        }
    }

    void instantiateIsland(int index)
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
        Instantiate(island);
    }

    GameObject getIslandByPosition(int index)
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
