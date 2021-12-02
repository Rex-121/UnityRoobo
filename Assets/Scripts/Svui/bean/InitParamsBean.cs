
using Newtonsoft.Json;
using System;

public class InitParamsBean:BaseParamsBean
{
    [JsonProperty]
    private String appPackageId;

    [JsonProperty]
    private String appToken;

    [JsonProperty]
    private String appUId;

    [JsonProperty]
    private bool isTest;

    public String getAppPackageId()
    {
        return appPackageId;
    }

    public void setAppPackageId(String appPackageId)
    {
        this.appPackageId = appPackageId;
    }

    public String getAppToken()
    {
        return appToken;
    }

    public void setAppToken(String appToken)
    {
        this.appToken = appToken;
    }

    public String getAppUId()
    {
        return appUId;
    }

    public void setAppUId(String appUId)
    {
        this.appUId = appUId;
    }

    public bool getIsTest()
    {
        return isTest;
    }

    public void setTest(bool test)
    {
        isTest = test;
    }
}
