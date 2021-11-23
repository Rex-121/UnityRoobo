using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//TODO support scroll verticaly
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Required]
    public float speed;
    [Required]
    public float speedLimit;
    private float speedInEditor = 3000;
    private Rigidbody2D rb;
    private float rightAnchor;
    // Start is called before the first frame update
    void Start()
    {
        FPS.Default.LockFrame();
        rb = GetComponent<Rigidbody2D>();
        rightAnchor = GetComponentInParent<PositionBridge>().getRight();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rightAnchor <= 0) {
            rb.gameObject.SetActive(false);
            return;
        }
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

    private void LateUpdate()
    {
        //没有手指滑动时也得检查
        checkSurpassLimit();
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
            else if (Input.touches[0].phase == TouchPhase.Stationary)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = 0;
            }
        }
    }

    private void move(float v)
    {
        Debug.Log("x:" + rb.transform.position.x + " ,right limit:" + rightAnchor + " ,v:" + v);
        //限速，不然容易飞出去
        if (v > speedLimit)
        {
            v = speedLimit;
        }
        if (!checkSurpassLimit())
        {
            rb.velocity = new Vector2(v, rb.velocity.y);
        }
    }

    //检查是否越界
    private bool checkSurpassLimit()
    {
        if (rb == null) {
            return true;
        }
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
        else
        {
            return false;
        }
    }
}
