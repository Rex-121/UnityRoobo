using System;
using Newtonsoft.Json;

public class BaseParamsBean 
{
    [JsonProperty]
    private String id;

    public String getId()
    {
        return id;
    }

    public void setId(String id)
    {
        this.id = id;
    }
}
