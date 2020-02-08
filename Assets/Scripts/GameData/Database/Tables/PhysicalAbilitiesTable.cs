using Mono.Data.Sqlite;

namespace SwordAndBored.GameData.Database.Tables
{
    public class PhysicalAbilitiesTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }
        public WeaponTable Weapon { get; set; }
        public AttackModifiersTable Stats { get; set; }

        public int Damage { get; set; }
        public int Range { get; set; }
        public int Accuracy { get; set; }
    }
}