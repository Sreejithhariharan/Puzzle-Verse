using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Main Menu button functions
    public void PlayGame() // Loads the first level when the play button is pressed
    {
        SceneManager.LoadScene("AnimeSelection");
    }

    public void QuitGame() // Quits the game when the quit button is pressed. Note: This will not work in the editor, but will work in a built version of the game.
    {
        Application.Quit();
    }

    public void GoToMainMenu() // Loads the main menu when the main menu button is pressed
    {
        SceneManager.LoadScene("MainMenu");
    }
}
