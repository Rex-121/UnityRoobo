using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SvuiDemo : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        //string json = @"{'data':{'recordPath':' / storage / emulated / 0 / Android / data / com.roobo.pudding.ai / files / wav / JD.813c0b516b3db6c78da42815e90eec4f.1638500035009.wav','score':0,'word':'test','words':[{'beginTime':240,'endTime':360,'scores':{'pronunciation':0},'word':'test'}]},'id':2,'isSuccess':true,'methodName':'oralEvaluate'}";
        //BaseResultBean<object> resultbean = JsonConvert.DeserializeObject<BaseResultBean<object>>(json);
        //Logging.Log("RESULT:" + resultbean.isSuccess + " ," + resultbean.methodName + " ," + resultbean.id+" ,data:"+resultbean.data);

        initSvui();
    }

    private void initSvui()
    {
        SvuiBridge.Shared.initSvui("JDJ5HOluh", "e3079fc40bf7fce29a0edcbf5fa7febb", "JD:813c0b516b3db6c78da42815e90eec4f", true).TakeLast(1)
        .DoOnError(e =>
        {
            Logging.Log("svui init failed:" + e.Message);
            text.text = "init failed";
        })
      .Subscribe(success =>
      {
          Logging.Log("success");
          text.text = "init success";
      });
    }

    public void oralEvaluate()
    {
        SvuiBridge.Shared.oralEvaluate("test", SvuiBridge.Language.ENG, false, 5).TakeLast(1)
            .DoOnError(e =>
            {
                Logging.Log(e.Message);
            })
            .Subscribe(result =>
            {
                Logging.Log(result.word + ":" + result.score);
                text.text = result.word + ":" + result.score;
            });
    }

    public void qa()
    {
        SvuiBridge.Shared.startQA(6965, "mjqykhpeo6337j0vsw").TakeLast(1)
          .DoOnError(e =>
          {
              Logging.Log(e.Message);
          })
            .Subscribe(result =>
            {
                text.text = result.ai.query;
            });
    }

    public void stopCapture() {
        SvuiBridge.Shared.stopCapture();
        text.text = "STOP";
    }

    public void tts()
    {
        SvuiBridge.Shared.tts("test tts",SvuiBridge.Language.ENG).TakeLast(1)
          .DoOnError(e =>
          {
              Logging.Log(e.Message);
          })
            .Subscribe(result =>
            {
                text.text = result;
            });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
