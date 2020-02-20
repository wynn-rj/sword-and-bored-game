/*
using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Abilities.Skills;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Modifiers;
using SwordAndBored.GameData.Roles;
using System.Collections.Generic;

namespace SwordAndBored.GameData.Creatures
{
    abstract class AbstractCharacter : AbstractCreature, ICharacter
    {
        public abstract IRole Role { get; }

        public abstract int XP { get; set; }

        public virtual int Level { get; protected set; }

        public abstract ISkillTree SkillTree { get; protected set; }

        public IEquipment ArmorSlot
        {
            get => armorSlot;
            set
            {
                armorSlot = value;
                RecalculateAll();
            }
        }
        public IEquipment WeaponSlot
        {
            get => weaponSlot;
            set
            {
                weaponSlot = value;
                RecalculateAll();
            }
        }

        public IEquipment[] AdditionalEquipmentSlots { get; set; }

        public override IEquipment[] Equipment
        {
            get
            {
                HashCheck();
                if (recalculateEquipment)
                {
                    List<IEquipment> equipmentList = new List<IEquipment>(AdditionalEquipmentSlots);
                    equipmentList.Add(ArmorSlot);
                    equipmentList.Add(WeaponSlot);
                    additionalEquipmentHash = AdditionalEquipmentSlots.GetHashCode();
                    recalculateEquipment = false;
                    equipment = equipmentList.ToArray();
                }
                return equipment;
            }
        }

        public override IAbility[] Abilities
        {
            get
            {
                HashCheck();
                return base.Abilities;
            }
        }

        public override IModifier[] Modifiers
        {
            get
            {
                HashCheck();
                return base.Modifiers;
            }
        }

        protected bool recalculateEquipment = true;

        private IEquipment armorSlot;
        private IEquipment weaponSlot;
        private IEquipment[] equipment;
        private int additionalEquipmentHash = -1;

        private void RecalculateAll()
        {
            recalculateEquipment = recalculateModifiers = recalculateAbiltiies = true;
        }

        private void HashCheck()
        {
            recalculateEquipment |= AdditionalEquipmentSlots.GetHashCode() != additionalEquipmentHash;
        }
    }
}
*/