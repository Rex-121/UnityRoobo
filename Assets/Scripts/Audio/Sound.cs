using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "声音", menuName = "单例SO/声音")]
public class Sound : SingletonSO<Sound>
{

    public GameObject gb;

    public AudioSource audio = new AudioSource();


    public AudioClip clip;

    public void Play()
    {

        var k = GameObject.Instantiate(gb);

        audio = k.GetComponent<AudioSource>();

        audio.clip = clip;

        audio.Play();
    }


    private void OnEnable()
    {
        //var k = GameObject.Instantiate(gb);

        //audio = k.GetComponent<AudioSource>();

        //Debug.Log("fasfasd");
    }


}
