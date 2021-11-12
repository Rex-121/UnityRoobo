using System;
using UnityEngine;
public class CoursewareCredit : MonoBehaviour, CoursewareCreditProtocol
{

    [SerializeField]
    private GameObject creditPanel;

    public void PlayCreditOnScreen(CreditData credit, Action endPlay)
    {

        Logging.Log("得分: " + credit.score);

        creditPanel.SetActive(true);

        Credit.Default.PlayCreditOnScreen(credit, () =>
        {
            endPlay();
            creditPanel.SetActive(false);
        });
    }

}
