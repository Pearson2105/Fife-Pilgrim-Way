using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer gameMixer;

    [Header("Master Volume")]
    public Slider masterSlider;
    public TMP_Text masterValueText;       

    [Header("Music Volume")]
    public Slider musicSlider;
    public TMP_Text musicValueText;

    [Header("SFX Volume")]
    public Slider sfxSlider;
    public TMP_Text sfxValueText;

    private const string MASTER_KEY = "MasterVol";
    private const string MUSIC_KEY = "MusicVol";
    private const string SFX_KEY = "SFXVol";

    private void Start()
    {
        LoadAndApplyVolumes();
    }

    private void LoadAndApplyVolumes()
    {
        float master = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float music = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfx = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        masterSlider.value = master * 100f;
        musicSlider.value = music * 100f;
        sfxSlider.value = sfx * 100f;

        ApplyVolume("MasterVolume", master * 100f, masterValueText);
        ApplyVolume("MusicVolume", music * 100f, musicValueText);
        ApplyVolume("SFXVolume", sfx * 100f, sfxValueText);
    }

    private void ApplyVolume(string exposedName, float value01to100, TMP_Text displayText)
    {
        float db = Mathf.Log10(value01to100 * 0.01f) * 20f; // -80 .. 0 dB
        gameMixer.SetFloat(exposedName, db);

        if (displayText != null)
            displayText.text = Mathf.RoundToInt(value01to100).ToString();
    }

    public void OnMasterVolumeChanged(float value)
    {
        ApplyVolume("MasterVolume", value, masterValueText);
        PlayerPrefs.SetFloat(MASTER_KEY, value / 100f);
    }

    public void OnMusicVolumeChanged(float value)
    {
        ApplyVolume("MusicVolume", value, musicValueText);
        PlayerPrefs.SetFloat(MUSIC_KEY, value / 100f);
    }

    public void OnSFXVolumeChanged(float value)
    {
        ApplyVolume("SFXVolume", value, sfxValueText);
        PlayerPrefs.SetFloat(SFX_KEY, value / 100f);
    }
}