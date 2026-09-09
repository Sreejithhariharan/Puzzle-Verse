using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject puzzleCompletePanel;

    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;

    [Header("Level Complete")]
    [SerializeField] private TMP_Text timeLeftText;

    [Header("GO Animation")]
    [SerializeField] private GameObject goText;


    // -----------------------------------------
    // NEXT LEVEL PANEL
    // -----------------------------------------

    public void ShowNextLevelPanel(float timeLeft)
    {
        nextLevelPanel.SetActive(true);

        int seconds = Mathf.CeilToInt(timeLeft);

        timeLeftText.text = "TIME LEFT\n" + seconds + " SECONDS";
    }


    // -----------------------------------------
    // GAME OVER
    // -----------------------------------------

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }


    // -----------------------------------------
    // PUZZLE COMPLETE
    // -----------------------------------------

    public void ShowPuzzleCompletePanel()
    {
        puzzleCompletePanel.SetActive(true);
    }


    // -----------------------------------------
    // HIDE ALL PANELS
    // -----------------------------------------

    public void HideAllPanels()
    {
        nextLevelPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        puzzleCompletePanel.SetActive(false);
    }


    // -----------------------------------------
    // GO
    // -----------------------------------------

    public void ShowGO()
    {
        if (goText != null)
            goText.SetActive(true);
    }

    public void HideGO()
    {
        if (goText != null)
            goText.SetActive(false);
    }


    // -----------------------------------------
    // TIMER
    // -----------------------------------------

    public void UpdateTimer(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);

        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}