using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SeekManager : CoursewarePlayer
{
    public struct Data {
        public int videoWidth;
        public int videoHeight;
        public List<SeekBean> items;
    }

    public struct SeekBean {
        public string audio;
        public float width;
        public float height;
        public float left;
        public float top;
    }

    private Data data;
    private int solvedCount = 0;
    [Required]
    public GameObject seekTargetPrefab;

    public void setData(Data data) {
        this.data = data;

        foreach (SeekBean item in data.items) {
            GameObject seekTarget = Instantiate(seekTargetPrefab,transform);
            //设置大小
            float width = CoordinateTransform.getAreaWidthByWidthRatio(item.width,data.videoWidth,data.videoHeight);
            float height = CoordinateTransform.getAreaHeightByHeightRatio(item.height, data.videoWidth, data.videoHeight);
            seekTarget.transform.localScale = new Vector3(width/1,height/1,seekTarget.transform.localScale.z);
            //设置位置
            float x = CoordinateTransform.getXByCenterRatio(item.left,data.videoWidth,data.videoHeight)+width/2;
            float y = CoordinateTransform.getYByCenterRatio(item.top,data.videoWidth,data.videoHeight)-height/2;
            seekTarget.transform.position = new Vector3(x,y,seekTarget.transform.position.z);

            SeekTarget seek = seekTarget.GetComponent<SeekTarget>();
            seek.setAudio(item.audio);
            seek.setOnEnd(()=>{
                solvedCount++;
                if (solvedCount >= data.items.Count) {
                    DidEndCourseware(this);
                }
            });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
