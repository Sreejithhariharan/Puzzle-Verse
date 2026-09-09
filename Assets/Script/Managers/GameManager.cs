using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PuzzleBoard board;
    [SerializeField] private UIManager ui;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Puzzle Materials")]
    [SerializeField] private Material[] narutoPuzzleMaterial;
    [SerializeField] private Material[] onePiecePuzzleMaterial;
    [SerializeField] private Material[] bleachPuzzleMaterial;

    [Header("Level Settings")]
    [SerializeField] private float[] levelTimeLimits = { 60f, 90f, 120f };

    // Current level
    private int currentLevel = 1;

    // Current puzzle size
    private int puzzleSize;

    // Timer
    private float timer;

    // Game state
    private bool gameEnded;

    private bool timerStarted = false;

    private void Start()
    {
        // Play music for selected anime
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlaySelectedAnimeMusic();
        }

        StartLevel();
    }

    private void Update()
    {
        if (gameEnded)
            return;

        // Wait until puzzle has finished shuffling
        if (!board.IsPuzzleReady)
            return;

        // Start timer only once
        if (!timerStarted)
        {
            StartCoroutine(StartTimerSequence());
            return;
        }

        timer += Time.deltaTime;

        float remainingTime =
            GetCurrentTimeLimit() - timer;

        timerText.text =
            Mathf.Max(0, Mathf.Ceil(remainingTime)).ToString();

        if (timer >= GetCurrentTimeLimit())
        {
            GameOver();
        }
    }

    private IEnumerator StartTimerSequence()
    {
        timerStarted = true;

        // Show GO
        ui.ShowGO();

        // Wait so player can see it
        yield return new WaitForSeconds(0.7f);

        // Hide GO
        ui.HideGO();

        // Timer officially starts AFTER GO
        timer = 0f;
    }

    // -----------------------------
    // START LEVEL
    // -----------------------------

    private void StartLevel()
    {
        gameEnded = false;
        timer = 0f;

        puzzleSize = GetPuzzleSize();

        // Show timer
        timerText.gameObject.SetActive(true);

        // Immediately reset timer display
        timerText.text = Mathf.Ceil(GetCurrentTimeLimit()).ToString();

        // Hide panels
        ui.HideAllPanels();

        // Get selected anime material
        Material selectedMaterial = GetSelectedPuzzleMaterial();

        // Create puzzle
        board.CreateBoard(puzzleSize, selectedMaterial);
    }

    // -----------------------------
    // PUZZLE SIZE
    // -----------------------------

    private int GetPuzzleSize()
    {
        switch (currentLevel)
        {
            case 1:
                return 3;

            case 2:
                return 4;

            case 3:
                return 5;

            default:
                return 5;
        }
    }

    // -----------------------------
    // TIME
    // -----------------------------

    private float GetCurrentTimeLimit()
    {
        int index = currentLevel - 1;

        if (index >= 0 && index < levelTimeLimits.Length)
        {
            return levelTimeLimits[index];
        }

        return 120f;
    }

    // -----------------------------
    // SELECTED ANIME MATERIAL
    // -----------------------------

    private Material GetSelectedPuzzleMaterial()
    {
        string selectedAnime =
            PlayerPrefs.GetString("SelectedAnime", "Naruto");

        int index = currentLevel - 1;

        switch (selectedAnime)
        {
            case "Naruto":
                return narutoPuzzleMaterial[index];

            case "OnePiece":
                return onePiecePuzzleMaterial[index];

            case "Bleach":
                return bleachPuzzleMaterial[index];

            default:
                return narutoPuzzleMaterial[index];
        }
    }

    // -----------------------------
    // LEVEL COMPLETED
    // -----------------------------

    public void LevelCompleted()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        timerText.gameObject.SetActive(false);

        if (currentLevel < 3)
        {
            float timeLeft =
                Mathf.Max(0f, GetCurrentTimeLimit() - timer);

            ui.ShowNextLevelPanel(timeLeft);
        }
        else
        {
            ui.ShowPuzzleCompletePanel();
        }
    }

    // -----------------------------
    // NEXT LEVEL
    // -----------------------------

    public void NextLevel()
    {
        if (currentLevel >= 3)
        {
            // Final level
            SceneManager.LoadScene("MainMenu");
            return;
        }

        currentLevel++;

        StartLevel();
    }

    // -----------------------------
    // GAME OVER
    // -----------------------------

    public void GameOver()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        timerText.gameObject.SetActive(false);

        ui.ShowGameOverPanel();
    }

    // -----------------------------
    // RETRY
    // -----------------------------

    public void Retry()
    {
        StartLevel();
    }

    // -----------------------------
    // MAIN MENU
    // -----------------------------

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        currentLevel = 1;

        StartLevel();
    }
}