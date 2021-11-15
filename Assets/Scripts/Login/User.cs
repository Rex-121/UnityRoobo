using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "用户", menuName = "单例SO/用户")]
public class User : SingletonSO<User>
{


    public static User Default => Instance("用户");

    [ReadOnly]
    public Token token;


    [Serializable]
    public struct Token
    {
        public string account;
        public string appUserID;
        public string accessToken;
    }

}
