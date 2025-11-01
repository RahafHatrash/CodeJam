using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PuzzleImageSplitter : MonoBehaviour
{
    public Sprite fullImage;       
    public int rows = 3;
    public int cols = 3;
    public GameObject piecePrefab; 
    public RectTransform puzzlePanel;
    public Vector2 pieceSize = new Vector2(150, 150);

    void Start()
    {
        SplitImage();
    }

    void SplitImage()
    {
        float pieceWidth = fullImage.texture.width / cols;
        float pieceHeight = fullImage.texture.height / rows;

        List<Vector2> positions = new List<Vector2>();
        float spacing = 10f;
        float totalWidth = (cols * (pieceSize.x + spacing)) - spacing;
        float totalHeight = (rows * (pieceSize.y + spacing)) - spacing;

        float startX = -totalWidth / 2 + pieceSize.x / 2;
        float startY = totalHeight / 2 - pieceSize.y / 2;

        // تخزين جميع المواقع الصحيحة
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                positions.Add(new Vector2(
                    startX + (c * (pieceSize.x + spacing)),
                    startY - (r * (pieceSize.y + spacing))
                ));
            }
        }

        // نخلط المواقع عشوائياً
        List<Vector2> shuffledPositions = new List<Vector2>(positions);
        for (int i = 0; i < shuffledPositions.Count; i++)
        {
            Vector2 temp = shuffledPositions[i];
            int randomIndex = Random.Range(i, shuffledPositions.Count);
            shuffledPositions[i] = shuffledPositions[randomIndex];
            shuffledPositions[randomIndex] = temp;
        }

        int index = 0;

        // انشاء القطع
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
                rt.sizeDelta = pieceSize;

                // توضع القطعة في موقع عشوائي
                rt.anchoredPosition = shuffledPositions[index];
                index++;

                var drag = piece.GetComponent<DragDropPiece>();
                if (drag != null)
                    drag.correctSlotName = $"Slot_{r}_{c}";
            }
        }
    }
}
