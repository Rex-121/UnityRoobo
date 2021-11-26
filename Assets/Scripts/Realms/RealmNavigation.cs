using UnityEngine;

public class RealmNavigation : MonoBehaviour
{

    private static RealmNavigation instance = null;

    private void Awake()
    {
 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }

        Navigation.Shared.Start();
    }
}
