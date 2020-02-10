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
                UnitStats unitStats = activePlayer.GetComponent<UnitStats>();
                nameText.SetText(activePlayer.creatureName);
                healthText.SetText(unitStats.health.ToString());
                pAttackText.SetText(unitStats.attack.ToString());
                pDefenseText.SetText(unitStats.defense.ToString());
                mAttackText.SetText(unitStats.magicAttack.ToString());
                mDefenseText.SetText(unitStats.magicDefense.ToString());
                movementText.SetText(unitStats.movement.ToString());
                initiativeText.SetText(unitStats.speedIntit.ToString());
                evasionText.SetText(unitStats.evasion.ToString());
                accuracyText.SetText(unitStats.accuracy.ToString());
                roleText.SetText(unitStats.role);
            }
        }
    }
}