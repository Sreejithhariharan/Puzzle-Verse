using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip narutoMusic;
    [SerializeField] private AudioClip onePieceMusic;
    [SerializeField] private AudioClip bleachMusic;

    private float musicVolume = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        transform.SetParent(null);

        DontDestroyOnLoad(gameObject);

        // Load saved music volume
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        if (audioSource != null)
        {
            audioSource.volume = musicVolume;
            audioSource.loop = true;
        }
    }

    private void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    // -----------------------------------------
    // SCENE MUSIC
    // -----------------------------------------

    private void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            PlayMusic(mainMenuMusic, "MainMenu");
        }
        else if (sceneName == "AnimeSelection")
        {
            PlayMusic(mainMenuMusic, "AnimeSelection");
        }
        else if (sceneName == "PuzzleGame")
        {
            PlaySelectedAnimeMusic();
        }
    }

    // -----------------------------------------
    // ANIME MUSIC
    // -----------------------------------------

    public void PlaySelectedAnimeMusic()
    {
        string selectedAnime =
            PlayerPrefs.GetString("SelectedAnime", "Naruto");


        switch (selectedAnime)
        {
            case "Naruto":
                PlayMusic(narutoMusic, "Naruto");
                break;

            case "OnePiece":
                PlayMusic(onePieceMusic, "One Piece");
                break;

            case "Bleach":
                PlayMusic(bleachMusic, "Bleach");
                break;

            default:
                PlayMusic(narutoMusic, "Naruto");
                break;
        }
    }

    // -----------------------------------------
    // PLAY MUSIC
    // -----------------------------------------

    private void PlayMusic(AudioClip clip, string musicName)
    {
        if (clip == null)
        {
            Debug.LogError("Music clip is NULL: " + musicName);
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is NOT assigned!");
            return;
        }

        if (audioSource.clip == clip && audioSource.isPlaying)
        {
            return;
        }

        audioSource.Stop();

        audioSource.clip = clip;

        // IMPORTANT:
        // Use saved slider volume.
        // DO NOT set this to 1.
        audioSource.volume = musicVolume;

        audioSource.loop = true;
        audioSource.Play();
    }

    // -----------------------------------------
    // VOLUME
    // -----------------------------------------

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;

        if (audioSource != null)
        {
            audioSource.volume = musicVolume;
        }

        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
}