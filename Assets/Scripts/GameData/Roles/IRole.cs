using SwordAndBored.GameData.Units;

namespace SwordAndBored.GameData.Roles
{
    public interface IRole : IDescriptable
    {
        IStats RoleStats { get; set; }
    }
}
