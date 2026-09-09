using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [Header("Sounds")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip selectSound;

    [Header("Volume")]
    [SerializeField] private float volumeMultiplier = 1f;

    private AudioSource audioSource;
    private Button button;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;

        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(PlaySelectSound);
        }
    }

    // -----------------------------------------
    // HOVER
    // -----------------------------------------

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(hoverSound);
    }

    // -----------------------------------------
    // SELECT / CLICK
    // -----------------------------------------

    private void PlaySelectSound()
    {
        PlaySound(selectSound);
    }

    // -----------------------------------------
    // PLAY SOUND
    // -----------------------------------------

    private void PlaySound(AudioClip clip)
    {
        if (clip == null)
            return;

        float sfxVolume = 0.5f;

        if (AudioSettingsManager.Instance != null)
        {
            sfxVolume =
                AudioSettingsManager.Instance.GetSFXVolume();
        }

        audioSource.PlayOneShot(
            clip,
            sfxVolume * volumeMultiplier
        );
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(PlaySelectSound);
        }
    }
}