using UnityEngine;
using System.Collections;

public class effectSoundScript : MonoBehaviour
{
    public AudioSource[] effectSound;
    public AudioSource music;

    public void effectSoundPlay(int i)
    {
        if (PlayerPrefs.GetInt("isMute") == 0)
            effectSound[i].Play();
    }

    public void Start()
    {
        music.Play();
    }

    public void Update()
    {
        if (PlayerPrefs.GetInt("isMute") == 0)
            music.mute = false;
        else
            music.mute = true;
    }
}