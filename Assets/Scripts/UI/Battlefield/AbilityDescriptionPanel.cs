using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using TMPro;

public class AbilityDescriptionPanel : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public void DisplayDataInUI(Ability ability)
    {
        nameText.text = ability.name;
    }
}
