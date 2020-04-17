using UnityEngine;
using TMPro;
using SwordAndBored.Strategy.Transitions;

namespace SwordAndBored.UI.MenuFunctions
{
    public class WinScreenFunctions : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        private void Awake()
        {
            if (SceneSharing.playerWonGame)
            {
                text.text = "You WIN!";
            }
            else
            {
                text.text = "You LOSE!";
            }
        }
    }
}
