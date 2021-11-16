
using UnityEngine;
using UniRx;

using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {

        Logging.Log("fasd");
        HttpRx.Post<User.Token>("pudding/manager/v1/provisional/tempAccount/login", new ABC("50000000593", "lu23t0gk5110")).Subscribe((r) =>
        {
            User.Default.token = r;
            Logging.Log(r.accessToken);
            Logging.Log("fasd");
        }, (e) =>
         {
             Logging.Log("fasd");
             Logging.Log("error");
             Logging.Log(e.Message);
         }, () => { Logging.Log("com"); }).AddTo(this);


    }

}


