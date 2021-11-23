
using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;
using BestHTTP;
using BestHTTP.Forms;
using SharpJson;
public class LoginManager : MonoBehaviour
{

    struct ABC
    {
        public string account;// = "50000000593";
        public string password;

        public ABC(string a, string p)
        {
            account = a;
            password = p;
        }
    }

    void Start()
    {

        Logging.Log(User.Default.token?.account);

        var date = DateTime.Now;

        Logging.Log(date);

        var date1 = date.AddMinutes(10);


        Logging.Log(date1);

        //HttpRx.Post<User.Token>("/pudding/manager/v1/provisional/tempAccount/login", new ABC("50000000593", "lu23t0gk5110")).Subscribe((r) =>
        //{
        //    User.Default.token = r;
        //    Logging.Log(r.accessToken);
        //    Logging.Log("fasd");
        //}, (e) =>
        // {
        //     Logging.Log("fasd");
        //     Logging.Log("error");
        //     Logging.Log((e as HttpError).message);
        //     Logging.Log((e as HttpError).code);
        // }, () => { Logging.Log("com"); }).AddTo(this);

        //var a = new Dictionary<string, string>();

        //a.Add("sujectId", "4");
        //a.Add("type", "delay");


        //HttpRx.Get<Dictionary<string, string>>("/pudding/teacher/v1/course/list", a).Subscribe(v =>
        //{
        //    Logging.Log(v);
        //    Logging.Log("fasdf");
        //}, e =>
        //{
        //    Logging.Log((e as HttpError).message);
        //});
    }


    [Serializable]
    public class X
    {
        public string id;

        
        //[JSON]
    }

}


