namespace SwordAndBored.GameData.Abilities
{
    class AbilityFactory
    {
        public static IAbility BuildAbility(string abilityName)
        {
            //TODO: Interface this with the database once that is available
            return BuildGenericAbility(abilityName, "A temporary description", "image");
        }

        private static GenericAbility BuildGenericAbility(string name, string desc, string image)
        {
            GenericAbility ability = new GenericAbility()
            {
                Name = name,
                Description = desc,
                ImageName = image
            };
            return ability;
        }
    }
}
