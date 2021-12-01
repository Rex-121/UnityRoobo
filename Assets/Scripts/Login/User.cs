using System;
using UnityEngine;
using Sirenix.OdinInspector;

using UniRx;


[CreateAssetMenu(fileName = "用户", menuName = "单例SO/用户")]
public class User : SingletonSO<User>
{


    public static User Default => Instance("用户");

    private Token _token;

    public bool isLogin
    {

        get
        {
            if (_token != null)
            {
                return !string.IsNullOrEmpty(_token.account);
            }
            return false;
        }

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

    [LabelText("踢出时间")]
    public int expireTime = 10;


    [LabelText("下次踢出时间")]
    [DisplayAsString]
    public DateTime nextExpireTime;


    private IDisposable expireDis;

    void ExpireCul()
    {
        expireDis?.Dispose();

        CheckTimeOut();

        if (token == null) return;


        nextExpireTime = DateTime.Now.AddMinutes(expireTime);

        expireDis = Observable.Timer(TimeSpan.FromMinutes(expireTime)).Subscribe(v =>
        {
            ClearToken();
        });
    }

    void ClearToken()
    {
        Logging.Log(DateTime.Now + "->踢出->" + token.account);
        token = null;
        nextExpireTime = DateTime.MinValue;
    }

//#if UNITY_EDITOR

    private IDisposable checkTimeOut;
    [LabelText("下次踢出倒计时")]
    [DisplayAsString]
    public TimeSpan nextExpireTimeCountDown;

//#endif

    void CheckTimeOut()
    {
//#if UNITY_EDITOR

        checkTimeOut?.Dispose();

        if (!isLogin) return;

        checkTimeOut = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {
            nextExpireTimeCountDown = nextExpireTime.Subtract(DateTime.Now);
        });
//#endif
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
