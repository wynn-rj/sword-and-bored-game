using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutCellSize : MonoBehaviour
{
    public int numberOfCells;
    public int numberOfColumns;

    private float width;
    private float height;

    private int padding;

    private RectTransform rectTransform;

    void Start()
    {
        numberOfCells = 6;
        numberOfColumns = 3;
        padding = 10;

        rectTransform = gameObject.GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        int numberOfRows = Mathf.CeilToInt(numberOfCells / numberOfColumns);

        Vector2 newCellSize = new Vector2(Mathf.CeilToInt(width / numberOfColumns) - (1.5f * padding), Mathf.CeilToInt(height / numberOfRows) - padding);
        gameObject.GetComponent<GridLayoutGroup>().cellSize = newCellSize;

        Debug.Log(height);
        Debug.Log(width);
        Debug.Log(numberOfRows);
        Debug.Log(numberOfColumns);
    }
}
