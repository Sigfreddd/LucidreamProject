using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [SerializeField] private GameObject validationWindow;
    [SerializeField] private Pause pause;

    //Initialisation des param?tres sauvegard?s
    public void Awake()
    {
        audioMixer.SetFloat("music", PlayerPrefs.GetFloat("musicVolume", 0f));
        audioMixer.SetFloat("sound", PlayerPrefs.GetFloat("soundVolume", 0f));
    }

    public void Start()
    {
        audioMixer.GetFloat("music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;

        audioMixer.GetFloat("sound", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("music", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetVolumeSound(float volume)
    {
        audioMixer.SetFloat("sound", volume);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public void ResetParameters()
    {
        SetVolumeMusic(0f);
        musicSlider.value = (0f);

        SetVolumeMusic(0f);
        soundSlider.value = (0f);
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteKey("Highscore");

        CloseValidationWindow();
    }

    public void OpenValidationWindow()
    {
        validationWindow.SetActive(true);
    }

    public void CloseValidationWindow()
    {
        validationWindow.SetActive(false);
    }

    public void OpenParametersWindow()
    {
        gameObject.SetActive(true);
        if (pause != null)
            pause.DesactivatePause();
    }

    public void CloseParametersWindow()
    {
        gameObject.SetActive(false);
        if (pause != null)
            pause.ReactivatePause();
        PlayerPrefs.Save();
    }
}
