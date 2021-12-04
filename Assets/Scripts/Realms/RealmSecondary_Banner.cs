using UnityEngine;
using UnityEngine.UI;

public class RealmSecondary_Banner : MonoBehaviour
{

    public Image title;


    


    public Image[] tintable;


    void Start()
    {

        SetTintColor();

    }

    public void SetTitle(Sprite titleImage)
    {
        title.sprite = titleImage;
    }

    public void SetTintColor()
    {
        var subject = Navigation.Shared.currentSubject;

        if (subject == null) return;


        var theme = subject.theme;

        foreach (var image in tintable)
        {
            image.color = theme;
        }
    }
}
