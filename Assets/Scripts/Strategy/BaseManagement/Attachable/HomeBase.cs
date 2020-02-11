using SwordAndBored.StrategyView.BaseManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public class HomeBase : MonoBehaviour
    {
        public BaseManager BaseManager;

        public Canvas canvasObject;
        [Range(1, 3)] public int tier;

        public Text tierDisplayed;

        Renderer renderer;

        private void Awake()
        {
            renderer = gameObject.GetComponent<Renderer>();
        }

        void Start()
        {
            tier = 1;
            renderer.material.color = Color.gray;
            tierDisplayed.text = tier.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                UpgradeTier();
            }
        }

        private void OnMouseOver()
        {
            renderer.material.color = Color.yellow;

            if (Input.GetMouseButtonDown(0))
            {
                canvasObject.gameObject.SetActive(true);
                BaseManager.GetComponent<BaseManager>().SetAllCanvasInactive();
            }
        }

        private void OnMouseExit()
        {
            renderer.material.color = Color.grey;
        }

        private void UpgradeTier()
        {
            if (tier < 3)
            {
                tier++;
            }

            tierDisplayed.text = tier.ToString();
            BaseManager.UnlockTier(tier);
        }

        public void ExitCanvas()
        {
            canvasObject.gameObject.SetActive(false);
        }
    }
}