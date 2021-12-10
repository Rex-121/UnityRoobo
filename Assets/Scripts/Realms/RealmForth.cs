using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmForth : MonoBehaviour
{

    public GameObject listPre;

    private GameObject list;

    private void OnEnable()
    {

        list = Instantiate(listPre, transform);

    }

    private void OnDisable()
    {
        if (list == null) return;
        Destroy(list);
    }

}
