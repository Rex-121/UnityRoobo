using System;
using UnityEngine;
using Sirenix.OdinInspector;

using UniRx;


[CreateAssetMenu(fileName = "用户", menuName = "单例SO/用户")]
public class User : SingletonSO<User>
{


    public static User Default => Instance("用户");

    private Token _token;


    private void OnEnable()
    {
        Logging.Log("fasfasgasfa");
    }

    [ReadOnly]
    [ShowInInspector]
    public Token token
    {

        set
        {
            _token = value;


            ExpireCul();

        }
        get { return _token; }

    }

    private IDisposable expireDis;

    void ExpireCul()
    {
        expireDis?.Dispose();

        if (token == null) return;

        var f = new DateTime();

        expireDis = Observable.Timer(TimeSpan.FromMinutes(3)).Subscribe();
    }

    void ClearToken()
    {
        token = null;
    }

    private void OnDestroy()
    {
        expireDis?.Dispose();
    }

    [Serializable]
    public class Token
    {
        [LabelText("账号")]
        public string account;
        public string appUserID;
        public string accessToken;
    }

}
