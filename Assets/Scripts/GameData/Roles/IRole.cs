using SwordAndBored.GameData.Units;

namespace SwordAndBored.GameData.Roles
{
<<<<<<< HEAD
    public interface IRole : IDescriptable, IDatabaseObject
=======
    public interface IRole : IDescriptable
>>>>>>> Make interfaces public
    {
        IStats RoleStats { get; set; }
    }
}
