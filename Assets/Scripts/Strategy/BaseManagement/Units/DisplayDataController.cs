using SwordAndBored.GameData.Units;
using TMPro;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class DisplayDataController : MonoBehaviour
    {
        [SerializeField] private TMP_Text MaxHP;
        [SerializeField] private TMP_Text CurrentHP;
        [SerializeField] private TMP_Text Initiative;
        [SerializeField] private TMP_Text PhysicalAttack;
        [SerializeField] private TMP_Text PhysicalDefense;
        [SerializeField] private TMP_Text MagicAttack;
        [SerializeField] private TMP_Text MagicDefense;
        [SerializeField] private TMP_Text Role;
        [SerializeField] private TMP_Text Name;

        public void SetData(IUnit unit)
        {
            IStats stats = unit.Stats;
            MaxHP.text = "Max HP: " + stats.Max_HP.ToString();
            CurrentHP.text = "Current HP: " + stats.Current_HP.ToString();
            Initiative.text = "Initiative: " + stats.Initiative.ToString();
            PhysicalAttack.text = "Physical Attack: " + stats.Physical_Attack.ToString();
            PhysicalDefense.text = "Physical Defense: " + stats.Physical_Defense.ToString();
            MagicAttack.text = "Magic Attack: " + stats.Magic_Attack.ToString();
            MagicDefense.text = "Magic Defense: " + stats.Magic_Defense.ToString();

            Role.text = unit.Role.Name;
            Name.text = unit.Name;
        }
    }
}
