using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float speedInEditor = 3000;
    private float currentX;
    public IslandGenerator islandGenerator;
    private Rigidbody2D rb;
    private float rightAnchor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rightAnchor = islandGenerator.getRightAnchor();
    }

    // Update is called once per frame
    void Update()
    {
        //没有手指滑动时也得检查
        checkSurpassLimit();
#if UNITY_EDITOR
        float axis = Input.GetAxis("Horizontal");
        if (axis != 0)
        {
            float v = axis * speedInEditor * Time.deltaTime;

            move(v);
        }
        return;
#elif UNITY_ANDROID
        scrollCameraInAndroid();
#endif
    }

    private void scrollCameraInAndroid()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }
        else if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Vector2 touchV2 = Input.touches[0].deltaPosition;
           
                float v = touchV2.x * speed * Time.deltaTime;
                v = -v;
                move(v);
            }
        }
    }
    private void move(float v)
    {
        Debug.Log("x:" + rb.transform.position.x + " ,right limit:" + rightAnchor + " ,v:" + v);
        //限速，不然容易飞出去
        if (v > 50) { 
            v = 50;
        }
        if (!checkSurpassLimit())
        {
            rb.velocity = new Vector2(v, rb.velocity.y);
        }
    }
    //检查是否越界
    private bool checkSurpassLimit() {
        if (rb.transform.position.x < 0)
        {
            rb.transform.position = new Vector3(0f, rb.transform.position.y, rb.transform.position.z);
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return true;
        }
        else if (rb.transform.position.x > rightAnchor)
        {
            rb.transform.position = new Vector3(rightAnchor, rb.transform.position.y, rb.transform.position.z);
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return true;
        }
        else {
            return false;
        }
    }
}
