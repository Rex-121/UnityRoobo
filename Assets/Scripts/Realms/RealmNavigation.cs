using UnityEngine;

public class RealmNavigation : MonoBehaviour
{

    private static RealmNavigation instance = null;

    private void Start()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }

        Navigation.Shared.Start();
    }
}
