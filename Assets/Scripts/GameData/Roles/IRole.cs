using SwordAndBored.GameData.Abilities.Skills;

namespace SwordAndBored.GameData.Roles
{
    interface IRole : IDescriptable
    {
        ISkillTree RoleSkillTree { get; }
    }
}
