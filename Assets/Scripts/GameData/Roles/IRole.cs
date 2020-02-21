using SwordAndBored.GameData.Units;

namespace SwordAndBored.GameData.Roles
{
    public interface IRole : IDescriptable
    {
        public IStats RoleStats { get; set; }
    }
}
