
using UnityEngine;
using UniRx;

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


    }


}

