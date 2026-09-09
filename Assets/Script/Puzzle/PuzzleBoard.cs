using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform piecePrefab;
    [SerializeField] private Transform boardParent;
    [SerializeField] private GameManager gameManager;

    [Header("Board Settings")]
    [SerializeField] private float boardWidth = 4f;
    [SerializeField] private float previewTime = 1.5f;
    [SerializeField] private float shuffleMoveTime = 0.08f;
    [SerializeField] private int shuffleMoves = 20;

    [Header("Completion Animation")]
    [SerializeField] private float finalTileMoveTime = 0.3f;
    [SerializeField] private float completionPause = 0.5f;

    [Header("Full Image Completion")]
    [SerializeField] private GameObject completeImage;
    [SerializeField] private float fullImagePause = 1.0f;

    [Header("Complete Image Pop")]
    [SerializeField] private float popDuration = 0.35f;
    [SerializeField] private float popStartScale = 0.85f;
    [SerializeField] private float popOvershoot = 1.05f;

    [Header("Complete Image Materials")]
    [SerializeField] private Material[] narutoCompleteMaterials;
    [SerializeField] private Material[] onePieceCompleteMaterials;
    [SerializeField] private Material[] bleachCompleteMaterials;

    private List<Transform> pieces = new();

    private int size;
    private int emptyLocation;

    // Prevent clicking while the puzzle is preparing
    private bool puzzleReady = false;
    public bool IsPuzzleReady => puzzleReady;

    private Material GetCompleteImageMaterial()
    {
        string selectedAnime =
            PlayerPrefs.GetString("SelectedAnime", "Naruto");

        int index = size - 3;

        switch (selectedAnime)
        {
            case "Naruto":

                if (index >= 0 &&
                    index < narutoCompleteMaterials.Length)
                {
                    return narutoCompleteMaterials[index];
                }

                break;


            case "OnePiece":

                if (index >= 0 &&
                    index < onePieceCompleteMaterials.Length)
                {
                    return onePieceCompleteMaterials[index];
                }

                break;


            case "Bleach":

                if (index >= 0 &&
                    index < bleachCompleteMaterials.Length)
                {
                    return bleachCompleteMaterials[index];
                }

                break;
        }

        return null;
    }
    // Create Puzzle Board
    public void CreateBoard(int newSize, Material puzzleMaterial)
    {
        StopAllCoroutines();

        size = newSize;
        puzzleReady = false;

        ClearBoard();

        GeneratePieces(puzzleMaterial);

        StartCoroutine(StartPuzzleAnimation());
    }

    // Generate Puzzle Pieces
    private void GeneratePieces(Material puzzleMaterial)
    {
        float gapThickness = 0.02f;

        float uvWidth = 1f / size;
        float tileSize = boardWidth / size;
        if (completeImage != null)
        {
            completeImage.SetActive(false);

            MeshRenderer completeRenderer =
                completeImage.GetComponent<MeshRenderer>();

            if (completeRenderer != null)
            {
                Material completeMaterial =
                    GetCompleteImageMaterial();

                if (completeMaterial != null)
                {
                    completeRenderer.material = completeMaterial;
                }
            }
        }

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece =
                    Instantiate(piecePrefab, boardParent);

                MeshRenderer renderer =
                    piece.GetComponent<MeshRenderer>();

                if (renderer != null)
                {
                    renderer.material = puzzleMaterial;
                }

                pieces.Add(piece);

                // Position
                float x =
                    -boardWidth / 2f +
                    tileSize / 2f +
                    col * tileSize;

                float y =
                    boardWidth / 2f -
                    tileSize / 2f -
                    row * tileSize;

                piece.localPosition =
                    new Vector3(x, y, 0f);

                // Scale
                piece.localScale =
                    Vector3.one * (tileSize - gapThickness);

                // Name
                piece.name =
                    $"{row * size + col}";

                // UV
                MeshFilter meshFilter =
                    piece.GetComponent<MeshFilter>();

                if (meshFilter != null)
                {
                    Mesh mesh = meshFilter.mesh;

                    float uvGap = 0.005f;

                    Vector2[] uv = new Vector2[4];

                    uv[0] = new Vector2(
                        (uvWidth * col) + uvGap,
                        1f - ((uvWidth * (row + 1)) - uvGap)
                    );

                    uv[1] = new Vector2(
                        (uvWidth * (col + 1)) - uvGap,
                        1f - ((uvWidth * (row + 1)) - uvGap)
                    );

                    uv[2] = new Vector2(
                        (uvWidth * col) + uvGap,
                        1f - ((uvWidth * row) + uvGap)
                    );

                    uv[3] = new Vector2(
                        (uvWidth * (col + 1)) - uvGap,
                        1f - ((uvWidth * row) + uvGap)
                    );

                    mesh.uv = uv;
                }

                // Setup tile
                Tile tile =
                    piece.GetComponent<Tile>();

                if (tile != null)
                {
                    tile.Setup(this);
                }
            }
        }

        // Start with COMPLETE image visible.
        // The last tile is still visible for the preview.
        emptyLocation = -1;
    }

    // -----------------------------------------
    // START ANIMATION
    // -----------------------------------------

    private IEnumerator StartPuzzleAnimation()
    {
        // Complete image stays visible
        yield return new WaitForSeconds(previewTime);

        // Now hide the last tile
        emptyLocation = (size * size) - 1;

        pieces[emptyLocation].gameObject.SetActive(false);

        // Smoothly shuffle
        yield return StartCoroutine(SmoothShuffle());

        // Player can now move pieces
        puzzleReady = true;
    }

    // -----------------------------------------
    // SMOOTH SHUFFLE
    // -----------------------------------------

    private IEnumerator SmoothShuffle()
    {
        int previousEmptyLocation = -1;

        for (int i = 0; i < shuffleMoves; i++)
        {
            List<int> possibleMoves = new();

            // Up
            if (emptyLocation - size >= 0)
                possibleMoves.Add(emptyLocation - size);

            // Down
            if (emptyLocation + size < pieces.Count)
                possibleMoves.Add(emptyLocation + size);

            // Left
            if (emptyLocation % size != 0)
                possibleMoves.Add(emptyLocation - 1);

            // Right
            if ((emptyLocation + 1) % size != 0)
                possibleMoves.Add(emptyLocation + 1);

            // Don't immediately reverse the previous move
            if (possibleMoves.Count > 1 &&
                previousEmptyLocation != -1)
            {
                possibleMoves.Remove(previousEmptyLocation);
            }

            int chosen = possibleMoves[
                Random.Range(0, possibleMoves.Count)
            ];

            int oldEmptyLocation = emptyLocation;

            int offset = emptyLocation - chosen;

            yield return StartCoroutine(
                SmoothSwap(chosen, offset)
            );

            // Remember where the empty space was
            previousEmptyLocation = oldEmptyLocation;
        }
    }

    // -----------------------------------------
    // SMOOTH SWAP
    // -----------------------------------------

    private IEnumerator SmoothSwap(int i, int offset)
    {
        // Tile that will move
        Transform movingPiece = pieces[i];

        // Current empty tile
        Transform emptyPiece = pieces[emptyLocation];

        // Save both positions
        Vector3 movingStartPosition = movingPiece.localPosition;
        Vector3 emptyPosition = emptyPiece.localPosition;

        float elapsed = 0f;

        // Animate visible tile into empty space
        while (elapsed < shuffleMoveTime)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / shuffleMoveTime);
            t = Mathf.SmoothStep(0f, 1f, t);

            movingPiece.localPosition =
                Vector3.Lerp(
                    movingStartPosition,
                    emptyPosition,
                    t
                );

            yield return null;
        }

        // Snap moving tile
        movingPiece.localPosition = emptyPosition;

        // IMPORTANT:
        // Move the hidden empty tile to the position
        // that the moving tile originally occupied.
        emptyPiece.localPosition = movingStartPosition;

        // Swap pieces in the list
        Transform temp = pieces[i];

        pieces[i] = pieces[emptyLocation];
        pieces[emptyLocation] = temp;

        // The new empty tile stays hidden
        pieces[i].gameObject.SetActive(false);

        // New empty position
        emptyLocation = i;
    }

    // -----------------------------------------
    // CLEAR BOARD
    // -----------------------------------------

    private void ClearBoard()
    {
        foreach (Transform piece in pieces)
        {
            if (piece != null)
                Destroy(piece.gameObject);
        }

        pieces.Clear();
    }

    // -----------------------------------------
    // PLAYER MOVE
    // -----------------------------------------

    public void TryMove(Transform tile)
    {
        if (!puzzleReady)
            return;

        int index = pieces.IndexOf(tile);

        if (index == -1)
            return;

        if (SwapIfValid(index, -size, size))
            return;

        if (SwapIfValid(index, size, size))
            return;

        if (SwapIfValid(index, -1, 0))
            return;

        if (SwapIfValid(index, 1, size - 1))
            return;
    }

    // -----------------------------------------
    // PLAYER SWAP
    // -----------------------------------------

    private bool SwapIfValid(
    int i,
    int offset,
    int colCheck)
    {
        if (((i % size) != colCheck) &&
            ((i + offset) == emptyLocation))
        {
            Transform movingPiece = pieces[i];
            Transform emptyPiece = pieces[i + offset];

            Vector3 movingPosition = movingPiece.localPosition;
            Vector3 emptyPosition = emptyPiece.localPosition;

            // Move visible piece into empty space
            movingPiece.localPosition = emptyPosition;

            // Move empty piece to old position
            emptyPiece.localPosition = movingPosition;

            // Swap list
            (
                pieces[i],
                pieces[i + offset]
            ) =
            (
                pieces[i + offset],
                pieces[i]
            );

            // Empty piece should remain hidden
            pieces[i].gameObject.SetActive(false);

            // Moving piece should be visible
            pieces[i + offset].gameObject.SetActive(true);

            // Update empty location
            emptyLocation = i;

            // Check completion
            emptyLocation = i;

            if (CheckCompletion())
            {
                StartCoroutine(CompletePuzzleAnimation());
            }

            return true;
        }

        return false;
    }

    // -----------------------------------------
    // CHECK COMPLETION
    // -----------------------------------------

    private bool CheckCompletion()
    {
        if (emptyLocation != pieces.Count - 1)
            return false;

        for (int i = 0; i < pieces.Count - 1; i++)
        {
            if (pieces[i].name != $"{i}")
                return false;
        }

        return true;
    }

    private IEnumerator CompletePuzzleAnimation()
    {
        puzzleReady = false;

        // The final empty position
        int finalPosition = pieces.Count - 1;

        // The tile that moves into the empty space
        int movingIndex = emptyLocation;

        Transform movingPiece = pieces[movingIndex];

        Vector3 startPosition = movingPiece.localPosition;
        Vector3 targetPosition = GetTilePosition(finalPosition);

        float elapsed = 0f;

        // -----------------------------------------
        // MOVE FINAL TILE INTO THE GAP
        // -----------------------------------------

        while (elapsed < finalTileMoveTime)
        {
            elapsed += Time.deltaTime;

            float t =
                Mathf.Clamp01(elapsed / finalTileMoveTime);

            t = Mathf.SmoothStep(0f, 1f, t);

            movingPiece.localPosition =
                Vector3.Lerp(
                    startPosition,
                    targetPosition,
                    t
                );

            yield return null;
        }

        // Snap final tile
        movingPiece.localPosition = targetPosition;

        // -----------------------------------------
        // UPDATE PUZZLE DATA
        // -----------------------------------------

        Transform temp = pieces[movingIndex];

        pieces[movingIndex] = pieces[finalPosition];

        pieces[finalPosition] = temp;

        emptyLocation = -1;

        // Show final tile
        pieces[finalPosition].gameObject.SetActive(true);

        // Make all pieces visible
        foreach (Transform piece in pieces)
        {
            piece.gameObject.SetActive(true);
        }

        // -----------------------------------------
        // SHOW CLEAN FULL IMAGE
        // -----------------------------------------

        yield return new WaitForSeconds(completionPause);

        if (completeImage != null)
        {
            completeImage.SetActive(true);

            yield return StartCoroutine(
                PopCompleteImage()
            );
        }

        // Hide puzzle pieces
        foreach (Transform piece in pieces)
        {
            piece.gameObject.SetActive(false);
        }

        // -----------------------------------------
        // SHOW COMPLETE IMAGE
        // -----------------------------------------

        yield return new WaitForSeconds(fullImagePause);

        // Tell GameManager level is complete
        gameManager.LevelCompleted();
    }

    private IEnumerator PopCompleteImage()
    {
        if (completeImage == null)
            yield break;

        Transform imageTransform = completeImage.transform;

        Vector3 finalScale = imageTransform.localScale;

        // Start slightly smaller
        imageTransform.localScale =
            finalScale * popStartScale;

        float elapsed = 0f;

        // Scale up with slight overshoot
        while (elapsed < popDuration)
        {
            elapsed += Time.deltaTime;

            float t =
                Mathf.Clamp01(elapsed / popDuration);

            // Smooth ease
            t = Mathf.SmoothStep(0f, 1f, t);

            // Slight overshoot
            float scale =
                Mathf.Lerp(
                    popStartScale,
                    popOvershoot,
                    t
                );

            imageTransform.localScale =
                finalScale * scale;

            yield return null;
        }

        // Settle back to normal size
        elapsed = 0f;

        float settleDuration = 0.08f;

        Vector3 overshootScale =
            finalScale * popOvershoot;

        while (elapsed < settleDuration)
        {
            elapsed += Time.deltaTime;

            float t =
                Mathf.Clamp01(elapsed / settleDuration);

            t = Mathf.SmoothStep(0f, 1f, t);

            imageTransform.localScale =
                Vector3.Lerp(
                    overshootScale,
                    finalScale,
                    t
                );

            yield return null;
        }

        imageTransform.localScale = finalScale;
    }

    private Vector3 GetTilePosition(int index)
    {
        int row = index / size;
        int col = index % size;

        float tileSize = boardWidth / size;

        float x =
            -boardWidth / 2f +
            tileSize / 2f +
            col * tileSize;

        float y =
            boardWidth / 2f -
            tileSize / 2f -
            row * tileSize;

        return new Vector3(x, y, 0f);
    }

}