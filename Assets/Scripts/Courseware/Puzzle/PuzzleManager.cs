using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PuzzleManager : CoursewarePlayer
{
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

    // Start is called before the first frame update
    void Start()
    {
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
        WebReqeust.GetTexture("https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-10-15/c5kfkggpjsa9p75btg90.png",
          (result) =>
          {
              var targetTab = Instantiate(targetTable);
              SpriteRenderer spriteRenderer = targetTab.GetComponent<SpriteRenderer>();
              //替换sprite 
              spriteRenderer.sprite = Sprite.Create(result, new Rect(Vector2.zero, new Vector2(result.width, result.height)), new Vector2(0f, 1f));
              //修改大小
              targetTab.transform.localScale = new Vector3(targetTableSize.x * 100 / result.width, targetTableSize.y * 100 / result.height, targetTab.transform.localScale.z);
              Debug.Log("image width:" + result.width + " ,height:" + result.height);
              //计算位置
              targetTab.transform.position = targetTablePosition;
              targetTab.transform.SetParent(transform);
          }, (msg) =>
          {
              Debug.Log("load image failed:" + msg);
          });
    }

    private void generateDragTarget()
    {
        //修改大小
        var dragTar = Instantiate(dragTarget);
        float dragTargetW = 0.52f * targetTableSize.x;
        float dragTargetH = 0.36f * targetTableSize.y;

        RectTransform rect = dragTar.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(dragTargetW, dragTargetH);

        BoxCollider2D boxCollider2D = dragTar.GetComponent<BoxCollider2D>();
        boxCollider2D.size = rect.sizeDelta/2;//碰撞体缩小一半

        //修改位置
        dragTar.transform.position = new Vector3(targetTablePosition.x + (-0.41f + 0.5f) * targetTableSize.x + dragTargetW / 2,
            targetTablePosition.y + (0.2899f - 0.5f) * targetTableSize.y - dragTargetH / 2, dragTar.transform.position.z);

        dragTar.transform.SetParent(transform);
    }

    private void generateDragItem()
    {
        WebReqeust.GetTexture("https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-10-15/c5kijiopjsa9p75btm30.png", (result) =>
        {
            GameObject dragI = Instantiate(dragItem);
            SpriteRenderer spriteRenderer = dragI.GetComponent<SpriteRenderer>();
            //替换sprite 
            spriteRenderer.sprite = Sprite.Create(result, new Rect(Vector2.zero, new Vector2(result.width, result.height)), new Vector2(0.5f, 0.5f));
            //修改大小
            float dragIW = 0.52f * targetTableSize.x;
            float dragIH = 0.36f * targetTableSize.y;
            float dragIWScale = dragIW * 100 / result.width;
            float dragIHscale = dragIH * 100 / result.height;
            dragI.transform.localScale = new Vector3(dragIWScale, dragIHscale, dragI.transform.localScale.z);
            Debug.Log("image width:" + result.width + " ,height:" + result.height);
            //修改碰撞体大小
            BoxCollider2D boxCollider2D = dragI.GetComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(dragIW/dragIWScale,dragIH/dragIHscale)/2;//因为已经整体放大了，所以需要除去
            ////计算位置
            dragI.transform.position = new Vector3(dragTable.transform.position.x,dragTable.transform.position.y,dragI.transform.position.z);

            dragI.transform.SetParent(transform);
            dragI.name = "dragItem1";
        }, (errMsg) =>
        {
            Logging.Log("load image failed:" + errMsg);
        });
    }
}
