using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement
{
    public class BaseGrid : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;

        private bool[,] baseGrid;

        void Start()
        {
            this.cellSize = 1;
            this.width = 10;
            this.height = 10;
        }

        public Vector3 ReturnGridPoint(Vector3 position)
        {
            position -= transform.position;

            int xCount = Mathf.RoundToInt(position.x / cellSize);
            int yCount = Mathf.RoundToInt(position.y / cellSize);
            int zCount = Mathf.RoundToInt(position.z / cellSize);

            Vector3 finalPosition = new Vector3((float)xCount * cellSize, (float)yCount * cellSize, (float)zCount * cellSize);

            finalPosition += transform.position;

            return finalPosition;
        }
    }
}
