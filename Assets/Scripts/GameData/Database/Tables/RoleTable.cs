
namespace SwordAndBored.GameData.Database.Tables {
    public class RoleTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }
        public StatsTable Stats { get; set; }
    }
}
