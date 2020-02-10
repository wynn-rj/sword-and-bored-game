using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Equipment
{
    class GenericEquipment : AbstractEquipment
    {
        public override IAbility[] Abilities { get; }

        public override IModifier[] Modifiers { get; }

        public GenericEquipment(IAbility[] abilities, IModifier[] modifiers)
        {
            Abilities = abilities;
            Modifiers = modifiers; 
        }
    }
}
