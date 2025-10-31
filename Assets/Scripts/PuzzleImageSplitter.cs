using UnityEngine;
using UnityEngine.UI;

public class PuzzleImageSplitter : MonoBehaviour
{
    public Sprite fullImage;       
    public int rows = 3;
    public int cols = 3;
    public GameObject piecePrefab; 
    public RectTransform puzzlePanel;

    [Header("SIZE OF EACH PIECE")]
    public Vector2 pieceSize = new Vector2(500, 200); 

    void Start()
    {
        SplitImage();
    }

    void SplitImage()
    {
        float pieceWidth = fullImage.texture.width / cols;
        float pieceHeight = fullImage.texture.height / rows;

        int totalPieces = rows * cols;
        int piecesPerRow = Mathf.CeilToInt(totalPieces / 2f);

        float spacing = 20f; 
        float startY = -puzzlePanel.rect.height / 2 + pieceSize.y / 2 + 50f; 
        float totalWidth = (piecesPerRow * (pieceSize.x + spacing)) - spacing;
        float startX = -totalWidth / 2 + pieceSize.x / 2;

        int pieceIndex = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                GameObject piece = Instantiate(piecePrefab, puzzlePanel);
                piece.name = $"Piece_{r}_{c}";

                Image img = piece.GetComponent<Image>();
                Rect rect = new Rect(
                    c * pieceWidth,
                    fullImage.texture.height - (r + 1) * pieceHeight,
                    pieceWidth,
                    pieceHeight
                );

                img.sprite = Sprite.Create(fullImage.texture, rect, new Vector2(0.5f, 0.5f));
                img.preserveAspect = true;

                RectTransform rt = piece.GetComponent<RectTransform>();
                rt.sizeDelta = pieceSize; // ← 500 × 200

                int rowPlacement = pieceIndex < piecesPerRow ? 0 : 1;
                int colIndex = rowPlacement == 0 ? pieceIndex : pieceIndex - piecesPerRow;

                float xPos = startX + (colIndex * (pieceSize.x + spacing));
                float yPos = startY + (rowPlacement * (pieceSize.y + spacing));

                rt.anchoredPosition = new Vector2(xPos, yPos);

                var drag = piece.GetComponent<DragDropPiece>();
                if (drag != null)
                    drag.correctSlotName = $"Slot_{r}_{c}";

                pieceIndex++;
            }
        }
    }
}
