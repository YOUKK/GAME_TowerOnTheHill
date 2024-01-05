using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
	[SerializeField]
	private GameObject settingPopup;

	[SerializeField]
	private AudioMixer audioMixer;
	[SerializeField]
	private Slider sliderMaster;
	[SerializeField]
	private Slider sliderBGM;
	[SerializeField]
	private Slider sliderEffect;


	private void Start()
	{
		if (PlayerPrefs.HasKey("MasterVolume"))
		{
			audioMixer.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
			sliderMaster.value = PlayerPrefs.GetFloat("MasterVolume");
		}

		if (PlayerPrefs.HasKey("BGMVolume"))
		{
			audioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGMVolume"));
			sliderBGM.value = PlayerPrefs.GetFloat("BGMVolume");
		}

		if (PlayerPrefs.HasKey("EffectVolume"))
		{
			audioMixer.SetFloat("Effect", PlayerPrefs.GetFloat("EffectVolume"));
			sliderEffect.value = PlayerPrefs.GetFloat("EffectVolume");
		}
	}

	public void MasterControl()
	{
		if (sliderMaster.value == -40f)
			audioMixer.SetFloat("Master", -80);
		else
			audioMixer.SetFloat("Master", sliderMaster.value);

		PlayerPrefs.SetFloat("MasterVolume", sliderMaster.value);
	}

	public void BGMControl()
	{
		if (sliderBGM.value == -40f)
			audioMixer.SetFloat("BGM", -80);
		else
			audioMixer.SetFloat("BGM", sliderBGM.value);

		PlayerPrefs.SetFloat("BGMVolume", sliderBGM.value);
	}

	public void EffectControl()
	{
		if (sliderEffect.value == -40f)
			audioMixer.SetFloat("Effect", -80);
		else
			audioMixer.SetFloat("Effect", sliderEffect.value);

		PlayerPrefs.SetFloat("EffectVolume", sliderEffect.value);
	}

	public void OpenSetting()
	{
		settingPopup.SetActive(true);
	}

	public void CloseSetting()
	{
		settingPopup.SetActive(false);
	}
}
