using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using TMPro;

public class AbilityDescriptionPanel : MonoBehaviour
{
    public TMP_Text nameText, descriptionText, damageText, rangeText, accuracyText;

    public void DisplayDataInUI(Ability ability)
    {
        nameText.text = ability.name;
        descriptionText.text = ability.description;
        if(ability.aoe)
        {
            rangeText.text = $"{ability.range} {ability.length}x{ability.width} Area";
        } else
        {
            rangeText.text = $"{ability.range} Single Target";
        }
        accuracyText.text = ability.accuracy.ToString();
        if (ability.damage < 0)
        {
            if (ability.isPhysical)
            {
                damageText.text = $"Heal {-ability.damage} Physical";
            }
            else
            {
                damageText.text = $"Heal {-ability.damage} Magical";
            }
        } else
        {
            if (ability.isPhysical)
            {
                damageText.text = $"{ability.damage} Physical";
            }
            else
            {
                damageText.text = $"{ability.damage} Magical";
            }
        }
        
    }
}
