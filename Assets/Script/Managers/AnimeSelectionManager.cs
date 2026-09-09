using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimeSelectionManager : MonoBehaviour
{
    [Header("Anime Cards")]
    [SerializeField] private AnimeCardHover narutoCard;
    [SerializeField] private AnimeCardHover onePieceCard;
    [SerializeField] private AnimeCardHover bleachCard;

    // -----------------------------------------
    // NARUTO
    // -----------------------------------------

    public void SelectNaruto()
    {
        SelectAnime("Naruto", narutoCard);
    }




    // -----------------------------------------
    // ONE PIECE
    // -----------------------------------------

    public void SelectOnePiece()
    {
        SelectAnime("OnePiece", onePieceCard);
    }



    // -----------------------------------------
    // BLEACH
    // -----------------------------------------

    public void SelectBleach()
    {
        SelectAnime("Bleach", bleachCard);
    }

    // -----------------------------------------
    // SELECT ANIME
    // -----------------------------------------

    private void SelectAnime(
     string animeName,
     AnimeCardHover selectedCard)
    {
        PlayerPrefs.SetString("SelectedAnime", animeName);
        PlayerPrefs.SetInt("CurrentPuzzle", 0);

        PlayerPrefs.Save();

        narutoCard.SetSelected(false);
        onePieceCard.SetSelected(false);
        bleachCard.SetSelected(false);

        selectedCard.SetSelected(true);

        SceneManager.LoadScene("PuzzleGame");
    }

    // -----------------------------------------
    // BACK TO MAIN MENU
    // -----------------------------------------

    public void BackToMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }
}