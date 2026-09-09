using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        if (AudioSettingsManager.Instance == null)
            return;

        musicSlider.value =
            AudioSettingsManager.Instance.GetMusicVolume();

        sfxSlider.value =
            AudioSettingsManager.Instance.GetSFXVolume();
    }
}