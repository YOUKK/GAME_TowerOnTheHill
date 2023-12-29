using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager instance;
	public static SoundManager Instance { get { Init(); return instance; } }

	AudioSource bgmSource;
	AudioSource effectSource;

	private void Awake()
	{
		Init();

		bgmSource = transform.GetChild(0).GetComponent<AudioSource>();
		effectSource = transform.GetChild(1).GetComponent<AudioSource>();

		PlayBGM("Title");
	}

	private static void Init()
	{
		// ΩÃ±€≈Ê
		if(instance == null)
		{
			GameObject go = GameObject.Find("SoundManager");
			DontDestroyOnLoad(go);
			instance = go.GetComponent<SoundManager>();
		}
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
	}
}
