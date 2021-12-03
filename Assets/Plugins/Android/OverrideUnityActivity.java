
package com.roobo.aiclasslibrary;
import android.os.Bundle;
import android.widget.FrameLayout;

import com.unity3d.player.UnityPlayerActivity;

public abstract class OverrideUnityActivity extends UnityPlayerActivity {
    public static OverrideUnityActivity instance = null;

    abstract protected void initSvui(String json);

    abstract protected void oralEvaluate(String json);

    abstract protected void startQa(String json);

    abstract protected void stopCapture();

    abstract protected void tts(String json);

    abstract protected void clearAudioRecord();

    abstract protected long getAudioRecordSize();

    abstract protected void clearPcm();

    abstract protected void release();


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        instance = this;
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        instance = null;
    }
}
