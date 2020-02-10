using SwordAndBored.GameData.Equipment;

namespace SwordAndBored.GameData.Creatures
{
    class GenericCreature : AbstractCreature
    {
        public override IEquipment[] Equipment { get; }

        public override IStats Stats { get; }

        public GenericCreature(IEquipment[] equipment = null, IStats stats = null)
        {
            Stats = stats ?? new GenericStats();
            Equipment = equipment ?? new IEquipment[0];
        }
    }
}
