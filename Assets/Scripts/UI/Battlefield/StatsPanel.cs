using UnityEngine;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.Battlefield.CreaturScripts;
using TMPro;
using SwordAndBored.Battlefield;

namespace SwordAndBored.UI.Battlefield
{
    public class StatsPanel : MonoBehaviour
    {
        public GameObject currentStatsPanel, otherUnitPanel;
        public TurnManager turnManager;
        public TMP_Text healthText, pAttackText, pDefenseText,
            mAttackText, mDefenseText, movementText, initiativeText, roleText, statusText;
        public TMP_Text healthTextOther, pAttackTextOther, pDefenseTextOther,
            mAttackTextOther, mDefenseTextOther, movementTextOther, initiativeTextOther, roleTextOther, statusTextOther;

        private BrainManager brain;
        private UniqueCreature currentRaycasted;

        // Update is called once per frame
        void Update()
        {
            if (!turnManager.statsPanel)
            {
                turnManager.statsPanel = currentStatsPanel;
                brain = turnManager.activePlayer.GetComponent<BrainManager>();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                currentStatsPanel.SetActive(!currentStatsPanel.activeSelf);
            }

            if (currentStatsPanel.activeSelf)
            {
                UnitStats unitStats = turnManager.activePlayer.creature.stats;

                healthText.text = $"HP: {unitStats.health} / {unitStats.maxHealth}";
                pAttackText.text = $"Physical Attack: {unitStats.physicalAttack}";
                pDefenseText.text = $"Physical Defense: {unitStats.physicalDefense}";
                mAttackText.text = $"Magic Attack: {unitStats.magicAttack}";
                mDefenseText.text = $"Magic Defense: {unitStats.magicDefense}";
                movementText.text = $"Movement: {turnManager.activePlayer.creature.movementLeft} / {unitStats.movement}";
                initiativeText.text = $"Initiative: {unitStats.speedIntit}";
                roleText.text = $"Role: {unitStats.role}";
                statusText.text = $"Status: {GetStatusText(unitStats, statusText, pDefenseText, mDefenseText, pAttackText, movementText)}";
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                RayCastToUnit();
            }
            if (currentRaycasted)
            {
                otherUnitPanel.SetActive(true);
                FillOtherStatPanel(currentRaycasted.stats);
            }
        }

        public void RayCastToUnit()
        {
            LayerMask lm = brain.selectingCreaturesLayerMask;
            Ray ray = brain.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, lm))
            {
                currentRaycasted = hit.collider.gameObject.GetComponent<UniqueCreature>();
                otherUnitPanel.SetActive(true);
                FillOtherStatPanel(currentRaycasted.stats);
            }
            else
            {
                currentRaycasted = null;
                otherUnitPanel.SetActive(false);
            }
        }

        public void FillOtherStatPanel(UnitStats otherStats)
        {
            healthTextOther.text = $"HP: {otherStats.health} / {otherStats.maxHealth}";
            pAttackTextOther.text = $"Physical Attack: {otherStats.physicalAttack}";
            pDefenseTextOther.text = $"Physical Defense: {otherStats.physicalDefense}";
            mAttackTextOther.text = $"Magic Attack: {otherStats.magicAttack}";
            mDefenseTextOther.text = $"Magic Defense: {otherStats.magicDefense}";
            movementTextOther.text = $"Movement: {otherStats.movement}";
            initiativeTextOther.text = $"Initiative: {otherStats.speedIntit}";
            roleTextOther.text = $"Role: {otherStats.role}";
            statusTextOther.text = $"Status: {GetStatusText(otherStats, statusTextOther, pDefenseTextOther, mDefenseTextOther, pAttackTextOther, movementTextOther)}";
        }

        public string GetStatusText(UnitStats stats, TMP_Text statusText, TMP_Text pDefenseText, TMP_Text mDefenseText, TMP_Text pAttackText, TMP_Text movementText)
        {
            if (stats.IsStunned)
            {
                statusText.faceColor = Color.yellow;
                movementText.faceColor = Color.red;
                return "Stunned";
            }
            else if (stats.IsFrozen)
            {
                statusText.faceColor = Color.cyan;
                movementText.faceColor = Color.red;
                pDefenseText.faceColor = Color.red;
                return "Frozen";
            }
            else if (stats.IsBurning)
            {
                statusText.faceColor = Color.magenta;
                pAttackText.faceColor = Color.red;
                return "Burning";
            }
            else if (stats.IsBleeding)
            {
                statusText.faceColor = Color.red;
                mDefenseText.faceColor = Color.red;
                return "Bleeding";
            }
            else
            {
                statusText.faceColor = Color.white;
                mDefenseText.faceColor = Color.white;
                pAttackText.faceColor = Color.white;
                pDefenseText.faceColor = Color.white;
                movementText.faceColor = Color.white;
                return "Healthy";
            }
        }
    }
}