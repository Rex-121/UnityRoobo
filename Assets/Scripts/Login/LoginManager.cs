
using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;
using BestHTTP;
using BestHTTP.Forms;
using SharpJson;

using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{

    [LabelText("密码")]
    public InputField passwordField;

    [LabelText("账号")]
    public InputField accountField;


    ReactiveProperty<Account> account = new ReactiveProperty<Account>(new Account());


    public Button loginBtn;

    struct Account
    {
        public string account;// = "50000000593";
        public string password;// lu23t0gk5110

        public Account(string a, string p)
        {
            account = a;
            password = p;
        }

        public override string ToString()
        {
            return account + "/" + password;
        }


        public bool validate => !string.IsNullOrEmpty(account) && !string.IsNullOrEmpty(password);
    }


    void Start()
    {

        account.Subscribe(v =>
        {
            loginBtn.interactable = v.validate;
        }).AddTo(this);


        Observable.CombineLatest(accountField.OnValueChangedAsObservable(), passwordField.OnValueChangedAsObservable()).Subscribe(v =>
          {
              var ac = account.Value;
              ac.account = v[0];
              ac.password = v[1];
              account.Value = ac;
          }).AddTo(this);


        loginBtn.OnClickAsObservable().Subscribe(_ => OnDidWantLogin()).AddTo(this);

    }


    public void OnDidWantLogin()
    {
        HttpRx.Post<User.Token>("/pudding/manager/v1/provisional/tempAccount/login", account.Value).Subscribe((r) =>
        {
            User.Default.token = r;
            Logging.Log(r.accessToken);
            Logging.Log("fasd");

            SceneManager.LoadScene("Realm");
        }, (e) =>
         {
             Logging.Log("fasd");
             Logging.Log("error");
             Logging.Log((e as HttpError).message);
             Logging.Log((e as HttpError).code);
         }, () => { Logging.Log("com"); }).AddTo(this);
    }

}


