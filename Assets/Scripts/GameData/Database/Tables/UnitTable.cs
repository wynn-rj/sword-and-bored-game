
namespace SwordAndBored.GameData.Database.Tables {
    public class UnitTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }
        public RoleTable Role { get; set; }
        public StatsTable Stats { get; set; }
    }
}
