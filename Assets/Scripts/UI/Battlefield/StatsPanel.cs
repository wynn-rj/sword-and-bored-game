using UnityEngine;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.Battlefield.CreaturScripts;
using TMPro;

namespace SwordAndBored.UI.Battlefield {
    public class StatsPanel : MonoBehaviour
    {
        public GameObject statsPanel;
        public TurnManager turnManager;

        public TMP_Text healthText, pAttackText, pDefenseText,
            mAttackText, mDefenseText, movementText, initiativeText, roleText;


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
                healthText.text = $"HP: {unitStats.health} / {unitStats.maxHealth}";
                pAttackText.text = $"Physical Attack: {unitStats.physicalAttack}";
                pDefenseText.text = $"Physical Defense: {unitStats.physicalDefense}";
                mAttackText.text = $"Magic Attack: {unitStats.magicAttack}";
                mDefenseText.text = $"Magic Defense: {unitStats.magicDefense}";
                movementText.text = $"Movement: {unitStats.movement}";
                initiativeText.text = $"Initiative: {unitStats.speedIntit}";
                roleText.text = $"Role: {unitStats.role}";
            }
        }
    }
}