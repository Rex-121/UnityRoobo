using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeImageView : MonoBehaviour
{

    [ShowInInspector]
    [LabelText("顶部Grid")]
    public GridLayoutGroup mTopGrid;

    [ShowInInspector]
    [LabelText("底部Grid")]
    public GridLayoutGroup mBottomGrid;

    [ShowInInspector]
    [LabelText("中部Grid")]
    public GridLayoutGroup mCenterGrid;

    [ShowInInspector]
    [LabelText("最上层Panel")]
    public GameObject mPanel;


    public GameObject prefabImage;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 初始化显示内容
    /// </summary>
    /// <param name="imageUrls"></param>
    public void InitGrids(List<string> imageUrls)
    {
        Debug.Log("FreezeImageView=>" + imageUrls.Count);
        if(imageUrls.Count>0)
        {
            if (imageUrls.Count <= 4)
            {
                mCenterGrid.gameObject.SetActive(true);
                mTopGrid.gameObject.SetActive(false);
                mBottomGrid.gameObject.SetActive(false);
            }
            else
            {
                mCenterGrid.gameObject.SetActive(false);
                mTopGrid.gameObject.SetActive(true);
                mBottomGrid.gameObject.SetActive(true);
            }
            InitImages(imageUrls);
        }
        else
        {
            mCenterGrid.gameObject.SetActive(false);
            mTopGrid.gameObject.SetActive(false);
            mBottomGrid.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 放置数据
    /// </summary>
    /// <param name="imageUrls"></param>
    private void InitImages(List<string> imageUrls)
    {
        Debug.Log("FreezeImageView=InitImages>>" + imageUrls.Count);
        if (imageUrls.Count <= 4)
        {
            imageUrls.ForEach(e =>
            {
                StartLoadImage(mCenterGrid, e);
            });
        }
        else
        {
            int counts = imageUrls.Count;
            if (imageUrls.Count > 8)
            {
                counts = 8;
            }

            int topCount = (counts + 1) / 2;
            
            List<string> topUrls=imageUrls.GetRange(0, topCount);
            
            topUrls.ForEach(e=> {
                StartLoadImage(mTopGrid, e);
            });

            List<string> buttomUrls = imageUrls.GetRange(topCount, counts - topCount);
            buttomUrls.ForEach(e => {
                StartLoadImage(mBottomGrid, e);
            });
            
        }
    }


    /// <summary>
    /// 添加到grid ,并加载网络图片
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public void StartLoadImage(GridLayoutGroup grid, string url)
    {
        Debug.Log("StartLoadImage" + grid + " " + grid.name);

        GameObject prefab = Instantiate(prefabImage, grid.transform);

        prefab.GetComponent<ImageAspectScript>().onClick += OnClickToMoteScale;
        Image[] asps = prefab.GetComponentsInChildren<Image>();
        Debug.Log(" StartLoadImage image Size:" + asps.Length);

        WebReqeust.GetTexture(url, (texture2d) =>
        {
            Sprite tempSprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));
            asps[1].sprite = tempSprite;
           
        }, (e) =>
        {
            Debug.Log("FreezeImageView StartLoadImage error:" + e);
        });
    }

    private bool isCenter = false;
    private Vector3 localPos;
    private Vector3 worldPos;
    public void OnClickToMoteScale(GameObject gb,Vector3 localPos,Vector3 worldPos)
    {
        GameObject gameObject = new GameObject("gob");
        Image image = gameObject.AddComponent<Image>();
        
        image.color = new Color(1,1,1,0.2f);
        gameObject.transform.localPosition = new Vector3(640, 400);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1280, 800);
        gameObject.transform.SetParent(transform);

        var outerPrafeb = Instantiate(gb, gameObject.transform);
        outerPrafeb.transform.localPosition = worldPos;
        if (isCenter)
        {
            outerPrafeb.transform.DOMove(new Vector3(640, 400, 1000), 1);
            outerPrafeb.transform.DOScale(new Vector3(2, 2, 1000), 1);
        }
        else
        {
            outerPrafeb.transform.DOMove(new Vector3(640, 400, 1000), 1);
            outerPrafeb.transform.DOScale(new Vector3(2, 2, 1000), 1);
        }
        isCenter = !isCenter;


        //Debug.Log("" + this.name + " localPos=" + localPos + " worldPos=" + worldPos);
        //if (worldPos.x == 0.0f)
        //{
        //    localPos = this.gameObject.transform.localPosition;
        //    Debug.Log("" + this.name + " localPos=" + localPos);
        //    worldPos = this.gameObject.transform.parent.transform.TransformPoint(localPos);
        //    Debug.Log("" + this.name + " worldPos=" + worldPos);
        //}
        //if (!isCenter)
        //{
        //    outerPrafeb.transform.DOMove(new Vector3(640, 400, 0), 1);
        //    outerPrafeb.transform.DOScale(new Vector3(2, 2, 0), 1);
        //}
        //else
        //{
        //    outerPrafeb.transform.DOMove(worldPos, 1);
        //    outerPrafeb.transform.DOScale(new Vector3(1, 1, 0), 1);
        //}
        //isCenter = !isCenter;
    }

}
