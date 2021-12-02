using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SvuiBridge : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AndroidJavaObject jo = new AndroidJavaObject("com.roobo.svuiforunity.UnitySvuiPlugin");
        InitParamsBean paramsBean = new InitParamsBean();
        paramsBean.setId("testId");
        paramsBean.setAppPackageId("JDJ5HOluh");
        paramsBean.setAppToken("e3079fc40bf7fce29a0edcbf5fa7febb");
        paramsBean.setAppUId("JD:813c0b516b3db6c78da42815e90eec4f");
        paramsBean.setTest(true);

        string json = JsonConvert.SerializeObject(paramsBean);
        Logging.Log("json:"+json);
        jo.Call("initSvui", json);
    }

    public void initSvuiResult(string result) {
        Logging.Log(result);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
