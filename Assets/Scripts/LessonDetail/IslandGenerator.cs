using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    public float speed;
    private float speedInEditor = 10;
    private float currentX;
    public Transform anchor;
    public GameObject island1, island2, island3, island4;
    private float left;
    private float rightAnchor;
    private int islandCount = 7;

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

    public float getRightAnchor() {
        return rightAnchor;
    }

//    // Update is called once per frame
//    void Update()
//    {
//#if UNITY_EDITOR
//        float axis = Input.GetAxis("Horizontal");
//        if (axis != 0)
//        {
//            currentX += axis * speedInEditor * Time.deltaTime;
//            Debug.Log("axis :" + axis + " ,delta time:" + Time.deltaTime + " ,speed:" + speedInEditor + "\n x:" + currentX);
//            if (currentX < 0)
//            {
//                currentX = 0;
//            }
//            else if (currentX > rightAnchor)
//            {
//                currentX = rightAnchor;
//            }

//            Camera.main.transform.position = new Vector3(currentX, Camera.main.transform.position.y, Camera.main.transform.position.z);
//        }
//        return;
//#elif UNITY_ANDROID
//        scrollCameraInAndroid();
//#endif
//    }
//    private void scrollCameraInAndroid()
//    {
//        if (Input.touchCount <= 0)
//        {
//            return;
//        }
//        else if (Input.touchCount > 0)
//        {
//            if (Input.touches[0].phase == TouchPhase.Moved)
//            {
//                Vector2 touchV2 = Input.touches[0].deltaPosition;
//                float x = touchV2.x * Time.deltaTime * speed;

//                //transform.Translate(new Vector3(x, transform.position.y, transform.position.z));
//                currentX += x;
//                if (currentX > 0)
//                {
//                    currentX = 0;
//                }
//                if (Mathf.Abs(currentX) > rightAnchor)
//                {
//                    currentX = -rightAnchor;
//                }
//                Debug.Log("delta x:" + touchV2.x + " ,delta time:" + Time.deltaTime + " ,speed:" + speed + "\n x:" + currentX);
//                Camera.main.transform.position = new Vector3(-currentX, Camera.main.transform.position.y, Camera.main.transform.position.z);
//            }
//        }
//    }
}
