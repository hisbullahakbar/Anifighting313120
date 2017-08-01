 using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	//class untuk mengatur music atau sfx yang akan diputar
	public AudioSource[] effectSound;
	public bool[] isOneEffectPerTime;

	public bool isDontHaveMusic;
	public AudioSource music;

	public void effectSoundPlay(int i)
	{
		if (PlayerPrefs.GetInt ("isMute") == 0) {
			if (PlayerPrefs.GetInt("isMute") == 0)
				effectSound [i].mute = false;
			
			effectSound [i].Play ();

			for (int j = 0; j < effectSound.Length; j++) {
				if (isOneEffectPerTime [i]) {
					if (i != j) {
						effectSound [j].mute = true;
					}
				}
			}
		}
	}
	
	public void Start()
	{
		if (!isDontHaveMusic) {
			music.Play ();
		}
	}

	public void StartDelayedMusic(AudioClip delayedMusic){
		isDontHaveMusic = false;
		music.clip = delayedMusic;
		music.Play ();
	}
	
	public void Update()
	{
		if (!isDontHaveMusic) {
			if (PlayerPrefs.GetInt ("isMute") == 0)
				music.mute = false;
			else
				music.mute = true;
		}
	}
}
