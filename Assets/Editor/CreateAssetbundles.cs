using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class CreateAssetbundles
{

    [MenuItem("AssetsBundle/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string dir = "AssetBundlesa";

        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }

        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.Android);
    }


}
