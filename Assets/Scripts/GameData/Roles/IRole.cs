using SwordAndBored.GameData.Units;

namespace SwordAndBored.GameData.Roles
{
    public interface IRole : IDescriptable
    {
        int ID { get; }
        IStats RoleStats { get; set; }
    }
}
