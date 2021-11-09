using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FPS", menuName = "单例SO/FPS")]
public class FPS : SingletonSO<FPS>
{

    public int fps = 30;

    public void LockFrame()
    {
        Application.targetFrameRate = fps;

        var gb = new GameObject("FPSDisplay");

        gb.AddComponent<FPSDisplayOnGUI>();

        DontDestroyOnLoad(gb);

    }
}
