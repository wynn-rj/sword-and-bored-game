using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.UI.Utility
{
    public class GridLayoutCellSize : MonoBehaviour
    {
        [SerializeField] private int numberOfCells;
        [SerializeField] private int numberOfColumns;
        [SerializeField] private int padding;

        private RectTransform rectTransform;
        private float width;
        private float height;

        void Start()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            width = rectTransform.rect.width;
            height = rectTransform.rect.height;

            int numberOfRows = Mathf.CeilToInt(numberOfCells / numberOfColumns);

            Vector2 newCellSize = new Vector2(Mathf.CeilToInt(width / numberOfColumns) - (1.5f * padding), Mathf.CeilToInt(height / numberOfRows) - padding);
            gameObject.GetComponent<GridLayoutGroup>().cellSize = newCellSize;
        }
    }
}
