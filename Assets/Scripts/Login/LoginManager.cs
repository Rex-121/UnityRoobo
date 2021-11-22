
using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;
using BestHTTP;
using BestHTTP.Forms;

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

        //Logging.Log(User.Default.token.account);

        //HttpRx.Post<User.Token>("pudding/manager/v1/provisional/tempAccount/login", new ABC("50000000593", "lu23t0gk5110")).Subscribe((r) =>
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

        var a = new Dictionary<string, string>();

        a.Add("sujectId", "4");
        a.Add("type", "delay");

        //HTTPManager.Logger.Level = BestHTTP.Logger.Loglevels.All;

        
        HttpRx.Get<Dictionary<string, string>>("/pudding/teacher/v1/course/list", a).Subscribe(v =>
        {
            Logging.Log(v);
            Logging.Log("fasdf");
        }, e =>
        {
            Logging.Log(e);
        });

        //var r = new HTTPRequest(new Uri("http://appcourse.roobo.com.cn/pudding/teacher/v1/course/list"), HTTPMethods.Get, callback: (r, re) =>
        //{
        //    Logging.Log(re.DataAsText);
        //    Logging.Log(r.Uri.Query);
        //    Logging.Log(r.Uri.ToString());
        //});

        //var aa = new Uri("");

       

        ////HTTPUrlEncodedForm
        ////r.SetForm
        //r.FormUsage = HTTPFormUsage.UrlEncoded;

        //var abc = new HTTPUrlEncodedForm();
        //abc.AddField("sujectId", "4");
        //abc.AddField("type", "delay");

        //r.AddField("sujectId", "4");
        //r.AddField("type", "delay");

        //r.AddHeader("Authorization", "GoRoobo eyJhcHBQYWNrYWdlSUQiOiJvUXdiOEhLQWgiLCJhcHBJRCI6Ik9HSTFaV0l5TkdJNE0yVTMiLCJ0cyI6NDAsImF1dGgiOnsiYXBwVXNlcklEIjoib1E6MjE2MWNjZTdkMDIyN2Q1N2EwYWY0NmQ5NjYwZTI5MWUiLCJhY2Nlc3NUb2tlbiI6IjMyM2Q3MzIxMmQ0YjZiOTU1OWY2YTNiNDYwY2U2NTU5IiwiYWNjZXNzdG9rZW5FeHBpcmVzIjoiMjAyMS0xMS0yNlQxMDoyNzozMyswODowMCIsInJlZnJlc2hUb2tlbiI6IjgwNzU2YWQ4OGM0NzAxMzllNDU0OGEyZmY3YWFkMzllIiwicmVmcmVzaFRva2VuRXhwaXJlcyI6IjIwMjEtMTItMTlUMTA6Mjc6MzMrMDg6MDAiLCJhY2NvdW50IjoiMTg1MTgzOTY3ODcifSwiYXBwIjp7InZpYSI6ImFuZHJvaWQiLCJhcHAiOiLluIPkuIFBSeivvuWggiIsImF2ZXIiOiI0LjIuMSIsImN2ZXIiOiIxMzIiLCJtb2RlbCI6IiIsImxvY2FsIjoiemhfQ04iLCJjaCI6IjEwMDAwIiwibmV0IjoiIn19.2a2aa9354c241495719b60b8612eb20e");
        //r.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        ////r.FormUsage = abc.f;
        //r.SetForm(abc);


        ////var ac = new UriBuilder();

        

        //r.Send();
    }


    [Serializable]
    public class X
    {
        public string id;
    }

}


