using UnityEngine;
using TMPro;

public class PuzzleBackgroundManager : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] private MeshRenderer backgroundRenderer;

    [Header("Background Materials")]
    [SerializeField] private Material narutoBackground;
    [SerializeField] private Material onePieceBackground;
    [SerializeField] private Material bleachBackground;

    [Header("Timer")]
    [SerializeField] private RectTransform timerText;

    [Header("Timer Positions")]
    [SerializeField] private Vector2 narutoTimerPosition;
    [SerializeField] private Vector2 onePieceTimerPosition;
    [SerializeField] private Vector2 bleachTimerPosition;

    private void Start()
    {
        SetBackgroundAndTimer();
    }

    private void SetBackgroundAndTimer()
    {
        string selectedAnime =
            PlayerPrefs.GetString("SelectedAnime", "OnePiece");

        switch (selectedAnime)
        {
            case "Naruto":

                backgroundRenderer.material = narutoBackground;

                timerText.anchoredPosition = narutoTimerPosition;

                break;

            case "OnePiece":

                backgroundRenderer.material = onePieceBackground;

                timerText.anchoredPosition = onePieceTimerPosition;

                break;

            case "Bleach":

                backgroundRenderer.material = bleachBackground;

                timerText.anchoredPosition = bleachTimerPosition;

                break;

            default:

                backgroundRenderer.material = onePieceBackground;

                timerText.anchoredPosition = onePieceTimerPosition;

                break;
        }
    }
}