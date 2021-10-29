
package com.roobo.aiclasslibrary;
import android.os.Bundle;
import android.widget.FrameLayout;

import com.unity3d.player.UnityPlayerActivity;

public abstract class OverrideUnityActivity extends UnityPlayerActivity {
    public static OverrideUnityActivity instance = null;

    abstract protected void initSvui(String id,String appPackageId,String appToken,String appUId, boolean isTest);

    abstract protected void oralEvaluate(String id,String text);

    abstract protected void startQa(String id,int lessionId, String dialogId);

    abstract protected void stopCapture(String id);

    abstract protected void tts(String id,String text, String language);

    abstract protected void clearAudioRecord(String id);

    abstract protected void getAudioRecordSize(String id);

    abstract protected void clearPcm(String id);

    abstract protected void release(String id);


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
