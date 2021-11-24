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

    [LabelText("踢出时间")]
    public int expireTime = 10;


    [LabelText("下次踢出时间")]
    [DisplayAsString]
    public DateTime nextExpireTime;


    private IDisposable expireDis;

    void ExpireCul()
    {
        expireDis?.Dispose();

        if (token == null) return;

        nextExpireTime = DateTime.Now.AddMinutes(expireTime);

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
