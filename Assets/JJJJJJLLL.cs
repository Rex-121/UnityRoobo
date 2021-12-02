using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RenderHeads.Media.AVProVideo;
using System;

public class JJJJJJLLL : MonoBehaviour
{

    public MediaPlayer p;

    private void Awake()
    {

      //  var k = "YYlhwC/FYuQKGR+kK5VVSJUicy2B+AYNbzwjBfOGX3u7w0zh3f9LNKEQFXDHyf9RIlcMCtbmtCsITsYfYhzpsKz7y9v+lbcJCVl40ucIhmhIn5I3n2OGpT6m0XMJ49i7YMvbOVzSGhNjnwNkZKA1N16sLRBG1mUAd321Rae4ZNY";

        p.PlatformOptionsAndroid.keyAuth.overrideDecryptionKey = Convert.FromBase64String("ZGRlNGIxZjhhOWU2YjgxNA==");
    }


    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}
