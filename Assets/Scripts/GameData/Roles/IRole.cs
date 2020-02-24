using SwordAndBored.GameData.Units;

namespace SwordAndBored.GameData.Roles
{
    public interface IRole : IDescriptable, IDatabaseObject
    {
        IStats RoleStats { get; set; }
    }
}
