using SwordAndBored.GameData.Units;
using SwordAndBored.Utilities;
using SwordAndBored.Utilities.Debug;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.Squads
{
    class SquadCanvasController : MonoBehaviour
    {
        [SerializeField] private Text squadMembersText;
        [SerializeField] private Text squadNameText;
        [SerializeField] private GameObject squadPanel;
        private RectTransform squadPanelTransform;

        public void UpdateDisplayedSquad(ISquad squad)
        {
            squadPanel.SetActive(!(squad is null));
            if (squad is null)
            {
                return;
            }
            squadNameText.text = squad.Name;
            IList<IUnit> units = squad.Units;
            squadMembersText.text = string.Join("\n", units.EnumerateOverValues((unit) => unit.Name));
            squadPanelTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 35 + 15 * units.Count);
        }

        private void Awake()
        {
            AssertHelper.IsSetInEditor(squadMembersText, this);
            AssertHelper.IsSetInEditor(squadNameText, this);
            AssertHelper.IsSetInEditor(squadPanel, this);
            squadPanelTransform = squadPanel.GetComponent<RectTransform>();
        }
    }
}
