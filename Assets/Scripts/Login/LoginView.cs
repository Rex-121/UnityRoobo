<<<<<<< HEAD
using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI;  using Sirenix.OdinInspector; using System;  public class LoginView : MonoBehaviour {     bool isPhonePwd = true;     Canvas canvas;     Button btnBack;     Button btnSend;     InputField inputName;     InputField inputPwd;     Button btnSubmit;     Text btnSubmitText;     Button btnSwitch;     Text inputPwdHint;     Text btnSwitchText;      [ShowInInspector]     [LabelText("发送验证码文本")]     private Text btnSendText;     // Start is called before the first frame update     void Start()     {         canvas = GameObject.Find("Canvas").GetComponent<Canvas>();         btnBack = GameObject.Find("Canvas/BtnBack").GetComponent<Button>();         btnSend = GameObject.Find("Canvas/InputPassword/BtnSend").GetComponent<Button>();         btnSend.gameObject.SetActive(!isPhonePwd);          inputName = GameObject.Find("Canvas/InputName").GetComponent<InputField>();         inputPwd = GameObject.Find("Canvas/InputPassword").GetComponent<InputField>();         btnSubmit = GameObject.Find("Canvas/BtnSubmit").GetComponent<Button>();         btnSubmitText = GameObject.Find("Canvas/BtnSubmit/Text").GetComponent<Text>();          btnSwitch = GameObject.Find("Canvas/BtnSwitch").GetComponent<Button>();         inputPwdHint = GameObject.Find("Canvas/InputPassword/Placeholder").GetComponent<Text>();         btnSwitchText = GameObject.Find("Canvas/BtnSwitch/Text").GetComponent<Text>();                GetBatteryLevel();          GetWifiInfo();        }       /// <summary>
    /// 获取电量信息
    /// </summary>     public void GetBatteryLevel()     {         try         {             float batteryLevel= SystemInfo.batteryLevel* 100;             Debug.Log("a=============batteryLevel =" + batteryLevel);             BatteryStatus batteryStatus = SystemInfo.batteryStatus ;             Debug.Log("a=============battery =" + batteryStatus);             inputName.text = "" + batteryStatus;                      }         catch (Exception e)         {             Debug.Log("Failed to read battery power; " + e.Message);         }     }       // Update is called once per frame     void Update()     {         CheckEnableLogin();     }      /// <summary>     /// 锟斤拷锟截帮拷钮     /// </summary>     public void OnBackPress()     {      }      /// <summary>     /// 锟斤拷锟斤拷欠锟缴碉拷锟斤拷锟铰?     /// </summary>     public void CheckEnableLogin()     {         string name = inputName.text;         string password = inputPwd.text;         bool enableSubmit = (name.Length > 0 && password.Length > 0);         btnSubmit.enabled = enableSubmit;         btnSubmit.interactable = enableSubmit;         Color nowColor;         string color = enableSubmit ? "#FFFFFF" : "#AEAEAE";         ColorUtility.TryParseHtmlString(color, out nowColor);         btnSubmitText.color = nowColor;     }      /// <summary>     /// 切换     /// </summary>     public void OnSwitchClick()     {         isPhonePwd = !isPhonePwd;         btnSwitchText.text = isPhonePwd ? "切换验证码登录 ":"切换密码登录";         inputPwdHint.text = isPhonePwd ? "请输入6位数密码" : "请输入验证码";         btnSend.gameObject.SetActive(!isPhonePwd);         inputPwd.text = "";         inputPwd.contentType = isPhonePwd ? InputField.ContentType.Standard : InputField.ContentType.IntegerNumber;         inputPwd.characterLimit = isPhonePwd ? 6 : 4;     }      /// <summary>     /// 进入设置页     /// </summary>     public void EnterSetting()     {         AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");         AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");         jo.Call("sendUserInfo", "18811112222");         jo.Call("sendUpdateInfo", 1);         jo.Call("enterSettingActivity");     }

    /// <summary>     /// 点击发送验证码     /// </summary>     public void OnSendVerifyCode()     {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");         AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");         TencentCaptchaConfig config = new TencentCaptchaConfig("2099959054", "roobo-tencent-captcha", true);         string configJson=JsonUtility.ToJson(config);         jo.Call("openTencentCapture", configJson);     }

    [System.Serializable]
    class TencentCaptchaConfig
    {
        public string appId;
        public string bizState;
        public bool enableDarkMode;

        public TencentCaptchaConfig(string appId,string bizState,bool enableDarkMode)
        {
            this.appId = appId;
            this.bizState = bizState;
            this.enableDarkMode = enableDarkMode;
        }
    }


    /// <summary>     /// login button     /// </summary>     public void OnLoginSubmit()     {         string name = inputName.text;         string pwd = inputPwd.text;         if (isPhonePwd)         {                     }         else         {                      }     }      /// <summary>     ///      /// </summary>     public void OnPrivacyClick()     {      }      /// <summary>     ///     /// </summary>     public void OnUserAgreement()     {      }      /// <summary>     ///      /// </summary>     public void GetWifiInfo()     {         //获取Wifi信息         AndroidJavaObject javaObject = new AndroidJavaObject("com.pudding.settingplugins.SettingPlugins");         string wifiData = javaObject.Call<string>("obtainWifiInfo");         OnWifiDataBack(wifiData);     }      [System.Serializable]     class WifiJson {         public int strength;         public string ssid;     }       void OnWifiDataBack(string wifiData)     {         string log = "";          if (wifiData.Length==0)         {             Debug.Log("a=============OnWifiDataBack=WIFI 未连接");             return;         }         WifiJson wifi = JsonUtility.FromJson<WifiJson>(wifiData);         //分析wifi信号强度         //获取RSSI，RSSI就是接受信号强度指示。         //得到的值是一个0到-100的区间值，是一个int型数据，其中0到-50表示信号最好，         //-50到-70表示信号偏差，小于-70表示最差，         //有可能连接不上或者掉线，一般Wifi已断则值为-200。                  log += wifiData + "\n";         log += "WIFI名："+wifi.ssid + "\n";         log +="信号强度："+ wifi.strength + "\n";                 Debug.Log("a=============OnWifiDataReturn=" + log);     } } 
=======
﻿using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI;  using Sirenix.OdinInspector; using System; using UnityEngine.SceneManagement;

public class LoginView : MonoBehaviour {     bool isPhonePwd = true;     Canvas canvas;     Button btnBack;     Button btnSend;     InputField inputName;     InputField inputPwd;     Button btnSubmit;     Text btnSubmitText;     Button btnSwitch;     Text inputPwdHint;     Text btnSwitchText;      [ShowInInspector]     [LabelText("发送验证码文本")]     private Text btnSendText;     // Start is called before the first frame update     void Start()     {         canvas = GameObject.Find("Canvas").GetComponent<Canvas>();         btnBack = GameObject.Find("Canvas/BtnBack").GetComponent<Button>();         btnSend = GameObject.Find("Canvas/InputPassword/BtnSend").GetComponent<Button>();         btnSend.gameObject.SetActive(!isPhonePwd);          inputName = GameObject.Find("Canvas/InputName").GetComponent<InputField>();         inputPwd = GameObject.Find("Canvas/InputPassword").GetComponent<InputField>();         btnSubmit = GameObject.Find("Canvas/BtnSubmit").GetComponent<Button>();         btnSubmitText = GameObject.Find("Canvas/BtnSubmit/Text").GetComponent<Text>();          btnSwitch = GameObject.Find("Canvas/BtnSwitch").GetComponent<Button>();         inputPwdHint = GameObject.Find("Canvas/InputPassword/Placeholder").GetComponent<Text>();         btnSwitchText = GameObject.Find("Canvas/BtnSwitch/Text").GetComponent<Text>();        }      /// <summary>     /// 锟斤拷锟截帮拷钮     /// </summary>     public void OnBackPress()     {         SceneManager.LoadScene("Realm");     }      /// <summary>     /// 锟斤拷锟斤拷欠锟缴碉拷锟斤拷锟铰?     /// </summary>     public void CheckEnableLogin()     {         string name = inputName.text;         string password = inputPwd.text;         bool enableSubmit = (name.Length > 0 && password.Length > 0);         btnSubmit.enabled = enableSubmit;         btnSubmit.interactable = enableSubmit;         Color nowColor;         string color = enableSubmit ? "#FFFFFF" : "#AEAEAE";         ColorUtility.TryParseHtmlString(color, out nowColor);         btnSubmitText.color = nowColor;     }      /// <summary>     /// 切换     /// </summary>     public void OnSwitchClick()     {         isPhonePwd = !isPhonePwd;         btnSwitchText.text = isPhonePwd ? "切换验证码登录 ":"切换密码登录";         inputPwdHint.text = isPhonePwd ? "请输入6位数密码" : "请输入验证码";         btnSend.gameObject.SetActive(!isPhonePwd);         inputPwd.text = "";         inputPwd.contentType = isPhonePwd ? InputField.ContentType.Standard : InputField.ContentType.IntegerNumber;         inputPwd.characterLimit = isPhonePwd ? 6 : 4;     }      ///// <summary>     ///// 进入设置页     ///// </summary>     //public void EnterSetting()     //{     //    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");     //    AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");     //    jo.Call("sendUserInfo", "18811112222");     //    jo.Call("sendUpdateInfo", 1);     //    jo.Call("enterSettingActivity");     //}      /// <summary>     /// 点击发送验证码     /// </summary>     public void OnSendVerifyCode()     {      }      /// <summary>     /// 锟斤拷录     /// </summary>     public void OnLoginSubmit()     {         string name = inputName.text;         string pwd = inputPwd.text;         if (isPhonePwd)         {                     }         else         {                      }     }      /// <summary>     ///      /// </summary>     public void OnPrivacyClick()     {      }      /// <summary>     ///     /// </summary>     public void OnUserAgreement()     {      } } 
>>>>>>> 81e2542955d8bc901791fe674ece904c785e6276
