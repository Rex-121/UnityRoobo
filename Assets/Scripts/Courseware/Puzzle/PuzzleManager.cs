using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;


public class PuzzleManager : CoursewarePlayer
{

    public struct Data
    {
        public DragItemBean board;
        public List<DragItemBean> list;

        public float height;

        public float width;
    }


    public struct DragItemBean
    {
        public string id;
        [JsonProperty("image")]
        public string imageUrl;
        [JsonProperty("width")]
        public float widthRatio;
        [JsonProperty("height")]
        public float heightRatio;
        [JsonProperty("left")]
        public float leftRatio;
        [JsonProperty("top")]
        public float topRatio;
    }






    [Title("拼图")]
    [LabelText("拼图背景版")]
    [Required]
    public GameObject targetTable;
    [LabelText("拼图碰撞体")]
    [Required]
    public GameObject dragTarget;
    [LabelText("拼图拖拽物")]
    [Required]
    public GameObject dragItem;
    [LabelText("拼图拖拽物背景版")]
    [Required]
    public GameObject dragTable;
    //TODO 以下两个值应由接口获取
    private float bgWidth = 1281f, bgHeight = 801f;
    private Vector2 targetTableSize;
    private Vector3 targetTablePosition;
    private List<DragItemBean> testData = new List<DragItemBean>();
    private int puzzleSolvedCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        DragItemBean item = new DragItemBean();
        item.id = "0";
        item.imageUrl = "https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-10-15/c5kijiopjsa9p75btm30.png";
        item.widthRatio = 50.453172205438065f;
        item.heightRatio = 35.90909090909091f;
        item.leftRatio = -41.20962119451546f;
        item.topRatio = 28.994755244755243f;
        testData.Add(item);
        DragItemBean item1 = new DragItemBean();
        item1.id = "1";
        item1.imageUrl = "https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-10-15/c5kijtgpjsa9p75btm40.png";
        item1.widthRatio = 51.963746223564954f;
        item1.heightRatio = 27.500000000000004f;
        item1.leftRatio = -1.911457122937487f;
        item1.topRatio = -5.625000000000001f;
        testData.Add(item1);
        DragItemBean item2 = new DragItemBean();
        item2.id = "2";
        item2.imageUrl = "https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-10-15/c5kikbopjsa9p75btm50.png";
        item2.widthRatio = 34.44108761329305f;
        item2.heightRatio = 26.36363636363636f;
        item2.leftRatio = -18.975133627701606f;
        item2.topRatio = 2.543706293706293f;
        testData.Add(item2);

        measureSize();
        generateTargetTable();
        generateDragTarget();
        generateDragItem();
    }

    private void measureSize()
    {
        //计算sprite大小
        targetTableSize = new Vector2(CoordinateTransform.getAreaWidthByWidthRatio(25.8f, bgWidth, bgHeight), CoordinateTransform.getAreaHeightByHeightRatio(54.9f, bgWidth, bgHeight));
        //计算位置
        targetTablePosition = new Vector3(CoordinateTransform.getXByCenterRatio(-25.95f, bgWidth, bgHeight),
          CoordinateTransform.getYByCenterRatio(38.46f, bgWidth, bgHeight), targetTable.transform.position.z);
    }

    private void generateTargetTable()
    {
        var targetTab = Instantiate(targetTable);
        SpriteRenderer spriteRenderer = targetTab.GetComponent<SpriteRenderer>();
        SpriteUtil.loadImageToSprite("https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-10-15/c5kfkggpjsa9p75btg90.png", spriteRenderer,
            targetTableSize.x, targetTableSize.y, new Vector2(0f, 1f), () =>
          {
              //计算位置
              targetTab.transform.position = targetTablePosition;
              targetTab.transform.SetParent(transform);
          });
    }

    private void generateDragTarget()
    {
        for (int i = 0; i < testData.Count; i++)
        {
            DragItemBean item = testData[i];
            //修改大小
            var dragTar = Instantiate(dragTarget);
            float dragTargetW = item.widthRatio / 100 * targetTableSize.x;
            float dragTargetH = item.heightRatio / 100 * targetTableSize.y;

            RectTransform rect = dragTar.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(dragTargetW, dragTargetH);

            BoxCollider2D boxCollider2D = dragTar.GetComponent<BoxCollider2D>();
            boxCollider2D.size = rect.sizeDelta / 2;//碰撞体缩小一半

            //修改位置
            dragTar.transform.position = new Vector3(targetTablePosition.x + (item.leftRatio / 100 + 0.5f) * targetTableSize.x + dragTargetW / 2,
                targetTablePosition.y + (item.topRatio / 100 - 0.5f) * targetTableSize.y - dragTargetH / 2, dragTar.transform.position.z);

            dragTar.transform.SetParent(transform);
            dragTar.name = "dragTarget" + item.id;
        }
    }

    private void generateDragItem()
    {
        List<GameObject> dragItems = new List<GameObject>();
        for (int i = 0; i < testData.Count; i++)
        {
            DragItemBean item = testData[i];

            GameObject dragI = Instantiate(dragItem);
            SpriteRenderer spriteRenderer = dragI.GetComponent<SpriteRenderer>();
            float dragIW = item.widthRatio / 100 * targetTableSize.x;
            float dragIH = item.heightRatio / 100 * targetTableSize.y;
            SpriteUtil.loadImageToSprite(item.imageUrl, spriteRenderer, dragIW, dragIH, () =>
            {
                //修改碰撞体大小
                BoxCollider2D boxCollider2D = dragI.GetComponent<BoxCollider2D>();
                boxCollider2D.size = new Vector2(dragIW / dragI.transform.localScale.x, dragIH / dragI.transform.localScale.y) / 2;//因为已经整体放大了，所以需要除去

                dragI.transform.SetParent(transform);
                dragI.name = "dragItem" + item.id;
                dragItems.Add(dragI);
                layoutDragItem(dragItems);
            });
        }
    }

    private void layoutDragItem(List<GameObject> list)
    {
        list.Shuffle();
        SpriteRenderer dragTableSpriteRenderer = dragTable.GetComponent<SpriteRenderer>();
        float dragTableHeight = dragTableSpriteRenderer.bounds.size.y;
        float spaceEven = dragTableHeight / (list.Count + 1);
        float start = dragTable.transform.position.y + dragTableHeight / 2f;

        for (int i = 0; i < list.Count; i++)
        {
            GameObject item = list[i];
            //计算位置
            item.transform.position = new Vector3(dragTable.transform.position.x, start - spaceEven * (i + 1), item.transform.position.z);
        }
    }

    public void onePuzzleSolved()
    {
        puzzleSolvedCount++;
        if (puzzleSolvedCount >= testData.Count)
        {
            DidEndCourseware(this);
        }
    }
}
