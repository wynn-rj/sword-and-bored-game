using UnityEngine;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.Battlefield.CreaturScripts;
using TMPro;

namespace SwordAndBored.UI.Battlefield {
    public class StatsPanel : MonoBehaviour
    {
        public GameObject statsPanel;
        public TurnManager turnManager;

        public TMP_Text nameText, healthText, pAttackText, pDefenseText,
            mAttackText, mDefenseText, movementText, initiativeText, evasionText,
            accuracyText, roleText;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                statsPanel.SetActive(!statsPanel.activeSelf);
            }

            if(statsPanel.activeSelf)
            {
                UniqueCreature activePlayer = turnManager.activePlayer.gameObject.GetComponent<UniqueCreature>();
                nameText.SetText(activePlayer.creatureName);
            }
        }
    }
}