using UnityEngine;
public class ScreenSize
{
    public static float getScreenWidth()
    {
        float width = UnityEngine.Screen.width;
        Logging.Log("screen width:" + width);
        return width;
    }

    public static float getScreenHeight()
    {
        float height = UnityEngine.Screen.height;
        Logging.Log("screen height:" + height);
        return height;
    }

    public static float getScreenWidthInUnit()
    {
        float widthInUnit = getScreenWidth() / getScreenHeight() * getScreenHeightInUnit();
        Logging.Log("width in unit:" + widthInUnit);
        return widthInUnit;
    }

    public static float getScreenHeightInUnit()
    {
        float cameraSize = Camera.main.orthographicSize;
        return cameraSize * 2;
    }

    //摄像机照射高度的两倍与屏高的比值
    public static float getScreenUnitRatio()
    {
        float cameraSize = Camera.main.orthographicSize;
        Logging.Log("camera size:" + cameraSize);
        return (cameraSize * 2) / getScreenHeight();
    }

    public static int getPPU() {
        return 100;
    }
}
