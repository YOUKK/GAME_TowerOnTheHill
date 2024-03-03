using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager instance = null;
	public static SoundManager Instance { 
		get
		{
			if (instance == null)
			{
				return null;
			}
		 return instance; 
		} 
	}

	AudioSource bgmSource;
	AudioSource effectSource;

	private void Awake()
	{
		// ΩÃ±€≈Ê
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}

		bgmSource = transform.GetChild(0).GetComponent<AudioSource>();
		effectSource = transform.GetChild(1).GetComponent<AudioSource>();

		PlayBGM("Title");
	}

	public void StopBGM()
	{
		bgmSource.Stop();
	}

	public void PlayBGM(string pileName)
	{
		string path = $"Sounds/BGM/{pileName}";
		AudioClip audioClip = Resources.Load<AudioClip>(path);
		bgmSource.clip = audioClip;
		bgmSource.Play();
	}

	public void PlayEffect(string pileName)
	{
		string path = $"Sounds/Effect/{pileName}";
		//print(path);
		AudioClip audioClip = Resources.Load<AudioClip>(path);
		effectSource.PlayOneShot(audioClip);
	}
}
