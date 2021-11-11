using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "FPS", menuName = "单例SO/FPS")]
public class FPS : SingletonSO<FPS>
{

    [ShowInInspector]
    [LabelText("默认帧率")]
    private int fps = 30;


    [ShowInInspector]
    [LabelText("当前帧率")]
    private int cFps = 30;

    /// <summary>
    /// 锁定帧率
    /// </summary>
    /// <param name="eFps">帧率</param>
    public void LockFrame(int eFps)
    {
        cFps = eFps;

        Lock();
    }

    private void Lock()
    {
        Application.targetFrameRate = cFps;

        var gb = new GameObject("FPSDisplay");

        gb.AddComponent<FPSDisplayOnGUI>();

        DontDestroyOnLoad(gb);
    }


    /// <summary>
    /// 锁定帧率, 默认 `30`
    /// </summary>
    public void LockFrame()
    {
        LockFrame(fps);
    }
}
