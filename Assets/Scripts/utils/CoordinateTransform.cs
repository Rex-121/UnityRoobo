using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateTransform
{
    ///areaWidthRatio:区域相对于图片宽度的比例 40%直接传40
    ///bgWidth bgHeight为背景图宽高
    public static float getAreaWidthByWidthRatio(float areaWidthRatio, float bgWidth, float bgHeight)
    {
        areaWidthRatio = areaWidthRatio / 100f;
        float width;
        float bgRatio = bgWidth / bgHeight;
        float screenRatio = ScreenSize.getScreenWidthInUnit() / ScreenSize.getScreenHeightInUnit();
        if (screenRatio < bgRatio)
        {
            float widthRatio = bgRatio / screenRatio; //背景图与屏幕宽比
            Debug.Log("areaWidthRatio:"+areaWidthRatio+" ,widthRatio:"+widthRatio);
            width = areaWidthRatio * widthRatio * ScreenSize.getScreenWidthInUnit();
        }
        else
        {
            width = areaWidthRatio * ScreenSize.getScreenWidthInUnit();
        }
        Debug.Log("width:"+width);
        return width;
    }


    ///areaHeightRatio:区域相对于图片高度的比例 40%直接传40
    ///bgWidth bgHeight为背景图宽高
    public static float getAreaHeightByHeightRatio(float areaHeightRatio, float bgWidth, float bgHeight)
    {
        areaHeightRatio = areaHeightRatio / 100f;
        float height;
        float bgRatio = bgWidth / bgHeight;
        float screenRatio = ScreenSize.getScreenWidthInUnit() / ScreenSize.getScreenHeightInUnit();
        if (screenRatio > bgRatio)
        {
            float heightRatio = screenRatio / bgRatio; //背景图与屏幕高比
            height = areaHeightRatio * heightRatio * ScreenSize.getScreenHeightInUnit();
        }
        else
        {
            height = areaHeightRatio * ScreenSize.getScreenHeightInUnit();
        }
        Debug.Log("height:"+height);
        return height;
    }

    ///centerRatioLeft:区域相对于图片中心点x的比例 40%直接传40
    ///此方法获取到到值是相对于左上角到值，既pivot（0，1）
    public static float getXByCenterRatio(float centerRatioLeft, float bgWidth, float bgHeight)
    {
        centerRatioLeft = centerRatioLeft / 100f;
        float left;
        float bgRatio = bgWidth / bgHeight;
        float screenRatio = ScreenSize.getScreenWidthInUnit() / ScreenSize.getScreenHeightInUnit();
        if (screenRatio < bgRatio)
        {
            float widthRatio = bgRatio / screenRatio; //背景图与屏幕宽比
            float emptyLeftRatio = (1 - widthRatio) / 2; //左侧空白占屏幕比例(负值)
            Debug.Log(
                "widthRatio:"+widthRatio+" ,emptyLeftRatio:"+emptyLeftRatio+" ,centerRatioLeft:"+centerRatioLeft);
            left = emptyLeftRatio * ScreenSize.getScreenWidthInUnit() + (centerRatioLeft + 0.5f) * widthRatio * ScreenSize.getScreenWidthInUnit();
        }
        else
        {
            left = (centerRatioLeft + 0.5f) * ScreenSize.getScreenWidthInUnit();
        }
        Debug.Log("left:"+left);
        return left-ScreenSize.getScreenWidthInUnit()/2;
    }

    ///此方法获取到到值是相对于左上角到值，既pivot（0，1）
    ///centerRatioTop:区域相对于视频中心点的位置的百分比(左上:-50,50;右下:50,-50)
    public static float getYByCenterRatio(float centerRatioTop, float bgWidth, float bgHeight)
    {
        centerRatioTop = centerRatioTop / 100f;
        float top;
        float bgRatio = bgWidth / bgHeight;
        float screenRatio = ScreenSize.getScreenWidthInUnit() / ScreenSize.getScreenHeightInUnit();
        if (screenRatio > bgRatio)
        {
            float heightRatio = screenRatio / bgRatio; //背景图与屏幕高比
            float emptyTopRatio = (1 - heightRatio) / 2;
            Debug.Log("emptyTopRatio:"+emptyTopRatio);
            top = emptyTopRatio * ScreenSize.getScreenHeightInUnit() + ((0.5f - centerRatioTop) * heightRatio) * ScreenSize.getScreenHeightInUnit();
        }
        else
        {
            top = (0.5f - centerRatioTop) * ScreenSize.getScreenHeightInUnit();
        }
        Debug.Log("top:"+top);
        return ScreenSize.getScreenHeightInUnit()/2- top;
    }
}
