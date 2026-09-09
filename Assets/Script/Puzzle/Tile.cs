using UnityEngine;

public class Tile : MonoBehaviour
{
    // Reference to the puzzle board script
    private PuzzleBoard board;

    // This method is called when the tile is created and gives the tile access to the PuzzleBoard script
    public void Setup(PuzzleBoard puzzleBoard)
    {
        board = puzzleBoard;
    }

    // Mouse Click Detection

    // This method runs when the player clicks the tile
    private void OnMouseDown()
    {
        // Ask the board to move this tile
        board.TryMove(transform);
    }
}