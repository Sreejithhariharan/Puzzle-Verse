using UnityEngine;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager Instance;

    [Header("Default Volume")]
    [Range(0f, 1f)]
    [SerializeField] private float defaultMusicVolume = 0.5f;

    [Range(0f, 1f)]
    [SerializeField] private float defaultSFXVolume = 0.5f;

    private float musicVolume;
    private float sfxVolume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicVolume = PlayerPrefs.GetFloat(
            "MusicVolume",
            defaultMusicVolume
        );

        sfxVolume = PlayerPrefs.GetFloat(
            "SFXVolume",
            defaultSFXVolume
        );
    }

    // -----------------------------------------
    // MUSIC
    // -----------------------------------------

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();

        ApplyMusicVolume();
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    private void ApplyMusicVolume()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetMusicVolume(musicVolume);
        }
    }

    // -----------------------------------------
    // SFX
    // -----------------------------------------

    public void SetSFXVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    // -----------------------------------------
    // INITIALIZE MUSIC VOLUME
    // -----------------------------------------

    public void ApplySavedMusicVolume()
    {
        ApplyMusicVolume();
    }
}