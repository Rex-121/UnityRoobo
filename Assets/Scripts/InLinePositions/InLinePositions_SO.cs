using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "多个一行位置", menuName = "位置/多个一行位置")]
public class InLinePositions_SO : SerializedScriptableObject
{

    public Dictionary<int, InLineDefaultPosition_SO> positionDic = new Dictionary<int, InLineDefaultPosition_SO>() { };


    public InLineDefaultPosition_SO GetPositionByLength(int length)
    {

        return positionDic[length];

    }
}
